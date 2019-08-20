(function (app) {
    app.controller('comboController', comboController);

    comboController.$inject = ['$scope','$state', 'ngDialog', 'apiService', 'notificationService'];

    function comboController($scope, $state,ngDialog, apiService, notificationService) {

        //generate index number in table
        $scope.serial = 1;
        $scope.itemPerPage = 25
        $scope.indexCount = function (newPageNumber) {

            $scope.serial = newPageNumber * $scope.itemPerPage - ($scope.itemPerPage - 1);
        }

        //get combo list from api
        function getListCombo() {
            apiService.get('/api/combo/getall', null, function (result) {
                $scope.combos = result.data;
            }, function () {
                //notificationService.displayError('Tải danh sách mã Combo không thành công');
            });
        }
        getListCombo();

        //show the dialog ditail combo by table
        $scope.comboDetails = comboDetails;
        function comboDetails(item) {
            $state.go('editCombo', { ID: item.ID });
        }

        //function sort by title tr tag
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        };

        //show dialog to create new combo
        $scope.comboAdd = function () {
            $state.go('addCombo');
        };

        //open popup to determine before delete combo
        $scope.comboDel = function (item) {
            alertify.confirm('Xóa combo', 'Bạn có muốn xóa combo: ' + item.Name + ' ?', function () {
                console.log(item.ID);
                var data = {
                    params: {
                        ID: item.ID
                    }
                }
                apiService.del('api/combo/delete', data,
                    function (result) {
                        notificationService.displaySuccess('Xóa combo thành công');
                        getListCombo();
                    }, function (error) {
                        notificationService.displayError('Đã xảy ra lỗi !');
                        console.log(error);
                    });

            }, function () {
                //notificationService.displayWarning('Thông tin chưa được lưu!');
            });

        }

    }
})(angular.module('SmartOrder.services'));