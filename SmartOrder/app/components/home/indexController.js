(function (app) {
    app.controller('indexController', indexController);
    function indexController() {
        var init = function () {
            $.getScript('../assets/scripts/main.js', function () { });
        }
        init();

    };
})(angular.module('SmartOrder'));