(function (app) {
    app.controller('comboAddController', comboAddController);

    comboAddController.$inject = ['$http','$scope', 'apiService', 'notificationService'];
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

    function comboAddController($http,$scope, apiService, notificationService) {

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

        //Upload file
        $scope.uploadFile = function (files) {
            var fd = new FormData();
            fd.append('Image', files[0]);
            $http.post('/api/image/upload', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .then(function (result) {
                    $scope.comboAdd.Image = result.data;
                })
                .then(function () {
                });
        }
    
    }
})(angular.module('SmartOrder.services'));