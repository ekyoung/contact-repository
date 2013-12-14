var contactsApp = angular.module('contactsApp', [
    'ngRoute',
    'contactsControllers'
]);

contactsApp.config(['$routeProvider',
    function($routeProvider) {
        $routeProvider.
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

contactsApp.service('contactsData', function () {
    var contacts = [];
    var alerts = [];
    return {
        contacts: contacts,
        alerts: alerts,
        addAlert: function(text, type) {
            this.alerts.push({ text: text, type: type });
        },
        clearAlerts: function() {
            this.alerts = [];
        }
    };
});