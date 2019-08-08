(function () {
    angular.module('SmartOrder.cashier',
        ['SmartOrder.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('cashier', {
                url: "/cashier",
                //parent: 'base',
                templateUrl: "/app/components/cashier/cashierHomeView.html",
                controller: "cashierHomeController"
            });
    }

})();
