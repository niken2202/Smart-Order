(function (app) {
    app.controller('tableAddController', tableAddController);

    tableAddController.$inject = ['$scope', 'apiService', 'notificationService'];

    function tableAddController($scope, apiService, notificationService) {

        //dish binding in dialog add table
        $scope.tableAdd = {
            //CreatedDate: new Date,
            Name: "Bàn số ",
            Status: 1,
        }
 
        //add table to database
        $scope.Createtable = Createtable;

        function Createtable() {
            console.log("table add infomation : " + $scope.tableAdd.Name)
            apiService.post('api/table/add', $scope.tableAdd,
                function (result) {
                    notificationService.displaySuccess('table ' + $scope.tableAdd.Name + ' đã được thêm mới');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    
    }
})(angular.module('SmartOrder.restaurant'));