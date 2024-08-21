Ext.define('app.view.register.RegisterModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.register',

    data: {
        firstName: '',
        lastName: '',
        identificationNumber: '',
        password: '',
        confirmPassword: '',
        dateOfBirth: null
    }
});