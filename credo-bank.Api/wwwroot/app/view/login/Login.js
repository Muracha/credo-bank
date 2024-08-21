Ext.define('app.view.login.Login', {
    extend: 'Ext.container.Viewport',
    xtype: 'loginform',

    requires: [
        'app.view.login.LoginController',
        'app.view.login.LoginModel'
    ],

    controller: 'login',
    viewModel: {
        type: 'login'
    },

    items: [{
        xtype: 'form',
        title: 'Login to CredoBank',
        titleAlign: 'center',
        width: 400,
        height: 280,
        frame: true,
        bodyPadding: 20,
        bodyStyle: 'background-color: #f8f8f8;',
        shadow: true,

        items: [{
            xtype: 'textfield',
            fieldLabel: 'Identification Number',
            name: 'identificationNumber',
            bind: '{identificationNumber}',
            allowBlank: false,
            maxLength: 11,
            minLength: 11,
            enforceMaxLength: true,
            emptyText: 'Enter 11 digit ID number',
            maskRe: /[0-9]/
        }, {
            xtype: 'textfield',
            fieldLabel: 'Password',
            name: 'password',
            bind: '{password}',
            inputType: 'password',
            allowBlank: false,
            emptyText: 'Enter your password'
        }],

        buttons: [{
            text: 'Login',
            formBind: true,
            handler: 'onLoginClick'
        }, {
            text: 'Register',
            handler: 'onRegisterClick'
        }]
    }],

    renderTo: Ext.getBody()
});
