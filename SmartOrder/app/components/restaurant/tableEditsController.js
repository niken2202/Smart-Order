(function (app) {
    app.controller('tableEditsController', tableEditsController);

    tableEditsController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams'];

    function tableEditsController($scope, apiService, notificationService, $stateParams) {


        //get table list from api
        function getListTable() {
            apiService.get('/api/table/getall', null, function (result) {
                $scope.tables = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã bàn không thành công');
            });
        }
        getListTable();


        var a = $scope.table;
        $scope.edtTable = {
            ID: a.ID, Name: a.Name, DeviceID: a.DeviceID, Status: a.Status, CreatedDate: a.CreatedDate
        };

        //update combo
        $scope.updateTable = updateTable;

        function updateTable() {
            var notify;
            var condition = new Boolean(true);
            var checkName = $scope.edtTable.Name;
            var curName;
            checkName = checkName.trim().toLowerCase();
            for (i = 0; i < $scope.tables.length; i++) {
                curName = $scope.tables[i].Name;
                curName = curName.trim().toLowerCase();
                if ($scope.edtTable.ID != $scope.tables[i].ID) {
                    if (checkName == curName) {
                        condition = false;
                        notify = "Tên bàn đã tồn tại";
                        break;
                    }
                    if ($scope.edtTable.DeviceID == $scope.tables[i].DeviceID && $scope.edtTable.DeviceID.length > 0) {
                        condition = false;
                        notify = "Mã thiết bị đã được đăng ký cho bàn khác";
                        break;
                    }  
                    
                }                                
            }
            if (condition == true) {
                apiService.put('api/table/update', $scope.edtTable,
                    function (result) {
                        notificationService.displaySuccess($scope.edtTable.Name + ' đã được cập nhật mới! ');
                        $scope.reload();
                    }, function (error) {
                        notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                    });
            } else {
                notificationService.displayError(notify);
            }                         
        }
    }
})(angular.module('SmartOrder.restaurant'));