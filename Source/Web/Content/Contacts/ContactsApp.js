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

