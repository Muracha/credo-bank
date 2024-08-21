Ext.define('app.view.register.Register', {
    extend: 'Ext.window.Window',
    xtype: 'registerform',

    requires: [
        'app.view.register.RegisterController',
        'app.view.register.RegisterModel'
    ],

    controller: 'register',
    viewModel: {
        type: 'register'
    },

    title: 'Register for CredoBank',
    width: 400,
    modal: true,
    closeAction: 'hide',
    layout: 'fit',

    items: [{
        xtype: 'form',
        bodyPadding: 20,
        defaults: {
            xtype: 'textfield',
            anchor: '100%',
            labelWidth: 120
        },
        items: [{
            fieldLabel: 'First Name',
            name: 'firstName',
            bind: '{firstName}',
            allowBlank: false
        }, {
            fieldLabel: 'Last Name',
            name: 'lastName',
            bind: '{lastName}',
            allowBlank: false
        }, {
            fieldLabel: 'Identification Number',
            name: 'identificationNumber',
            bind: '{identificationNumber}',
            allowBlank: false,
            maxLength: 11,
            minLength: 11,
            enforceMaxLength: true,
            maskRe: /[0-9]/
        }, {
            fieldLabel: 'Password',
            name: 'password',
            bind: '{password}',
            inputType: 'password',
            allowBlank: false
        }, {
            fieldLabel: 'Confirm Password',
            name: 'confirmPassword',
            bind: '{confirmPassword}',
            inputType: 'password',
            allowBlank: false,
            validator: function(value) {
                var password = this.up('window').getViewModel().get('password');
                return (value === password) ? true : 'Passwords do not match';
            }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Date of Birth',
            name: 'dateOfBirth',
            bind: '{dateOfBirth}',
            allowBlank: false,
            maxValue: new Date(),
            format: 'Y-m-d'
        }]
    }],

    buttons: [{
        text: 'Register',
        formBind: true,
        handler: 'onRegisterClick'
    }, {
        text: 'Cancel',
        handler: 'onCancelClick'
    }]
});