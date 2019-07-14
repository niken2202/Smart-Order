(function () {
    angular.module('SmartOrder.dishes',
        ['SmartOrder.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
              .state('dishes_add', {
                url: "/dishes_add",
                parent: 'base',
                templateUrl: "/app/components/dishes/dishAddView.html",
                controller: "dishAddController"
            }).state('dishes_edits', {
                url: "/dishes_edits",
                parent: 'base',
                templateUrl: "/app/components/dishes/dishEditsView.html",
                controller: "dishEditsController"
            }).state('dishes_list', {
                url: "/dishes_list",
                parent: 'base',
                templateUrl: "/app/components/dishes/listDishView.html",
                controller: "listDishController"
            });
    }

})();
