Ext.define('app.view.adminApplication.AdminApplicationModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.adminapplication',

    stores: {
        loans: {
            model: 'app.model.AdminApplication',
            proxy: {
                type: 'ajax',
                url: 'https://localhost:44313/admin',
                reader: {
                    type: 'json',
                    rootProperty: 'data.loanApplicationDtos',
                    successProperty: 'success',
                    messageProperty: 'message'
                }
            },
            autoLoad: false
        }
    }
});

