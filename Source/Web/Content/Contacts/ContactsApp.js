var contactsApp = angular.module('eyContactsApp', [
    'ngRoute',
    'eyAlerts',
    'eyGuids',
    'eyTasks',
    'eyContactGroups',
    'eyContacts'
]);

contactsApp.config(['$routeProvider', function($routeProvider) {
    $routeProvider.
        when('/contacts', {
            templateUrl: '/Content/Contacts/ListContactsView.html',
            controller: 'listContactsController'
        }).
        when('/create', {
            templateUrl: '/Content/Contacts/CreateContactView.html',
            controller: 'createContactController'
        }).
        when('/edit/:contactIdentifier', {
            templateUrl: '/Content/Contacts/EditContactView.html',
            controller: 'editContactController'
        }).
        when('/delete/:contactIdentifier', {
            templateUrl: '/Content/Contacts/DeleteContactView.html',
            controller: 'deleteContactController'
        }).
        when('/contactGroups', {
            templateUrl: '/Content/ContactGroups/ListContactGroupsView.html',
            controller: 'listContactGroupsController'
        }).
        when('/contactGroups/create', {
            templateUrl: '/Content/ContactGroups/CreateContactGroupView.html',
            controller: 'createContactGroupController'
        }).
        when('/contactGroups/:contactGroupIdentifier', {
            templateUrl: '/Content/ContactGroups/ContactGroupOverview.html',
            controller: 'contactGroupOverviewController'
        }).
        when('/contactGroups/:contactGroupIdentifier/rename', {
            templateUrl: '/Content/ContactGroups/RenameContactGroupView.html',
            controller: 'renameContactGroupController'
        }).
        when('/contactGroups/:contactGroupIdentifier/delete', {
            templateUrl: '/Content/ContactGroups/DeleteContactGroupView.html',
            controller: 'deleteContactGroupController'
        }).
        when('/contactGroups/:contactGroupIdentifier/addMember', {
            templateUrl: '/Content/ContactGroups/AddContactGroupMemberView.html',
            controller: 'addContactGroupMemberController'
        }).
        when('/contactGroups/:contactGroupIdentifier/editMember/:contactIdentifier', {
            templateUrl: '/Content/ContactGroups/EditContactGroupMemberView.html',
            controller: 'editContactGroupMemberController'
        }).
        when('/contactGroups/:contactGroupIdentifier/removeMember/:contactIdentifier', {
            templateUrl: '/Content/ContactGroups/RemoveContactGroupMemberView.html',
            controller: 'removeContactGroupMemberController'
        }).
        otherwise({
            redirectTo: '/contactGroups'
        });
}]);

contactsApp.controller('listContactsController', ['$scope', 'alerts', 'Contacts', function ($scope, alerts, Contacts) {
    $scope.contacts = Contacts.query();

    alerts.displayAlerts($scope);
}]);

contactsApp.controller('createContactController', ['$scope', 'tasks', 'alerts', 'Contacts', function ($scope, tasks, alerts, Contacts) {
    tasks.setDefaultOrigin('/contacts');
    
    $scope.contact = Contacts.create();

    $scope.save = function () {
        $scope.contact.$save(function () {
            alerts.addSuccess('A new contact has been created.');
            tasks.redirectBack();
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Creation of a new contact has been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.controller('editContactController', ['$scope', '$routeParams', 'tasks', 'alerts', 'Contacts', function ($scope, $routeParams, tasks, alerts, Contacts) {
    tasks.setDefaultOrigin('/contacts');
    
    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier }, function (contact) {
        $scope.originalName = contact.FirstName + ' ' + contact.LastName;
    });

    $scope.save = function () {
        $scope.contact.$update(function () {
            alerts.addSuccess('Changes to the contact have been saved.');
            tasks.redirectBack();
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Changes to the contact have been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.controller('deleteContactController', ['$scope', '$routeParams', 'tasks', 'alerts', 'Contacts', function ($scope, $routeParams, tasks, alerts, Contacts) {
    tasks.setDefaultOrigin('/contacts');
    
    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier });

    $scope.continue = function () {
        Contacts.delete({ contactIdentifier: $routeParams.contactIdentifier }, null, function () {
            alerts.addSuccess('The contact has been deleted.');
            tasks.redirectBack();
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Deletion of the contact has been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.directive('eyEditContact', function () {
    return {
        templateUrl: '/Content/Contacts/EditContact.html',
        restrict: 'E',
        scope: { contact: '=' },
    };
});

contactsApp.directive('eyContactList', function () {
    return {
        templateUrl: '/Content/Contacts/ContactList.html',
        restrict: 'E',
        scope: { contacts: '=' },
    };
});

contactsApp.controller('listContactGroupsController', ['$scope', 'alerts', 'ContactGroups', function ($scope, alerts, ContactGroups) {
    $scope.contactGroups = ContactGroups.query();

    alerts.displayAlerts($scope);
}]);

contactsApp.controller('createContactGroupController', ['$scope', 'tasks', 'alerts', 'ContactGroups', function ($scope, tasks, alerts, ContactGroups) {
    tasks.setDefaultOrigin('/contactGroups');
    
    $scope.contactGroup = ContactGroups.create();

    $scope.save = function () {
        $scope.contactGroup.$save(function () {
            alerts.addSuccess('A new contact group has been created.');
            tasks.redirectBack();
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Creation of a new contact group has been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.controller('contactGroupOverviewController', ['$scope', '$routeParams', 'alerts', 'ContactGroups', function ($scope, $routeParams, alerts, ContactGroups) {
    $scope.contactGroup = ContactGroups.get({ contactGroupIdentifier: $routeParams.contactGroupIdentifier });
    $scope.contactGroupMembers = ContactGroups.getMembers({ contactGroupIdentifier: $routeParams.contactGroupIdentifier });

    alerts.displayAlerts($scope);
}]);

contactsApp.controller('renameContactGroupController', ['$scope', '$routeParams', 'tasks', 'alerts', 'ContactGroups', function ($scope, $routeParams, tasks, alerts, ContactGroups) {
    tasks.setDefaultOrigin('/contactGroups');
    
    $scope.contactGroup = ContactGroups.get({ contactGroupIdentifier: $routeParams.contactGroupIdentifier }, function (contactGroup) {
        $scope.originalName = contactGroup.Name;
    });

    $scope.save = function () {
        $scope.contactGroup.$update(function () {
            alerts.addSuccess('The contact group has been renamed.');
            tasks.redirectBack();
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Renaming the contact group has been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.controller('deleteContactGroupController', ['$scope', '$routeParams', 'tasks', 'alerts', 'ContactGroups', function ($scope, $routeParams, tasks, alerts, ContactGroups) {
    tasks.setDefaultOrigin('/contactGroups');
    
    $scope.contactGroup = ContactGroups.get({ contactGroupIdentifier: $routeParams.contactGroupIdentifier });

    $scope.continue = function () {
        ContactGroups.delete({ contactGroupIdentifier: $routeParams.contactGroupIdentifier }, null, function () {
            alerts.addSuccess('The contact group has been deleted.');
            tasks.redirectBack();
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Deletion of the contact group has been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.controller('addContactGroupMemberController', ['$scope', '$routeParams', 'tasks', 'alerts', 'ContactGroups', 'Contacts', 'guids', function($scope, $routeParams, tasks, alerts, ContactGroups, Contacts, guids) {
    tasks.setDefaultOrigin('/contactGroups/' + $routeParams.contactGroupIdentifier);

    $scope.contactGroup = ContactGroups.get({ contactGroupIdentifier: $routeParams.contactGroupIdentifier });

    $scope.contact = Contacts.create();

    $scope.save = function () {
        var contactIdentifier = guids.create();
        $scope.contact.Identifier = contactIdentifier;
        $scope.contact.$save(function() {
            $scope.contactGroup.addMember(contactIdentifier);
            $scope.contactGroup.$update(function () {
                alerts.addInfo('A new member has been added to the contact group.');
                tasks.redirectBack();
            });
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Addition of a new member to the contact group has been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.controller('editContactGroupMemberController', ['$scope', '$routeParams', 'tasks', 'alerts', 'ContactGroups', 'Contacts', function ($scope, $routeParams, tasks, alerts, ContactGroups, Contacts) {
    tasks.setDefaultOrigin('/contactGroups/' + $routeParams.contactGroupIdentifier);

    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier }, function (contact) {
        $scope.originalName = contact.FirstName + ' ' + contact.LastName;
    });

    $scope.contactGroup = ContactGroups.get({ contactGroupIdentifier: $routeParams.contactGroupIdentifier });

    $scope.save = function () {
        $scope.contact.$update(function() {
            $scope.contactGroup.$update(function () {
                alerts.addSuccess('Changes to the contact group member have been saved.');
                tasks.redirectBack();
            });
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Changes to the contact group member have been cancelled.');
        tasks.redirectBack();
    };
}]);

contactsApp.controller('removeContactGroupMemberController', ['$scope', '$routeParams', 'tasks', 'alerts', 'ContactGroups', 'Contacts', function($scope, $routeParams, tasks, alerts, ContactGroups, Contacts) {
    tasks.setDefaultOrigin('/contactGroups/' + $routeParams.contactGroupIdentifier);

    $scope.contactGroup = ContactGroups.get({ contactGroupIdentifier: $routeParams.contactGroupIdentifier });

    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier });

    $scope.continue = function () {
        $scope.contactGroup.removeMember($routeParams.contactIdentifier);
        
        $scope.contactGroup.$update(function () {
            alerts.addSuccess('The contact has been removed from the contact group.');
            tasks.redirectBack();
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Removal of a contact from the contact group has been cancelled.');
        tasks.redirectBack();
    };
}]);