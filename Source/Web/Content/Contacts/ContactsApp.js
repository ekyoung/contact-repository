var contactsApp = angular.module('contactsApp', [
    'ngRoute',
    'contactsControllers'
]);

contactsApp.config(['$routeProvider',
    function($routeProvider) {
        $routeProvider.
            when('/contacts/:contactIdentifier', {
                templateUrl: '/Content/Contacts/ContactTemplate.html',
                controller: 'contactController'
            }).
            otherwise({
                templateUrl: '/Content/Contacts/ListTemplate.html',
                controller: 'listController'
            });
    }]);

contactsApp.service('contactsData', function () {
    var contacts = [];
    return {
        contacts: contacts
    };
});