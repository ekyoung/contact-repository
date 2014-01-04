var contactsApp = angular.module('contactsApp', [
    'ngRoute',
    'contactsControllers'
]);

contactsApp.config(['$routeProvider',
    function($routeProvider) {
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
