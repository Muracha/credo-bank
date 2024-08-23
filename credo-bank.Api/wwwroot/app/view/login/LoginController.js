Ext.define('app.view.login.LoginController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.login',

    onLoginClick: function() {
        var view = this.getView();
        var form = view.down('form');

        if (form && form.isValid()) {
            var values = form.getValues();
            
            var loginInputDto = {
                identificationNumber: values.identificationNumber,
                password: values.password
            };

            Ext.Ajax.request({
                url: 'https://localhost:44313/auth/login',
                method: 'POST',
                jsonData: loginInputDto,
                success: this.onLoginSuccess,
                failure: this.onLoginFailure,
                scope: this
            });
        } else {
            Ext.Msg.alert('Invalid Form', 'Please correct the errors in the form.');
        }
    },

    onLoginSuccess: function(response) {
        var result = Ext.decode(response.responseText);
        if (result.data && result.data.authReposnoseDto) {
            var token = result.data.authReposnoseDto.token;

            localStorage.setItem('authToken', token);

            Ext.Ajax.setDefaultHeaders({
                'Authorization': 'Bearer ' + token
            });

            // Simplified role check
            var isAdmin = this.hasRole(token, 'Admin');

            Ext.Msg.alert('Success', 'Login successful!', function() {
                if (isAdmin) {
                    localStorage.setItem('userRole', 'Admin');
                    this.navigateToAdminDashboard();
                } else {
                    localStorage.setItem('userRole', 'User');
                    this.navigateToLoanApplication();
                }
            }, this);
        } else {
            Ext.Msg.alert('Error', 'Invalid response from server.');
        }
    },

    hasRole: function(token, roleName) {
        var payload = JSON.parse(atob(token.split('.')[1]));
        return payload.role;
    },

    onLoginFailure: function(response) {
        var errorMessage = 'Login failed. Please try again.';

        if (response.status === 401) {
            errorMessage = 'Invalid credentials. Please check your Identification Number and Password.';
        } else if (response.status === 400) {
            var result = Ext.decode(response.responseText);
            errorMessage = result.message || 'Invalid input. Please check your entries.';
        }

        Ext.Msg.alert('Error', errorMessage);
    },

    onRegisterClick: function() {
        Ext.create('app.view.register.Register').show();
    },

    navigateToAdminDashboard: function() {

        this.getView().destroy();
        
        Ext.create('app.view.adminApplication.AdminApplication', {
            renderTo: Ext.getBody(),
            fullscreen: true
        });
    },
    
    navigateToLoanApplication: function() {

        this.getView().destroy();

        Ext.create('app.view.loanapplication.LoanApplication', {
            renderTo: Ext.getBody(),
            fullscreen: true
        });
    }
});