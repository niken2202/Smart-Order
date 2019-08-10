(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {           

         $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.adminLogin = function () {
                var stateService = $injector.get('$state');
                stateService.go('admin-login');
            };

            $scope.crashierLogin = function () {
                var stateService = $injector.get('$state');
                stateService.go('crashier-login');
            };


        }]);
})(angular.module('SmartOrder'));