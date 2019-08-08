(function (app) {
    app.controller('cashierHomeController', cashierHomeController);

    cashierHomeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService','$interval'];

    function cashierHomeController($scope, ngDialog, apiService, notificationService, $interval) {

        $scope.checkCart; 
        //get table list from api
        function getListTable() {
            apiService.get('/api/table/getall', null, function (result) {
                $scope.tables = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã bàn không thành công');
            });
        }
        getListTable();

        //get dishes list from api
        function getDish() {
            apiService.get('/api/dish/getall', null, function (result) {
                $scope.dishes = result.data.listDish;                
                if ($scope.dishes.length === 0) {
                    //notificationService.displayWarning('Danh sách trống !');
                }
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        getDish();

        //show billdetail 
        $scope.showBill = showBill;
        function showBill(item) {
            $scope.curTableID = item.ID;
            var transfer = {
                params: {
                    billId: $scope.curTableID,
                }
            }
            apiService.get('/api/cart/getbytable', transfer, function (result) {
                $scope.cartDetails = result.data;
                if (result.data.length == 0) {
                    $scope.checkCart = false;//it mean cart is not created
                };
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
            });

        };

        //add dish to cart
        $scope.addDish = addDish;
        function addDish(item) {
            //create new cart for table
            if ($scope.checkCart == false) {

                $scope.checkCart == true;
            };
            if ($scope.checkCart == true) {
            var transfer = {
                params: {
                    billId: $scope.curTableID,
                }
            }
            apiService.get('/api/cart/getbytable', transfer, function (result) {
                $scope.cartDetails = result.data;
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
                    });
            }
        };

        //auto update current cart
        function autoUpdateCart() {
            var date = new Date();
            console.log("Timeout occurred" + date);
        }

        //set schedule to auto call update view
        $interval(rellTime, 3000);

        function rellTime() {
            getListTable();
            autoUpdateCart();
        }

    }
})(angular.module('SmartOrder.cashier'));