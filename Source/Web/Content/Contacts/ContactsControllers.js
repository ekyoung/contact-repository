var contactsControllers = angular.module('contactsControllers', []);

contactsControllers.controller('listController', ['$http', '$scope', 'contactsData', function slideController($http, $scope, contactsData) {
    $http.get('/api/contacts')
        .success(function (data) {
            $scope.contacts = data;
        });
}]);

contactsControllers.controller('contactController', ['$scope', '$routeParams', '$location', 'contactsData', function slideController($scope, $routeParams, $location, contactsData) {
    //Nothing here yet
}]);
