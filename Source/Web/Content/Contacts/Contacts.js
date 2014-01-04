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

    Contacts.prototype.addEmailAddress = function() {
        var isNewAddressPrimary = this.EmailAddresses.length == 0;
        this.EmailAddresses.push({ EmailAddress: null, NickName: null, IsPrimary: isNewAddressPrimary });
    };
    
    Contacts.prototype.removeEmailAddress = function (contactEmailAddressToRemove) {
        var emailAddresses = this.EmailAddresses,
            index = emailAddresses.indexOf(contactEmailAddressToRemove);

        if (index >= 0) {
            emailAddresses.splice(index, 1);
        }

        if (emailAddresses.length > 0) {
            var anyPrimary = false;
            for (var i = 0; i < emailAddresses.length; i++) {
                anyPrimary = anyPrimary || emailAddresses[i].IsPrimary;
            }

            if (!anyPrimary) {
                emailAddresses[0].IsPrimary = true;
            }
        }
    };
    
    Contacts.prototype.setPrimaryEmailAddress = function (newPrimaryEmailAddress) {
        var emailAddresses = this.EmailAddresses;

        for (var i = 0; i < emailAddresses.length; i++) {
            emailAddresses[i].IsPrimary = false;
        }

        newPrimaryEmailAddress.IsPrimary = true;
    };
    
    return Contacts;
}]);
