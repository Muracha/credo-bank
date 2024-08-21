Ext.application({
    name: 'CredoBank',

    requires: [
        'app.view.login.Login',
        'app.view.register.Register',
        'app.view.loanapplication.LoanApplication'
    ],

    launch: function() {
        // Check if user is already logged in
        var authToken = localStorage.getItem('authToken');
        if (authToken) {
            // User is logged in, show fullscreen loan application view
            Ext.create('app.view.loanapplication.LoanApplication');
        } else {
            // User is not logged in, show login form
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