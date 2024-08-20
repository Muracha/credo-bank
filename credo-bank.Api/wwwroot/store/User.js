Ext.define('store.Users', {
    extend: 'Ext.data.Store',
    alias: 'store.users',
    model: 'Model.User',
    proxy: {
        type: 'ajax',
        url: '/api/users',
        reader: {
            type: 'json',
            rootProperty: 'data'
        }
    }
});