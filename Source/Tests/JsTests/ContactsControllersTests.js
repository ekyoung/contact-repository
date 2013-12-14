describe('Contacts Controllers', function () {
    beforeEach(module('contactsApp'));

    var alerts;

    beforeEach(function() {
        alerts = {
            addSuccess: jasmine.createSpy(),
            addInfo: jasmine.createSpy(),
            displayAlerts: jasmine.createSpy()
        };
    });

    describe('listController', function () {
        var $httpBackend, $scope, createController;

        var apiRoot = '/api';
        
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

    describe('editController', function () {
        var $httpBackend, $scope, $routeParams, $location, createController;

        var apiRoot = '/api';
        var contactIdentifier = 'id1';
        var contact = { Identifier: contactIdentifier, FirstName: 'Joe', LastName: 'One' };

        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');
            $httpBackend.when('GET', apiRoot + '/contacts/' + contactIdentifier).respond(contact);

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            $location = $injector.get('$location');
            $location.path('/edit/' + contactIdentifier);

            // The $controller service is used to create instances of controllers
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

        it('should redirect to the list view when cancel is clicked', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.cancel();

            expect($location.path()).toBe('/');
        });

        it('should set an alert on the contacts data when cancel is clicked', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.cancel();

            expect(alerts.addInfo).toHaveBeenCalledWith('Changes to the contact have been cancelled.');
        });

        it('should put some data to the server when save is clicked', function() {
            var controller = createController();
            $httpBackend.flush();

            $httpBackend.expectPUT(apiRoot + '/contacts/' + contactIdentifier, angular.toJson(contact)).respond(202, '');

            $scope.save();

            $httpBackend.flush();
        });
        
        it('should redirect to the list view when save is clicked and the operation is successful', function () {
            var controller = createController();
            $httpBackend.flush();

            $scope.save();

            $httpBackend.when('PUT', apiRoot + '/contacts/' + contactIdentifier).respond(202, '');
            $httpBackend.flush();
            
            expect($location.path()).toBe('/');
        });

        it('should set an alert on the contacts data when save is clicked and the operation is successful', function () {
            var controller = createController();
            $httpBackend.flush();
            
            $scope.save();

            $httpBackend.when('PUT', apiRoot + '/contacts/' + contactIdentifier).respond(202, '');
            $httpBackend.flush();

            expect(alerts.addSuccess).toHaveBeenCalledWith('Changes to the contact have been saved.');
        });
    });

});
