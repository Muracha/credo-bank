﻿Ext.define('app.view.loanapplication.LoanApplicationModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.loanapplication',

    stores: {
        loans: {
            model: 'app.model.LoanApplication',
            proxy: {
                type: 'ajax',
                url: 'https://localhost:44313/loan',
                reader: {
                    type: 'json',
                    rootProperty: 'data.loanApplicationsDto',
                    successProperty: 'success',
                    messageProperty: 'message'
                }
            },
            autoLoad: false
        }
    }
});

