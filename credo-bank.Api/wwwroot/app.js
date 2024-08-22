Ext.application({
    name: 'CredoBank',

    requires: [
        'app.view.login.Login',
        'app.view.register.Register',
        'app.view.loanapplication.LoanApplication'
    ],

    launch: function() {
        var authToken = localStorage.getItem('authToken');
        if (authToken) {
            Ext.Ajax.setDefaultHeaders({
                'Authorization': 'Bearer ' + authToken
            });
            Ext.create('app.view.loanapplication.LoanApplication');
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