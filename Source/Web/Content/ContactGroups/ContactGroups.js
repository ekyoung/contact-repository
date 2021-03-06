﻿var eyContactGroups = angular.module('eyContactGroups', [
    'ngResource'
]);

eyContactGroups.factory('ContactGroups', ['$resource', 'apiRootUrl', function ($resource, apiRootUrl) {
    var ContactGroupMember = function(contactIdentifier) {
        this.ContactIdentifier = contactIdentifier;
        this.Relationships = [];
    };

    ContactGroupMember.prototype.addRelationship = function(relationshipName) {
        this.Relationships.push({ Name: relationshipName });
    };

    ContactGroupMember.prototype.removeRelationship = function(relationship) {
        var index = this.Relationships.indexOf(relationship);

        if (index >= 0) {
            this.Relationships.splice(index, 1);
        }
    };
    
    var ContactGroups = $resource(apiRootUrl + '/contactGroups/:contactGroupIdentifier', null,
        {
            'get': {
                method: 'GET',
                transformResponse: function (data, headersGetter) {
                    var contactGroup = angular.fromJson(data);
                    angular.forEach(contactGroup.Members, function (item, idx) {
                        var contactGroupMember = new ContactGroupMember(item.ContactIdentifier);
                        angular.forEach(item.Relationships, function(relationship, idx) {
                            contactGroupMember.addRelationship(relationship.Name);
                        });
                        contactGroup.Members[idx] = contactGroupMember;
                    });
                    return contactGroup;
                }
            },
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
        this.Members.push(new ContactGroupMember(contactIdentifier));
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
