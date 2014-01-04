var eyContacts = angular.module('eyContacts', [
    'ngResource'
]);

eyContacts.factory('Contacts', ['$resource', 'apiRootUrl', function($resource, apiRootUrl) {
    return $resource(apiRootUrl + '/contacts/:contactIdentifier', null,
        {
            'insert': { method: 'POST' },
            'update': { method: 'PUT', params: {contactIdentifier: '@Identifier'} }
        });
}]);
