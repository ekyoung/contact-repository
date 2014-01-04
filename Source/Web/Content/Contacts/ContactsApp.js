var contactsApp = angular.module('eyContactsApp', [
    'ngRoute',
    'eyAlerts',
    'eyContacts'
]);

contactsApp.config(['$routeProvider', function($routeProvider) {
    $routeProvider.
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
        otherwise({
            templateUrl: '/Content/Contacts/ListContactsView.html',
            controller: 'listController'
        });
}]);

contactsApp.controller('listController', ['$scope', 'alerts', 'Contacts', function ($scope, alerts, Contacts) {
    $scope.contacts = Contacts.query();

    alerts.displayAlerts($scope);
}]);

contactsApp.controller('createController', ['$scope', '$location', 'alerts', 'Contacts', function ($scope, $location, alerts, Contacts) {
    $scope.contact = Contacts.create();

    $scope.save = function () {
        $scope.contact.$save(function () {
            alerts.addSuccess('A new contact has been created.');
            $location.path('/');
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Creation of a new contact has been cancelled.');
        $location.path('/');
    };
}]);

contactsApp.controller('editController', ['$scope', '$routeParams', '$location', 'alerts', 'Contacts', function ($scope, $routeParams, $location, alerts, Contacts) {
    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier }, function (contact) {
        $scope.originalName = contact.FirstName + ' ' + contact.LastName;
    });

    $scope.save = function () {
        $scope.contact.$update(function () {
            alerts.addSuccess('Changes to the contact have been saved.');
            $location.path('/');
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Changes to the contact have been cancelled.');
        $location.path('/');
    };
}]);

contactsApp.controller('deleteController', ['$scope', '$routeParams', '$location', 'alerts', 'Contacts', function ($scope, $routeParams, $location, alerts, Contacts) {
    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier });

    $scope.continue = function () {
        Contacts.delete({ contactIdentifier: $routeParams.contactIdentifier }, null, function () {
            alerts.addSuccess('The contact has been deleted.');
            $location.path('/');
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Deletion of the contact has been cancelled.');
        $location.path('/');
    };
}]);

contactsApp.directive('eyEditContact', function () {
    return {
        templateUrl: '/Content/Contacts/EditContact.html',
        restrict: 'E',
        scope: { contact: '=' },
    };
});