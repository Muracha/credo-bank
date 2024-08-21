Ext.define('app.view.loanapplication.LoanApplicationController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.loanapplication',

    renderCurrency: function(value, meta, record) {
        var currencySymbol = this.getCurrencySymbol(record.get('currencyTypeText'));
        return Ext.util.Format.currency(value, currencySymbol, 2);
    },

    getCurrencySymbol: function(currencyType) {
        switch(currencyType) {
            case 'GEL': return '₾';
            case 'USD': return '$';
            case 'EUR': return '€';
            default: return '';
        }
    },

    onAddLoanClick: function() {
        Ext.Msg.alert('Add Loan', 'Add Loan Application functionality to be implemented.');
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
        Ext.Msg.alert('Update', 'Update loan application for ID: ' + record.get('id'));
        // Implement update logic here
    },

    onDeleteClick: function(grid, rowIndex, colIndex, item, e, record) {
        Ext.Msg.confirm('Delete', 'Are you sure you want to delete this loan application?', function(choice) {
            if (choice === 'yes') {
                // Implement delete logic here
                console.log('Deleting loan application with ID: ' + record.get('id'));
            }
        });
    },

    onApproveClick: function(grid, rowIndex, colIndex, item, e, record) {
        Ext.Msg.confirm('Approve', 'Are you sure you want to approve this loan application?', function(choice) {
            if (choice === 'yes') {
                // Implement approve logic here
                console.log('Approving loan application with ID: ' + record.get('id'));
            }
        });
    }
});