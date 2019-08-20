(function (app) {
    app.controller('comboEditsController', comboEditsController);

    comboEditsController.$inject = ['$http', '$state', '$scope', 'apiService', 'notificationService', '$stateParams'];
    app.directive('ngFiles', ['$parse', function ($parse) {
        function fn_link(scope, element, attrs) {
            var onChange = $parse(attrs.ngFiles);
            element.on('change', function (event) {
                onChange(scope, { $files: event.target.files });
                // getTheFiles(event.target.files);
            });
        };

        return {
            link: fn_link
        }
    }])

    function comboEditsController($http, $state, $scope, apiService, notificationService, $stateParams) {
        $scope.comboEdt = null;
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

        //get combo details by id
        function getComboByID() {
            var transfer = {
                params: {
                    comboId: $stateParams.ID,
                }
            }
            apiService.get('api/combo/getbyid', transfer,
                function (result) {
                    $scope.comboEdt = result.data;
                }, function (error) {
                    notificationService.displayError('Đã xảy ra lỗi trong quá trình tải dữ liệu');
                });
        }
        getComboByID();

        //add dish to combo
        $scope.addDish = addDish;
        function addDish(item) {
            if ($scope.comboEdt.dishes == null || $scope.comboEdt.dishes.length < 1) {
                var dishInCombo = {
                    ID: item.ID,
                    Amount: 1,
                    Name: item.Name
                }
                $scope.comboEdt.dishes = [];
                $scope.comboEdt.dishes.push(dishInCombo);
            } else {
                var condition = new Boolean(false);
                for (i = 0; i < $scope.comboEdt.dishes.length; i++) {
                    if (item.ID == $scope.comboEdt.dishes[i].ID) {
                        $scope.comboEdt.dishes[i].Amount += 1;
                        condition = true;
                    }
                }
                if (condition == false) {
                    dishInCombo = {
                        ID: item.ID,
                        Amount: 1,
                        Name: item.Name
                    }
                    $scope.comboEdt.dishes.push(dishInCombo);
                }
            }
        }

        //reduce amount of dish in combo
        $scope.delDishInCombo = delDishInCombo;
        function delDishInCombo(obj) {
            for (i = 0; i < $scope.comboEdt.dishes.length; i++) {
                if (obj.ID == $scope.comboEdt.dishes[i].ID) {
                    if ($scope.comboEdt.dishes[i].Amount == 1) {
                        $scope.comboEdt.dishes.splice(i, 1);
                        break;
                    } else {
                        $scope.comboEdt.dishes[i].Amount -= 1;
                        break;
                    }
                }
            }
        }

        //update combo
        $scope.updateCombo = function updateCombo() {
            if ($scope.comboEdt.dishes == null || $scope.comboEdt.dishes.length < 2) {
                notificationService.displayError('Số lượng món trong combo phải lớn hơn 2');
            } else if ($scope.comboEdt.Image == null) {
                notificationService.displayError('Vui lòng chọn ảnh');
            } else {
                if ($scope.comboEdt.Description == null || $scope.comboEdt.Description.length < 1) {
                    $scope.comboEdt.Description = "Nội dung được tạo tự động";
                }
                var updateCombo = {
                    ID: $scope.comboEdt.ID,
                    Name: $scope.comboEdt.Name,
                    Description: $scope.comboEdt.Description,
                    Price: $scope.comboEdt.Price,
                    Amount: 1,
                    Image: $scope.comboEdt.Image,
                    CreatedDate: $scope.comboEdt.CreatedDate,
                    Status: $scope.comboEdt.Status,
                    DishComboMappings: $scope.comboEdt.dishes
                }
                apiService.put('api/combo/update', updateCombo,
                    function (result) {
                        notificationService.displaySuccess(updateCombo.Name + ' đã được cập nhật mới! ');
                        $state.go('combo');
                    }, function (error) {
                        notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                    });
            }
        }

        //cancel button to back list combo
        $scope.Cancel = Cancel;
        function Cancel() {
            $state.go('combo');
        }

        //Upload file
        $scope.uploadFile = function (files) {
            var fd = new FormData();
            fd.append('Image', files[0]);
            $http.post('/api/image/upload', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .then(function (result) {
                    $scope.comboEdt.Image = result.data;
                })
                .then(function () {
                });
        }
    }
})(angular.module('SmartOrder.services'));