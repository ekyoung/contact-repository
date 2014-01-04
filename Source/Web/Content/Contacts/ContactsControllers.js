var contactsControllers = angular.module('contactsControllers', [
    'eyContacts'
]);

contactsControllers.controller('listController', ['$scope', 'alerts', 'Contacts', function ($scope, alerts, Contacts) {
    $scope.contacts = Contacts.query();

    alerts.displayAlerts($scope);
}]);

contactsControllers.controller('createController', ['$scope', '$location', 'alerts', 'Contacts', function ($scope, $location, alerts, Contacts) {
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

contactsControllers.controller('editController', ['$scope', '$routeParams', '$location', 'alerts', 'Contacts', function ($scope, $routeParams, $location, alerts, Contacts) {
    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier }, function(contact) {
        $scope.originalName = contact.FirstName + ' ' + contact.LastName;
    });
    
    $scope.save = function () {
        $scope.contact.$update(function() {
            alerts.addSuccess('Changes to the contact have been saved.');
            $location.path('/');
        });
    };

    $scope.cancel = function () {
        alerts.addInfo('Changes to the contact have been cancelled.');
        $location.path('/');
    };
}]);

contactsControllers.controller('deleteController', ['$scope', '$routeParams', '$location', 'alerts', 'Contacts', function ($scope, $routeParams, $location, alerts, Contacts) {
    $scope.contact = Contacts.get({ contactIdentifier: $routeParams.contactIdentifier });

    $scope.continue = function () {
        Contacts.delete({ contactIdentifier: $routeParams.contactIdentifier }, null, function() {
            alerts.addSuccess('The contact has been deleted.');
            $location.path('/');
        });
    };
    
    $scope.cancel = function () {
        alerts.addInfo('Deletion of the contact has been cancelled.');
        $location.path('/');
    };
}]);

contactsControllers.controller('eyEditContactController', ['$scope', function($scope) {
    $scope.addEmailAddress = function () {
        var isNewAddressPrimary = $scope.contact.EmailAddresses.length == 0;
        $scope.contact.EmailAddresses.push({ EmailAddress: null, NickName: null, IsPrimary: isNewAddressPrimary });
    };

    $scope.removeEmailAddress = function (contactEmailAddressToRemove) {
        var emailAddresses = $scope.contact.EmailAddresses,
            index = emailAddresses.indexOf(contactEmailAddressToRemove);

        if (index >= 0) {
            emailAddresses.splice(index, 1);
        }

        if (emailAddresses.length > 0) {
            var anyPrimary = false;
            for (var i = 0; i < emailAddresses.length; i++) {
                anyPrimary = anyPrimary || emailAddresses[i].IsPrimary;
            }

            if (!anyPrimary) {
                emailAddresses[0].IsPrimary = true;
            }
        }
    };

    $scope.setPrimaryEmailAddress = function (newPrimaryEmailAddress) {
        var emailAddresses = $scope.contact.EmailAddresses;

        for (var i = 0; i < emailAddresses.length; i++) {
            emailAddresses[i].IsPrimary = false;
        }

        newPrimaryEmailAddress.IsPrimary = true;
    };
}]);

contactsControllers.directive('eyEditContact', function() {
    return {
        templateUrl: '/Content/Contacts/EditContact.html',
        restrict: 'E',
        scope: { contact: '=' },
        controller: 'eyEditContactController'
    };
});