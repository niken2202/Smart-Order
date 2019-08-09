(function () {
    angular.module('SmartOrder.restaurant', 
        ['SmartOrder.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('list_bills', {
                url: "/list_bills",
                parent: 'base',
                templateUrl: "/app/components/restaurant/listBillView.html",
                controller: "listBillController"
            }).state('revenus', {
                url: "/revenus",
                parent: 'base',
                templateUrl: "/app/components/restaurant/revenusView.html",
                controller: "revenusController"
            }).state('tables', {
                url: "/tables",
                parent: 'base',
                templateUrl: "/app/components/restaurant/tablesView.html",
                controller: "tableController"
            });
    }

})();
