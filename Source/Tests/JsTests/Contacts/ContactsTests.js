describe('eyContacts', function () {
    var apiRootUrl = '/api';
    
    beforeEach(module('eyContacts', function($provide) {
        $provide.value('apiRootUrl', apiRootUrl);
    }));

    describe('Contacts', function () {
        var createResource;
        
        beforeEach(inject(function ($injector) {
            createResource = function () {
                return $injector.get('Contacts');
            };
        }));

        it('should return an empty instance when create is called', function () {
            var Contacts = createResource();

            var instance = Contacts.create();

            expect(instance.Identifier).toBeUndefined();
            expect(instance.FirstName).toBe(null);
            expect(instance.LastName).toBe(null);
            expect(instance.EmailAddresses.length).toBe(0);
        });
    });

    describe('Contacts instance', function() {
        var instance;
        
        beforeEach(inject(function ($injector) {
            instance = $injector.get('Contacts').create();
        }));

        it('should add a contact email address with IsPrimary true to the contact when addEmailAddress is called and the contact has no email addresses', function () {
            instance.addEmailAddress();

            expect(instance.EmailAddresses.length).toBe(1);
            expect(instance.EmailAddresses[0].IsPrimary).toBe(true);
        });

        it('should add a contact email address with IsPrimary false to the contact when addEmailAddress is called and the contact already has an email address', function () {
            instance.EmailAddresses = [{ EmailAddress: 'fake@email.com', NickName: null, IsPrimary: true }];

            instance.addEmailAddress();

            expect(instance.EmailAddresses.length).toBe(2);
            expect(instance.EmailAddresses[1].IsPrimary).toBe(false);
        });
        
        it('should remove a contact email address when removeEmailAddress is called with a contact email address that is not the primary address', function () {
            var primaryEmailAddress = { EmailAddress: 'primary@email.com', NickName: null, IsPrimary: true },
                otherEmailAddress = { EmailAddress: 'other@email.com', NickName: null, IsPrimary: false };
            instance.EmailAddresses = [primaryEmailAddress, otherEmailAddress];

            instance.removeEmailAddress(otherEmailAddress);

            expect(instance.EmailAddresses.length).toBe(1);
            expect(instance.EmailAddresses[0]).toBe(primaryEmailAddress);
        });

        it('should remove a contact email address and make the first remaining address primary when removeEmailAddress is called with the primary address', function () {
            var primaryEmailAddress = { EmailAddress: 'primary@email.com', NickName: null, IsPrimary: true },
                otherEmailAddress = { EmailAddress: 'other@email.com', NickName: null, IsPrimary: false };
            instance.EmailAddresses = [primaryEmailAddress, otherEmailAddress];

            instance.removeEmailAddress(primaryEmailAddress);

            expect(instance.EmailAddresses.length).toBe(1);
            expect(instance.EmailAddresses[0]).toBe(otherEmailAddress);
            expect(otherEmailAddress.IsPrimary).toBe(true);
        });
        
        it('should make only the specified email address primary when setPrimaryEmailAddress is called', function () {
            var primaryEmailAddress = { EmailAddress: 'primary@email.com', NickName: null, IsPrimary: true },
                otherEmailAddress = { EmailAddress: 'other@email.com', NickName: null, IsPrimary: false };
            instance.EmailAddresses = [primaryEmailAddress, otherEmailAddress];

            instance.setPrimaryEmailAddress(otherEmailAddress);

            expect(primaryEmailAddress.IsPrimary).toBe(false);
            expect(otherEmailAddress.IsPrimary).toBe(true);
        });

    });
});