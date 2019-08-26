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
        $scope.CustomerName = "";
        $scope.CrashierName = "";
        $scope.ContentBill = "";
        $scope.CustomerPromotions = "";
        $scope.paymentPrice = 0;
        $scope.cusPaymentPrice = 0;
        $scope.Promotions = {
            Code: "Không",
            Discount: 0
        };

        //get table list from api
        function getListTable() {
            apiService.get('/api/table/getall', null, function (result) {
                $scope.tables = result.data;
                if ($scope.tables.length == 0 || result.data == null) {
                    notificationService.displayInfo('Vui lòng tạo bàn cho nhà hàng!');
                }
            }, function () {
                //notificationService.displayError('Tải danh sách mã bàn không thành công');
            });
        }
        getListTable();

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
                //notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        getDish();

        //get list combos from api
        function getCombo() {
            apiService.get('/api/combo/getall', null, function (result) {
                $scope.combos = result.data;
                if ($scope.combos.length === 0) {
                    //notificationService.displayWarning('Danh sách trống !');
                }
            }, function () {
                notificationService.displayError('Rất tiếc đã sảy ra lỗi trong quá trình tải danh sách!');
            });
        }
        getCombo();

        //get promotion list from api
        function getPromotion() {
            apiService.get('/api/promotioncode/getall', null, function (result) {
                $scope.promotions = result.data;
            }, function () {
                //notificationService.displayError('Tải danh sách mã khuyến mại không thành công');
            });
        }
        getPromotion();

        //show cart by select table
        $scope.showBill = showBill;
        function showBill(item) {
            $scope.CustomerName = "";
            $scope.CrashierName = "";
            $scope.ContentBill = "";
            $scope.CustomerPromotions = "";
            $scope.avaiablePromotion = false;
            $scope.Promotions = {
                Code: "Không",
                Discount: 0
            };
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
            $scope.paymentPrice = $scope.totalPrice - (($scope.totalPrice / 100) * $scope.Promotions.Discount);
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
                    Type: 1,
                    TableName: $scope.curTable.Name,
                    TableID: $scope.curTable.ID
                };
                $scope.listDish.push($scope.dishCart);
                countTotalPrice();
                $scope.curCart = {
                    CartDetails: $scope.listDish,
                    TableID: $scope.curTable.ID,
                    CartPrice: $scope.paymentPrice
                }

                apiService.post('/api/cart/add', $scope.curCart, function (result) {
                    notificationService.displaySuccess('Ok !' + result.data.ID);
                    $scope.curCart = result.data;
                    changeTableStatusOff($scope.curTable);
                }, function () {
                    //notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                });


            } else {
                //add dish by update quantity of dish in cart details
                for (i = 0; i < $scope.curCart.CartDetails.length; i++) {
                    if ($scope.curCart.CartDetails[i].Type == 1 && item.ID == $scope.curCart.CartDetails[i].ProID &&
                        $scope.curCart.CartDetails[i].Status != 2 && $scope.curCart.CartDetails[i].Status != 3)
                    {
                        $scope.curCart.CartDetails[i].Quantity += 1;
                        condition = false;
                        apiService.put('api/cartdetail/update', $scope.curCart.CartDetails[i], function (result) {
                            //notificationService.displaySuccess('Món đã được cập nhật !');
                            bol = true;
                        }, function () {
                            //notificationService.displayError('api/cartdetail/update in add dish !');
                        });
                        break;
                    }
                }
                //add new dish to cart detail
                if (condition == true) {
                    var obj = {
                        CartID: $scope.curCart.ID,
                        Name: item.Name,
                        Price: item.Price,
                        Quantity: 1,
                        Image: item.Image,
                        Status: 1,
                        ProID: item.ID,
                        Type: 1,
                        Note: null,
                        TableName: $scope.curTable.Name,
                        TableID: $scope.curTable.ID
                    };
                    $scope.curCart.CartDetails.push(obj);
                    apiService.post('api/cartdetail/add', obj, function (result) {
                        notificationService.displaySuccess('Món đã được thêm vào cartdetails !');
                        changeTableStatusOff($scope.curTable);
                    }, function () {
                        //    notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                    });

                    countTotalPrice();
                }
            }
        };

        //add combo to cart
        $scope.addCombo = addCombo;
        function addCombo(item) {
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
                var comboCart = {
                    Name: item.Name,
                    Price: item.Price,
                    Quantity: 1,
                    Image: item.Image,
                    Status: 1,
                    ProID: item.ID,
                    Note: null,
                    Type: 2
                };
                var listDish = [];
                listDish.push(comboCart);
                countTotalPrice();
                $scope.curCart = {
                    CartDetails: listDish,
                    TableID: $scope.curTable.ID,
                    CartPrice: $scope.paymentPrice
                }
                apiService.post('/api/cart/add', $scope.curCart, function (result) {
                    notificationService.displaySuccess('Ok !' + result.data.ID);
                    $scope.curCart = result.data;
                    changeTableStatusOff($scope.curTable);
                }, function () {
                    //notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                });


            } else {
                //add dish by update quantity of combo in cart details
                for (i = 0; i < $scope.curCart.CartDetails.length; i++) {
                    if ($scope.curCart.CartDetails[i].Type == 2 && item.ID == $scope.curCart.CartDetails[i].ProID &&
                        $scope.curCart.CartDetails[i].Status != 2 && $scope.curCart.CartDetails[i].Status != 3) {
                        $scope.curCart.CartDetails[i].Quantity += 1;
                        condition = false;
                        apiService.put('api/cartdetail/update', $scope.curCart.CartDetails[i], function (result) {
                            //notificationService.displaySuccess('Món đã được cập nhật !');
                            changeTableStatusOff($scope.curTable);
                        }, function () {
                                //notificationService.displayError('add dish by update quantity of combo in cart details !');
                        });
                        break;
                    }
                }
                //add new combo to cart detail
                if (condition == true) {
                    var comboCart = {
                        CartID: $scope.curCart.ID,
                        Name: item.Name,
                        Price: item.Price,
                        Quantity: 1,
                        Image: item.Image,
                        Status: 1,
                        ProID: item.ID,
                        Type: 2,
                        Note: null,
                        TableName: $scope.curTable.Name,
                        TableID: $scope.curTable.ID
                    };
                    $scope.curCart.CartDetails.push(comboCart);
                    apiService.post('api/cartdetail/add', comboCart, function (result) {
                        //notificationService.displaySuccess('Món đã được thêm vào cartdetails !');
                        changeTableStatusOff($scope.curTable);
                    }, function () {
                        //    notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                    });
                    countTotalPrice();
                }
            }
        };

        //delete combo or dish from cart
        $scope.dishDel = dishDel;
        function dishDel(item) {
            for (i = 0; i < $scope.curCart.CartDetails.length; i++) {
                if (item.Type == $scope.curCart.CartDetails[i].Type && $scope.curCart.CartDetails[i].Type) {
                    if (item.ProID == $scope.curCart.CartDetails[i].ProID) {
                        $scope.curCart.CartDetails.splice(i, 1);
                        apiService.del('api/cartdetail/delete?id=' + item.ID, null, function (result) {
                            //notificationService.displaySuccess('Món đã được xóa khỏi cartdetails !');
                        }, function () {
                            //    notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
                        });
                        countTotalPrice();
                    }
                }
            }
        }

        //event catch input promotion
        $scope.promotionEvt = promotionEvt;
        function promotionEvt() {
            if ($scope.CustomerPromotions != null && $scope.CustomerPromotions.length > 0) {
                apiService.post('/api/promotioncode/checkvalid?Code=' + $scope.CustomerPromotions, null, function (result) {
                    if (result.data != null) {
                        $scope.Promotions = result.data
                        $scope.avaiablePromotion = true;
                    } else {
                        $scope.avaiablePromotion = false;
                        $scope.Promotions = {
                            Code: "Không có",
                            Discount: 0
                        }
                    }
                }, function () {
                    //notificationService.displayError('Tải danh sách mã khuyến mại không thành công');
                });
            } else {
                $scope.avaiablePromotion = false;
                $scope.Promotions = {
                    Code: "Không có",
                    Discount: 0
                }
            }
            var payPrice = $scope.totalPrice - ($scope.totalPrice / 100) * $scope.Promotions.Discount;
            $scope.paymentPrice = payPrice;
        }

        //order send list cart detail to kitchen
        $scope.Order = Order;
        function Order() {
            //if ($scope.curCart == null) {
            //    notificationService.displayWarning("Vui lòng chọn bàn và món");
            //} else {
            //    //create new cart
            //    if ($scope.checkCart == false) {
            //        apiService.post('/api/cart/add', $scope.curCart, function (result) {
            //            notificationService.displaySuccess('Ok !' + result.data.ID);
            //            $scope.curCart = result.data;
            //            changeTableStatusOff($scope.curTable);
            //        }, function () {
            //            //notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
            //        });
            //    } else { //update current cart
            //        console.log('update cart : ' + $scope.curCart);
            //        apiService.put('api/cart/update', $scope.curCart, function (result) {
            //            notificationService.displaySuccess('Món đã được cập nhật !' );
            //            //$scope.curCart.ID = result.data.ID;
            //            console.log($scope.curCart);
            //            changeTableStatusOff($scope.curTable);
            //        }, function () {
            //        //    notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
            //        });

            //    }
            //}
        }

        //payment method
        $scope.Payment = Payment;
        function Payment() {
            if ($scope.curCart == null) {
                notificationService.displayWarning("Vui lòng chọn hóa đơn thanh toán!");
            } else {
                $scope.listDish = [];
                for (i = 0; i < $scope.curCart.CartDetails.length; i++) {
                    var dish = {
                        Name: $scope.curCart.CartDetails[i].Name,
                        Price: $scope.curCart.CartDetails[i].Price,
                        Amount: $scope.curCart.CartDetails[i].Quantity,
                        Description: $scope.ContentBill,
                        Image: $scope.curCart.CartDetails[i].Image
                    };
                    $scope.listDish.push(dish);
                }
                if ($scope.CustomerName.length < 1 || $scope.CustomerName == null) {
                    $scope.CustomerName = "Không tên";
                } if ($scope.CrashierName.length < 1 || $scope.CrashierName == null) {
                    $scope.CrashierName = "Nhân viên đứng quầy";
                } if ($scope.ContentBill.length < 1 || $scope.ContentBill == null) {
                    $scope.ContentBill = "Nội dung được tạo tự động";
                }
                $scope.addBill = {
                    BillDetail: $scope.listDish,
                    Voucher: $scope.Promotions.Code,
                    CustomerName: $scope.CustomerName,
                    Content: $scope.ContentBill,
                    TableID: $scope.curTable.ID,
                    CreatedDate: new Date,
                    CreatedBy: $scope.CrashierName,
                    Discount: $scope.Promotions.Discount,
                    Total: $scope.paymentPrice,
                    Status: true
                }
                $scope.listDish = [];
                apiService.post('/api/bill/add', $scope.addBill, function (result) {
                    $scope.CustomerName = "";
                    $scope.CrashierName = "";
                    $scope.ContentBill = "";
                    $scope.CustomerPromotions = "";
                    $scope.cusPaymentPrice = 0;
                    $scope.Promotions = {
                        Code: "Không",
                        Discount: 0
                    };
                    deleteCart($scope.curCart.ID);
                }, function () {
                    notificationService.displayError('Thanh toan khong thanh cong!');
                });
            }
        }

        //add number ordercount for each dish in bill
        function orderCountDish(item) {
            if (item.Type == 1) {
                apiService.get('/api/dish/getbyid?ID=' + item.ProID , null , function (result) {
                    var updateDish = result.data;
                    if (updateDish != null) {
                        updateDish.OrderCount += item.Quantity;
                        updateDish.Amount -= item.Quantity;
                        apiService.put('api/dish/update', updateDish, function (result) {                           

                        }, function () {
                            //notificationService.displayError('');
                        });

                    }                 

                }, function () {
                    //notificationService.displayError('');
                });
            } else if (item.Type == 2) {
                apiService.get('api/combo/getbyid?ID=' + item.ProID,null , function (result) {
                    var updateCombo = result.data;
                    if (updateCombo != null) {
                        //updateCombo.OrderCount += item.Quantity;
                        updateCombo.Amount -= item.Quantity;
                        apiService.put('api/combo/update', updateCombo, function (result) {

                        }, function () {
                            //notificationService.displayError('');
                        });

                    }

                }, function () {
                    //notificationService.displayError('');
                });
            } else {
                //something exception 
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
                for (i = 0; i < $scope.curCart.CartDetails.length; i++) {
                    orderCountDish($scope.curCart.CartDetails[i]);
                }
                changeTableStatusOn($scope.curTable);
                $scope.curCart = null;
                $scope.curTable = null;
                countTotalPrice();
            }, function () {
                //notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
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
            } else if ($scope.changeTable == $scope.curTable.ID) {
                notificationService.displayWarning("Bạn đang ở bàn này !");
            } else {
                $scope.curCart.TableID = $scope.changeTable;
                apiService.put('/api/cart/changetable', $scope.curCart, function (result) {
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

        //get amount of dish or combo by id
        function getAmount(ID) {
            var a;
            apiService.get('api/dish/getbyid?ID=' + ID, null, function (result) {
                a = result.data.Amount;
            }, function () {
                //notificationService.displayError('');
            });
            return a;
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
                apiService.put('api/cartdetail/update', item, function (result) {
                    //notificationService.displaySuccess('Món đã được cập nhật !');
                }, function () {
                        notificationService.displayError('num < 1 == true || num > 25 == true !');
                });
                bol = false;
            } else {
                if (item.Type == 1) {
                    console.log(getAmount(item.ProID));
                    apiService.put('api/cartdetail/update', item, function (result) {
                    //notificationService.displaySuccess('Món đã được cập nhật !');
                    bol = true;
                }, function () {
                        //notificationService.displayError('else !');
                });

                } else {

                }
                

            }
            if (bol === false) {
                //notificationService.displayError('Dữ liệu nhập vào không hợp lệ');
            }
            countTotalPrice();
        };

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
                //notificationService.displayError('Rất tiếc đã sảy ra lỗi !');
            });
        }

        //auto update current cart
        function autoUpdateCart() {
            var date = new Date();
            console.log("Timeout occurred" + date);
        }
        //set schedule to auto call update view
        $interval(rellTime, 5000);
        function rellTime() {
            getListTable();
            autoUpdateCart();
            getPromotion();
            getDish();
            getCombo();
            if ($scope.curTable != null) {
                getCartDetails($scope.curTable);
            }            
        }

    }
})(angular.module('SmartOrder.cashier'));