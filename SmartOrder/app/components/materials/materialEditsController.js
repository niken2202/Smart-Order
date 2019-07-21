(function (app) {
    app.controller('materialEditsController', materialEditsController);

    materialEditsController.$inject = ['$scope', '$state', 'ngDialog', 'apiService', 'notificationService', '$stateParams'];

    function materialEditsController($scope, $state, ngDialog, apiService, notificationService, $stateParams) {

        var a = $scope.selectMate;
        $scope.edtMate = {
            ID: a.ID, Name: a.Name, Price: a.Price, Amount: a.Amount, Unit: a.Unit,CreatedDate: a.CreatedDate
        };

        //update dish
        $scope.updateMate = updateMate;

        function updateMate() {
            apiService.put('api/material/update', $scope.edtMate,
                function (result) {
                    notificationService.displaySuccess($scope.edtMate.Name + ' đã được cập nhật mới! ');
                    //console.log('update success');
                    $scope.reloadMate();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });
        }


    }
})(angular.module('SmartOrder.materials'));