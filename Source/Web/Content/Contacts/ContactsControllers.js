var contactsControllers = angular.module('contactsControllers', []);

contactsControllers.controller('listController', ['$http', '$scope', 'contactsData', function slideController($http, $scope, contactsData) {
    $http.get('/api/contacts')
        .success(function(data) {
            $scope.contacts = data;
        });

    $scope.alerts = contactsData.alerts;
    contactsData.clearAlerts();
}]);

contactsControllers.controller('editController', ['$http', '$scope', '$routeParams', '$location', 'contactsData', function slideController($http, $scope, $routeParams, $location, contactsData) {
    $http.get('/api/contacts/' + $routeParams.contactIdentifier)
        .success(function(data) {
            $scope.contact = data;
        });

    $scope.save = function () {
        $http.put('/api/contacts/' + $routeParams.contactIdentifier, angular.toJson($scope.contact))
            .success(function(data) {
                contactsData.addAlert('Changes to the contact have been saved.', 'success');
                $location.path('/');
            });
    };

    $scope.cancel = function () {
        contactsData.addAlert('Changes to the contact have been cancelled.', 'info');
        $location.path('/');
    };
}]);
