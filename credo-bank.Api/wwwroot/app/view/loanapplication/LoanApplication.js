Ext.define('app.view.loanapplication.LoanApplication', {
    extend: 'Ext.container.Viewport',
    xtype: 'loanapplicationview',

    requires: [
        'app.view.loanapplication.LoanApplicationController',
        'app.view.loanapplication.LoanApplicationModel',
        'app.model.LoanApplication'
    ],

    controller: 'loanapplication',
    viewModel: {
        type: 'loanapplication'
    },

    layout: 'border',

    items: [{
        region: 'north',
        xtype: 'toolbar',
        items: [{
            xtype: 'component',
            html: 'Loan Applications',
            style: 'font-size: 24px;'
        }, '->', {
            xtype: 'button',
            text: 'Logout',
            handler: 'onLogoutClick'
        }]
    }, {
        region: 'center',
        xtype: 'grid',
        bind: {
            store: '{loans}'
        },
        columns: [
            { text: 'Loan Amount', dataIndex: 'loanAmount', renderer: 'renderCurrency', flex: 1 },
            { text: 'Term (months)', dataIndex: 'loanTermInMonths', flex: 1 },
            {
                text: 'Currency',
                dataIndex: 'currencyType',
                renderer: function(value) {
                    var currencies = {
                        1: 'GEL',
                        2: 'USD',
                        3: 'EUR'
                    };
                    return currencies[value] || 'Unknown';
                },
                flex: 1
            },
            { text: 'Status', dataIndex: 'applicationStatusText', flex: 1 },
            { text: 'Loan Type', dataIndex: 'loanTypeText', flex: 1 },
            { text: 'Application Date', dataIndex: 'createDateFormatted', flex: 1 },
            {
                xtype: 'actioncolumn',
                text: 'Actions',
                items: [{
                    iconCls: 'x-fa fa-edit',
                    tooltip: 'Update',
                    handler: 'onUpdateClick'
                }, {
                    iconCls: 'x-fa fa-trash',
                    tooltip: 'Delete',
                    handler: 'onDeleteClick'
                }, {
                    iconCls: 'x-fa fa-check',
                    tooltip: 'Approve',
                    handler: 'onApproveClick'
                }],
                width: 100
            }
        ],
        tbar: [{
            text: 'Add New Loan Application',
            handler: 'onAddLoanClick'
        }]
    }]
});