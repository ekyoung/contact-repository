describe('Contacts Controllers', function () {
    beforeEach(module('contactsApp'));

    describe('contactsController', function() {
        //Nothing here yet
    });

    describe('listController', function () {
        var $httpBackend, $scope, contactsData, createController;

        var apiRoot = '/api';
        
        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');

            $scope = $injector.get('$rootScope');

            contactsData = {};
            
            // The $controller service is used to create instances of controllers
            var $controller = $injector.get('$controller');

            createController = function() {
                return $controller('listController', { $scope: $scope, contactsData: contactsData });
            };
        }));

        it('should set an array of contacts on the scope when given an array of contacts', function () {
            var contacts = [{ FirstName: 'Joe', LastName: 'One' }, { FirstName: 'Frank', LastName: 'Two' }];
            $httpBackend.when('GET', apiRoot + '/contacts').respond(contacts);
            var controller = createController();
            $httpBackend.flush();
            
            expect($scope.contacts.length).toBe(2);
        });

    });
});
