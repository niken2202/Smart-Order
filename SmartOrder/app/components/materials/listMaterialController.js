(function (app) {
    app.controller('listMaterialController', listMaterialController);

    listMaterialController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];

    function listMaterialController($scope, ngDialog, apiService, notificationService) {

        //get material list from api
        function getMaterial(pageIndex, pageSize, totalRow) {
            pageIndex = pageIndex || 0;
            pageSize = pageSize || 10;
            totalRow = totalRow || 0;

            var config = {
                params: {
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    totalRow: totalRow,
                }
            }
            apiService.get('/api/material/getall', config, function (result) {
                $scope.materials = result.data;
                var obj = { a: $scope.materials };
                console.log(obj);
            }, function () {

            });
        }
        getMaterial();

        //function sort by title tr tag
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        };

        //show dialog to create new dish
        $scope.materialAdd = function () {
            var addDialog = ngDialog.openConfirm({
                template: 'app/components/materials/materialAddView.html',
                scope: $scope,
                controller: 'materialAddController',
                //controllerAs: "file",
                closeByDocument: false, //can not close dialog by click out of dialog area
                className: 'ngdialog',
                showClose: false,
            }).then(
                function (value) {
                    //save the contact form
                },
                function (value) {
                    //Cancel or do nothing
                }
            );
        }

    }
})(angular.module('SmartOrder.materials'));