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

        it('should remove a contact group member when removeMember is called with a contact identifier', function() {
            var contactIdentifier1 = 'id1';
            var contactIdentifier2 = 'id2';
            var contactIdentifier3 = 'id3';

            instance.addMember(contactIdentifier1);
            instance.addMember(contactIdentifier2);
            instance.addMember(contactIdentifier3);
            
            instance.removeMember(contactIdentifier2);

            expect(instance.Members.length).toBe(2);
            expect(instance.Members[0].ContactIdentifier).toBe(contactIdentifier1);
            expect(instance.Members[1].ContactIdentifier).toBe(contactIdentifier3);
        });
    });

    describe('ContactGroupMember', function () {
        var contactGroupMember;

        beforeEach(inject(function ($injector) {
            var contactGroup = $injector.get('ContactGroups').create();
            contactGroup.addMember('id1');
            contactGroupMember = contactGroup.Members[0];
        }));

        it('should add a relationship when addRelationship is called with a relationship name', function() {
            var relationshipName = 'Friend';

            contactGroupMember.addRelationship(relationshipName);

            expect(contactGroupMember.Relationships.length).toBe(1);
            expect(contactGroupMember.Relationships[0].Name).toBe(relationshipName);
        });

        it('should remove a relationship when removeRelationship is called with a relationship', function() {
            contactGroupMember.addRelationship('Friend');

            contactGroupMember.removeRelationship(contactGroupMember.Relationships[0]);
            
            expect(contactGroupMember.Relationships.length).toBe(0);
        });
    });
});