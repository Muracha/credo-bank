Ext.application({
    name: 'CredoBank',
    requires: ['view.login.Login', 'view.register.Register'],
    launch: function() {
        Ext.create('Ext.container.Viewport', {
            layout: {
                type: 'vbox',
                align: 'center',
                pack: 'center'
            },
            style: {
                backgroundColor: 'white'
            },
            items: [{
                xtype: 'component',
                html: '<h1 style="color: #00008B; font-size: 28px; margin-bottom: 20px; font-weight: bold;">Welcome to CredoBank</h1>'
            }, {
                xtype: 'loginform'
            }]
        });
    }
});