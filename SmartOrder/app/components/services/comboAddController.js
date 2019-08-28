(function (app) {
    app.controller('comboAddController', comboAddController);

    comboAddController.$inject = ['$http', '$state', '$scope', 'apiService', 'notificationService'];
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

    function comboAddController($http, $state, $scope, apiService, notificationService) {
        //combo binding in dialog add combo
        $scope.comboAdd = {
            Status: true,
        }
        $scope.listDishInCombo = null;

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

        //add dish to combo
        $scope.addDish = addDish;
        function addDish(item) {
            if ($scope.listDishInCombo == null || $scope.listDishInCombo.length < 1) {
                var dishInCombo = {
                    DishID: item.ID,
                    Amount: 1,
                    Name: item.Name
                }
                $scope.listDishInCombo = [];
                $scope.listDishInCombo.push(dishInCombo);
            } else {
                var condition = new Boolean(false);
                for (i = 0; i < $scope.listDishInCombo.length; i++) {
                    if (item.ID == $scope.listDishInCombo[i].DishID) {
                        $scope.listDishInCombo[i].Amount += 1;
                        condition = true;
                    }
                }
                if (condition == false) {
                    dishInCombo = {
                        DishID: item.ID,
                        Amount: 1,
                        Name: item.Name
                    }
                    $scope.listDishInCombo.push(dishInCombo);
                }
            }
        }

        //reduce amount of dish in combo
        $scope.delDishInCombo = delDishInCombo;
        function delDishInCombo(obj) {
            for (i = 0; i < $scope.listDishInCombo.length; i++) {
                if (obj.DishID == $scope.listDishInCombo[i].DishID) {
                    if ($scope.listDishInCombo[i].Amount == 1) {
                        $scope.listDishInCombo.splice(i, 1);
                        break;
                    } else {
                        $scope.listDishInCombo[i].Amount -= 1;
                        break;
                    }
                }
            }
        }

        //add combo to database
        $scope.CreateCombo = CreateCombo;

        function CreateCombo() {
            if ($scope.listDishInCombo == null || $scope.listDishInCombo.length < 2) {
                notificationService.displayError('Số lượng món trong combo phải lớn hơn 2');
            } else if ($scope.comboAdd.Image == null) {
                notificationService.displayError('Vui lòng chọn ảnh');
            } else {
                if ($scope.comboAdd.Description == null || $scope.comboAdd.Description.length < 1) {
                    $scope.comboAdd.Description = "Nội dung được tạo tự động";
                }
                var createCombo = {
                    Name: $scope.comboAdd.Name,
                    Description: $scope.comboAdd.Description,
                    Price: $scope.comboAdd.Price,
                    Amount: $scope.comboAdd.Amount,
                    Image: $scope.comboAdd.Image,
                    Status: true,
                    CreatedDate: new Date,                   
                    DishComboMappings: $scope.listDishInCombo
                }
                apiService.post('api/combo/add', createCombo,
                    function (result) {
                        notificationService.displaySuccess('Combo ' + createCombo.Name + ' đã được thêm mới');
                        $state.go('combo');
                    }, function (error) {
                        notificationService.displayError('Thêm mới không thành công.');
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
                    $scope.comboAdd.Image = result.data;
                })
                .then(function () {
                });
        }
    }
})(angular.module('SmartOrder.services'));