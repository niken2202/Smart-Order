(function (app) {
    app.controller('listMaterialController', listMaterialController);

    listMaterialController.$inject = ['$scope', '$http'];

    function listMaterialController($scope,$http) {
        $http.get('https://localhost:44366/api/material/getall?pageIndex=1&pageSize=10&totalRow=0').then(function (response) {
            $scope.materials = response.data;
        });
    }
})(angular.module('SmartOrder.materials'));