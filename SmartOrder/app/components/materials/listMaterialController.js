(function (app) {
    app.controller('listMaterialController', listMaterialController);

    listMaterialController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function listMaterialController($scope, ngDialog, apiService, notificationService) {

        //get material list from api
        function getMaterial() {
            apiService.get('/api/material/getall', null, function (result) {
                $scope.materials = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách nguyên liệu không thành công');
            });
        }
        getMaterial();

        //function sort by title tr tag
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        };

        //reload table material
        $scope.reloadMate = function (data) {
            getMaterial();
            ngDialog.close(vm.dialogId);
        };

        //show the dialog ditail material by table
        var vm = this;
        $scope.openDialog = function ($event, item) {            
            vm.init = function () {
                $scope.selectMate = item;
            }

            vm.init();

            // Show the dialog in the main controller
            var dialog = ngDialog.openConfirm({
                template: '/app/components/materials/materialEditsView.html',
                scope: $scope,
                controller: 'materialEditsController',
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

        //show dialog to create new material
        $scope.materialAdd = function () {
            var addDialog = ngDialog.openConfirm({
                template: 'app/components/materials/materialAddView.html',
                scope: $scope,
                controller: 'materialAddController',
                //controllerAs: "file",
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

        //open popup to determine before delete material
        $scope.mateDel = function (item) {
            alertify.confirm('Xóa nguyên liệu', 'Bạn có muốn xóa nguyên liệu: ' + item.Name + ' ?', function () {
                console.log(item.ID);
                var data = {
                    params: {
                        ID: item.ID
                    }
                }
                apiService.del('api/material/delete', data,
                    function (result) {
                        notificationService.displaySuccess('Đã xóa nguyên liệu ' + item.Name + ' !');
                        getMaterial();
                    }, function (error) {
                        notificationService.displayError('Đã xảy ra lỗi !');
                        console.log(error);
                    });

            }, function () {
                notificationService.displayWarning('Thông tin chưa được lưu!');
            });

        }

    }
})(angular.module('SmartOrder.materials'));