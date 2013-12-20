var contactsApp = angular.module('contactsApp', [
    'ngRoute',
    'contactsControllers'
]);

contactsApp.config(['$routeProvider',
    function($routeProvider) {
        $routeProvider.
            when('/create', {
                templateUrl: '/Content/Contacts/CreateContact.html',
                controller: 'createController'
            }).
            when('/edit/:contactIdentifier', {
                templateUrl: '/Content/Contacts/EditContact.html',
                controller: 'editController'
            }).
            when('/delete/:contactIdentifier', {
                templateUrl: '/Content/Contacts/DeleteContact.html',
                controller: 'deleteController'
            }).
            otherwise({
                templateUrl: '/Content/Contacts/ListContacts.html',
                controller: 'listController'
            });
    }]);

contactsApp.service('alerts', function () {
    var alerts = [];
    return {
        readAlerts: function() {
            return alerts.slice(0);
        },
        addAlert: function(text, type) {
            alerts.push({ text: text, type: type });
        },
        addSuccess: function(text) {
            this.addAlert(text, 'success');
        },
        addInfo: function(text) {
            this.addAlert(text, 'info');
        },
        addWarning: function(text) {
            this.addAlert(text, 'warning');
        },
        addDanger: function(text) {
            this.addAlert(text, 'danger');
        },
        displayAlerts: function(scope) {
            scope.alerts = alerts;
            alerts = [];
        }
    };
});

contactsApp.factory('contactRepository', ['$http', 'apiRootUrl', function($http, apiRootUrl) {
    return {
        getContacts: function () {
            var success = function(response) {
                return response.data;
            };

            var error = function (response) {
                if (response.status == 404) {
                    return 'The server returned 404.';
                } else {
                    var exceptionMessage = '[Couldn\'t find a message]';
                    if (response.data && response.data.ExceptionMessage) {
                        exceptionMessage = response.data.ExceptionMessage;
                    }
                    return 'The server returned the following error message: ' + exceptionMessage;
                }
            };
            
            return $http.get(apiRootUrl + '/contacts')
                .then(success, error);
        },
        
        getContact: function(contactIdentifier) {
            return $http.get(apiRootUrl + '/contacts/' + contactIdentifier);
        },
        
        insertContact: function(contact) {
            return $http.post(apiRootUrl + '/contacts', angular.toJson(contact));
        },
        
        updateContact: function(contact) {
            return $http.put(apiRootUrl + '/contacts/' + contact.Identifier, angular.toJson(contact));
        },
        
        deleteContact: function (contactIdentifier) {
            return $http.delete(apiRootUrl + '/contacts/' + contactIdentifier);
        }
    };
}]);