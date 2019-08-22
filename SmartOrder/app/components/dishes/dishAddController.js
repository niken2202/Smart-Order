
(function (app) {
    app.controller('dishAddController', dishAddController);


    dishAddController.$inject = ['$http', '$scope', 'apiService', 'notificationService', '$rootScope',];
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

    function dishAddController($http, $scope, apiService, notificationService, $rootScope) {

        $scope.dishes = [];
        //dish binding in dialog add dish
        $scope.dishAdd = {
            CreatedDate: new Date,
            Status: 1,
            OrderCount: 0,
            Image: null
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
                    $scope.dishAdd.Image = result.data;
                })
                .then(function () {
                });
        }

        //get list dish category to the select box
        $scope.DishCategory = [];
        function getDishCategory() {
            apiService.get('/api/dishcategory/getall', null, function (result) {
                $scope.DishCategory = result.data;
            }, function () {
                //console.log('Can get lish dish category');
            });
        }
        getDishCategory();

        //get list dish from api
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

        //add dish to database
        $scope.CreateDish = CreateDish;
        function CreateDish() {
            var condition = new Boolean(true);
            var checkName = $scope.dishAdd.Name;
            var curName;
            checkName = checkName.trim().toLowerCase();
            for (i = 0; i < $scope.dishes.length; i++) {
                curName = $scope.dishes[i].Name;
                curName = curName.trim().toLowerCase();
                if (checkName == curName)
                    condition = false;
            }
            if (condition == true) {
                if ($scope.dishAdd.Image != null) {
                    apiService.post('api/dish/add', $scope.dishAdd,
                        function (result) {
                            notificationService.displaySuccess($scope.dishAdd.Name + ' đã được thêm mới.');
                            $scope.reload();
                        }, function (error) {
                            notificationService.displayError('Thêm mới không thành công');
                        });
                } else {
                    notificationService.displayError('Vui lòng chọn ảnh !');
                }                
            } else {
                notificationService.displayError('Món đã tồn tại');
            }            
        }

    }
})(angular.module('SmartOrder.dishes'));
