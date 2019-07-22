(function (app) {
    app.controller('listBillController', listBillController);

    listBillController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function listBillController($scope, ngDialog, apiService, notificationService) {

        //for user customize date from to .. 
        $scope.dateSeclect = {
            from: new Date(),
            to: new Date
        };

        var optionArr = new Array();
        function tmp(key, val) {
            this.key = key;
            this.val = val;
        }
        var op = new tmp(1, 'Hôm nay');
        optionArr.push(op);
        op = new tmp(2, 'Tuần này');
        optionArr.push(op);
        op = new tmp(3, 'Tháng này');
        optionArr.push(op);

        $scope.options = optionArr;
        $scope.userOption;


            

    }
})(angular.module('SmartOrder.restaurant'));