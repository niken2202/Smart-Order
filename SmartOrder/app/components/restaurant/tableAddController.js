(function (app) {
    app.controller('tableAddController', tableAddController);

    tableAddController.$inject = ['$scope', 'apiService', 'notificationService'];

    function tableAddController($scope, apiService, notificationService) {

        //dish binding in dialog add table
        $scope.tableAdd = {
            CreatedDate: new Date,
            Name: "Bàn ",
            Status: 1,
        }

        //get table list from api
        function getListTable() {
            apiService.get('/api/table/getall', null, function (result) {
                $scope.tables = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã bàn không thành công');
            });
        }
        getListTable();

        //add table to database
        $scope.Createtable = Createtable;

        function Createtable() {
            var notify;
            var condition = new Boolean(true);
            var checkName = $scope.tableAdd.Name;
            var curName;
            checkName = checkName.trim().toLowerCase();
            for (i = 0; i < $scope.tables.length; i++) {
                curName = $scope.tables[i].Name;
                curName = curName.trim().toLowerCase();
                    if (checkName == curName) {
                        condition = false;
                        notify = "Tên bàn đã tồn tại";
                        break;
                    }
                if ($scope.tableAdd.DeviceID == $scope.tables[i].DeviceID && $scope.tableAdd.DeviceID != null) {
                        condition = false;
                        notify = "Mã thiết bị đã được đăng ký cho bàn khác";
                        break;
                    }
            }
            if (condition == true) {
                apiService.post('api/table/add', $scope.tableAdd,
                    function (result) {
                        notificationService.displaySuccess('table ' + $scope.tableAdd.Name + ' đã được thêm mới');
                        $scope.reload();
                    }, function (error) {
                        notificationService.displayError('Thêm mới không thành công.');
                    }); 
            } else {
                notificationService.displayError(notify);
            }
          
        }
    
    }
})(angular.module('SmartOrder.restaurant'));