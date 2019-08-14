(function (app) {
    app.controller('listDishController', listDishController);

    listDishController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function listDishController($scope, ngDialog, apiService, notificationService) {
        //get data from api
        function getDish() {            
            apiService.get('/api/dish/getall', null, function (result) {
                $scope.dishes = result.data.listDish;
                if ($scope.dishes.length === 0) {
                    //notificationService.displayWarning('Danh sách trống !');                    
                }
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        getDish();

        //generate index number in table
        $scope.serial = 1;
        $scope.itemPerPage = 10
        $scope.indexCount = function (newPageNumber) {

            $scope.serial = newPageNumber * $scope.itemPerPage - ($scope.itemPerPage - 1);
        }

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

        
        //open popup to create new category
        $scope.AddCategory = function () {
            alertify.prompt('Thêm mới nhóm sản phẩm', 'Tên nhóm sản phẩm', ''
                , function (evt, value) {
                    var category = {
                        CreatedDate: new Date,
                        Name: value
                    };
                    apiService.post('/api/dishcategory/add', category,
                        function (result) {
                            notificationService.displaySuccess('Nhóm sản phẩm: ' + category.Name + ' đã được thêm mới.');
                            $scope.reload();
                        }, function (error) {
                            notificationService.displayError('Thêm mới không thành công!');
                        });

                }
                , function () {
                    notificationService.displayWarning('Thông tin chưa được thêm !');
                });

        }

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
                var data = {
                    params: {
                        ID: item.ID
                    }
                }
                apiService.del('api/dish/delete', data,
                    function (result) {                        
                        notificationService.displaySuccess('Xóa món thành công');
                        getDish();
                    }, function (error) {
                        notificationService.displayError('Đã xảy ra lỗi !');
                        console.log(error);
                    });
                
            }, function () {
                //notificationService.displayWarning('Thông tin chưa được lưu!');
            });
 
        }
    }
})(angular.module('SmartOrder.dishes'));