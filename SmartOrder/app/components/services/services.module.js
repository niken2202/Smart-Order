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
                templateUrl: "/app/components/services/promotionListView.html",
                controller: "promotionListController"
            }).state('editPromotion', {
                url: "/editPromotion",
                parent: 'base',
                templateUrl: "/app/components/services/promotionListView.html",
                controller: "promotionEditsController"
            }).state('addPromotion', {
                url: "/addPromotion",
                parent: 'base',
                templateUrl: "/app/components/services/promotionAddView.html",
                controller: "promotionAddController"
            });
    }

})();
