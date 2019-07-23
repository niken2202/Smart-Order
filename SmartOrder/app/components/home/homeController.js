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

        $scope.fruits = {
            labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9"],
            data: [950000, 1400000, 1200000, 1200000, 1600000, 1400000, 1300000, 1300000, 1200000,],
            color: ["#FEBD01", "#FF8C00", "#FFCBA6", "FFCBA6", "FFCBA6", "#FFCBA6", "#FEBD01", "#FF8C00", "#FFCBA6"]
        };
        var ctx = document.getElementById("dvCanvas").getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                datasets: [{
                    data: $scope.fruits.data,
                    backgroundColor: $scope.fruits.color
                }],
                labels: $scope.fruits.labels
            },
            options: {
                responsive: true
            }
        });


    };
})(angular.module('SmartOrder'));