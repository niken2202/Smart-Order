(function (app) {
    app.controller('listDishController', listDishController);

    listDishController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function listDishController($scope, ngDialog, apiService, notificationService) {
        //get data from api
        function getDish(index, pageSize, totalRow) {
            index = index || 0;
            pageSize = pageSize || 300;
            totalRow = totalRow || 0;

            var config = {
                params: {
                    index: index,
                    pageSize: pageSize,
                    totalRow: totalRow,
                }
            }
            apiService.get('/api/dish/getall', config, function (result) {
                $scope.dishes = result.data.listDish;
                if ($scope.dishes.length === 0) {
                    notificationService.displayWarning('Danh sách chưa có món nào !');
                }
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        getDish();

        //reload table
        $scope.reload = function (data) {
            getDish();
            ngDialog.close(vm.dialogId);
        };

        //close dialog when have problem (can not get list category)
        $scope.closeDialog = function (data) {
            getDish();
            ngDialog.close(vm.dialogId);
        };

        //show the dialog ditail dish by table
        var vm = this;
        vm.openDialog = function ($event, item) {
            //var dish = item
            vm.init = function () {
                $scope.dish = item;
            }

            vm.init();

            // Show the dialog in the main controller
            var dialog = ngDialog.openConfirm({
                template: '/app/components/dishes/dishEditsView.html',
                scope: $scope,
                controller: 'dishEditsController',
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

        //show dialog to create new dish
        $scope.dishAdd = function () {
            var addDialog = ngDialog.openConfirm({
                template: '/app/components/dishes/dishAddView.html',
                scope: $scope,
                controller: 'dishAddController',
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

        //open popup to determine before delete dish
        $scope.dishDel = function (item) {
            alertify.confirm('Xóa món', 'Bạn có muốn xóa món: ' + item.Name + ' ?', function () {
                console.log(item.ID);
                var data = {
                    params: {
                        ID: item.ID
                    }
                }
                apiService.del('api/dish/delete', data,
                    function (result) {
                        notificationService.displaySuccess('Xóa món thành công');
                    }, function (error) {
                        notificationService.displayError('Fuck off');
                        console.log(error);
                    });
                
            }, function () {
                notificationService.displayError('Xóa món thành công');
            });
 
        }
    }
})(angular.module('SmartOrder.dishes'));