describe('Contacts Controllers', function () {
    beforeEach(module('contactsApp'));

    describe('listController', function () {
        var $httpBackend, $scope, contactsData, createController;

        var apiRoot = '/api';
        
        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');

            $scope = $injector.get('$rootScope');

            contactsData = {};
            
            // The $controller service is used to create instances of controllers
            var $controller = $injector.get('$controller');

            createController = function() {
                return $controller('listController', { $scope: $scope, contactsData: contactsData });
            };
        }));

        it('should set an array of contacts on the scope when given an array of contacts', function () {
            var contacts = [{ FirstName: 'Joe', LastName: 'One' }, { FirstName: 'Frank', LastName: 'Two' }];
            $httpBackend.when('GET', apiRoot + '/contacts').respond(contacts);
            var controller = createController();
            $httpBackend.flush();
            
            expect($scope.contacts).toBe(contacts);
        });

        it('should set the alerts on the scope in all cases', function () {
            var alerts = [{ text: 'The alert text', type: 'info' }];
            contactsData.alerts = alerts;

            var controller = createController();

            expect($scope.alerts).toBe(alerts);
        });

        it('should clear the alerts in all cases', function () {
            contactsData.alerts = [{ text: 'The alert text', type: 'info' }];

            var controller = createController();

            expect(contactsData.alerts.length).toBe(0);
        });
    });

    describe('editController', function () {
        var $httpBackend, $scope, $routeParams, $location, contactsData, createController;

        var apiRoot = '/api';
        var contactIdentifier = 'id1';

        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            $location = $injector.get('$location');
            $location.path('/edit/' + contactIdentifier);
            
            contactsData = {alerts: []};

            // The $controller service is used to create instances of controllers
            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('editController', { $scope: $scope, $routeParams: $routeParams, $location: $location, contactsData: contactsData });
            };
        }));

        it('should set a contact on the scope when given a contact', function () {
            var contact = { Identifier: contactIdentifier, FirstName: 'Joe', LastName: 'One' };
            $httpBackend.when('GET', apiRoot + '/contacts/' + contactIdentifier).respond(contact);
            
            var controller = createController();
            $httpBackend.flush();

            expect($scope.contact).toBe(contact);
        });

        it('should redirect to the list view when cancel is clicked', function () {
            var controller = createController();
            $scope.cancel();

            expect($location.path()).toBe('/');
        });

        it('should set an alert on the contacts data when cancel is clicked', function () {
            var controller = createController();
            $scope.cancel();

            expect(contactsData.alerts.length).toBe(1);
        });
    });

});
