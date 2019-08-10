(function (app) {
    app.controller('tableController', tableController);

    tableController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function tableController($scope, ngDialog, apiService, notificationService) {

        //get table list from api
        function getListTable() {
            apiService.get('/api/table/getall', null, function (result) {
                $scope.tables = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã bàn không thành công');
            });
        }
        getListTable();

        //generate index number in table
        $scope.serial = 1;
        $scope.itemPerPage = 10
        $scope.indexCount = function (newPageNumber) {

            $scope.serial = newPageNumber * $scope.itemPerPage - ($scope.itemPerPage - 1);
        }

        //reload table
        $scope.reload = function (data) {
            getListTable();
            ngDialog.close(vm.dialogId);
        };

        //show the dialog ditail table by table
        var vm = this;
        vm.openDialog = function ($event, item) {
            vm.init = function () {
                $scope.table = item;
            }

            vm.init();

            // Show the dialog in the main controller
            var dialog = ngDialog.openConfirm({
                template: '/app/components/restaurant/tableEditsView.html',
                scope: $scope,
                controller: 'tableEditsController',
                controllerAs: "file",
                closeByDocument: false, //can not close dialog by click out of dialog area
                className: 'ngdialog',
                showClose: false,
            }).then(
                function (value) {
                    //save the contact form
                },
                function (value) {
                    //Cancel or do nothing
                }
            );
        };

        //function sort by title tr tag
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        };

        //show dialog to create new table
        $scope.tableAdd = function () {
            var addDialog = ngDialog.openConfirm({
                template: '/app/components/restaurant/tableAddView.html',
                scope: $scope,
                controller: 'tableAddController',
                closeByDocument: false, //can not close dialog by click out of dialog area
                className: 'ngdialog',
                showClose: false,
            }).then(
                function (value) {
                    //save the contact form
                },
                function (value) {
                    //Cancel or do nothing
                }
            );
        };

        //open popup to determine before delete table
        $scope.tableDel = function (item) {
            alertify.confirm('Xóa bàn', 'Bạn có muốn xóa: ' + item.Name + ' ?', function () {
                var data = {
                    params: {
                        ID: item.ID
                    }
                }
                apiService.del('api/table/delete', data,
                    function (result) {
                        notificationService.displaySuccess('Xóa bàn thành công');
                        getListTable();
                    }, function (error) {
                        notificationService.displayError('Đã xảy ra lỗi !');
                        console.log(error);
                    });

            }, function () {
                //notificationService.displayWarning('Thông tin chưa được lưu!');
            });

        }

    }
})(angular.module('SmartOrder.restaurant'));