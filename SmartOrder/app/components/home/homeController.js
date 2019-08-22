(function (app) {
    app.controller('homeController', homeController);

    homeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function homeController($scope, ngDialog, apiService, notificationService) {
        $scope.title;
        //user option from slect box
        $scope.userOption;
        $scope.label = [];
        $scope.data = [];

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
            //var config = {
            //    params: {
            //    }
            //}
            apiService.get('/api/history/getlast7day', null, function (result) {
                $scope.history = result.data;
            }, function () {
                //console.log('Load history failed.');
            });
        }
        getHistory();

        //generate index number in table
        $scope.serial = 1;
        $scope.indexCount = function (newPageNumber) {
            $scope.serial = newPageNumber * 10 - 9;
        }

        //get top hot dish
        function getTopHotDish() {
            //var config = {
            //    params: {
            //    }
            //}
            apiService.get('/api/dish/gettophot', null, function (result) {
                if (result.data == null) {
                   //data is empty it mean is not have any top hot dish
                } else {
                    $scope.hotDishes = result.data.tophot;
                }
            }, function () {
                console.log('Load history failed.');
            });
        }
        getTopHotDish();

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

            apiService.get('/api/bill/getrevenuebymonth', config, function (result) {
                $scope.bills = result.data;
                $scope.title = "7 ngày gần đây";
                console.log($scope.bills);
                if ($scope.bills.length === 0) {
                    //notificationService.displayWarning('Danh sách trống !');
                } else {
                }
            }, function () {
                //notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        };

        //catch event user select box change
        $scope.selectEvent = function () {
            if ($scope.userOption === 1) {
                getRevenue7day();
            } else if ($scope.userOption === 2) {
                fdate = new Date();
                tDate = new Date(fdate);

                tDate.setDate(tDate.getDate() - 30);

                console.log(fdate);
                console.log(tDate);

                var config = {
                    params: {
                        fromDate: fdate,
                        toDate: tDate
                    }
                }
                apiService.get('/api/bill/getrevenuebymonth', config, function (result) {
                    $scope.bills = result.data;
                    $scope.title = "30 ngày gần đây";
                    if ($scope.bills.length === 0) {
                        //notificationService.displayWarning('Danh sách trống !');
                    } else {
                        //notificationService.displaySuccess('Tổng cộng có: ' + $scope.bills.length + ' hóa đơn');
                        //$scope.countBill = $scope.bills.length;
                    }
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                });
            } else if ($scope.userOption === 3) {
                fdate = new Date();
                tDate = new Date(fdate);
                //fdate.setMonth(tDate.getMonth() - 12);

                var fromDate = fdate.getFullYear() + "-" + 01 + "-" + 01;
                var toDate = tDate.getFullYear() + "-" + (tDate.getMonth() + 1) + "-" + tDate.getDate() ;
                var url = "/api/bill/getrevenuebymonth?fromDate=" + fromDate + "&toDate=" + toDate; 
                console.log(fromDate);
                console.log(toDate);
                console.log(url);

                apiService.get(url, null, function (result) {

                    console.log(result.data.length);
                    $scope.title = "12 Tháng gần đây";
                    for (i = 0; i < result.data.length; i++) {
                        $scope.label.push(result.data[i].Month);
                        $scope.data.push(result.data[i].Revenue);
                    }                   
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                });
            }
        };

        $scope.revenues = {
            labels: $scope.label,
            data: $scope.data,
            color: ["#4f8bcc"]
        };

        var ctx = document.getElementById("dvCanvas").getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                datasets: [{
                    data: $scope.revenues.data,
                    backgroundColor: $scope.revenues.color,
                    label: "Doanh thu",
                }],
                labels: $scope.revenues.labels,
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