(function (app) {
    app.controller('cashierHomeController', cashierHomeController);

    cashierHomeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService', '$interval'];

    function cashierHomeController($scope, ngDialog, apiService, notificationService, $interval) {
        $scope.totalPrice = 0;
        $scope.checkCart = false;
        $scope.curTable = null;
        $scope.tables;
        $scope.curCart = null;
        $scope.changeTable;
        $scope.listDish = [];
        $scope.CustomerName = " ";
        $scope.CrashierName = " ";
        $scope.ContentBill = " ";
        $scope.Promotions = { Code: "Không có", Discount: 0};
        //get table list from api
        function getListTable() {
            apiService.get('/api/table/getall', null, function (result) {
                $scope.tables = result.data;
                getListTableAvaiable();
            }, function () {
                notificationService.displayError('Tải danh sách mã bàn không thành công');
            });
        }
        getListTable();

        //get list table avaiable
        function getListTableAvaiable() {
            $scope.lsAvaiableTables = [];
            for (i = 0; i < $scope.tables.length; i++) {
                if ($scope.tables[i].Status === 1) {
                    $scope.lsAvaiableTables.push($scope.tables[i]);
                }
            }
        }

        //generate index number in table
        $scope.serial = 1;
        $scope.indexCount = function (newPageNumber) {
            $scope.serial = newPageNumber * 10 - 9;
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

        //get promotion list from api
        function getPromotion() {
            apiService.get('/api/promotioncode/getall', null, function (result) {
                $scope.promotions = result.data;
            }, function () {
                notificationService.displayError('Tải danh sách mã khuyến mại không thành công');
            });
        }
        getPromotion();

        //show cart by select table
        $scope.showBill = showBill;
        function showBill(item) {
            getCartDetails(item);
        };

        //function get cart details by table id
        function getCartDetails(item) {
            $scope.listDish = [];
            $scope.curTable = item;
            var transfer = {
                params: {
                    tableId: $scope.curTable.ID,
                }
            }
            apiService.get('/api/cart/getbytable', transfer, function (result) {
                if (result.data == null) {
                    $scope.checkCart = false;//it mean cart is not created
                    $scope.curCart = null;
                } else {
                    $scope.curCart = {
                        CartDetails: result.data.CartDetails,
                        ID: result.data.ID,
                        TableID: result.data.TableID,
                        CartPrice: result.data.CartPrice
                    }
                    $scope.checkCart = true;
                    countTotalPrice();
                };
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
            });
        }

        //count total cart price
        function countTotalPrice() {
            var total = 0;
            if ($scope.curCart != null && $scope.curCart.CartDetails.length > 0) {
                for (var i = 0; i < $scope.curCart.CartDetails.length; i++) {
                    var product = $scope.curCart.CartDetails[i];
                    total += (product.Price * product.Quantity);
                    $scope.curCart.CartDetails.order = i;
                }
            }
            $scope.totalPrice = total;
        }

        //add dish to cart
        $scope.addDish = addDish;
        function addDish(item) {
            //create new cart for table
            var condition = new Boolean(true);
            if ($scope.curCart == null) {
                if ($scope.curTable == null) {
                    for (i = 0; i < $scope.tables.length; i++) {
                        if ($scope.tables[i].Status == 1) {
                            $scope.curTable = $scope.tables[i];
                            break;
                        }
                    }
                }
                $scope.dishCart = {
                    Name: item.Name,
                    Price: item.Price,
                    Quantity: 1,
                    Image: item.Image,
                    Status: 1,
                    ProID: item.ID,
                    Note: null,
                    Type: 1
                };
                $scope.listDish.push($scope.dishCart);
                $scope.curCart = {
                    CartDetails: $scope.listDish,
                    TableID: $scope.curTable.ID,
                    CartPrice: 256
                }
            } else {
                //add dish by update quantity of dish in cart details
                for (i = 0; i < $scope.curCart.CartDetails.length; i++) {
                    if ($scope.curCart.CartDetails[i].Type == 1 && item.ID == $scope.curCart.CartDetails[i].ProID) {
                        $scope.curCart.CartDetails[i].Quantity += 1;
                        condition = false;
                        break;
                    }
                }
                //add new dish to cart detail
                if (condition == true) {
                    $scope.dishCart = {
                        Name: item.Name,
                        Price: item.Price,
                        Quantity: 1,
                        Image: item.Image,
                        Status: 1,
                        ProID: item.ID,
                        Note: null,
                        Type: 1
                    };
                    $scope.curCart.CartDetails.push($scope.dishCart);
                    countTotalPrice();
                }
            }
        };

        //order send list cart detail to kitchen
        $scope.Order = Order;
        function Order() {
            if ($scope.curCart == null) {
                notificationService.displayWarning("Vui lòng chọn bàn và món");
            } else {
                //create new cart
                if ($scope.checkCart == false) {
                    apiService.post('/api/cart/add', $scope.curCart, function (result) {
                        notificationService.displaySuccess('Ok !' + result.data.ID);
                        $scope.curCart.ID = result.data.ID;
                        changeTableStatusOff($scope.curTable);
                    }, function () {
                        notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                    });
                }
            }
        }

        //payment method
        $scope.Payment = Payment;
        function Payment() {
            if ($scope.curCart == null) {
                notificationService.displayWarning("Không có bàn thanh toán");
            } else {
                $scope.listDish = [];
                for (i = 0; i < $scope.curCart.CartDetails.length; i++) {
                    var dish = {
                        Name: $scope.curCart.CartDetails[i].Name,
                        Price: $scope.curCart.CartDetails[i].Price,
                        Amount: $scope.curCart.CartDetails[i].Quantity,
                        Description: "Test demo",
                        Image: $scope.curCart.CartDetails[i].Image
                    };
                    $scope.listDish.push(dish);
                }
                $scope.addBill = {
                    BillDetail: $scope.listDish,
                    Voucher: $scope.Promotions.Name,
                    CustomerName: $scope.CustomerName,
                    Content: $scope.ContentBill,
                    TableID: $scope.curTable.ID,
                    CreatedDate: new Date,
                    CreatedBy: $scope.CrashierName,
                    Discount: $scope.Promotions.Discount,
                    Status: true
                }
                apiService.post('/api/bill/add', $scope.addBill, function (result) {
                    deleteCart($scope.curCart.ID);
                    changeTableStatusOn($scope.curTable);
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                    console.log('add bill falied');
                });
            }
        }

        //delete cart after add bill
        function deleteCart(cartID) {
            var data = {
                params: {
                    ID: cartID
                }
            }
            apiService.del('/api/cart/delete', data, function (result) {
                changeTableStatusOn($scope.curTable);
                $scope.curCart = null;
                countTotalPrice();
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                console.log('detele cart falied');
            });
        };

        //change cart to other table which is avaiable when customer require
        $scope.changeTableEvt = changeTableEvt;
        function changeTableEvt() {
            if ($scope.curCart == null) {
                notificationService.displayWarning("Hóa đơn trống");
            } else if ($scope.curTable == null) {
                notificationService.displayWarning("Vui lòng chọn bàn cần chuyển");
            } else if ($scope.changeTable == null) {
                notificationService.displayWarning("Vui lòng chọn bàn chuyển đến");
            } else {
                $scope.curCart.TableID = $scope.changeTable;
                apiService.put('/api/cart/update', $scope.curCart, function (result) {
                    changeTableStatusOn($scope.curTable);
                    for (i = 0; i < $scope.tables.length; i++) {
                        if ($scope.tables[i].ID === $scope.changeTable) {
                            $scope.curTable = $scope.tables[i];
                            break;
                        }
                    }
                    changeTableStatusOff($scope.curTable);
                }, function () {
                    notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                });
            }
        }

        //catch event when user change quality of dish
        $scope.quantityChange = quantityChange;
        function quantityChange(item) {
            var num = item.Quantity;
            var bol = true;
            if (num == null) {
                item.Quantity = 1;
            }
            if (num < 1 == true || num > 25 == true) {
                item.Quantity = 1;
                bol = false;
            } else {
                bol = true;
            }
            if (bol === false) {
                notificationService.displayError('Dữ liệu nhập vào không hợp lệ');
            }
            countTotalPrice();
        };

        //auto update current cart
        function autoUpdateCart() {
            var date = new Date();
            console.log("Timeout occurred" + date);
        }

        //changer table status is not avaiable
        function changeTableStatusOff(item) {
            item.Status = 0;
            apiService.put('api/table/update', item, function (result) {
                //console.log('update table succes')
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
            });
        }

        //changer table status avaiable
        function changeTableStatusOn(item) {
            item.Status = 1;
            apiService.put('api/table/update', item, function (result) {
                //console.log('update table succes')
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
            });
        }

        //set schedule to auto call update view
        $interval(rellTime, 15000);
        function rellTime() {
            getListTable();            
            autoUpdateCart();            
        }
        $interval(longRellTime, 120000);
        function longRellTime() {
            var b = new Date;
            console.log('long' +b)
            getPromotion();
            getDish();
        }
    }
})(angular.module('SmartOrder.cashier'));