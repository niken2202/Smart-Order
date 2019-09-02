(function (app) {
    app.controller('editsAccountController', editsAccountController);

    editsAccountController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams','$state'];

    function editsAccountController($scope, apiService, notificationService, $stateParams, $state) {
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
        function getAccountByID() {
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
                    $scope.accEdt.role = result.data.Roles[0];
                    //console.log(result.data.Roles[0]);
                }, function (error) {
                    notificationService.displayError('Tải thông tin không thành công!');
                });
        }
        getAccountByID();

        $scope.updatePsw = updatePsw;
        function updatePsw() {
            if (angular.equals($scope.newPsw, $scope.reNewPsw) == true) {
                var updatePsw = {
                    Id: $scope.accEdt.Id,
                    CurrentPassword: $scope.current,
                    NewPassword: $scope.newPsw
                }
                apiService.put('/api/user/changepassword', updatePsw, function (result) {                    
                    notificationService.displaySuccess('Cập nhật mật khẩu thành công!');
                    $scope.accEdt.Id = "",
                    $scope.current = "",
                    $scope.newPsw ="",
                    $scope.reNewPsw = "";
                    $scope.changePswCondition = false;
                    document.getElementById("divChangePassword").style.display = "none";
                }, function () {
                    $scope.current = "",
                    notificationService.displayError('Mật khẩu hiện tại không đúng !');
                });

            } else {
                  $scope.newPsw ="",
                  $scope.reNewPsw = "";
                notificationService.displayError('Mật khẩu nhập lại không khớp!');
            }
        }


        //update account
        $scope.updateAcc = updateAcc;
        function updateAcc() {
            if ($scope.changePswCondition == false) {
                var listRole = [];
                listRole.push($scope.accEdt.role.Name);
                var registerData = {
                    Id: $scope.accEdt.Id,
                    FullName: $scope.accEdt.FullName,
                    BirthDay: $scope.accEdt.BirthDay,
                    UserName: $scope.accEdt.UserName,
                    PhoneNumber: $scope.accEdt.PhoneNumber,
                    PassWord: null,
                    CurrentPassword: null,
                    NewPassword: null,
                    Address: $scope.accEdt.Address,
                    Roles: listRole
                }
                apiService.put('/api/user/update', registerData, function (result) {
                    $state.go('list_account');
                    notificationService.displaySuccess('Cập nhật tài khoản thành công!');
                }, function () {
                    notificationService.displayError('Cập nhật tài khoản xảy ra lỗi !');
                });
            } else {
                notificationService.displayWarning('Vui lòng hoàn thành và đóng phần thay đổi mật khẩu!');
            }
        }

    }
})(angular.module('SmartOrder.accounts'));