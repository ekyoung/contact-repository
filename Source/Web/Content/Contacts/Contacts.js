var eyContacts = angular.module('eyContacts', [
    'ngResource'
]);

eyContacts.factory('Contacts', ['$resource', 'apiRootUrl', function($resource, apiRootUrl) {
    var Contacts =  $resource(apiRootUrl + '/contacts/:contactIdentifier', null,
        {
            'update': { method: 'PUT', params: {contactIdentifier: '@Identifier'} }
        });

    Contacts.create = function() {
        return new Contacts({
            FirstName: null,
            LastName: null,
            EmailAddresses: []
        });
    };
    
    return Contacts;
}]);
