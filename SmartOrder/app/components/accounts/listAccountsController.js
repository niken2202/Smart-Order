(function (app) {
    app.controller('listAccountsController', listAccountsController);

    listAccountsController.$inject = ['$scope', '$state', 'apiService', 'notificationService'];

    function listAccountsController($scope, $state, apiService, notificationService) {
        //get list data account
        function getListAccount() {
            apiService.get('/api/user/getall', null, function (result) {
                $scope.accounts = result.data;
            }, function () {
                //notificationService.displayError('Tải danh sách nguyên liệu không thành công');
            });
        }
        getListAccount();

        //generate index number in table
        $scope.serial = 1;
        $scope.itemPerPage = 20
        $scope.indexCount = function (newPageNumber) {
            $scope.serial = newPageNumber * $scope.itemPerPage - ($scope.itemPerPage - 1);
        }

        $scope.accountAdd = accountAdd;
        function accountAdd() {
            $state.go('add_account');
        }

        //show the dialog ditail account infomation by id
        $scope.accountDetails = accountDetails;
        function accountDetails(item) {
            $state.go('edits_account', { ID: item.Id });
        }

        //function sort by title tr tag
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        };
    }
})(angular.module('SmartOrder.accounts'));