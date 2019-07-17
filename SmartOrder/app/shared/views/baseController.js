(function (app) {
    app.controller('baseController', baseController);
    function baseController() {
        angular.element(document).ready(function () {
            $.getScript('../assets/scripts/main.js', function () { });
            //console.log('x')
        });

    };
})(angular.module('SmartOrder'));