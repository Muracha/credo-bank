Ext.define('view.register.Register', {
    extend: 'Ext.window.Window',
    xtype: 'registerwindow',

    title: 'Register for CredoBank',
    width: 400,
    height: 350,
    modal: true,
    layout: 'fit',
    closeAction: 'hide',

    items: [{
        xtype: 'form',
        bodyPadding: 20,
        defaults: {
            anchor: '100%',
            labelWidth: 120,
            labelStyle: 'font-weight: bold; color: #00008B;',
            padding: '10 0'
        },
        items: [{
            xtype: 'textfield',
            fieldLabel: 'Full Name',
            name: 'fullName',
            allowBlank: false
        }, {
            xtype: 'textfield',
            fieldLabel: 'Identification Number',
            name: 'idNumber',
            allowBlank: false,
            maxLength: 11,
            minLength: 11,
            enforceMaxLength: true,
            maskRe: /[0-9]/,
            validator: function(value) {
                if (!/^\d{11}$/.test(value)) {
                    return 'Identification Number must be exactly 11 digits';
                }
                return true;
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Email',
            name: 'email',
            vtype: 'email',
            allowBlank: false
        }, {
            xtype: 'textfield',
            fieldLabel: 'Password',
            name: 'password',
            inputType: 'password',
            allowBlank: false
        }, {
            xtype: 'textfield',
            fieldLabel: 'Confirm Password',
            name: 'confirmPassword',
            inputType: 'password',
            allowBlank: false,
            validator: function(value) {
                var password = this.up('form').down('[name=password]').getValue();
                return (value === password) ? true : 'Passwords do not match';
            }
        }]
    }],

    buttons: [{
        text: 'Register',
        formBind: true,
        style: {
            backgroundColor: '#00008B',
            borderColor: '#00008B'
        },
        textStyle: {
            color: 'white'
        },
        handler: function() {
            var form = this.up('window').down('form').getForm();
            if (form.isValid()) {
                console.log('Registration data:', form.getValues());
                // Here you would typically send the registration data to your server
                Ext.Msg.alert('Success', 'Registration successful!');
                this.up('window').hide();
            }
        }
    }, {
        text: 'Cancel',
        handler: function() {
            this.up('window').hide();
        }
    }]
});