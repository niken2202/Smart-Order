(function (app) {
    app.controller('listBillController', listBillController);

    listBillController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function listBillController($scope, ngDialog, apiService, notificationService) {

        $scope.countBill = 0;
        $scope.title;
        //user option from slect box
        $scope.userOption;
        $scope.currentBill;

        //generate index number in table
        $scope.serial = 1;
        $scope.itemPerPage = 20
        $scope.indexCount = function (newPageNumber) {

            $scope.serial = newPageNumber * $scope.itemPerPage - ($scope.itemPerPage - 1);
        }
        //generate index number in table bill details
        $scope.serialBillDetails = 1;
        $scope.indexCountBillDetails = function (newPageNumber) {
            $scope.serialBillDetails = newPageNumber * 10 - 9;
        }  

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

        //get default list bills today
        function getListToday() {

            apiService.get('/api/bill/gettoday', null, function (result) {
                $scope.bills = result.data;
                $scope.title = "Hôm nay";
                    $scope.countBill = $scope.bills.length;                
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });

        };
        
        //catch event user select box change
        $scope.selectEvent = function () {
            if ($scope.userOption === 1) {

                apiService.get('/api/bill/gettoday', null, function (result) {
                    $scope.bills = result.data;
                    $scope.title = "Hôm nay";
                    
                    if ($scope.bills.length === 0) {
                        notificationService.displayWarning('Danh sách trống !');
                    } else {
                        $scope.countBill = $scope.bills.length;
                    }
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
                });

            } else if ($scope.userOption === 2) {

                apiService.get('/api/bill/getlast7day', null, function (result) {
                    $scope.bills = result.data;
                    $scope.title = "7 ngày gần đây";
                    console.log($scope.bills);
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

        //catch event user select customize from date to date
        $scope.cusDateEvent = function () {

            var transfer = {
                params: {
                    fromDate: $scope.dateSeclect.from,
                    toDate: $scope.dateSeclect.to,
                }
            }
            
            apiService.get('/api/bill/gettimerange', transfer, function (result) {
                $scope.bills = result.data;
                $scope.title = "";
                if ($scope.bills.length === 0) {
                    notificationService.displayWarning('Danh sách trống !');
                } else {
                    notificationService.displaySuccess('Tổng cộng có: ' + $scope.bills.length + ' hóa đơn');
                    $scope.countBill = $scope.bills.length;
                }
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });

        }


        //show bill details
        $scope.billDetailEvent = function (data) {
            $scope.currentBill = data;
            var transfer = {
                params: {
                    billId: data.ID,
                }
            }

            apiService.get('/api/billdetail/getbybillid', transfer, function (result) {
                $scope.billDetails = result.data;
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
            });

            var detailDialog = ngDialog.openConfirm({
                template: '/app/components/restaurant/billDetailView.html',
                scope: $scope,
                className: 'ngdialog',
                showClose: false,
            }).then(
                function (value) {
                    //save the contact form
                },
                function (value) {
                    //Cancel or do nothing
                }
            );
        };
        

    }
})(angular.module('SmartOrder.restaurant'));