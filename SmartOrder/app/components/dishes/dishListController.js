(function (app) {
    app.controller('listDishController', listDishController);

    listDishController.$inject = ['$scope', '$http', 'ngDialog','apiService'];

    function listDishController($scope, $http, ngDialog, apiService) {
     
        function getDish(index, pageSize, totalRow) {
            index = index || 0;
            pageSize = pageSize || 100;
            totalRow = totalRow || 0;

            var config = {
                params: {
                    index: index ,
                    pageSize: pageSize, 
                     totalRow : totalRow,
                }
            }

            apiService.get('/api/dish/getall', config, function (result) {
                $scope.dishes = result.data.listDish;
                console.log(result.data.listDish);
            }, function () {
                console.log('Load dish failed.');
            });
        }

        getDish();




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

        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        }

    }
})(angular.module('SmartOrder.dishes'));