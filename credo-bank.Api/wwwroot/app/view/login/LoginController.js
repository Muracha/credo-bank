Ext.define('app.view.login.LoginController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.login',

    onLoginClick: function() {
        var view = this.getView();
        var form = view.down('form'); // Find the form within the view

        if (form && form.isValid()) {
            var values = form.getValues();

            // Create the LoginInputDto object
            var loginInputDto = {
                identificationNumber: values.identificationNumber,
                password: values.password
            };

            Ext.Ajax.request({
                url: 'https://localhost:44313/auth/login', // Adjust this URL to match your API endpoint
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
        if (result.data && result.data.authReposnoseDto && result.data.authReposnoseDto.token && result.data.authReposnoseDto.refreshToken) {
            // Store the tokens
            localStorage.setItem('authToken', result.data.authReposnoseDto.token);
            localStorage.setItem('refreshToken', result.data.authReposnoseDto.refreshToken);

            // Set up default headers for future requests
            Ext.Ajax.setDefaultHeaders({
                'Authorization': 'Bearer ' + result.data.authReposnoseDto.token
            });

            Ext.Msg.alert('Success', 'Login successful!', function() {
                console.log('Login successful.');
                this.navigateToLoanApplication();
            }, this);
        } else {
            Ext.Msg.alert('Error', 'Invalid response from server.');
        }
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

    navigateToLoanApplication: function() {
        // Remove the login view
        this.getView().destroy();

        // Create and show the Loan Application view
        Ext.create('app.view.loanapplication.LoanApplication', {
            renderTo: Ext.getBody(),
            fullscreen: true
        });
    }
});