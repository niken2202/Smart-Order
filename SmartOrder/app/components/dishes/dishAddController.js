(function (app) {
    app.controller('dishAddController', dishAddController);

    dishAddController.$inject = ['$scope', '$state', 'ngDialog', 'apiService','notificationService'];

    function dishAddController($scope, $state, ngDialog, apiService, notificationService) {

        //dish binding in dialog add dish
        $scope.dish = {
            CreatedDate: new Date,
            Status: 1
        }

        //get list dish category to the select box
        $scope.DishCategory = [];

        function getDishCategory() {
            apiService.get('/api/dishcategory/getall', null, function (result) {
                $scope.DishCategory = result.data;
            }, function () {
                console.log('Can get lish dish category');
            });
        }
        getDishCategory();

        //create new dish 
        $scope.CreateDish = CreateDish;

        function CreateDish() {
            apiService.post('api/dish/add', $scope.dish,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    var obj = { a: $scope.dish.Status, b: $scope.dish.CreatedDate };
                    console.log(obj);
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                    var obj = { a: $scope.dish.Status, b: $scope.dish.CreatedDate };
                    console.log(obj);
                });
        }
    
    }
})(angular.module('SmartOrder.dishes'));