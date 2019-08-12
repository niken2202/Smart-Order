(function (app) {
    app.controller('cashierHomeController', cashierHomeController);

    cashierHomeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService', '$interval'];

    function cashierHomeController($scope, ngDialog, apiService, notificationService, $interval) {
        $scope.totalPrice = 0;
        $scope.checkCart = false;
        $scope.curTableID = 0;
        $scope.curTableName = "";
        $scope.cartDetails;
        $scope.tables;
        $scope.curCart = null;
        $scope.changeTable;
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
        $scope.lsAvaiableTables = [];
        function getListTableAvaiable() {
            var j = 0;
            for (i = 0; i < $scope.tables.length; i++) {                
                if ($scope.tables[i].Status === 1) {
                    $scope.lsAvaiableTables[j] = $scope.tables[i];
                    j++;
                } 
            }
        }

        //catch event click to change table
        function changeTableEvt() {
            
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

        //show cart by select table
        $scope.showBill = showBill;
        function showBill(item) {
            getCartDetails(item.ID, item.Name);
        };

        //function get cart details by table id
        function getCartDetails(tbID, tbName) {

            $scope.curTableID = tbID;
            $scope.curTableName = tbName;
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
                    $scope.curCart = {
                        CartDetails: $scope.cartDetails,
                        ID: result.data.ID,
                        TableID: result.data.TableID,
                        CartPrice: result.data.CartPrice
                    }
                };
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải dữ liệu!');
            });

        }

        //add dish to cart
        $scope.addDish = addDish;
        function addDish(item) {
            //create new cart for table
            var condition = new Boolean(true);
            if ($scope.curCart == null) {
                
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
                        //ID : 3,
                        CartID: $scope.curCart.ID,
                        Name: item.Name,
                        Price: item.Price,
                        Quantity: 1,
                        Image: item.Image,
                        Status: 1,
                        ProID: item.ID,
                        Note: null,
                        Type: 1
                    };
                    var index = $scope.curCart.CartDetails.length;
                    $scope.curCart.CartDetails[index] = $scope.dishCart;
                    console.log('before ' + $scope.curCart.CartDetails[1].Name);
                    apiService.put('/api/cart/update', $scope.curCart, function (result) {
                        console.log('after ' + $scope.curCart.CartDetails[2].Name);
                    }, function () { 
                        notificationService.displayError('Rất tiếc đã sảy lỗi xảy ra!');
                    });

                }

            }             

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