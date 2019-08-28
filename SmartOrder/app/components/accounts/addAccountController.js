(function (app) {
    app.controller('addAccountController', addAccountController);

    addAccountController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function addAccountController($scope, ngDialog, apiService, notificationService) {
        $scope.notification = "";
        $scope.psw = "";
        $scope.rePsw = "";
        $scope.accAdd;
        $scope.condition = true;

        //get list roles to the select box
        $scope.Roles = [];
        function getRoles() {
            apiService.get('api/role/getall', null, function (result) {
                $scope.Roles = result.data;
            }, function () {
                //console.log('Can get lish dish category');
            });
        }
        getRoles();

        //check password and rePassword
        $scope.checkPSW = checkPSW;
        function checkPSW() {
            if (angular.equals($scope.psw, $scope.rePsw) == true) {
                $scope.condition = false;
                $scope.notification = ""
            } else {
                $scope.condition = true;
                $scope.notification = "Mật khẩu không khớp!"
            }
        }

        $scope.CreateAcc = CreateAcc;
        function CreateAcc() {
            if (angular.equals($scope.psw, $scope.rePsw) == true) {
                //notificationService.displaySuccess('Done');
                var listRole = [];
                listRole.push($scope.accAdd.role.Name);
                var registerData = {
                    FullName: $scope.accAdd.FullName,
                    BirthDay: $scope.accAdd.BirthDay,
                    UserName: $scope.accAdd.UserName,
                    PhoneNumber: $scope.accAdd.PhoneNumber,
                    Password: $scope.psw,
                    Address: $scope.accAdd.Address,
                    Roles: listRole
                }
                apiService.post('/api/user/add', registerData, function (result) {
                    notificationService.displaySuccess('Thêm mới tài khoản thành công!');
                }, function () {
                    notificationService.displayError('Tạo mới tài khoản không thành công !');
                });
            } else {
                notificationService.displayError('Mật khẩu nhập lại không khớp !');
            }
        }

        $scope.changePSW = changePSW;
        function changePSW() {
            var password = $scope.psw;
            password = password.trim();
            if (password.length < 9 || password == null) {
                $scope.notificationPSW = "Mật khẩu quá ngắn (Yêu cầu mật khẩu trên 9 ký tự!)"
            }
        }

        $scope.reChangePSW = reChangePSW;
        function reChangePSW() {
            var password = $scope.psw;
            password = password.trim();
            var rePassword = $scope.rePsw;
            rePassword = rePassword.trim();
            if (password.length < 9 || password == null) {
                //$scope.notificationPSW = "Mật khẩu quá ngắn (Yêu cầu mật khẩu trên 9 ký tự!)"
            } else {
                if (angular.equals(password, rePassword) == false) {
                    $scope.notificationRePSW = "Mật khẩu không khớp!"
                }
            }
        }
    }
})(angular.module('SmartOrder.accounts'));