var eyTasks = angular.module('eyTasks', []);

eyTasks.service('tasks', ['$location', function ($location) {
    var currentTask = {
        origin: null,
        defaultOrigin: null
    };
    
    return {
        getOrigin: function() {
            if (currentTask.origin) {
                return currentTask.origin;
            }

            return currentTask.defaultOrigin;
        },
        setDefaultOrigin: function(defaultOrigin) {
            currentTask.defaultOrigin = defaultOrigin;
        },
        start: function(firstStepLocation) {
            currentTask.origin = $location.path();
            $location.path(firstStepLocation);
        },
        redirectBack: function() {
            $location.path(this.getOrigin());
        }
    };
}]);

eyTasks.directive('eyStartTask', ['tasks', function (tasks) {
    var link = function (scope, element, attrs) {
        element.on("click", function () {
            tasks.start(attrs.eyStartTask);
            scope.$apply();
        });
    };

    return {
        link: link
    };
}]);