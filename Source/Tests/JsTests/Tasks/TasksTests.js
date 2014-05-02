describe('eyTasks', function() {
    beforeEach(module('eyTasks'));

    describe('tasks', function () {
        var $location, createService;

        var overviewLocation = '/someEntity/id1';
        
        beforeEach(inject(function ($injector) {
            $location = $injector.get('$location');
            $location.path(overviewLocation);

            createService = function () {
                return $injector.get('tasks');
            };
        }));

        it('should return null when getOrigin is called before a task has been started and no default origin has been set', function () {
            var service = createService();
            
            expect(service.getOrigin()).toBe(null);
        });

        it('should return the default origin when getOrigin is called before a task has been started and a default origin has been set', function () {
            var service = createService();

            var defaultOrigin = '/someDefaultEntity/id1';
            service.setDefaultOrigin(defaultOrigin);
            
            expect(service.getOrigin()).toBe(defaultOrigin);
        });

        it('should return the origin when getOrigin is called after a task has been started', function() {
            var service = createService();

            service.start('/someEntity/id1/edit');

            expect(service.getOrigin()).toBe(overviewLocation);
        });

        it('should redirect to the specified path when start is called', function() {
            var service = createService();

            var firstStepPath = '/someEntity/id1/edit';
            service.start(firstStepPath);

            expect($location.path()).toBe(firstStepPath);
        });

        it('should redirect to the origin when redirect back is called from the first task step', function() {
            var service = createService();

            service.start('/someEntity/id1/edit');
            service.redirectBack();

            expect($location.path()).toBe(overviewLocation);
        });

        it('should redirect to the default origin when redirect back is called without a task having been started after setting the default origin', function() {
            var service = createService();

            var defaultOrigin = '/someDefaultEntity/id1';
            service.setDefaultOrigin(defaultOrigin);
            service.redirectBack();

            expect($location.path()).toBe(defaultOrigin);
        });
    });
});
