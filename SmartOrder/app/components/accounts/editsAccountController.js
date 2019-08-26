(function (app) {
    app.controller('editsAccountController', editsAccountController);

    editsAccountController.$inject = ['$scope', 'apiService', 'notificationService','$stateParams'];

    function editsAccountController($scope, apiService, notificationService, $stateParams) {

        $scope.edtAcc = {
            Id: "",
            FullName: "",
            BirthDay: new Date($scope.selectAcction.BirthDay),
            UserName = "",
            PhoneNumber = "",
            Address = "",
            Roles = "",
        };

        //get account infomation by id
        function getAccountByID() {
            var transfer = {
                params: {
                    comboId: $stateParams.ID,
                }
            }
            apiService.get('api/combo/getbyid', transfer,
                function (result) {
                    $scope.comboEdt = result.data;
                }, function (error) {
                    notificationService.displayError('Đã xảy ra lỗi trong quá trình tải dữ liệu');
                });
        }
        getAccountByID();

    }
})(angular.module('SmartOrder.accounts'));