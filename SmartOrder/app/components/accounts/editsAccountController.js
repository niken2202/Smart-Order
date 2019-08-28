(function (app) {
    app.controller('editsAccountController', editsAccountController);

    editsAccountController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams'];

    function editsAccountController($scope, apiService, notificationService, $stateParams) {
        $scope.notification = "";
        $scope.psw = "";
        $scope.rePsw = "";
        $scope.condition = false;
        $scope.changePswCondition = false;

        $scope.changerPSW = changerPSW;
        function changerPSW() {
            if ($scope.changePswCondition == false) {
                $scope.changePswCondition = true;
                $scope.condition = true;
            } else {
                $scope.changePswCondition = false;
            }
        }

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
            apiService.get('api/user/getbyid', transfer,
                function (result) {
                    $scope.accEdt = result.data;
                    $scope.accEdt.BirthDay = new Date(result.data.BirthDay);
                }, function (error) {
                    notificationService.displayError('Đã xảy ra lỗi trong quá trình tải dữ liệu');
                });
        }
        getComboByID();
    }
})(angular.module('SmartOrder.accounts'));