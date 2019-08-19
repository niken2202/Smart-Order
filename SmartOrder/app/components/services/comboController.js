(function (app) {
    app.controller('comboController', comboController);

    comboController.$inject = ['$scope','$state', 'ngDialog', 'apiService', 'notificationService'];

    function comboController($scope, $state,ngDialog, apiService, notificationService) {


        //get combo list from api
        function getListCombo() {
            apiService.get('/api/combo/getall', null, function (result) {
                $scope.combos = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã khuyến mại không thành công');
            });
        }
        getListCombo();

        //reload table
        $scope.reload = function (data) {
            getListCombo();
            ngDialog.close(vm.dialogId);
        };

        //show the dialog ditail combo by table
        $scope.comboDetails = comboDetails;
        function comboDetails(item) {
            //var abc = {
            //    ID: item.ID, Name: item.Name, Price: item.Price, Amount: item.Amount, Description: item.Description,
            //    Image: item.Image, Status: item.Status
            ////, CreatedDate: a.CreatedDate
            //};
            $state.go('editCombo', { ID: item.ID });
        }


        //var vm = this;
        //vm.openDialog = function ($event, item) {
        //    vm.init = function () {
        //        $scope.combo = item;
        //    }

        //    vm.init();

        //    // Show the dialog in the main controller
        //    var dialog = ngDialog.openConfirm({
        //        template: '/app/components/services/comboEditsView.html',
        //        scope: $scope,
        //        controller: 'comboEditsController',
        //        controllerAs: "file",
        //        closeByDocument: false, //can not close dialog by click out of dialog area
        //        className: 'ngdialog',
        //        showClose: false,
        //    }).then(
        //        function (value) {
        //            //save the contact form
        //        },
        //        function (value) {
        //            //Cancel or do nothing
        //        }
        //    );
        //};

        //function sort by title tr tag
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        };

        //show dialog to create new combo
        $scope.comboAdd = function () {
            $state.go('addCombo');
            //var addDialog = ngDialog.openConfirm({
            //    template: '/app/components/services/comboAddView.html',
            //    scope: $scope,
            //    controller: 'comboAddController',
            //    closeByDocument: false, //can not close dialog by click out of dialog area
            //    className: 'ngdialog',
            //    showClose: false,
            //}).then(
            //    function (value) {
            //        //save the contact form
            //    },
            //    function (value) {
            //        //Cancel or do nothing
            //    }
            //);
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