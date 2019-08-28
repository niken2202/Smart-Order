(function (app) {
    app.controller('editsAccountController', editsAccountController);

    editsAccountController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams'];

    function editsAccountController($scope, apiService, notificationService, $stateParams) {
        $scope.notification = "";
        $scope.newPsw = "";
        $scope.reNewPsw = "";
        $scope.current = "";
        $scope.changePswCondition = false;

        $scope.changerPSW = changerPSW;
        function changerPSW() {
            if ($scope.changePswCondition == false) {
                $scope.changePswCondition = true;
                document.getElementById("divChangePassword").style.display = "block";
            } else {
                $scope.changePswCondition = false;
                document.getElementById("divChangePassword").style.display = "none";
            }
        }

        //check password and rePassword
        $scope.checkPSW = checkPSW;
        function checkPSW() {
            if (angular.equals($scope.newPsw, $scope.reNewPsw) == true) {
                $scope.notification = ""
            } else {
                $scope.notification = "Mật khẩu không khớp!"
            }
        }

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
        
        //get account infomation by id
        function getComboByID() {
            var transfer = {
                params: {
                    id: $stateParams.ID,
                }
            }
            document.getElementById("divChangePassword").style.display = "none";
            apiService.get('api/user/getbyid', transfer,
                function (result) {
                    $scope.accEdt = result.data;
                    $scope.accEdt.BirthDay = new Date(result.data.BirthDay);
                }, function (error) {
                    notificationService.displayError('Đã xảy ra lỗi trong quá trình tải dữ liệu');
                });
        }
        getComboByID();

        $scope.updatePsw = updatePsw;
        function updatePsw() {
            if (angular.equals($scope.newPsw, $scope.reNewPsw) == true) {
                var updatePsw = {
                    Id: $scope.accEdt.Id,
                    CurrentPassword: $scope.newPsw,
                    NewPassword: $scope.current
                }
                apiService.post('/api/user/changepassword', updatePsw, function (result) {
                    notificationService.displaySuccess('Thêm mới tài khoản thành công!');
                }, function () {
                    notificationService.displayError('Tạo mới tài khoản không thành công !');
                });

            }
        }


        //update account
        $scope.updateAcc = updateAcc;
        function updateAcc() {
            if (angular.equals($scope.psw, $scope.rePsw) == true) {
                //notificationService.displaySuccess('Done');
                var registerData = {
                    FullName: $scope.accEdt.FullName,
                    BirthDay: $scope.accEdt.BirthDay,
                    UserName: $scope.accEdt.UserName,
                    PhoneNumber: $scope.accEdt.PhoneNumber,
                    Password: $scope.psw,
                    Address: $scope.accEdt.Address,
                    Roles: [$scope.accEdt.role.Name]
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

    }
})(angular.module('SmartOrder.accounts'));