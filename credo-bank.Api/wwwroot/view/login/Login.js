Ext.define('view.login.Login', {
    extend: 'Ext.form.Panel',
    xtype: 'loginform',

    requires: ['view.register.Register'],

    title: 'Login to CredoBank',
    titleAlign: 'center',
    width: 400,
    height: 280,
    frame: true,
    bodyPadding: 20,
    bodyStyle: 'background-color: #f8f8f8;',
    shadow: true,

    defaultType: 'textfield',
    defaults: {
        anchor: '100%',
        labelWidth: 150,
        labelStyle: 'font-weight: bold; color: #00008B;',
        padding: '10 0'
    },

    items: [{
        fieldLabel: 'Identification Number',
        name: 'identificationNumber',
        allowBlank: false,
        maxLength: 11,
        minLength: 11,
        enforceMaxLength: true,
        emptyText: 'Enter 11 digit ID number',
        maskRe: /[0-9]/,
        validator: function(value) {
            if (!/^\d{11}$/.test(value)) {
                return 'Identification Number must be exactly 11 digits';
            }
            return true;
        }
    }, {
        fieldLabel: 'Password',
        name: 'password',
        inputType: 'password',
        allowBlank: false,
        emptyText: 'Enter your password'
    }],

    buttons: [{
        text: 'Login',
        scale: 'large',
        formBind: true,
        style: {
            backgroundColor: '#00008B',
            borderColor: '#00008B'
        },
        textStyle: {
            color: 'white'
        },
        handler: function() {
            var form = this.up('form').getForm();
            if (form.isValid()) {
                Ext.Msg.wait('Logging in...', 'Please wait');

                var formValues = form.getValues();
                var loginData = {
                    identificationNumber: parseInt(formValues.identificationNumber, 10),
                    password: formValues.password
                };

                Ext.Ajax.request({
                    url: '/api/auth/login', // Adjust this URL to match your API endpoint
                    method: 'POST',
                    jsonData: loginData,
                    success: function(response) {
                        Ext.Msg.hide();
                        var result = Ext.decode(response.responseText);
                        if (result.success) {
                            Ext.Msg.alert('Success', 'Login successful!', function() {
                                // Redirect or perform actions after successful login
                                console.log('Login successful:', result);
                            });
                        } else {
                            Ext.Msg.alert('Error', result.message || 'Login failed. Please try again.');
                        }
                    },
                    failure: function(response) {
                        Ext.Msg.hide();
                        Ext.Msg.alert('Error', 'Server error. Please try again later.');
                    }
                });
            } else {
                Ext.Msg.alert('Invalid Form', 'Please correct the errors in the form.');
            }
        }
    }, {
        text: 'Register',
        scale: 'large',
        style: {
            backgroundColor: 'white',
            borderColor: '#00008B'
        },
        textStyle: {
            color: '#00008B'
        },
        handler: function() {
            var registerWindow = Ext.create('view.register.Register');
            registerWindow.show();
        }
    }]
});