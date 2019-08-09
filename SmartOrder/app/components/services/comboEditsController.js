(function (app) {
    app.controller('comboEditsController', comboEditsController);

    comboEditsController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams'];

    function comboEditsController($scope, apiService, notificationService, $stateParams) {

        var a = $scope.combo;
        $scope.edtCombo = {
            ID: a.ID, Name: a.Name, Price: a.Price, Amount: a.Amount, Description: a.Description,
            Image: a.Image, Status: a.Status
            //, CreatedDate: a.CreatedDate
        };

        //update combo
        $scope.updateCombo = updateCombo;

        function updateCombo() {            
            apiService.put('api/combo/update', $scope.edtCombo,
                function (result) {
                    notificationService.displaySuccess($scope.edtCombo.Name + ' đã được cập nhật mới! ');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });
        }

    }
})(angular.module('SmartOrder.services'));