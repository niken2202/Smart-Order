(function (app) {
    app.controller('homeController', homeController);

    homeController.$inject = ['$scope', '$http'];

    function homeController($scope,$http) {

        $http.get('https://localhost:44366/api/dish/getall?index=1&pageSize=7&totalRow=0').then(function (response) {
            $scope.history = response.data;
        });

    };
})(angular.module('SmartOrder'));