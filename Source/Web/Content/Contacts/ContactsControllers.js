var contactsControllers = angular.module('contactsControllers', []);

contactsControllers.controller('listController', ['$scope', 'alerts', 'contactRepository', function($scope, alerts, contactRepository) {
    contactRepository.getContacts().then(function (result) {
        if (typeof result === 'string') {
            alerts.addDanger(result);
            alerts.displayAlerts($scope);
        } else {
            $scope.contacts = result;
        }
    });

    alerts.displayAlerts($scope);
}]);

contactsControllers.controller('createController', ['$scope', '$location', 'alerts', 'contactRepository', function ($scope, $location, alerts, contactRepository) {
    $scope.contact = {
        FirstName: null,
        LastName: null
    };
    
    $scope.save = function () {
        contactRepository.insertContact($scope.contact).then(function () {
            alerts.addSuccess('A new contact has been created.');
            $location.path('/');
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Creation of a new contact has been cancelled.');
        $location.path('/');
    };
}]);

contactsControllers.controller('editController', ['$scope', '$routeParams', '$location', 'alerts', 'contactRepository', function ($scope, $routeParams, $location, alerts, contactRepository) {
    contactRepository.getContact($routeParams.contactIdentifier).then(function(result) {
        $scope.contact = result;
        $scope.originalName = result.FirstName + ' ' + result.LastName;
    });

    $scope.addEmailAddress = function () {
        var isNewAddressPrimary = $scope.contact.EmailAddresses.length == 0;
        $scope.contact.EmailAddresses.push({ EmailAddress: null, NickName: null, IsPrimary: isNewAddressPrimary });
    };

    $scope.save = function () {
        contactRepository.updateContact($scope.contact).then(function(result) {
                alerts.addSuccess('Changes to the contact have been saved.');
                $location.path('/');
            });
    };

    $scope.cancel = function () {
        alerts.addInfo('Changes to the contact have been cancelled.');
        $location.path('/');
    };
}]);

contactsControllers.controller('deleteController', ['$scope', '$routeParams', '$location', 'alerts', 'contactRepository', function ($scope, $routeParams, $location, alerts, contactRepository) {
    contactRepository.getContact($routeParams.contactIdentifier).then(function (result) {
            $scope.contact = result;
        });

    $scope.continue = function () {
        contactRepository.deleteContact($routeParams.contactIdentifier).then(function (result) {
                alerts.addSuccess('The contact has been deleted.');
                $location.path('/');
            });
    };
    
    $scope.cancel = function () {
        alerts.addInfo('Deletion of the contact has been cancelled.');
        $location.path('/');
    };
}]);