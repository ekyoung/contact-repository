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
});