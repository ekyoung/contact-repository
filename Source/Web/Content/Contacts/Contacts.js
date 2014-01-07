var eyContacts = angular.module('eyContacts', [
    'ngResource'
]);

eyContacts.factory('Contacts', ['$resource', 'apiRootUrl', function ($resource, apiRootUrl) {
    var Contacts =  $resource(apiRootUrl + '/contacts/:contactIdentifier', null,
        {
            'update': {
                method: 'PUT',
                params: { contactIdentifier: '@Identifier' }
            }
        });

    Contacts.create = function() {
        return new Contacts({
            FirstName: null,
            LastName: null,
            EmailAddresses: [],
            PhoneNumbers: []
        });
    };

    Contacts.prototype._removeContactInfo = function(toRemove, arr) {
        var index = arr.indexOf(toRemove);

        if (index >= 0) {
            arr.splice(index, 1);
        }

        if (arr.length > 0) {
            var anyPrimary = false;
            for (var i = 0; i < arr.length; i++) {
                anyPrimary = anyPrimary || arr[i].IsPrimary;
            }

            if (!anyPrimary) {
                arr[0].IsPrimary = true;
            }
        }
    };

    Contacts.prototype._setPrimaryContactInfo = function(newPrimary, arr) {
        for (var i = 0; i < arr.length; i++) {
            arr[i].IsPrimary = false;
        }

        newPrimary.IsPrimary = true;
    };
    
    Contacts.prototype.addEmailAddress = function() {
        var isNewEmailAddressPrimary = this.EmailAddresses.length == 0;
        this.EmailAddresses.push({ EmailAddress: null, NickName: null, IsPrimary: isNewEmailAddressPrimary });
    };
    
    Contacts.prototype.removeEmailAddress = function (contactEmailAddressToRemove) {
        this._removeContactInfo(contactEmailAddressToRemove, this.EmailAddresses);
    };
    
    Contacts.prototype.setPrimaryEmailAddress = function (newPrimaryEmailAddress) {
        this._setPrimaryContactInfo(newPrimaryEmailAddress, this.EmailAddresses);
    };

    Contacts.prototype.addPhoneNumber = function() {
        var isNewPhoneNumberPrimary = this.PhoneNumbers.length == 0;
        this.PhoneNumbers.push({ PhoneNumber: null, NickName: null, IsPrimary: isNewPhoneNumberPrimary });
    };

    Contacts.prototype.removePhoneNumber = function(contactPhoneNumberToRemove) {
        this._removeContactInfo(contactPhoneNumberToRemove, this.PhoneNumbers);
    };

    Contacts.prototype.setPrimaryPhoneNumber = function(newPrimaryPhoneNumber) {
        this._setPrimaryContactInfo(newPrimaryPhoneNumber, this.PhoneNumbers);
    };
    return Contacts;
}]);
