(function (app) {
    app.controller('homeController', homeController);

    homeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function homeController($scope, ngDialog, apiService, notificationService) {

        $scope.title;
        //user option from slect box
        $scope.userOption;

        var optionArr = new Array();
        function tmp(key, val) {
            this.key = key;
            this.val = val;
        }
        var op = new tmp(1, '7 ngày gần đây');
        optionArr.push(op);
        op = new tmp(2, '30 ngày gần đây');
        optionArr.push(op);
        op = new tmp(3, '12 tháng gần đây');
        optionArr.push(op);
        $scope.options = optionArr;

        //get all history
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

        //get default data in recently 7 days
        function getRevenue7day() {

            var fdate = new Date();
            var tDate = new Date(fdate);

            tDate.setDate(tDate.getDate() - 7);

            console.log(fdate);
            console.log(tDate);

            var config = {
                params: {
                    fromDate: fdate,
                    toDate: tDate
                }
            }

            apiService.get('/api/bill/getrevenue', config, function (result) {
                $scope.bills = result.data;
                $scope.title = "7 ngày gần đây";
                console.log($scope.bills);
                if ($scope.bills.length === 0) {
                    notificationService.displayWarning('Danh sách trống !');
                } else {
                    $scope.countBill = $scope.bills.length;
                }
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });

        };

        //catch event user select box change
        $scope.selectEvent = function () {
            if ($scope.userOption === 1) {
                getRevenue7day();
            } else if ($scope.userOption === 2) {

                apiService.get('/api/bill/getlast7day', null, function (result) {
                    $scope.bills = result.data;
                    $scope.title = "7 ngày gần đây";
                    if ($scope.bills.length === 0) {
                        notificationService.displayWarning('Danh sách trống !');
                    } else {
                        notificationService.displaySuccess('Tổng cộng có: ' + $scope.bills.length + ' hóa đơn');
                        $scope.countBill = $scope.bills.length;
                    }
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                });

            } else if ($scope.userOption === 3) {
                apiService.get('/api/bill/getlastmonth', null, function (result) {
                    $scope.bills = result.data;
                    $scope.title = "Tháng gần đây";
                    if ($scope.bills.length === 0) {
                        notificationService.displayWarning('Danh sách trống !');
                    } else {
                        notificationService.displaySuccess('Tổng cộng có: ' + $scope.bills.length + ' hóa đơn');
                        $scope.countBill = $scope.bills.length;
                    }
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                    $scope.countBill = $scope.bills.length;
                });
            }
        };


        $scope.fruits = {
            labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
            data: [950000, 1400000, 1200000, 1200000, 1600000, 1400000, 1300000, 1300000, 1200000, 1200000, 1100000, 1200000],
            color: ["#4f8bcc"]
        };
        var ctx = document.getElementById("dvCanvas").getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                datasets: [{
                    data: $scope.fruits.data,
                    backgroundColor: $scope.fruits.color,
                    label: "Doanh thu",
                }],
                labels: $scope.fruits.labels,
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Doanh thu trong 12 tháng',
                    position: 'bottom',
                },
                showDatasetLabels: false
            }
        });


    };
})(angular.module('SmartOrder'));