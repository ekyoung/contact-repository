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

    ContactGroups.prototype.removeMember = function(contactIdentifier) {
        for (var i = 0; i < this.Members.length; i++) {
            if (this.Members[i].ContactIdentifier == contactIdentifier) {
                this.Members.splice(i, 1);
                return;
            }
        }
    };
    
    return ContactGroups;
}]);
