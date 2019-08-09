(function (app) {
    app.controller('registerController', ['$scope', 'notificationService', 'apiService',
        function ($scope, notificationService, apiService) {

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

            $scope.registerData = {
                BirthDay: new Date()
            }; 

            $scope.registerSubmit = function () {

                if (angular.equals($scope.psw, $scope.psw) == true) {
                    //notificationService.displaySuccess('Done');

                    apiService.post('/api/user/add', $scope.registerData, function (result) {
                        notificationService.displaySuccess('Ok rồi bạn iê');
                    }, function () {
                        notificationService.displayError('Tạo mới tài khoản không thành công !');
                    });

                } else {                    
                    notificationService.displayError('Mật khẩu nhập lại không khớp !');
                }
            }

        }]);
})(angular.module('SmartOrder'));