describe('Contacts Services', function() {
    beforeEach(module('contactsApp'));

    describe('alerts', function () {
        var createService;
        
        beforeEach(inject(function ($injector) {
            createService = function() {
                return $injector.get('alerts');
            };
        }));

        it('should store an alert when adding an alert with text and type', function () {
            var text = 'The text';
            var type = 'info';
            
            var service = createService();
            
            service.addAlert(text, type);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe(type);
        });

        it('should store an alert with type of info when adding info with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addInfo(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('info');
        });

        it('should store an alert with type of success when adding success with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addSuccess(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('success');
        });

        it('should store an alert with type of warning when adding warning with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addWarning(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('warning');
        });

        it('should store an alert with type of danger when adding danger with text', function () {
            var text = 'The text';
            
            var service = createService();
            
            service.addDanger(text);
            
            var storedAlerts = service.readAlerts();
            expect(storedAlerts.length).toBe(1);
            expect(storedAlerts[0].text).toBe(text);
            expect(storedAlerts[0].type).toBe('danger');
        });

        it('should set an array of alerts on the scope when displaying alerts', function () {
            var text = 'The text';
            var type = 'info';
            
            var service = createService();
            service.addAlert(text, type);

            var scope = {};
            service.displayAlerts(scope);
            
            expect(scope.alerts.length).toBe(1);
            expect(scope.alerts[0].text).toBe(text);
            expect(scope.alerts[0].type).toBe(type);
        });

        it('should clear the alerts when displaying alerts', function () {
            var text = 'The text';
            var type = 'info';
            
            var service = createService();
            service.addAlert(text, type);

            service.displayAlerts({});
            
            expect(service.readAlerts().length).toBe(0);
        });
    });
});
