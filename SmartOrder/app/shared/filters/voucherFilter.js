(function (app) {
    app.filter('voucherFilter', function () {
        return function (input) {
            if (input == null) {
                return 'Không';
            } else {
                return input;
            }
        }
    });
})(angular.module('SmartOrder.common'));