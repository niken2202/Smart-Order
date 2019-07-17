(function (app) {
    app.controller('listDishController', listDishController);

    listDishController.$inject = ['$scope', '$http','ngDialog'];

    function listDishController($scope, $http, ngDialog) {

        function getHistory() {
            var config = {
                params: {

                }
            }

            apiService.get('/api/history/getall', config, function (result) {
                $scope.history = result.data;
                console.log(result.data);
            }, function () {
                console.log('Load history failed.');
            });
        }



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
                className: 'ngdialog',
                showClose: false,
            });           
        };

    }
})(angular.module('SmartOrder.dishes'));