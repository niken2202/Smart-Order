(function (app) {
    app.controller('materialAddController', materialAddController);

    materialAddController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function materialAddController($scope, ngDialog, apiService, notificationService) {

        //dish binding in dialog add dish
        $scope.mateAdd = {
            CreatedDate: new Date,
            Amount:0
        }

        //add dish to database
        $scope.CreateMate = CreateMate;
        function CreateMate() {
            apiService.post('api/material/add', $scope.mateAdd,
                function (result) {
                    notificationService.displaySuccess($scope.mateAdd.Name + ' đã được thêm mới.');
                    $scope.reloadMate();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        

    }
})(angular.module('SmartOrder.materials'));