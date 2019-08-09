(function (app) {
    app.controller('comboAddController', comboAddController);

    comboAddController.$inject = ['$scope', 'apiService', 'notificationService'];

    function comboAddController($scope, apiService, notificationService) {

        //dish binding in dialog add dish
        $scope.comboAdd = {
            //CreatedDate: new Date,
            Status: true,
        }
 
        //add combo to database
        $scope.CreateCombo = CreateCombo;

        function CreateCombo() {
            apiService.post('api/combo/add', $scope.comboAdd,
                function (result) {
                    notificationService.displaySuccess('Combo ' + $scope.comboAdd.Name + ' đã được thêm mới');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    
    }
})(angular.module('SmartOrder.services'));