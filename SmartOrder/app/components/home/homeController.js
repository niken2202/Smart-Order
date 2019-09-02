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

        getRevenueByday(1);

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
        function getRevenueByday(ops) {
            var fdate = new Date();
            var tDate = new Date(fdate);
            var drawID = 1;

            if (ops == 1) {
                drawID = 1;
                document.getElementById("dvCanvas").style.display = "block";
                document.getElementById("dvCanvas2").style.display = "none";
                document.getElementById("dvCanvas3").style.display = "none";
                tDate.setDate(tDate.getDate() - 7);
                $scope.title = "7 ngày gần đây";
            } else {
                drawID = 2;
                document.getElementById("dvCanvas").style.display = "none";
                document.getElementById("dvCanvas2").style.display = "block";
                document.getElementById("dvCanvas3").style.display = "none";
                tDate.setDate(tDate.getDate() - 30);
                $scope.title = "30 ngày gần đây";
            }
            
            var config = {
                params: {
                    fromDate: tDate,
                    toDate: fdate
                }
            }
            apiService.get('/api/bill/getrevenue', config, function (result) {
                var k = 0;
                $scope.revenues.labels = [];
                $scope.revenues.data = []
                for (i = 0; i < result.data.length; i++) {
                    var getDate = new Date(result.data[i].Date);
                    var str = getDate.getDate() + '/' + (getDate.getMonth() + 1);
                    if ($scope.revenues.labels[k] == null) {
                        $scope.revenues.labels.push(str);
                        $scope.revenues.data.push(result.data[i].Revenue);
                    } else {
                        if (str == $scope.revenues.labels[k]) {
                            $scope.revenues.data[k] += result.data[i].Revenue;
                        } else {
                            $scope.revenues.labels.push(str);
                            $scope.revenues.data.push(result.data[i].Revenue);
                            k++;
                        }
                    }
                }
                drawChart(drawID);
            }, function () {
                //notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        };

        //catch event user select box change
        $scope.selectEvent = function () {
            if ($scope.userOption == 1) {
                getRevenueByday(1);
            } else if ($scope.userOption == 2) {     
                getRevenueByday(2);                   
            } else if ($scope.userOption === 3) {
                getRevenuesInYear();
            }
        };

        //function formatRevenuesData(data) {
        //    var a = data.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
        //    return a;
        //}

        function getRevenuesInYear() {
            var fMonth = new Date();
            var tMonth = new Date(fMonth);
            //fMonth.setMonth(tMonth.getMonth() - 12);

            document.getElementById("dvCanvas").style.display = "none";
            document.getElementById("dvCanvas2").style.display = "none";
            document.getElementById("dvCanvas3").style.display = "block";

            var fromDate = fMonth.getFullYear() + "-" + 01 + "-" + 01;
            var toDate = tMonth.getFullYear() + "-" + (tMonth.getMonth() + 1) + "-" + 30;
            var url = "/api/bill/getrevenuebymonth?fromDate=" + fromDate + "&toDate=" + toDate;
            $scope.revenues.data = [];
            $scope.revenues.labels = [];
            apiService.get(url, null, function (result) {
                $scope.title = "Trong năm nay";
                var k = 0;
                for (i = 0; i < result.data.length; i++) {
                    var month = 'Tháng ' + result.data[i].Month
                    $scope.revenues.labels.push(month);
                    $scope.revenues.data.push(result.data[i].Revenue);
                    //$scope.colors.push($scope.libColor[k]);
                    //k++;
                    //if (k == 9) {
                    //    k = 0;
                    //}
                }
                drawChart(3);
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        //getRevenuesInYear();

        function drawChart(ops) {
            var canvasID = "";
            if (ops == 1) {
                canvasID = "dvCanvas";
            } else if (ops == 2) {
                canvasID = "dvCanvas2";
            } else {
                canvasID = "dvCanvas3";
            }
            var ctx = document.getElementById(canvasID).getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    datasets: [{
                        data: $scope.revenues.data,
                        backgroundColor: '#3e95cd',
                        label: "Doanh thu",
                    }],
                    labels: $scope.revenues.labels,
                },
                options: {
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                return "₫ " + Number(tooltipItem.yLabel).toFixed(0).replace(/./g, function (c, i, a) {
                                    return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
                                });
                            }
                        }
                    },
                    responsive: true,
                    scales: {
                        yAxes: [{
                            scaleLabel: {
                                display: false,
                                labelString: 'VNĐ'
                            },

                               ticks: {
                                // Include a dollar sign in the ticks
                                callback: function (value, index, values) {
                                    return '' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                }
                            }

                        }]
                    },
                    title: {
                        display: true,
                        text: $scope.title,
                        position: 'bottom',
                    },
                    showDatasetLabels: false,
                }
            });
            myChart.update();
        }
    };
})(angular.module('SmartOrder'));