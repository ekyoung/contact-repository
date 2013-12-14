var contactsControllers = angular.module('contactsControllers', []);

contactsControllers.controller('listController', ['$http', '$scope', 'alerts', function ($http, $scope, alerts) {
    $http.get('/api/contacts')
        .success(function(data) {
            $scope.contacts = data;
        });

    alerts.displayAlerts($scope);
}]);

contactsControllers.controller('createController', ['$http', '$scope', '$location', 'alerts', function ($http, $scope, $location, alerts) {
    $scope.contact = {
        FirstName: null,
        LastName: null
    };
    
    $scope.save = function() {
        $http.post('/api/contacts', angular.toJson($scope.contact))
            .success(function(data) {
                alerts.addSuccess('A new contact has been created.');
                $location.path('/');
            });
    };

    $scope.cancel = function () {
        alerts.addInfo('Creation of a new contact has been cancelled.');
        $location.path('/');
    };
}]);

contactsControllers.controller('editController', ['$http', '$scope', '$routeParams', '$location', 'alerts', function ($http, $scope, $routeParams, $location, alerts) {
    $http.get('/api/contacts/' + $routeParams.contactIdentifier)
        .success(function(data) {
            $scope.contact = data;
            $scope.originalName = data.FirstName + ' ' + data.LastName;
        });

    $scope.save = function () {
        $http.put('/api/contacts/' + $routeParams.contactIdentifier, angular.toJson($scope.contact))
            .success(function(data) {
                alerts.addSuccess('Changes to the contact have been saved.');
                $location.path('/');
            });
    };

    $scope.cancel = function () {
        alerts.addInfo('Changes to the contact have been cancelled.');
        $location.path('/');
    };
}]);

contactsControllers.controller('deleteController', ['$http', '$scope', '$routeParams', '$location', 'alerts', function ($http, $scope, $routeParams, $location, alerts) {
    $http.get('/api/contacts/' + $routeParams.contactIdentifier)
        .success(function (data) {
            $scope.contact = data;
        });

    $scope.continue = function () {
        $http.delete('/api/contacts/' + $routeParams.contactIdentifier)
            .success(function (data) {
                alerts.addSuccess('The contact has been deleted.');
                $location.path('/');
            });
    };
    
    $scope.cancel = function () {
        alerts.addInfo('Deletion of the contact has been cancelled.');
        $location.path('/');
    };
}]);