describe('Contacts Services', function() {
    var apiRoot = '/api';

    beforeEach(module('contactsApp', function ($provide) {
        $provide.value('apiRootUrl', apiRoot);
    }));

    describe('alerts', function () {
        var createService;
        
        beforeEach(inject(function ($injector) {
            createService = function() {
                return $injector.get('alerts');
            };
        }));

        it('should store an alert when adding an alert with text and type', function () {
            var text = 'The text';
            var type = 'info';
            
            var service = createService();
            
            service.addAlert(text, type);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe(type);
        });

        it('should store an alert with type of info when adding info with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addInfo(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('info');
        });

        it('should store an alert with type of success when adding success with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addSuccess(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('success');
        });

        it('should store an alert with type of warning when adding warning with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addWarning(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('warning');
        });

        it('should store an alert with type of danger when adding danger with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addDanger(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('danger');
        });

        it('should set an array of alerts on the scope when displaying alerts', function () {
            var text = 'The text';
            var type = 'info';
            
            var service = createService();
            service.addAlert(text, type);

            var scope = {};
            service.displayAlerts(scope);
            
            expect(scope.alerts.length).toBe(1);
            expect(scope.alerts[0].text).toBe(text);
            expect(scope.alerts[0].type).toBe(type);
        });

        it('should clear the alerts when displaying alerts', function () {
            var text = 'The text';
            var type = 'info';
            
            var service = createService();
            service.addAlert(text, type);

            service.displayAlerts({});
            
            expect(service.readAlerts().length).toBe(0);
        });
    });

    describe('contactRepository', function() {
        var $httpBackend, createRepository;
        
        beforeEach(inject(function ($injector) {
            $httpBackend = $injector.get('$httpBackend');

            createRepository = function () {
                return $injector.get('contactRepository');
            };
        }));

        afterEach(function () {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });

        it('should make a get request when asked to get contacts', function() {
            var repository = createRepository();

            var contacts = [];
            $httpBackend.expectGET(apiRoot + '/contacts').respond(contacts);

            repository.getContacts();
            $httpBackend.flush();
        });

        it('should make a get request when asked to get a single contact', function () {
            var contactIdentifier = 'id1';
            
            var repository = createRepository();

            var contact = {};
            $httpBackend.expectGET(apiRoot + '/contacts/' + contactIdentifier).respond(contact);

            repository.getContact(contactIdentifier);
            $httpBackend.flush();
        });

        it('should make a post request when asked to insert a contact', function () {
            var contact = {
                FirstName: 'Joe',
                LastName: 'Contact'
            };

            var repository = createRepository();

            $httpBackend.expectPOST(apiRoot + '/contacts', angular.toJson(contact)).respond(204, '');

            repository.insertContact(contact);
            $httpBackend.flush();
        });

        it('should make a put request when asked to update a contact', function () {
            var contactIdentifier = 'id1';
            var contact = {
                Identifier: contactIdentifier,
                FirstName: 'Joe',
                LastName: 'Contact'
            };

            var repository = createRepository();

            $httpBackend.expectPUT(apiRoot + '/contacts/' + contactIdentifier, angular.toJson(contact)).respond(204, '');

            repository.updateContact(contact);
            $httpBackend.flush();
        });

        it('should make a delete request when asked to delete a contact', function () {
            var contactIdentifier = 'id1';

            var repository = createRepository();

            $httpBackend.expectDELETE(apiRoot + '/contacts/' + contactIdentifier).respond(204, '');

            repository.deleteContact(contactIdentifier);
            $httpBackend.flush();
        });
    });
});
