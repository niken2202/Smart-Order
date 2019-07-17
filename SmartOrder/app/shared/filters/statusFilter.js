(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == 1) {
                return 'Có sẵn';
            } else {
                return 'Tạm hết';
            }
        }
    });
})(angular.module('SmartOrder.common'));