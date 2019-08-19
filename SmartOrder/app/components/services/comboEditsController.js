(function (app) {
    app.controller('comboEditsController', comboEditsController);

    comboEditsController.$inject = ['$http', '$scope', 'apiService', 'notificationService', '$stateParams'];
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

    function comboEditsController($http, $scope, apiService, notificationService, $stateParams) {

        $scope.listDishInCombo = null;
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
            console.log('comboID : ' + $stateParams.ID);
        }
        getComboByID();

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

        //update combo
        $scope.updateCombo = updateCombo;
        var createCombo = {
            Name: $scope.comboEdt.Name,
            Description: $scope.comboEdt.Description,
            Price: $scope.comboEdt.Price,
            Amount: 1,
            Image: $scope.comboEdt.Image,
            Status: true,
            DishComboMappings: $scope.listDishInCombo
        }
        function updateCombo() {            
            apiService.put('api/combo/update', createCombo,
                function (result) {
                    notificationService.displaySuccess(createCombo.Name + ' đã được cập nhật mới! ');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });
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
                    $scope.edtCombo.Image = result.data;
                })
                .then(function () {
                });
        }

    }
})(angular.module('SmartOrder.services'));