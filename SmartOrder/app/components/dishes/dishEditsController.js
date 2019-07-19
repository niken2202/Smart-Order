(function (app) {
    app.controller('dishEditsController', dishEditsController);

    dishEditsController.$inject = ['$scope', '$state', 'ngDialog', 'apiService', 'notificationService'];

    function dishEditsController($scope, $state, ngDialog, apiService, notificationService) {
        
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

    }
})(angular.module('SmartOrder.dishes'));