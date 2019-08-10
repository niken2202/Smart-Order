﻿(function (app) {
    app.controller('adminLoginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {
            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.adminLogin = function () {
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        notificationService.displayError("Đăng nhập không đúng.");
                    }
                    else {
                        var stateService = $injector.get('$state');
                        stateService.go('home');
                        //stateService.go('cashier');
                    }
                });
            }
        }]);
})(angular.module('SmartOrder'));