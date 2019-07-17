(function (app) {
    app.controller('listDishController', listDishController);

    listDishController.$inject = ['$scope', '$http','ngDialog'];

    function listDishController($scope, $http, ngDialog) {
        $http.get('https://localhost:44366/api/dish/getall?index=1&pageSize=10&totalRow=0').then(function (response) {
            $scope.dishes = response.data;
        });

        $scope.getData = function (dish) {
            if (dish != null) {
                $scope.currentItem = dish;
                console.log('Selected person is ' + dish.Name);
            }

        };


        var vm = this;

        vm.openDialog = function ($event,item) {

            var dish = item
            

            vm.init = function () {
                $scope.dish = dish;
            }

            vm.init();

            // Show the dialog in the main controller 
            var dialog = ngDialog.open({
                template: '/app/components/dishes/dishEditsView.html',
                scope: $scope,
                controller: 'dishEditsController',
                controllerAs: "file",
                closeByDocument: false, //can not close dialog by click out of dialog area
                className: 'ngDialog',
                closeByNavigation: true,
                showClose: false,
            });           
        };

    }
})(angular.module('SmartOrder.dishes'));