(function (app) {
    app.controller('promotionEditsController', promotionEditsController);

    promotionEditsController.$inject = ['$scope', 'apiService', 'notificationService'];

    function promotionEditsController($scope, ngDialog, apiService, notificationService) {

        var a = $scope.selectPromotion;
        var ExpiredDate = new Date(a.ExpiredDate);
        console.log("test date " + a.Status);
        $scope.edtPromo = {
            ID: a.ID, Code: a.Code, CreatedDate: a.CreatedDate, ExpiredDate: ExpiredDate, Discount: a.Discount, Status: a.Status
        };

        //update dish
        $scope.updatePromo = updatePromo;

        function updatePromo() {
            apiService.put('api/promotioncode/update', $scope.edtPromo,
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