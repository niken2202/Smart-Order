(function (app) {
    app.filter('percentFilter', function () {
        return function (input) {
            if (input == null) {
                return '0%';
            } else {
                return input + "%";
            }
        }
    });
})(angular.module('SmartOrder.common'));