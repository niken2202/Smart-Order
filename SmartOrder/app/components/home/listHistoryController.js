﻿(function (app) {
    app.controller('listHistoryController', ['$scope', 'apiService', 'notificationService',
        function ($scope, apiService, notificationService) {



            $scope.count = 0;
            $scope.title = "";
            //user option from slect box
            $scope.userOption;

            //for user customize date from to .. 
            $scope.dateSeclect = {
                from: new Date(),
                to: new Date
            };
            getListToday();
            //create select option
            var optionArr = new Array();
            function tmp(key, val) {
                this.key = key;
                this.val = val;
            }
            var op = new tmp(1, 'Hôm nay');
            optionArr.push(op);
            op = new tmp(2, '7 ngày gần đây');
            optionArr.push(op);
            op = new tmp(3, '30 ngày gần đây');
            optionArr.push(op);
            $scope.options = optionArr;

            //function sort by title tr tag
            $scope.sort = function (keyname) {
                $scope.sortKey = keyname;   //set the sortKey to the param passed
                $scope.reverse = !$scope.reverse; //if true make it false and vice versa
            };

            //get default list history today
            function getListToday() {

                apiService.get('/api/history/getall', null, function (result) {
                    $scope.histories = result.data;
                    $scope.title = "Hôm nay";
                    $scope.count = $scope.histories.length;
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                });

            };

            //catch event user select box change
            $scope.selectEvent = function () {
                if ($scope.userOption === 1) {

                    apiService.get('/api/history/getall', null, function (result) {
                        $scope.histories = result.data;
                        $scope.title = "Hôm nay";

                        if ($scope.histories.length === 0) {
                            notificationService.displayWarning('Danh sách trống !');
                        } else {
                            $scope.countBill = $scope.histories.length;
                        }
                    }, function () {
                        notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                    });

                } else if ($scope.userOption === 2) {

                    apiService.get('/api/history/getall', null, function (result) {
                        $scope.histories = result.data;
                        $scope.title = "7 ngày gần đây";
                        console.log($scope.histories);
                        if ($scope.histories.length === 0) {
                            notificationService.displayWarning('Danh sách trống !');
                        } else {
                            notificationService.displaySuccess('Tổng cộng có: ' + $scope.histories.length + ' hóa đơn');
                            $scope.countBill = $scope.histories.length;
                        }
                    }, function () {
                        notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                    });

                } else if ($scope.userOption === 3) {
                    apiService.get('/api/history/getall', null, function (result) {
                        $scope.histories = result.data;
                        $scope.title = "Tháng gần đây";
                        if ($scope.histories.length === 0) {
                            notificationService.displayWarning('Danh sách trống !');
                        } else {
                            notificationService.displaySuccess('Tổng cộng có: ' + $scope.histories.length + ' hóa đơn');
                            $scope.countBill = $scope.histories.length;
                        }
                    }, function () {
                        notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                        $scope.countBill = $scope.histories.length;
                    });
                }
            };

            //catch event user select customize from date to date
            $scope.cusDateEvent = function () {

                var transfer = {
                    params: {
                        fromDate: $scope.dateSeclect.from,
                        toDate: $scope.dateSeclect.to,
                    }
                }

                apiService.get('/api/bill/gettimerange', transfer, function (result) {
                    $scope.histories = result.data;
                    $scope.title = "";
                    if ($scope.histories.length === 0) {
                        notificationService.displayWarning('Danh sách trống !');
                    } else {
                        notificationService.displaySuccess('Tổng cộng có: ' + $scope.histories.length + ' hóa đơn');
                        $scope.countBill = $scope.histories.length;
                    }
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                });

            }

        //show bill details
        //$scope.billDetailEvent = function (data) {
        //    $scope.currentBill = data;
        //    var transfer = {
        //        params: {
        //            billId: data.ID,
        //        }
        //    }

        //    apiService.get('/api/billdetail/getbybillid', transfer, function (result) {
        //        $scope.billDetails = result.data;
        //        console.log($scope.billDetails);
        //    }, function () {
        //        notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
        //    });

        //    var detailDialog = ngDialog.openConfirm({
        //        template: '/app/components/restaurant/billDetailView.html',
        //        scope: $scope,
        //        className: 'ngdialog',
        //        showClose: false,
        //    }).then(
        //        function (value) {
        //            //save the contact form
        //        },
        //        function (value) {
        //            //Cancel or do nothing
        //        }
        //    );
        //};

        }]);
})(angular.module('SmartOrder'));