Ext.define('app.view.loanapplication.LoanApplicationController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.loanapplication',

    init: function() {
        var store = this.getViewModel().getStore('loans');
        store.on('load', this.onStoreLoad, this);
    },
    
    renderCurrency: function(value, meta, record) {
        var currencySymbol = this.getCurrencySymbol(record.get('currencyType'));
        return currencySymbol + ' ' + Ext.util.Format.number(value, '0,000.00');
    },

    getCurrencySymbol: function(currencyType) {
        switch(currencyType) {
            case 1: return '₾'; // GEL
            case 2: return '$'; // USD
            case 3: return '€'; // EUR
            default: return '';
        }
    },

    onAddLoanClick: function() {
        var view = this.getView();
        var formWindow = Ext.create('Ext.window.Window', {
            title: 'Add New Loan Application',
            width: 400,
            modal: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                bodyPadding: 10,
                items: [
                    { xtype: 'numberfield', name: 'loanAmount', fieldLabel: 'Loan Amount', allowBlank: false },
                    { xtype: 'numberfield', name: 'loanTermInMonths', fieldLabel: 'Term (months)', allowBlank: false },
                    {
                        xtype: 'combobox',
                        name: 'currencyType',
                        fieldLabel: 'Currency',
                        store: [[1, 'GEL'], [2, 'USD'], [3, 'EUR']],
                        allowBlank: false
                    },
                    {
                        xtype: 'combobox',
                        name: 'loanType',
                        fieldLabel: 'Loan Type',
                        store: [[1, 'QUICK LOAN'], [2, 'AUTO LOAN'], [3, 'INSTALLMENT LOAN']],
                        allowBlank: false
                    }
                ],
                buttons: [{
                    text: 'Submit',
                    handler: function() {
                        var form = this.up('form');
                        if (form.isValid()) {
                            var values = form.getValues();
                            // Here you would typically send the data to your server
                            console.log('Submitting new loan application:', values);
                            formWindow.close();
                            view.getViewModel().getStore('loans').reload();
                        }
                    }
                }]
            }]
        });
        formWindow.show();
    },

    onLogoutClick: function() {
        Ext.Msg.confirm('Logout', 'Are you sure you want to logout?', function(choice) {
            if (choice === 'yes') {
                localStorage.removeItem('authToken');
                localStorage.removeItem('refreshToken');
                this.getView().destroy();
                window.location.reload();
            }
        }, this);
    },

    onUpdateClick: function(grid, rowIndex, colIndex, item, e, record) {
        var view = this.getView();
        var formWindow = Ext.create('Ext.window.Window', {
            title: 'Update Loan Application',
            width: 400,
            modal: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                bodyPadding: 10,
                items: [
                    { xtype: 'numberfield', name: 'loanAmount', fieldLabel: 'Loan Amount', value: record.get('loanAmount'), allowBlank: false },
                    { xtype: 'numberfield', name: 'loanTermInMonths', fieldLabel: 'Term (months)', value: record.get('loanTermInMonths'), allowBlank: false },
                    {
                        xtype: 'combobox',
                        name: 'currencyType',
                        fieldLabel: 'Currency',
                        store: [[1, 'GEL'], [2, 'USD'], [3, 'EUR']],
                        value: record.get('currencyType'),
                        allowBlank: false
                    },
                    {
                        xtype: 'combobox',
                        name: 'loanType',
                        fieldLabel: 'Loan Type',
                        store: [[1, 'QUICK LOAN'], [2, 'AUTO LOAN'], [3, 'INSTALLMENT LOAN']],
                        value: record.get('loanType'),
                        allowBlank: false
                    }
                ],
                buttons: [{
                    text: 'Update',
                    handler: function() {
                        var form = this.up('form');
                        if (form.isValid()) {
                            var values = form.getValues();
                            // Here you would typically send the updated data to your server
                            console.log('Updating loan application:', values);
                            formWindow.close();
                            view.getViewModel().getStore('loans').reload();
                        }
                    }
                }]
            }]
        });
        formWindow.show();
    },

    onDeleteClick: function(grid, rowIndex, colIndex, item, e, record) {
        var view = this.getView();
        Ext.Msg.confirm('Delete', 'Are you sure you want to delete this loan application?', function(choice) {
            if (choice === 'yes') {
                // Here you would typically send a delete request to your server
                console.log('Deleting loan application with ID:', record.get('id'));
                view.getViewModel().getStore('loans').remove(record);
            }
        });
    },

    onApproveClick: function(grid, rowIndex, colIndex, item, e, record) {
        var view = this.getView();
        Ext.Msg.confirm('Approve', 'Are you sure you want to approve this loan application?', function(choice) {
            if (choice === 'yes') {
                // Here you would typically send an approval request to your server
                console.log('Approving loan application with ID:', record.get('id'));
                record.set('applicationStatus', 1); // Assuming 1 is the 'ACCEPTED' status
                record.commit();
            }
        });
    },

    onRefreshClick: function() {
        var store = this.getViewModel().getStore('loans');
        if (!store) {
            console.error('Store not found');
            Ext.Msg.alert('Error', 'Unable to refresh data. Store not found.');
            return;
        }
        store.removeAll();
        store.load({
            callback: function(records, operation, success) {
                console.log('Store load callback', success, operation);
                if (success) {
                    Ext.toast('Loan applications refreshed successfully');
                } else {
                    var error = operation.getError();
                    Ext.Msg.alert('Error', 'Failed to refresh loan applications. Please try again.');
                    console.error('Refresh error:', error);
                    if (error && error.status) {
                        console.error('Status code:', error.status);
                        console.error('Status text:', error.statusText);
                    }
                    if (error && error.responseText) {
                        console.error('Server response:', error.responseText);
                    }
                }
            },
            scope: this
        });
    }
});