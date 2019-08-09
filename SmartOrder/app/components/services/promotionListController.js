(function (app) {
    app.controller('promotionListController', promotionListController);

    promotionListController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function promotionListController($scope, ngDialog, apiService, notificationService) {


        //get promotion list from api
        function getPromotion() {
            apiService.get('/api/promotioncode/getall', null, function (result) {
                $scope.promotions = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã khuyến mại không thành công');
            });
        }
        getPromotion();

        //function sort by title tr tag
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        };

        //reload table promotion
        $scope.reloadPromo = function () {
            getPromotion();
            ngDialog.close(vm.dialogId);
        };

        //show the dialog ditail promotion by table
        var vm = this;
        $scope.openDialog = function ($event, item) {
            vm.init = function () {
                $scope.selectPromotion = item;
            }

            vm.init();

            // Show the dialog detail promotion 
            var dialog = ngDialog.openConfirm({
                template: '/app/components/services/promotionEditsView.html',
                scope: $scope,
                controller: 'promotionEditsController',
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

        //show dialog to create new promotion
        $scope.promotionAdd = function () {
            var addDialog = ngDialog.openConfirm({
                template: 'app/components/services/promotionAddView.html',
                scope: $scope,
                controller: 'promotionAddController',
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

        

        //open popup to determine before delete promotion
        //$scope.mateDel = function (item) {
        //    alertify.confirm('Xóa mã giảm giá', 'Bạn có muốn xóa mã giảm giá: ' + item.Name + ' không ?', function () {
        //        console.log(item.ID);
        //        var data = {
        //            params: {
        //                ID: item.ID
        //            }
        //        }
        //        apiService.del('api/material/delete', data,
        //            function (result) {
        //                notificationService.displaySuccess('Đã xóa mã giảm giá ' + item.Name + ' !');
        //                getPromotion();
        //            }, function (error) {
        //                notificationService.displayError('Đã xảy ra lỗi !');
        //                console.log(error);
        //            });

        //    }, function () {
        //        //notificationService.displayWarning('Thông tin chưa được lưu!');
        //    });

        //}

    }
})(angular.module('SmartOrder.services'));