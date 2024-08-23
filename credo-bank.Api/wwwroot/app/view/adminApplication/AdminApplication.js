Ext.define('app.view.adminApplication.AdminApplication', {
    extend: 'Ext.container.Viewport',
    xtype: 'adminapplicationview',

    requires: [
        'app.view.adminApplication.AdminApplicationController',
        'app.view.adminApplication.AdminApplicationModel',
        'app.model.AdminApplication'
    ],

    controller: 'adminapplication',
    viewModel: {
        type: 'adminapplication'
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
            { text: 'Currency', dataIndex: 'currencyTypeText', flex: 1 },
            { text: 'Status', dataIndex: 'applicationStatusText', flex: 1 },
            { text: 'Loan Type', dataIndex: 'loanTypeText', flex: 1 },
            { text: 'Application Date', dataIndex: 'createDateFormatted', flex: 1 },
            {
                xtype: 'actioncolumn',
                text: 'Actions',
                items: [ {
                    iconCls: 'x-fa fa-edit',
                    tooltip: 'Update',
                    handler: 'onUpdateClick'
                },{
                    iconCls: 'x-fa fa-trash',
                    tooltip: 'Delete',
                    handler: 'onDeleteClick'
                }, {
                    iconCls: 'x-fa fa-check',
                    tooltip: 'Approve',
                    handler: 'onApproveClick'
                }, {
                    iconCls: 'x-fa fa-times',
                    tooltip: 'Reject',
                    handler: 'onRejectClick',
                }],
                width: 100
            }
        ],
        tbar: ['->', {
            text: 'Refresh',
            iconCls: 'x-fa fa-refresh',
            handler: 'onRefreshClick'
        }]
    }]
});