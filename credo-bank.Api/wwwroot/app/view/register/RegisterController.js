Ext.define('app.view.register.RegisterController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.register',

    onRegisterClick: function() {
        var view = this.getView();
        var form = view.down('form').getForm();
        var values = form.getValues();

        if (form.isValid()) {
            Ext.Ajax.request({
                url: 'https://localhost:44313/auth/register',
                method: 'POST',
                jsonData: {
                    firstName: values.firstName,
                    lastName: values.lastName,
                    identificationNumber: values.identificationNumber,
                    password: values.password,
                    dateOfBirth: Ext.Date.format(new Date(values.dateOfBirth), 'Y-m-d')
                },
                success: this.onRegisterSuccess,
                failure: this.onRegisterFailure,
                scope: this
            });
        }
    },

    onRegisterSuccess: function(response) {
        var result = Ext.decode(response.responseText);
        if (result.success) {
            Ext.Msg.alert('Success', 'Registration successful!', function() {
                this.getView().close();
            }, this);
        } else {
            Ext.Msg.alert('Error', result.message || 'Registration failed.');
        }
    },

    onRegisterFailure: function() {
        Ext.Msg.alert('Error', 'Server error. Please try again later.');
    },

    onCancelClick: function() {
        this.getView().close();
    }
});