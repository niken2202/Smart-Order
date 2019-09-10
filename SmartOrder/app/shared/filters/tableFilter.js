(function (app) {
    app.filter('tableFilter', function () {
        return function (input) {
            if (input == 1) {
                return 'Khả dụng';
            } else if (input == 0) {
                return 'Có khách';
            } else {
                return 'Khả dụng';
            }
        }
    });
})(angular.module('SmartOrder.common'));