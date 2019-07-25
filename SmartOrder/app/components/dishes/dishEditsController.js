(function (app) {
    app.controller('dishEditsController', dishEditsController);

    dishEditsController.$inject = ['$scope', '$state', 'ngDialog', 'apiService', 'notificationService', '$stateParams'];

    function dishEditsController($scope, $state, ngDialog, apiService, notificationService, $stateParams) {
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

        var a = $scope.dish;
        $scope.edtDish = {
            ID: a.ID, Name: a.Name, Price: a.Price, Amount: a.Amount, Description: a.Description,
            Image: a.Image, OrderCount: a.OrderCount, CategoryID: a.CategoryID, Status: a.Status, CreatedDate: a.CreatedDate
        };

        $scope.slectedChange = function (evt) {
            console.log('event' + evt);
        }

        //update dish
        $scope.updateDish = updateDish;
        function updateDish() {
            console.log($scope.edtDish)
            apiService.put('api/dish/update', $scope.edtDish,
                function (result) {
                    notificationService.displaySuccess($scope.edtDish.Name + ' đã được cập nhật mới! ');
                    //console.log('update success');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });
        }
    }
})(angular.module('SmartOrder.dishes'));