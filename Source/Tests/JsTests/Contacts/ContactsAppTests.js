describe('Contacts App', function () {

    var mockAlertsService, mockTasksService, mockContactGroupsResource, mockContactsResource;

    beforeEach(module('eyContactsApp', function($provide) {
        mockAlertsService = {
            addSuccess: jasmine.createSpy(),
            addInfo: jasmine.createSpy(),
            addWarning: jasmine.createSpy(),
            addDanger: jasmine.createSpy(),
            displayAlerts: jasmine.createSpy()
        };

        $provide.value('alerts', mockAlertsService);

        mockTasksService = {
            setDefaultOrigin: jasmine.createSpy(),
            redirectBack: jasmine.createSpy()
        };

        $provide.value('tasks', mockTasksService);

        mockContactsResource = function () {
            this.FirstName = null;
            this.LastName = null;
            this.EmailAddresses = [];
        };

        mockContactsResource.create = function () {
            return new mockContactsResource();
        };

        mockContactsResource.get = function () {
            return {};
        };

        mockContactsResource.query = function () {
            return [];
        };

        mockContactsResource.delete = function (params, data, success) {
            success();
        };
        
        mockContactsResource.prototype.$save = function (callback) {
            callback();
        };

        mockContactsResource.prototype.addEmailAddress = jasmine.createSpy();
        mockContactsResource.prototype.removeEmailAddress = jasmine.createSpy();
        mockContactsResource.prototype.setPrimaryEmailAddress = jasmine.createSpy();

        $provide.value('Contacts', mockContactsResource);

        mockContactGroupsResource = function() {
            this.Name = null;
            this.Members = [];
        };

        mockContactGroupsResource.query = function () {
            return [];
        };

        mockContactGroupsResource.getMembers = function() {
            return [];
        };
        
        mockContactGroupsResource.delete = function (params, data, success) {
            success();
        };

        mockContactGroupsResource.prototype.$save = function (callback) {
            callback();
        };

        $provide.value('ContactGroups', mockContactGroupsResource);

        $provide.value('guids', {
            create: function() {
                return 'newGuid';
            }
        });
    }));

    describe('listContactsController', function () {
        var $scope, createController;

        beforeEach(inject(function ($injector) {
            $scope = $injector.get('$rootScope');

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('listContactsController', { $scope: $scope });
            };
        }));

        it('should set an array of contacts on the scope when given an array of contacts', function () {
            var contacts = [{ FirstName: 'Joe', LastName: 'One' }, { FirstName: 'Frank', LastName: 'Two' }];
            mockContactsResource.query = function () { return contacts; };

            var controller = createController();

            expect($scope.contacts).toBe(contacts);
        });

        it('should display alerts in all cases', function () {
            var controller = createController();

            expect(mockAlertsService.displayAlerts).toHaveBeenCalledWith($scope);
        });
    });

    describe('createContactController', function() {
        var $scope, createController;

        var contact = {
            $save: function(callback) {
                 callback();
            }
        };

        beforeEach(inject(function ($injector) {
            mockContactsResource.create = function () { return contact; };
            
            $scope = $injector.get('$rootScope');

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('createContactController', { $scope: $scope });
            };
        }));

        it('should set the default origin to the list of contacts', function () {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contacts');
        });

        it('should use the factory method to create an empty contact in all cases', function () {
            spyOn(mockContactsResource, 'create').andCallThrough();

            var controller = createController();

            expect(mockContactsResource.create).toHaveBeenCalled();
        });

        it('should set an empty contact on the scope in all cases', function () {
            var controller = createController();

            expect($scope.contact).toBe(contact);
        });

        it('should save the contact when save is clicked', function () {
            spyOn(contact, '$save').andCallThrough();
            
            var controller = createController();
            
            $scope.save();

            expect(contact.$save).toHaveBeenCalled();
        });
        
        it('should redirect back when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add a success alert when save is clicked and the operation is successful', function() {
            var controller = createController();

            $scope.save();

            expect(mockAlertsService.addSuccess).toHaveBeenCalledWith('A new contact has been created.');
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Creation of a new contact has been cancelled.');
        });
    });
    
    describe('editContactController', function () {
        var $scope, $routeParams, createController;

        var contactIdentifier = 'id1';
        var contact = {
            Identifier: contactIdentifier,
            FirstName: 'Joe',
            LastName: 'One',
            $update: function (callback) { callback(); }
        };

        beforeEach(inject(function ($injector) {
            mockContactsResource.get = function () { return contact; };
            spyOn(mockContactsResource, 'get').andCallThrough();
            spyOn(contact, '$update').andCallThrough();

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('editContactController', { $scope: $scope, $routeParams: $routeParams });
            };
        }));

        it('should set the default origin to the list of contacts', function () {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contacts');
        });

        it('should use the contact identifier to get a contact', function () {
            var controller = createController();

            expect(mockContactsResource.get).toHaveBeenCalledWith({ contactIdentifier: contactIdentifier }, jasmine.any(Function));
        });
        
        it('should set a contact on the scope when given a contact', function () {
            var controller = createController();

            expect($scope.contact).toBe(contact);
        });
        
        it('should set the original name of the contact on the scope when given a contact', function () {
            mockContactsResource.get = function (x, callback) {
                callback(contact);
                return contact;
            };

            var controller = createController();

            expect($scope.originalName).toBe(contact.FirstName + ' ' + contact.LastName);
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Changes to the contact have been cancelled.');
        });

        it('should update the contact when save is clicked', function () {
            var controller = createController();

            $scope.save();

            expect(contact.$update).toHaveBeenCalled();
        });
        
        it('should redirect back when save is clicked and the operation is successful', function () {

            var controller = createController();

            $scope.save();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add a success alert when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();

            expect(mockAlertsService.addSuccess).toHaveBeenCalledWith('Changes to the contact have been saved.');
        });
    });

    describe('deleteContactController', function() {
        var $scope, $routeParams, createController;

        var contactIdentifier = 'id1';
        var contact = { Identifier: contactIdentifier, FirstName: 'Joe', LastName: 'One' };

        beforeEach(inject(function ($injector) {
            mockContactsResource.get = function () { return contact; };
            spyOn(mockContactsResource, 'get').andCallThrough();
            spyOn(mockContactsResource, 'delete').andCallThrough();

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactIdentifier = contactIdentifier;

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('deleteContactController', { $scope: $scope, $routeParams: $routeParams });
            };
        }));

        it('should set the default origin to the list of contacts', function () {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contacts');
        });

        it('should set a contact on the scope when given a contact', function () {
            var controller = createController();

            expect($scope.contact).toBe(contact);
        });

        it('should delete the contact when continue is clicked', function () {
            var controller = createController();

            $scope.continue();

            expect(mockContactsResource.delete).toHaveBeenCalledWith({ contactIdentifier: contactIdentifier }, null, jasmine.any(Function));
        });

        it('should redirect back when continue is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.continue();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add a success alert when continue is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.continue();

            expect(mockAlertsService.addSuccess).toHaveBeenCalledWith('The contact has been deleted.');
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Deletion of the contact has been cancelled.');
        });

    });
    
    describe('listContactGroupsController', function () {
        var $scope, createController;

        beforeEach(inject(function ($injector) {
            $scope = $injector.get('$rootScope');

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('listContactGroupsController', { $scope: $scope });
            };
        }));

        it('should set an array of contact groups on the scope when given an array of contact groups', function () {
            var contactGroups = [{ Name: 'My Contacts' }, { Name: 'Your Contacts' }];
            mockContactGroupsResource.query = function () { return contactGroups; };

            var controller = createController();

            expect($scope.contactGroups).toBe(contactGroups);
        });

        it('should display alerts in all cases', function () {
            var controller = createController();

            expect(mockAlertsService.displayAlerts).toHaveBeenCalledWith($scope);
        });
    });

    describe('createContactGroupController', function () {
        var $scope, createController;

        var contactGroup = {
            $save: function (callback) {
                callback();
            }
        };

        beforeEach(inject(function ($injector) {
            mockContactGroupsResource.create = function () { return contactGroup; };

            $scope = $injector.get('$rootScope');

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('createContactGroupController', { $scope: $scope });
            };
        }));

        it('should set the default origin to the list of contact groups', function () {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contactGroups');
        });

        it('should use the factory method to create an empty contact group in all cases', function () {
            spyOn(mockContactGroupsResource, 'create').andCallThrough();

            var controller = createController();

            expect(mockContactGroupsResource.create).toHaveBeenCalled();
        });

        it('should set an empty contact group on the scope in all cases', function () {
            var controller = createController();

            expect($scope.contactGroup).toBe(contactGroup);
        });

        it('should save the contact group when save is clicked', function () {
            spyOn(contactGroup, '$save').andCallThrough();

            var controller = createController();

            $scope.save();

            expect(contactGroup.$save).toHaveBeenCalled();
        });

        it('should redirect back when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add a success alert when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();

            expect(mockAlertsService.addSuccess).toHaveBeenCalledWith('A new contact group has been created.');
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Creation of a new contact group has been cancelled.');
        });
    });

    describe('contactGroupOverviewController', function () {
        var $scope, $routeParams, createController;

        var contactGroupIdentifier = 'id1';
        var contactGroup = {
            Identifier: contactGroupIdentifier,
            Name: 'My Contacts'
        };
        var contactGroupMembers = [
            { FirstName: 'Joe', LastName: 'Contact' }
        ];

        beforeEach(inject(function ($injector) {
            mockContactGroupsResource.get = function () { return contactGroup; };
            spyOn(mockContactGroupsResource, 'get').andCallThrough();
            mockContactGroupsResource.getMembers = function () { return contactGroupMembers; };
            spyOn(mockContactGroupsResource, 'getMembers').andCallThrough();

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactGroupIdentifier = contactGroupIdentifier;

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('contactGroupOverviewController', { $scope: $scope, $routeParams: $routeParams });
            };
        }));

        it('should use the contact group identifier to get a contact group', function () {
            var controller = createController();

            expect(mockContactGroupsResource.get).toHaveBeenCalledWith({ contactGroupIdentifier: contactGroupIdentifier });
        });

        it('should set a contact group on the scope when given a contact group', function () {
            var controller = createController();

            expect($scope.contactGroup).toBe(contactGroup);
        });

        it('should use the contact group identifier to get the members of the contact group', function() {
            var controller = createController();

            expect(mockContactGroupsResource.getMembers).toHaveBeenCalledWith({ contactGroupIdentifier: contactGroupIdentifier });
        });

        it('should set the members of the group on the scope when given members', function() {
            var controller = createController();

            expect($scope.contactGroupMembers).toBe(contactGroupMembers);
        });
        
        it('should display alerts in all cases', function () {
            var controller = createController();

            expect(mockAlertsService.displayAlerts).toHaveBeenCalledWith($scope);
        });
    });

    describe('renameContactGroupController', function () {
        var $scope, $routeParams, createController;

        var contactGroupIdentifier = 'id1';
        var contactGroup = {
            Identifier: contactGroupIdentifier,
            Name: 'My Contacts',
            $update: function (callback) { callback(); }
        };

        beforeEach(inject(function ($injector) {
            mockContactGroupsResource.get = function () { return contactGroup; };
            spyOn(mockContactGroupsResource, 'get').andCallThrough();
            spyOn(contactGroup, '$update').andCallThrough();

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactGroupIdentifier = contactGroupIdentifier;

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('renameContactGroupController', { $scope: $scope, $routeParams: $routeParams });
            };
        }));

        it('should set the default origin to the list of contact groups', function() {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contactGroups');
        });
        
        it('should use the contact group identifier to get a contact group', function () {
            var controller = createController();

            expect(mockContactGroupsResource.get).toHaveBeenCalledWith({ contactGroupIdentifier: contactGroupIdentifier }, jasmine.any(Function));
        });

        it('should set a contact group on the scope when given a contact group', function () {
            var controller = createController();

            expect($scope.contactGroup).toBe(contactGroup);
        });

        it('should set the original name of the contact group on the scope when given a contact group', function () {
            mockContactGroupsResource.get = function (x, callback) {
                callback(contactGroup);
                return contactGroup;
            };

            var controller = createController();

            expect($scope.originalName).toBe(contactGroup.Name);
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Renaming the contact group has been cancelled.');
        });

        it('should update the contact group when save is clicked', function () {
            var controller = createController();

            $scope.save();

            expect(contactGroup.$update).toHaveBeenCalled();
        });

        it('should redirect back when save is clicked and the operation is successful', function () {

            var controller = createController();

            $scope.save();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add a success alert when save is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.save();

            expect(mockAlertsService.addSuccess).toHaveBeenCalledWith('The contact group has been renamed.');
        });
    });

    describe('deleteContactGroupController', function () {
        var $scope, $routeParams, createController;

        var contactGroupIdentifier = 'id1';
        var contactGroup = { Identifier: contactGroupIdentifier, Name: 'My Contacts' };

        beforeEach(inject(function ($injector) {
            mockContactGroupsResource.get = function () { return contactGroup; };
            spyOn(mockContactGroupsResource, 'get').andCallThrough();
            spyOn(mockContactGroupsResource, 'delete').andCallThrough();

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactGroupIdentifier = contactGroupIdentifier;

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('deleteContactGroupController', { $scope: $scope, $routeParams: $routeParams });
            };
        }));

        it('should set the default origin to the list of contact groups', function () {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contactGroups');
        });

        it('should set a contact group on the scope when given a contact group', function () {
            var controller = createController();

            expect($scope.contactGroup).toBe(contactGroup);
        });

        it('should delete the contact group when continue is clicked', function () {
            var controller = createController();

            $scope.continue();

            expect(mockContactGroupsResource.delete).toHaveBeenCalledWith({ contactGroupIdentifier: contactGroupIdentifier }, null, jasmine.any(Function));
        });

        it('should redirect back when continue is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.continue();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add a success alert when continue is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.continue();

            expect(mockAlertsService.addSuccess).toHaveBeenCalledWith('The contact group has been deleted.');
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Deletion of the contact group has been cancelled.');
        });
    });

    describe('addContactGroupMemberController', function() {
        var $scope, $routeParams, createController;

        var contactGroupIdentifier = 'id1';
        var contactGroup = {
            Identifier: contactGroupIdentifier,
            Name: 'My Contacts',
            addMember: jasmine.createSpy(),
            $update: function (callback) { callback(); }
        };
        var contact = {
            $save: function (callback) {
                callback();
            }
        };

        beforeEach(inject(function ($injector) {
            contactGroup.Members = [];
            spyOn(contactGroup, '$update').andCallThrough();
            mockContactGroupsResource.get = function () { return contactGroup; };
            spyOn(mockContactGroupsResource, 'get').andCallThrough();

            mockContactsResource.create = function () { return contact; };

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactGroupIdentifier = contactGroupIdentifier;

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('addContactGroupMemberController', { $scope: $scope, $routeParams: $routeParams });
            };
        }));

        it('should set the default origin to the contact group overview page', function () {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contactGroups/' + contactGroupIdentifier);
        });

        it('should set a contact group on the scope when given a contact group', function () {
            var controller = createController();

            expect($scope.contactGroup).toBe(contactGroup);
        });

        it('should use the factory method to create an empty contact in all cases', function () {
            spyOn(mockContactsResource, 'create').andCallThrough();

            var controller = createController();

            expect(mockContactsResource.create).toHaveBeenCalled();
        });

        it('should set an empty contact on the scope in all cases', function () {
            var controller = createController();

            expect($scope.contact).toBe(contact);
        });

        it('should save the contact when save is clicked', function () {
            spyOn(contact, '$save').andCallThrough();

            var controller = createController();

            $scope.save();

            expect(contact.$save).toHaveBeenCalled();
        });

        it('should add the new contact as a member of the contact group when saving the contact is successful', function() {
            var controller = createController();

            $scope.save();

            expect(contact.Identifier).toBe('newGuid');
            expect(contactGroup.addMember).toHaveBeenCalledWith('newGuid');
        });

        it('should update the contact group when saving the contact is successful', function() {
            var controller = createController();

            $scope.save();

            expect(contactGroup.$update).toHaveBeenCalled();
        });

        it('should redirect back when updating the contact group is successful', function() {
            var controller = createController();

            $scope.save();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when updating the contact group is successful', function() {
            var controller = createController();

            $scope.save();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('A new member has been added to the contact group.');
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Addition of a new member to the contact group has been cancelled.');
        });
    });

    describe('removeContactGroupMemberController', function() {
        var $scope, $routeParams, createController;

        var contactGroupIdentifier = 'id1';
        var contactGroup = {
            Identifier: contactGroupIdentifier,
            Name: 'My Contacts',
            removeMember: jasmine.createSpy(),
            $update: function (callback) { callback(); }
        };

        var contactIdentifier = 'id2';
        var contact = {
            Identifier: contactIdentifier,
            FirstName: 'Joe',
            LastName: 'One'
        };

        beforeEach(inject(function ($injector) {
            mockContactGroupsResource.get = function () { return contactGroup; };
            spyOn(contactGroup, '$update').andCallThrough();

            mockContactsResource.get = function () { return contact; };

            $scope = $injector.get('$rootScope');

            $routeParams = $injector.get('$routeParams');
            $routeParams.contactGroupIdentifier = contactGroupIdentifier;
            $routeParams.contactIdentifier = contactIdentifier;

            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('removeContactGroupMemberController', { $scope: $scope, $routeParams: $routeParams });
            };
        }));

        it('should set the default origin to the contact group overview page', function () {
            var controller = createController();

            expect(mockTasksService.setDefaultOrigin).toHaveBeenCalledWith('/contactGroups/' + contactGroupIdentifier);
        });

        it('should set a contact group on the scope when given a contact group', function () {
            var controller = createController();

            expect($scope.contactGroup).toBe(contactGroup);
        });

        it('should set a contact on the scope when given a contact', function() {
            var controller = createController();

            expect($scope.contact).toBe(contact);
        });

        it('should remove the contact from the group when continue is clicked', function() {
            var controller = createController();

            $scope.continue();

            expect(contactGroup.removeMember).toHaveBeenCalledWith(contactIdentifier);
        });

        it('should update the contact group when continue is clicked', function () {
            var controller = createController();

            $scope.continue();

            expect(contactGroup.$update).toHaveBeenCalled();
        });

        it('should redirect back when continue is clicked and the operation is successful', function () {

            var controller = createController();

            $scope.continue();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add a success alert when continue is clicked and the operation is successful', function () {
            var controller = createController();

            $scope.continue();

            expect(mockAlertsService.addSuccess).toHaveBeenCalledWith('The contact has been removed from the contact group.');
        });

        it('should redirect back when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockTasksService.redirectBack).toHaveBeenCalled();
        });

        it('should add an info alert when cancel is clicked', function () {
            var controller = createController();

            $scope.cancel();

            expect(mockAlertsService.addInfo).toHaveBeenCalledWith('Removal of a contact from the contact group has been cancelled.');
        });
    });
});
