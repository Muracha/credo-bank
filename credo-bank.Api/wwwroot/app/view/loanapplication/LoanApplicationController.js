Ext.define('app.view.loanapplication.LoanApplicationController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.loanapplication',

    init: function() {
        this.loadLoanData();
    },
    
    renderCurrency: function(value, meta, record) {
        var currencySymbol = this.getCurrencySymbol(record.get('currencyType'));
        return currencySymbol + ' ' + Ext.util.Format.number(value, '0,000.00');
    },

    getCurrencySymbol: function(currencyType) {
        switch(currencyType) {
            case 1: return '₾';
            case 2: return '$';
            case 3: return '€';
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
                        queryMode: 'local',
                        editable: false,
                        forceSelection: true,
                        allowBlank: false
                    },
                    {
                        xtype: 'combobox',
                        name: 'loanType',
                        fieldLabel: 'Loan Type',
                        store: [[1, 'QUICK LOAN'], [2, 'AUTO LOAN'], [3, 'INSTALLMENT LOAN']],
                        queryMode: 'local',
                        editable: false,
                        forceSelection: true,
                        allowBlank: false
                    }
                ],
                buttons: [{
                    text: 'Submit',
                    handler: function() {
                        var form = this.up('form');
                        if (form.isValid()) {
                            var values = form.getValues();
                            var postData = {
                                LoanAmount: parseFloat(values.loanAmount),
                                LoanTermInMonths: parseInt(values.loanTermInMonths),
                                CurrencyType: parseInt(values.currencyType),
                                ApplicationStatus: 0,
                                LoanType: parseInt(values.loanType)
                            };

                            Ext.Ajax.request({
                                url: 'https://localhost:44313/loan',
                                method: 'POST',
                                jsonData: postData,
                                success: function(response) {
                                    var result = Ext.decode(response.responseText);
                                    console.log('New loan application submitted:', result);
                                    Ext.toast('Loan application submitted successfully');
                                    formWindow.close();
                                    view.getViewModel().getStore('loans').reload();
                                },
                                failure: function(response) {
                                    console.error('Loan application submission failed:', response);
                                    Ext.Msg.alert('Error', 'Failed to submit loan application. Please try again.');
                                }
                            });
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
    
    onRejectClick: function(grid, rowIndex, colIndex, item, e, record) {
        if (record.get('applicationStatus') !== 4) {
            Ext.Msg.alert('Action Not Allowed', 'You can only reject loan applications that are in progress.');
            return;
        }

        var view = this.getView();
        var loanId = record.get('id');

        Ext.Msg.confirm('Reject', 'Are you sure you want to reject this loan application?', function(choice) {
            if (choice === 'yes') {
                Ext.Ajax.request({
                    url: 'https://localhost:44313/loan/reject/' + loanId,
                    method: 'POST',
                    success: function(response) {
                        var result = Ext.decode(response.responseText);
                        if (result.success) {
                            Ext.toast('Loan application rejected successfully');
                            view.getViewModel().getStore('loans').reload();
                        } else {
                            Ext.Msg.alert('Error', 'Failed to reject loan application: ' + result.message);
                        }
                    },
                    failure: function(response) {
                        console.error('Loan application rejection failed:', response);
                        Ext.Msg.alert('Error', 'Failed to reject loan application. Please try again.');
                    }
                });
            }
        });
    },
    
    onUpdateClick: function(grid, rowIndex, colIndex, item, e, record) {
        if (record.get('applicationStatus') !== 4) {
            Ext.Msg.alert('Action Not Allowed', 'You can only update loan applications that are in progress.');
            return;
        }

        var view = this.getView();
        var loanId = record.get('id');

        var formWindow = Ext.create('Ext.window.Window', {
            title: 'Update Loan Application',
            width: 300,
            modal: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                bodyPadding: 10,
                items: [
                    { xtype: 'numberfield', name: 'loanAmount', fieldLabel: 'Loan Amount', value: record.get('loanAmount'), allowBlank: false },
                    { xtype: 'numberfield', name: 'loanTermInMonths', fieldLabel: 'Term (months)', value: record.get('loanTermInMonths'), allowBlank: false }
                ],
                buttons: [{
                    text: 'Update',
                    handler: function() {
                        var form = this.up('form');
                        if (form.isValid()) {
                            var values = form.getValues();
                            var updateData = {
                                LoanTermInMonths: parseInt(values.loanTermInMonths),
                                LoanAmount: parseFloat(values.loanAmount)
                            };

                            Ext.Ajax.request({
                                url: 'https://localhost:44313/loan/' + loanId,
                                method: 'PUT',
                                jsonData: updateData,
                                success: function(response) {
                                    var result = Ext.decode(response.responseText);
                                    if (result.success) {
                                        Ext.toast('Loan application updated successfully');
                                        formWindow.close();
                                        view.getViewModel().getStore('loans').reload();
                                    } else {
                                        Ext.Msg.alert('Error', 'Failed to update loan application: ' + result.message);
                                    }
                                },
                                failure: function(response) {
                                    console.error('Loan application update failed:', response);
                                    Ext.Msg.alert('Error', 'Failed to update loan application. Please try again.');
                                }
                            });
                        }
                    }
                }]
            }]
        });
        formWindow.show();
    },

    onDeleteClick: function(grid, rowIndex, colIndex, item, e, record) {
        var status = record.get('applicationStatus');
        if (status !== 1 && status !== 2) {
            Ext.Msg.alert('Action Not Allowed', 'You cannot delete a loan application that is not accepted or canceled.');
            return;
        }
        
        var view = this.getView();
        var loanId = record.get('id');
        
        Ext.Msg.confirm('Delete', 'Are you sure you want to delete this loan application?', function(choice) {
            if (choice === 'yes') {
                Ext.Ajax.request({
                    url: 'https://localhost:44313/loan/' + loanId,
                    method: 'DELETE',
                    success: function(response) {
                        var result = Ext.decode(response.responseText);
                        if (result.success) {
                            Ext.toast('Loan application deleted successfully');
                            view.getViewModel().getStore('loans').reload();
                        } else {
                            Ext.Msg.alert('Error', 'Failed to delete loan application: ' + result.message);
                        }
                    },
                    failure: function(response) {
                        console.error('Loan application delete failed:', response);
                        Ext.Msg.alert('Error', 'Failed to delete loan application. Please try again.');
                    }
                });
            }
        });
    },

    onApproveClick: function(grid, rowIndex, colIndex, item, e, record) {
        if (record.get('applicationStatus') !== 4) {
            Ext.Msg.alert('Action Not Allowed', 'You cannot approve a loan it needs to be in progress position.');
            return;
        }

        var view = this.getView();
        var loanId = record.get('id');

        Ext.Msg.confirm('Approve', 'Are you sure you want to approve this loan application?', function(choice) {
            if (choice === 'yes') {
                Ext.Ajax.request({
                    url: 'https://localhost:44313/loan/approve/' + loanId,
                    method: 'POST',
                    success: function(response) {
                        var result = Ext.decode(response.responseText);
                        if (result.success) {
                            Ext.toast('Loan application approved successfully');
                            view.getViewModel().getStore('loans').reload();
                        } else {
                            Ext.Msg.alert('Error', 'Failed to approve loan application: ' + result.message);
                        }
                    },
                    failure: function(response) {
                        console.error('Loan application approval failed:', response);
                        Ext.Msg.alert('Error', 'Failed to approve loan application. Please try again.');
                    }
                });
            }
        });
    },

    loadLoanData: function() {
        var view = this.getView();
        var store = this.getViewModel().getStore('loans');

        view.mask('Loading...');

        Ext.Ajax.request({
            url: 'https://localhost:44313/loan',
            method: 'GET',
            success: function(response) {
                view.unmask();
                var result = Ext.decode(response.responseText);
                console.log('Received data:', result);
                store.loadData(result.data.loanApplications);
                Ext.toast('Data loaded successfully');
            },
            failure: function(response) {
                view.unmask();
                console.error('Data load failed:', response);
                Ext.Msg.alert('Error', 'Failed to load data. Please refresh the page.');
            }
        });
    },

    onRefreshClick: function() {
        this.loadLoanData();
    },
});