describe('Contacts Controllers', function () {
    beforeEach(module('contactsApp'));

    describe('listController', function () {
        var $httpBackend, $scope, contactsData, createController;

        var apiRoot = '/api';
        
        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');

            $scope = $injector.get('$rootScope');

            //TODO: Figure out how to make a real alert thingy
            var alerts = [];
            contactsData = {
                alerts: alerts,
                addAlert: function (text, type) {
                    this.alerts.push({ text: text, type: type });
                },
                clearAlerts: function () {
                    this.alerts = [];
                }
            };

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
            contactsData.addAlert('The alert text', 'info');

            var controller = createController();

            expect(contactsData.alerts.length).toBe(0);
        });
    });

    describe('editController', function () {
        var $httpBackend, $scope, $routeParams, $location, contactsData, createController;

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

            //TODO: Figure out how to make a real alert thingy
            var alerts = [];
            contactsData = {
                alerts: alerts,
                addAlert: function (text, type) {
                    this.alerts.push({ text: text, type: type });
                },
                clearAlerts: function() {
                    this.alerts = [];
                }
            };

            // The $controller service is used to create instances of controllers
            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('editController', { $scope: $scope, $routeParams: $routeParams, $location: $location, contactsData: contactsData });
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

            expect(contactsData.alerts[0].text).toBe('Changes to the contact have been cancelled.');
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

            expect(contactsData.alerts[0].text).toBe('Changes to the contact have been saved.');
        });
    });

});
