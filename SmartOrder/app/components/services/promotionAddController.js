(function (app) {
    app.controller('promotionAddController', promotionAddController);

    promotionAddController.$inject = ['$scope', 'apiService', 'notificationService'];

    function promotionAddController($scope, apiService, notificationService) {

        //dish binding in dialog add dish
        $scope.promoAdd = {
            CreatedDate: new Date,
            ExpiredDate: new Date,
            Status:1
        }

        //add addPromo to database
        $scope.addPromo = addPromo;
        function addPromo() {
            apiService.post('api/promotioncode/add', $scope.promoAdd,              
                function (result) {
                    notificationService.displaySuccess('Mã giảm giá ' + $scope.promoAdd.Code + ' đã được thêm mới! ');
                    //console.log('update success');
                    $scope.reloadPromo();
                }, function (error) {
                    notificationService.displayError('Lỗi ! Vui lòng kiểm tra lại mã giảm giá không được trùng');
                });
        }


    }
})(angular.module('SmartOrder.services'));