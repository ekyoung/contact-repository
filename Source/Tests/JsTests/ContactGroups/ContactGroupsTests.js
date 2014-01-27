describe('eyContactGroups', function () {
    var apiRootUrl = '/api';

    beforeEach(module('eyContactGroups', function ($provide) {
        $provide.value('apiRootUrl', apiRootUrl);
    }));

    describe('ContactGroups', function () {
        var createResource;
        
        beforeEach(inject(function ($injector) {
            createResource = function () {
                return $injector.get('ContactGroups');
            };
        }));

        it('should return an empty instance when create is called', function () {
            var ContactGroups = createResource();

            var instance = ContactGroups.create();

            expect(instance.Identifier).toBeUndefined();
            expect(instance.Name).toBe(null);
            expect(instance.Members.length).toBe(0);
        });
    });

    describe('ContactGroups instance', function() {
        var instance;

        beforeEach(inject(function ($injector) {
            instance = $injector.get('ContactGroups').create();
        }));

        it('should add a contact group member when addMember is called with a contact identifier', function() {
            var contactIdentifier = 'blah';

            instance.addMember(contactIdentifier);

            expect(instance.Members.length).toBe(1);
            expect(instance.Members[0].ContactIdentifier).toBe(contactIdentifier);
        });
    });
});