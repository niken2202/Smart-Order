(function (app) {
    app.filter('avaiableFilter', function () {
        return function (input) {
            if (input == 1 || input == true) {
                return 'Khả dụng';
            } else {
                return 'Tạm dừng';
            }
        }
    });
})(angular.module('SmartOrder.common'));