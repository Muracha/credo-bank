Ext.application({
    name: 'CredoBank',

    requires: [
        'app.view.login.Login',
        'app.view.register.Register',
        'app.view.loanapplication.LoanApplication',
        'app.view.adminApplication.AdminApplication'
    ],

    launch: function() {
        var authToken = localStorage.getItem('authToken');
        var userRole = localStorage.getItem('userRole');

        if (authToken) {
            Ext.Ajax.setDefaultHeaders({
                'Authorization': 'Bearer ' + authToken
            });

            if (userRole === 'Admin') {
                Ext.create('app.view.adminApplication.AdminApplication');
            } else {
                Ext.create('app.view.loanapplication.LoanApplication');
            }
        } else {
            Ext.create('app.view.login.Login', {
                layout: {
                    type: 'vbox',
                    align: 'center',
                    pack: 'center'
                },
            });
        }
    }
});