(function (app) {
    app.controller('dishEditsController', dishEditsController);

    function dishEditsController($scope) {
        
        var f = this;

        // get the call back from the pop up
        // This works ....
        $scope.saveContact = function (img) {
            alert(img.name + " is saved by $scope.")
        }

        // This one does not work!!!???
        f.saveContact = function (img) {
            alert(img.name + " is saved by controllerAs.")
        }

    }
})(angular.module('SmartOrder.dishes'));