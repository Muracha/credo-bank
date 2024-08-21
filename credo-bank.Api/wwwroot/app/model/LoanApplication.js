Ext.define('app.model.LoanApplication', {
    extend: 'Ext.data.Model',

    fields: [
        { name: 'id', type: 'int' },
        { name: 'loanAmount', type: 'int' },
        { name: 'loanTermInMonths', type: 'int' },
        { name: 'currencyType', type: 'int' },
        { name: 'applicationStatus', type: 'int' },
        { name: 'loanType', type: 'int' },
        {
            name: 'createDate',
            type: 'date',
            dateFormat: 'c'
        },
        {
            name: 'currencyTypeText',
            calculate: function(data) {
                var currencyTypes = {
                    1: 'GEL',
                    2: 'USD',
                    3: 'EUR'
                };
                return currencyTypes[data.currencyType];
            }
        },
        {
            name: 'applicationStatusText',
            calculate: function(data) {
                var statuses = ['Unknown', 'Pending', 'Approved', 'Rejected', 'Under Review'];
                return statuses[data.applicationStatus] || 'Unknown';
            }
        },
        {
            name: 'loanTypeText',
            calculate: function(data) {
                var types = ['Unknown', 'Personal', 'Business', 'Mortgage', 'Auto'];
                return types[data.loanType] || 'Unknown';
            }
        },
        {
            name: 'createDateFormatted',
            calculate: function(data) {
                return Ext.Date.format(data.createDate, 'Y-m-d H:i:s');
            }
        }
    ]
});