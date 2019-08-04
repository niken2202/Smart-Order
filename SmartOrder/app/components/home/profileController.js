(function (app) {
    app.controller('profileController', ['$scope', 'apiService', '$injector', 'notificationService',
        function ($scope, apiService, $injector, notificationService) {

            $scope.registerData;
            //get role from api
            function getRole() {

                apiService.get('/api/role/getall', null, function (result) {
                    $scope.roles = result.data;
                    if ($scope.roles.length === 0) {
                        //notificationService.displayWarning('Danh sách trống !');
                    }
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                });

            }
            getRole();

        }]);
})(angular.module('SmartOrder'));