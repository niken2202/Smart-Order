
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

        //dish binding in dialog add dish
        $scope.dishAdd = {
            CreatedDate: new Date,
            Status: 1,
            OrderCount: 0,
            Image: null
        }

        //$scope.imageSrc = "";

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
                console.log('Can get lish dish category');
            });
        }
        getDishCategory();

        //add dish to database
        $scope.CreateDish = CreateDish;
        function CreateDish() {
            apiService.post('api/dish/add', $scope.dishAdd,
                function (result) {
                    notificationService.displaySuccess($scope.dishAdd.Name + ' đã được thêm mới.');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

    }
})(angular.module('SmartOrder.dishes'));
