(function (app) {
    app.controller('homeController', homeController);

    homeController.$inject = $inject = ['$scope', 'apiService'];

    function homeController($scope, apiService) {

        function getHistory() {

            var config = {
                params: {

                }
            }
            apiService.get('/api/history/getall', config, function (result) {      
                $scope.history = result.data;
                //console.log(result.data);
            }, function () {
                console.log('Load history failed.');
            });
        }
        getHistory();
    };
})(angular.module('SmartOrder'));