var contactsControllers = angular.module('contactsControllers', []);

contactsControllers.controller('listController', ['$http', '$scope', 'contactsData', function slideController($http, $scope, contactsData) {
    $http.get('/api/contacts')
        .success(function(data) {
            $scope.contacts = data;
        });

    $scope.alerts = contactsData.alerts;
    contactsData.alerts = [];
}]);

contactsControllers.controller('editController', ['$http', '$scope', '$routeParams', '$location', 'contactsData', function slideController($http, $scope, $routeParams, $location, contactsData) {
    $http.get('/api/contacts/' + $routeParams.contactIdentifier)
        .success(function(data) {
            $scope.contact = data;
        });

    $scope.save = function() {

    };

    $scope.cancel = function () {
        contactsData.alerts.push({ text: 'Changes to the contact have been cancelled.', type: 'info' });
        $location.path('/');
    };
}]);
