(function (app) {
    app.filter('avaiableFilter', function () {
        return function (input) {
            if (input == 1) {
                return 'Khả dụng';
            } else {
                return 'Tạm ngưng';
            }
        }
    });
})(angular.module('SmartOrder.common'));