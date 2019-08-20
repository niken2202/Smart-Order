(function (app) {
    app.controller('promotionEditsController', promotionEditsController);

    promotionEditsController.$inject = ['$scope', 'apiService', 'notificationService'];

    function promotionEditsController($scope, apiService, notificationService) {
        $scope.edtPromo = {
            ID: $scope.selectPromotion.ID,
            Code: $scope.selectPromotion.Code,
            CreatedDate: $scope.selectPromotion.CreatedDate,
            ExpiredDate: new Date($scope.selectPromotion.ExpiredDate),
            Discount: $scope.selectPromotion.Discount,
            Status: $scope.selectPromotion.Status
        };



        //update dish
        $scope.updatePromo = updatePromo;
        function updatePromo() {

            apiService.put('/api/promotioncode/update', $scope.edtPromo,
                function (result) {
                    notificationService.displaySuccess($scope.edtPromo.Code + ' đã được cập nhật mới! ');
                    $scope.reloadPromo();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });

        }


    }
})(angular.module('SmartOrder.services'));