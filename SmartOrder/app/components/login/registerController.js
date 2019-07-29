(function (app) {
    app.controller('registerController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.registerData = {
                BirthDay: new Date()
            }; 

            $scope.registerSubmit = function () {
               
            }

        }]);
})(angular.module('SmartOrder'));