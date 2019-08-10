(function (app) {
    app.controller('tableEditsController', tableEditsController);

    tableEditsController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams'];

    function tableEditsController($scope, apiService, notificationService, $stateParams) {

        var a = $scope.table;
        $scope.edtTable = {
            ID: a.ID, Name: a.Name, DeviceID: a.DeviceID, Status: a.Status ,CreatedDate: a.CreatedDate
        };

        //update combo
        $scope.updateTable = updateTable;

        function updateTable() {            
            apiService.put('api/table/update', $scope.edtTable,
                function (result) {
                    notificationService.displaySuccess($scope.edtTable.Name + ' đã được cập nhật mới! ');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });
        }

    }
})(angular.module('SmartOrder.restaurant'));