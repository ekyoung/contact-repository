var contactsApp = angular.module('eyContactsApp', [
    'ngRoute',
    'eyAlerts',
    'eyTasks',
    'eyContactGroups',
    'eyContacts'
]);

contactsApp.config(['$routeProvider', function($routeProvider) {
    $routeProvider.
        when('/contacts', {
            templateUrl: '/Content/Contacts/ListContactsView.html',
            controller: 'listController'
        }).
        when('/create', {
            templateUrl: '/Content/Contacts/CreateContactView.html',
            controller: 'createController'
        }).
        when('/edit/:contactIdentifier', {
            templateUrl: '/Content/Contacts/EditContactView.html',
            controller: 'editController'
        }).
        when('/delete/:contactIdentifier', {
            templateUrl: '/Content/Contacts/DeleteContactView.html',
            controller: 'deleteController'
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
        when('/contactGroups/rename/:contactGroupIdentifier', {
            templateUrl: '/Content/ContactGroups/RenameContactGroupView.html',
            controller: 'renameContactGroupController'
        }).
        when('/contactGroups/delete/:contactGroupIdentifier', {
            templateUrl: '/Content/ContactGroups/DeleteContactGroupView.html',
            controller: 'deleteContactGroupController'
        }).
        otherwise({
            redirectTo: '/contactGroups'
        });
}]);

contactsApp.controller('listController', ['$scope', 'alerts', 'Contacts', function ($scope, alerts, Contacts) {
    $scope.contacts = Contacts.query();

    alerts.displayAlerts($scope);
}]);

contactsApp.controller('createController', ['$scope', 'tasks', 'alerts', 'Contacts', function ($scope, tasks, alerts, Contacts) {
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

contactsApp.controller('editController', ['$scope', '$routeParams', 'tasks', 'alerts', 'Contacts', function ($scope, $routeParams, tasks, alerts, Contacts) {
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

contactsApp.controller('deleteController', ['$scope', '$routeParams', 'tasks', 'alerts', 'Contacts', function ($scope, $routeParams, tasks, alerts, Contacts) {
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
