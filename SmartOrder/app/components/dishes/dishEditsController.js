(function (app) {
    app.controller('dishEditsController', dishEditsController);

    dishEditsController.$inject = ['$http','$scope', '$state', 'apiService', 'notificationService', '$stateParams'];
    app.directive('ngFiles', ['$parse', function ($parse) {
        function fn_link(scope, element, attrs) {

            var onChange = $parse(attrs.ngFiles);
            element.on('change', function (event) {
                onChange(scope, { $files: event.target.files });
                // getTheFiles(event.target.files);
            });
        };

        return {
            link: fn_link
        }
    }])

    function dishEditsController($http, $scope, $state, apiService, notificationService, $stateParams) {

        //get list dish category to the select box
        $scope.DishCategory = [];

        function getDishCategory() {
            apiService.get('/api/dishcategory/getall', null, function (result) {
                $scope.DishCategory = result.data;
                if ($scope.DishCategory.length === 0) {
                    $scope.closeDialog();
                    notificationService.displayWarning('Vui lòng thêm nhóm danh mục !');
                }
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        getDishCategory();

        //get list dish from api
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

        var a = $scope.dish;
        $scope.edtDish = {
            ID: a.ID, Name: a.Name, Price: a.Price, Amount: a.Amount, Description: a.Description,
            Image: a.Image, OrderCount: a.OrderCount, CategoryID: a.CategoryID, Status: a.Status, CreatedDate: a.CreatedDate
        };

        $scope.slectedChange = function (evt) {
            console.log('event' + evt);
        }


        //Upload file
        $scope.uploadFile = function (files) {
            var fd = new FormData();
            fd.append('Image', files[0]);
            $http.post('/api/image/upload', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .then(function (result) {
                    $scope.edtDish.Image = result.data;
                })
                .then(function () {
                });
        }

        //update dish
        $scope.updateDish = updateDish;
        function updateDish() {
            var condition = new Boolean(true);
            var checkName = $scope.edtDish.Name;
            var curName;
            checkName = checkName.trim().toLowerCase();
            for (i = 0; i < $scope.dishes.length; i++) {
                curName = $scope.dishes[i].Name;
                curName = curName.trim().toLowerCase();
                if ($scope.edtDish.ID != $scope.dishes[i].ID && checkName == curName)
                    condition = false;
            }
            if (condition == true) {
                apiService.put('api/dish/update', $scope.edtDish,
                    function (result) {
                        notificationService.displaySuccess($scope.edtDish.Name + ' đã được cập nhật mới! ');
                        $scope.reload();
                    }, function (error) {
                        notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                    });
            } else {
                notificationService.displayError('Món đã tồn tại');
            }            
        }
    }
})(angular.module('SmartOrder.dishes'));
