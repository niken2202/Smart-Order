(function (app) {
    app.controller('dishAddController', dishAddController);

    dishAddController.$inject = ['$scope','apiService','notificationService'];

    function dishAddController($scope, apiService, notificationService) {

        //dish binding in dialog add dish
        $scope.dishAdd = {
            CreatedDate: new Date,
            Status: 1,
            OrderCount: 0
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
 
        //add dish to database
        $scope.CreateDish = CreateDish;
        function CreateDish() {
            apiService.post('api/dish/add', $scope.dishAdd,
                function (result) {
                    notificationService.displaySuccess($scope.dishAdd.Name + ' đã được thêm mới.');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    
    }
})(angular.module('SmartOrder.dishes'));