(function (app) {
    app.controller('promotionEditsController', promotionEditsController);

    promotionEditsController.$inject = ['$scope', 'apiService', 'notificationService'];

    function promotionEditsController() {

        var a = $scope.selectPromotion;
        $scope.edtPromo = {
            ID: a.ID, Code: a.Code, CreatedDate: a.CreatedDate, ExpiredDate: a.ExpiredDate, Discount: a.Discount, Status: a.Status
        };

        //update dish
        $scope.updatePromo = updatePromo;

        function updatePromo() {
            apiService.put('api/material/update', $scope.edtPromo,
                function (result) {
                    notificationService.displaySuccess($scope.edtPromo.Name + ' đã được cập nhật mới! ');
                    //console.log('update success');
                    $scope.reloadMate();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });
        }


    }
})(angular.module('SmartOrder.services'));