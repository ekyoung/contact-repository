var contactsControllers = angular.module('contactsControllers', []);

contactsControllers.controller('listController', ['$http', '$scope', 'alerts', 'apiRootUrl', function($http, $scope, alerts, apiRootUrl) {
    $http.get(apiRootUrl + '/contacts').
        success(function(data) {
            $scope.contacts = data;
        }).
        error(function (data, status) {
            if (status == 404) {
                alerts.addDanger('The server returned 404.');
            } else {
                var exceptionMessage = '[Couldn\'t find a message]';
                if (data && data.ExceptionMessage) {
                    exceptionMessage = data.ExceptionMessage;
                }
                alerts.addDanger('The server returned the following error message: ' + exceptionMessage);
            }
            alerts.displayAlerts($scope);
        });

    alerts.displayAlerts($scope);
}]);

contactsControllers.controller('createController', ['$http', '$scope', '$location', 'alerts', 'apiRootUrl', function ($http, $scope, $location, alerts, apiRootUrl) {
    $scope.contact = {
        FirstName: null,
        LastName: null
    };
    
    $scope.save = function() {
        $http.post(apiRootUrl + '/contacts', angular.toJson($scope.contact))
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

contactsControllers.controller('editController', ['$http', '$scope', '$routeParams', '$location', 'alerts', 'apiRootUrl', function ($http, $scope, $routeParams, $location, alerts, apiRootUrl) {
    $http.get(apiRootUrl + '/contacts/' + $routeParams.contactIdentifier)
        .success(function(data) {
            $scope.contact = data;
            $scope.originalName = data.FirstName + ' ' + data.LastName;
        });

    $scope.save = function () {
        $http.put(apiRootUrl + '/contacts/' + $routeParams.contactIdentifier, angular.toJson($scope.contact))
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

contactsControllers.controller('deleteController', ['$http', '$scope', '$routeParams', '$location', 'alerts', 'apiRootUrl', function ($http, $scope, $routeParams, $location, alerts, apiRootUrl) {
    $http.get(apiRootUrl + '/contacts/' + $routeParams.contactIdentifier)
        .success(function (data) {
            $scope.contact = data;
        });

    $scope.continue = function () {
        $http.delete(apiRootUrl + '/contacts/' + $routeParams.contactIdentifier)
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