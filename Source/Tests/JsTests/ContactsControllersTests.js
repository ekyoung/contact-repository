describe('Contacts Controllers', function () {
    beforeEach(module('contactsApp'));

    var alerts,
        apiRoot = '/api';

    beforeEach(function() {
        alerts = {
            addSuccess: jasmine.createSpy(),
            addInfo: jasmine.createSpy(),
            displayAlerts: jasmine.createSpy()
        };
    });

    describe('listController', function () {
        var $httpBackend, $scope, createController;
        
        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');

            $scope = $injector.get('$rootScope');

            // The $controller service is used to create instances of controllers
            var $controller = $injector.get('$controller');

            createController = function() {
                return $controller('listController', { $scope: $scope, alerts: alerts });
            };
        }));

        it('should set an array of contacts on the scope when given an array of contacts', function () {
            var contacts = [{ FirstName: 'Joe', LastName: 'One' }, { FirstName: 'Frank', LastName: 'Two' }];
            $httpBackend.when('GET', apiRoot + '/contacts').respond(contacts);
            var controller = createController();
            $httpBackend.flush();
            
            expect($scope.contacts).toBe(contacts);
        });

        it('should display alerts in all cases', function () {
            var alertsArray = [{ text: 'The alert text', type: 'info' }];
            alerts.alerts = alertsArray;

            var controller = createController();

            expect(alerts.displayAlerts).toHaveBeenCalledWith($scope);
        });
    });

    describe('createController', function() {
        var $httpBackend, $scope, $location, createController;

        beforeEach(inject(function($injector) {
            $httpBackend = $injector.get('$httpBackend');

            $scope = $injector.get('$rootScope');

            $location = $injector.get('$location');
            $location.path('/create');

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('createController', { $scope: $scope, $location: $location, alerts: alerts });
            };
        }));

        afterEach(function () {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });

        it('should set an empty contact on the scope in all cases', function() {
            var controller = createController();

            expect($scope.contact.FirstName).toBe(null);
            expect($scope.contact.LastName).toBe(null);
        });

        it('should post json for the contact to the server when save is clicked', function () {
            var contact = {
                FirstName: 'Joe',
                LastName: 'Contact'
            };
            
            var controller = createController();
            $scope.contact = contact;

            $httpBackend.expectPOST(apiRoot + '/contacts', angular.toJson(contact)).respond(204, '');

            $scope.save();

            $httpBackend.flush();
        });
        
        it('should redirect to the list view when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();

            $httpBackend.when('POST', apiRoot + '/contacts').respond(204, '');
            $httpBackend.flush();

            expect($location.path()).toBe('/');
        });

        it('should add a success alert when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();

            $httpBackend.when('POST', apiRoot + '/contacts').respond(204, '');
            $httpBackend.flush();

            expect(alerts.addSuccess).toHaveBeenCalledWith('A new contact has been created.');
        });

        it('should redirect to the list view when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect($location.path()).toBe('/');
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(alerts.addInfo).toHaveBeenCalledWith('Creation of a new contact has been cancelled.');
        });
    });
    
    describe('editController', function () {
        var $httpBackend, $scope, $routeParams, $location, createController;

        var contactIdentifier = 'id1';
        var contact = { Identifier: contactIdentifier, FirstName: 'Joe', LastName: 'One' };

        beforeEach(inject(function ($injector) {
            $httpBackend = $injector.get('$httpBackend');
            $httpBackend.when('GET', apiRoot + '/contacts/' + contactIdentifier).respond(contact);

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            $location = $injector.get('$location');
            $location.path('/edit/' + contactIdentifier);

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('editController', { $scope: $scope, $routeParams: $routeParams, $location: $location, alerts: alerts });
            };
        }));
        
        afterEach(function() {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });
        
        it('should set a contact on the scope when given a contact', function () {
            var controller = createController();
            $httpBackend.flush();

            expect($scope.contact).toBe(contact);
        });
        
        it('should set the original name on the scope when given a contact', function () {
            var controller = createController();
            $httpBackend.flush();

            expect($scope.originalName).toBe(contact.FirstName + ' ' + contact.LastName);
        });

        it('should redirect to the list view when cancel is clicked', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.cancel();

            expect($location.path()).toBe('/');
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.cancel();

            expect(alerts.addInfo).toHaveBeenCalledWith('Changes to the contact have been cancelled.');
        });

        it('should put json for the contact to the server when save is clicked', function() {
            var controller = createController();
            $httpBackend.flush();

            $httpBackend.expectPUT(apiRoot + '/contacts/' + contactIdentifier, angular.toJson(contact)).respond(204, '');

            $scope.save();

            $httpBackend.flush();
        });
        
        it('should redirect to the list view when save is clicked and the operation is successful', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.save();

            $httpBackend.when('PUT', apiRoot + '/contacts/' + contactIdentifier).respond(204, '');
            $httpBackend.flush();
            
            expect($location.path()).toBe('/');
        });

        it('should add a success alert when save is clicked and the operation is successful', function () {
            var controller = createController();
            $httpBackend.flush();
            
            $scope.save();

            $httpBackend.when('PUT', apiRoot + '/contacts/' + contactIdentifier).respond(204, '');
            $httpBackend.flush();

            expect(alerts.addSuccess).toHaveBeenCalledWith('Changes to the contact have been saved.');
        });
    });

    describe('deleteController', function() {
        var $httpBackend, $scope, $routeParams, $location, createController;

        var contactIdentifier = 'id1';
        var contact = { Identifier: contactIdentifier, FirstName: 'Joe', LastName: 'One' };

        beforeEach(inject(function ($injector) {
            $httpBackend = $injector.get('$httpBackend');
            $httpBackend.when('GET', apiRoot + '/contacts/' + contactIdentifier).respond(contact);

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            $location = $injector.get('$location');
            $location.path('/delete/' + contactIdentifier);

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('deleteController', { $scope: $scope, $routeParams: $routeParams, $location: $location, alerts: alerts });
            };
        }));

        afterEach(function () {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });

        it('should set a contact on the scope when given a contact', function () {
            var controller = createController();
            $httpBackend.flush();

            expect($scope.contact).toBe(contact);
        });

        it('should send a delete message to the server when continue is clicked', function () {
            var controller = createController();
            $httpBackend.flush();

            $httpBackend.expectDELETE(apiRoot + '/contacts/' + contactIdentifier).respond(204, '');

            $scope.continue();

            $httpBackend.flush();
        });

        it('should redirect to the list view when continue is clicked and the operation is successful', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.continue();

            $httpBackend.when('DELETE', apiRoot + '/contacts/' + contactIdentifier).respond(204, '');
            $httpBackend.flush();

            expect($location.path()).toBe('/');
        });

        it('should add a success alert when continue is clicked and the operation is successful', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.continue();

            $httpBackend.when('DELETE', apiRoot + '/contacts/' + contactIdentifier).respond(204, '');
            $httpBackend.flush();

            expect(alerts.addSuccess).toHaveBeenCalledWith('The contact has been deleted.');
        });

        it('should redirect to the list view when cancel is clicked', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.cancel();

            expect($location.path()).toBe('/');
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.cancel();

            expect(alerts.addInfo).toHaveBeenCalledWith('Deletion of the contact has been cancelled.');
        });

    });
});
