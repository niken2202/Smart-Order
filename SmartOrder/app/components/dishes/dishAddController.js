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
 
        //add dish to database
        $scope.CreateDish = CreateDish;
        function CreateDish() {
            apiService.post('api/dish/add', $scope.dish,
                function (result) {
                    notificationService.displaySuccess($scope.dish.Name + ' đã được thêm mới.');
                    
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    
    }
})(angular.module('SmartOrder.dishes'));