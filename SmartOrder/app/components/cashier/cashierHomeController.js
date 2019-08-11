(function (app) {
    app.controller('cashierHomeController', cashierHomeController);

    cashierHomeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService', '$interval'];

    function cashierHomeController($scope, ngDialog, apiService, notificationService, $interval) {
        $scope.totalPrice = 0;
        $scope.checkCart = false;
        $scope.curTableID = 0;
        $scope.curTableName = "";
        $scope.curCart = {
            TableID: $scope.curTableID,
            CartPrice : 256
        }
        //get table list from api
        function getListTable() {
            apiService.get('/api/table/getall', null, function (result) {
                $scope.tables = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã bàn không thành công');
            });
        }
        getListTable();

        //generate index number in table
        $scope.serial = 1;
        $scope.itemPerPage = 20
        $scope.indexCount = function (newPageNumber) {
            $scope.serial = newPageNumber * $scope.itemPerPage - ($scope.itemPerPage - 1);
        }

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

        //show cart by select table
        $scope.showBill = showBill;
        function showBill(item) {
            $scope.curTableID = item.ID;
            $scope.curTableName = item.Name;
            var transfer = {
                params: {
                    tableId: $scope.curTableID,
                }
            }
            apiService.get('/api/cart/getbytable', transfer, function (result) {
                if (result.data == null) {
                    $scope.checkCart = false;//it mean cart is not created
                } else {
                    $scope.cartDetails = result.data.CartDetails;
                    var total = 0;
                    for (var i = 0; i < $scope.cartDetails.length; i++) {
                        var product = $scope.cartDetails[i];
                        total += (product.Price * product.Quantity);
                        $scope.cartDetails.order = i;
                    }
                    $scope.totalPrice = total;
                };
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
            });
        };

        //add dish to cart
        $scope.addDish = addDish;
        function addDish(item) {
            //create new cart for table           
                if ($scope.curTableID === 0) {
                    //create new cart and add the dish which has been choose by user
                    if ($scope.tables.lenght > 0) {

                        for (i = 0; i < $scope.tables.length; i++) {
                            if ($scope.tables[i].Status == 1) {
                                
                                $scope.curCart = {
                                    TableID: $scope.tables[i].ID,
                                }
                                apiService.post('/api/cart/add', $scope.curCart, function (result) {
                                    console.log('add cart success');
                                    //apiService.post('/api/cartdetail/update', $scope.curCartDetails, function (result) {

                                    //}, function () {
                                    //    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
                                    //});
                                                                                                                 
                                }, function () {
                                    notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
                                });

                                break;
                            }
                        }

                    }                
                                        
                } else {
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
            };

            //for (i = 0; i < $scope.cartDetails.length; i++) {
            //    if (item.ID === $scope.cartDetails) {
            //    }
            //};
        };

        //catch event when user change quality of dish
        $scope.quantityChange = quantityChange;
        function quantityChange(item) {
            var num = item.Quantity;
            var bol = true;
            if (num < 1 == true || num > 25 == true) {
                item.Quantity = 1;
                bol = false;
            } else {
                bol = true;
            }
            if (bol === false) {
                notificationService.displayError('Dữ liệu nhập vào không hợp lệ');
            }
        };

        //auto update current cart
        function autoUpdateCart() {
            var date = new Date();
            console.log("Timeout occurred" + date);
        }

        //set schedule to auto call update view
        $interval(rellTime, 10000);

        function rellTime() {
            getListTable();
            autoUpdateCart();
        }
    }
})(angular.module('SmartOrder.cashier'));