(function (app) {
    app.controller('homeController', homeController);

    homeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function homeController($scope, ngDialog, apiService, notificationService) {
        $scope.title;
        //user option from slect box
        $scope.userOption;
        $scope.label = [];
        $scope.data = [];
        $scope.libColor = ["#3e95cd", "#fd7e14", "#6c757d", "#16aaff", "#6610f2", "#794c8a", "#ffc107", "#e8c3b9", "#2f1161", "#8e5ea2"]
        $scope.colors = [];

        $scope.revenues = {
            labels: $scope.label,
            data: $scope.data,
            color: $scope.colors
        };

        var optionArr = new Array();
        function tmp(key, val) {
            this.key = key;
            this.val = val;
        }
        var op = new tmp(1, '7 ngày gần đây');
        optionArr.push(op);
        op = new tmp(2, '30 ngày gần đây');
        optionArr.push(op);
        op = new tmp(3, 'Trong năm nay');
        optionArr.push(op);
        $scope.options = optionArr;

        //get history display in home page
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
                //Load history failed
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

                    }
                }, function () {
                    //notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
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
                    $scope.title = "Trong năm nay";
                    for (i = 0; i < result.data.length; i++) {
                        var month = 'Tháng ' + result.data[i].Month
                        $scope.label.push(month);
                        $scope.data.push(result.data[i].Revenue);
                    }                   
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                });
            }
        };

        function formatRevenuesData(data) {
            var a = data.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
            return a;
        }

        function getRevenuesInYear() {
            fdate = new Date();
            tDate = new Date(fdate);
            //fdate.setMonth(tDate.getMonth() - 12);

            var fromDate = fdate.getFullYear() + "-" + 01 + "-" + 01;
            var toDate = tDate.getFullYear() + "-" + (tDate.getMonth() + 2) + "-" + tDate.getDate();
            var url = "/api/bill/getrevenuebymonth?fromDate=" + fromDate + "&toDate=" + toDate;

            apiService.get(url, null, function (result) {

                $scope.title = "Trong năm nay";
                var k = 0;
                for (i = 0; i < result.data.length; i++) {
                    var month = 'Tháng ' + result.data[i].Month
                    $scope.label.push(month);
                    $scope.data.push(result.data[i].Revenue);
                    $scope.colors.push($scope.libColor[k]);
                    k++;
                    if (k == 9) {
                        k = 0;
                    }
                }
                drawChart();

            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        getRevenuesInYear();

        function drawChart() {
            
            var ctx = document.getElementById("dvCanvas").getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
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
                        text: 'Doanh thu trong năm nay',
                        position: 'bottom',
                    },
                    showDatasetLabels: false,
                }
            });

        }
      
    };
})(angular.module('SmartOrder'));