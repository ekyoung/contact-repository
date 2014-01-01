describe('Contacts Controllers', function () {
    beforeEach(module('contactsApp'));

    var alerts, mockContactRepository, q, deferred,
        apiRoot = '/api';

    beforeEach(function() {
        alerts = {
            addSuccess: jasmine.createSpy(),
            addInfo: jasmine.createSpy(),
            addWarning: jasmine.createSpy(),
            addDanger: jasmine.createSpy(),
            displayAlerts: jasmine.createSpy()
        };
    });

    beforeEach(function() {
        mockContactRepository = {
            getContacts: function() {
                deferred = q.defer();
                return deferred.promise;
            },
            getContact: function(contactIdentifier) {
                deferred = q.defer();
                return deferred.promise;
            },
            insertContact: function(contact) {
                deferred = q.defer();
                return deferred.promise;
            },
            updateContact: function(contact) {
                deferred = q.defer();
                return deferred.promise;
            },
            deleteContact: function(contactIdentifier) {
                deferred = q.defer();
                return deferred.promise;
            }
        };
        spyOn(mockContactRepository, 'insertContact').andCallThrough();
        spyOn(mockContactRepository, 'updateContact').andCallThrough();
        spyOn(mockContactRepository, 'deleteContact').andCallThrough();
    });

    describe('listController', function () {
        var $scope, createController;

        beforeEach(inject(function ($injector) {
            $scope = $injector.get('$rootScope');

            q = $injector.get('$q');

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('listController', { $scope: $scope, alerts: alerts, contactRepository: mockContactRepository });
            };
        }));

        it('should set an array of contacts on the scope when given an array of contacts', function () {
            var contacts = [{ FirstName: 'Joe', LastName: 'One' }, { FirstName: 'Frank', LastName: 'Two' }];

            var controller = createController();
            deferred.resolve(contacts);

            $scope.$apply();
            expect($scope.contacts).toBe(contacts);
        });
        /*
        it('should add a danger alert when given a 404 error', function() {
            $httpBackend.when('GET', apiRoot + '/contacts').respond(404);
            var controller = createController();
            $httpBackend.flush();

            expect(alerts.addDanger).toHaveBeenCalledWith('The server returned 404.');
            expect(alerts.displayAlerts).toHaveBeenCalledWith($scope);
            expect(alerts.displayAlerts.callCount).toBe(2);
        });

        it('should add a danger alert when given a 500 error', function () {
            var exceptionMessage = "Some exception";
            $httpBackend.when('GET', apiRoot + '/contacts').respond(500, {ExceptionMessage: exceptionMessage});
            var controller = createController();
            $httpBackend.flush();

            expect(alerts.addDanger).toHaveBeenCalledWith('The server returned the following error message: ' + exceptionMessage);
            expect(alerts.displayAlerts).toHaveBeenCalledWith($scope);
            expect(alerts.displayAlerts.callCount).toBe(2);
        });
        */
        it('should display alerts in all cases', function () {
            var alertsArray = [{ text: 'The alert text', type: 'info' }];
            alerts.alerts = alertsArray;

            var controller = createController();

            expect(alerts.displayAlerts).toHaveBeenCalledWith($scope);
        });
    });

    describe('createController', function() {
        var $scope, $location, createController;

        beforeEach(inject(function($injector) {
            $scope = $injector.get('$rootScope');

            q = $injector.get('$q');

            $location = $injector.get('$location');
            $location.path('/create');

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('createController', { $scope: $scope, $location: $location, alerts: alerts, contactRepository: mockContactRepository });
            };
        }));

        it('should set an empty contact on the scope in all cases', function() {
            var controller = createController();

            expect($scope.contact.FirstName).toBe(null);
            expect($scope.contact.LastName).toBe(null);
        });

        it('should insert a contact when save is clicked', function () {
            var contact = {
                FirstName: 'Joe',
                LastName: 'Contact'
            };
            
            var controller = createController();
            $scope.contact = contact;
            
            $scope.save();
            deferred.resolve();
            $scope.$apply();

            expect(mockContactRepository.insertContact).toHaveBeenCalledWith(contact);
        });
        
        it('should redirect to the list view when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();
            deferred.resolve();
            $scope.$apply();

            expect($location.path()).toBe('/');
        });

        it('should add a success alert when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();
            deferred.resolve();
            $scope.$apply();

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
        var $scope, $routeParams, $location, createController;

        var contactIdentifier = 'id1';
        var contact = { Identifier: contactIdentifier, FirstName: 'Joe', LastName: 'One' };

        beforeEach(inject(function ($injector) {
            $scope = $injector.get('$rootScope');

            q = $injector.get('$q');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            $location = $injector.get('$location');
            $location.path('/edit/' + contactIdentifier);

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('editController', { $scope: $scope, $routeParams: $routeParams, $location: $location, alerts: alerts, contactRepository: mockContactRepository });
            };
        }));
        
        it('should set a contact on the scope when given a contact', function () {
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            expect($scope.contact).toBe(contact);
        });
        
        it('should set the original name on the scope when given a contact', function () {
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            expect($scope.originalName).toBe(contact.FirstName + ' ' + contact.LastName);
        });

        it('should redirect to the list view when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect($location.path()).toBe('/');
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(alerts.addInfo).toHaveBeenCalledWith('Changes to the contact have been cancelled.');
        });

        it('should update the contact when save is clicked', function() {
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            $scope.save();
            deferred.resolve();
            $scope.$apply();

            expect(mockContactRepository.updateContact).toHaveBeenCalledWith(contact);
        });
        
        it('should redirect to the list view when save is clicked and the operation is successful', function () {
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            $scope.save();
            deferred.resolve();
            $scope.$apply();

            expect($location.path()).toBe('/');
        });

        it('should add a success alert when save is clicked and the operation is successful', function () {
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            $scope.save();
            deferred.resolve();
            $scope.$apply();

            expect(alerts.addSuccess).toHaveBeenCalledWith('Changes to the contact have been saved.');
        });

        it('should add a contact email address with IsPrimary true to the contact when addEmailAddress is called and the contact has no email addresses', function() {
            contact.EmailAddresses = [];
            
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            $scope.addEmailAddress();

            expect(contact.EmailAddresses.length).toBe(1);
            expect(contact.EmailAddresses[0].IsPrimary).toBe(true);
        });

        it('should add a contact email address with IsPrimary false to the contact when addEmailAddress is called and the contact already has an email address', function() {
            contact.EmailAddresses = [{ EmailAddress: 'fake@email.com', NickName: null, IsPrimary: true }];
            
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            $scope.addEmailAddress();

            expect(contact.EmailAddresses.length).toBe(2);
            expect(contact.EmailAddresses[1].IsPrimary).toBe(false);
        });

        it('should remove a contact email address when removeEmailAddress is called with a contact email address that is not the primary address', function () {
            var primaryEmailAddress = { EmailAddress: 'primary@email.com', NickName: null, IsPrimary: true },
                otherEmailAddress = { EmailAddress: 'other@email.com', NickName: null, IsPrimary: false };
            contact.EmailAddresses = [ primaryEmailAddress, otherEmailAddress ];

            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            $scope.removeEmailAddress(otherEmailAddress);

            expect(contact.EmailAddresses.length).toBe(1);
            expect(contact.EmailAddresses[0]).toBe(primaryEmailAddress);
        });

        it('should remove a contact email address and make the first remaining address primary when removeEmailAddress is called with the primary address', function () {
            var primaryEmailAddress = { EmailAddress: 'primary@email.com', NickName: null, IsPrimary: true },
                otherEmailAddress = { EmailAddress: 'other@email.com', NickName: null, IsPrimary: false };
            contact.EmailAddresses = [ primaryEmailAddress, otherEmailAddress ];

            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            $scope.removeEmailAddress(primaryEmailAddress);

            expect(contact.EmailAddresses.length).toBe(1);
            expect(contact.EmailAddresses[0]).toBe(otherEmailAddress);
            expect(otherEmailAddress.IsPrimary).toBe(true);
        });
    });

    describe('deleteController', function() {
        var $scope, $routeParams, $location, createController;

        var contactIdentifier = 'id1';
        var contact = { Identifier: contactIdentifier, FirstName: 'Joe', LastName: 'One' };

        beforeEach(inject(function ($injector) {
            $scope = $injector.get('$rootScope');

            q = $injector.get('$q');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            $location = $injector.get('$location');
            $location.path('/delete/' + contactIdentifier);

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('deleteController', { $scope: $scope, $routeParams: $routeParams, $location: $location, alerts: alerts, contactRepository: mockContactRepository });
            };
        }));

        it('should set a contact on the scope when given a contact', function () {
            var controller = createController();
            deferred.resolve(contact);
            $scope.$apply();

            expect($scope.contact).toBe(contact);
        });

        it('should delete the contact when continue is clicked', function () {
            var controller = createController();

            $scope.continue();
            deferred.resolve();
            $scope.$apply();

            expect(mockContactRepository.deleteContact).toHaveBeenCalledWith(contactIdentifier);
        });

        it('should redirect to the list view when continue is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.continue();
            deferred.resolve();
            $scope.$apply();

            expect($location.path()).toBe('/');
        });

        it('should add a success alert when continue is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.continue();
            deferred.resolve();
            $scope.$apply();

            expect(alerts.addSuccess).toHaveBeenCalledWith('The contact has been deleted.');
        });

        it('should redirect to the list view when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect($location.path()).toBe('/');
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(alerts.addInfo).toHaveBeenCalledWith('Deletion of the contact has been cancelled.');
        });

    });
});
