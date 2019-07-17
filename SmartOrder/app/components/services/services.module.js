(function () {
    angular.module('SmartOrder.services', 
        ['SmartOrder.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('combo', {
                url: "/combo",
                parent: 'base',
                templateUrl: "/app/components/services/comboView.html",
                controller: "comboController"
            }).state('promotion', {
                url: "/promotion",
                parent: 'base',
                templateUrl: "/app/components/services/promotionView.html",
                controller: "promotionController"
            });
    }

})();
