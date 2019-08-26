(function (app) {
    app.controller('addAccountController', addAccountController);

    addAccountController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function addAccountController($scope, ngDialog, apiService, notificationService) {

        $scope.accAdd;

        $scope.CreateAcc = CreateAcc;
        function CreateAcc() {

        }


    }
})(angular.module('SmartOrder.accounts'));