var eyContactGroups = angular.module('eyContactGroups', [
    'ngResource'
]);

eyContactGroups.factory('ContactGroups', ['$resource', 'apiRootUrl', function ($resource, apiRootUrl) {
    var ContactGroups = $resource(apiRootUrl + '/contactGroups/:contactGroupIdentifier', null,
        {
            'update': {
                method: 'PUT',
                params: { contactGroupIdentifier: '@Identifier' }
            }
        });

    ContactGroups.create = function() {
        return new ContactGroups({
            Name: null,
            Members: []
        });
    };

    return ContactGroups;
}]);
