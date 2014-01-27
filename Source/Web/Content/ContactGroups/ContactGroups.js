var eyContactGroups = angular.module('eyContactGroups', [
    'ngResource'
]);

eyContactGroups.factory('ContactGroups', ['$resource', 'apiRootUrl', function ($resource, apiRootUrl) {
    var ContactGroups = $resource(apiRootUrl + '/contactGroups/:contactGroupIdentifier', null,
        {
            'update': {
                method: 'PUT',
                params: { contactGroupIdentifier: '@Identifier' }
            },
            'getMembers': {
                method: 'GET',
                isArray: true,
                url: apiRootUrl + '/contactGroups/:contactGroupIdentifier/members'
            }
        });

    ContactGroups.create = function() {
        return new ContactGroups({
            Name: null,
            Members: []
        });
    };

    ContactGroups.prototype.addMember = function(contactIdentifier) {
        this.Members.push({ ContactIdentifier: contactIdentifier });
    };
    
    return ContactGroups;
}]);
