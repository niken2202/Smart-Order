(function (app) {
    app.controller('dishAddController', dishAddController);

    dishAddController.$inject = ['$scope', 'apiService', 'notificationService','$rootScope'];

    function dishAddController($scope, apiService, notificationService, $rootScope) {

        //dish binding in dialog add dish
        $scope.dishAdd = {
            CreatedDate: new Date,
            Status: 1,
            OrderCount: 0
        }

        //$scope.test = function () {
        //    console.log('in the add controller :------' + $scope.imageName);
        //};
        $scope.imageSrc = "" ;
        $scope.sendData = function (data) {
            console.log("Data upload ", data);
            $scope.imageSrc = data;

            apiService.post('/api/image/api/upload', data, function (result) {
                console.log(result);
            }, function () {
                console.log('Can get lish dish category');
            });

        }

        $scope.setFile = function (element) {
            $scope.$apply(function ($scope) {
                $scope.theFile = element.files[0];

                apiService.post('/api/image/api/upload', null, function (result) {
                    $scope.DishCategory = result.data;
                    if ($scope.DishCategory.lenght === 0) {
                        notificationService.displayWarning("Vui lòng nhập thêm nhóm sản phẩm");
                    }
                }, function () {
                    console.log('Can get lish dish category');
                });

            });
        };

        //get list dish category to the select box
        $scope.DishCategory = [];        
        function getDishCategory() {
            apiService.get('/api/dishcategory/getall', null, function (result) {
                $scope.DishCategory = result.data;
                if ($scope.DishCategory.lenght === 0) {
                    notificationService.displayWarning("Vui lòng nhập thêm nhóm sản phẩm");                    
                }
            }, function () {
                console.log('Can get lish dish category');
            });
        };
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