(function (app) {
    app.controller('comboEditsController', comboEditsController);

    comboEditsController.$inject = ['$http', '$scope', 'apiService', 'notificationService', '$stateParams'];
    app.directive('ngFiles', ['$parse', function ($parse) {
        function fn_link(scope, element, attrs) {

            var onChange = $parse(attrs.ngFiles);
            element.on('change', function (event) {
                onChange(scope, { $files: event.target.files });
                // getTheFiles(event.target.files);
            });
        };

        return {
            link: fn_link
        }
    }])

    function comboEditsController($http,$scope, apiService, notificationService, $stateParams) {

        var a = $scope.combo;
        $scope.edtCombo = {
            ID: a.ID, Name: a.Name, Price: a.Price, Amount: a.Amount, Description: a.Description,
            Image: a.Image, Status: a.Status
            //, CreatedDate: a.CreatedDate
        };

        //update combo
        $scope.updateCombo = updateCombo;

        function updateCombo() {            
            apiService.put('api/combo/update', $scope.edtCombo,
                function (result) {
                    notificationService.displaySuccess($scope.edtCombo.Name + ' đã được cập nhật mới! ');
                    $scope.reload();
                }, function (error) {
                    notificationService.displayError('Cập nhật mới không thành công ! Vui lòng kiểm tra lại thông tin đã nhập');
                });
        }

        //Upload file
        $scope.uploadFile = function (files) {
            var fd = new FormData();
            fd.append('Image', files[0]);
            $http.post('/api/image/upload', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .then(function (result) {
                    $scope.edtCombo.Image = result.data;
                })
                .then(function () {
                });
        }

    }
})(angular.module('SmartOrder.services'));