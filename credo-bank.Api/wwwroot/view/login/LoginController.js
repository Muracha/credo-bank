Ext.define('view.login.LoginController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.login',

    onLoginClick: function() {
        var form = this.getView().getForm();
        if (form.isValid()) {
            Ext.Msg.alert('Login', 'Login successful!');
        }
    },

    onRegisterClick: function() {
        Ext.Msg.alert('Register', 'Registration form would open here.');
    }
});