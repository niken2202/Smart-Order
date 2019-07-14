(function () {
    angular.module('SmartOrder.materials',
        ['SmartOrder.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('materials_add', {
                url: "/materials_add",
                parent:'base',
                templateUrl: "app/components/materials/listMaterialView.html",
                controller: "materialAddController"
            }).state('materials_edits', {
                url: "/materials_edits",
                parent: 'base',
                templateUrl: "/app/components/materials/materialEditsView.html",
                controller: "materialEditsController"
            }).state('materiales_list', {
                url: "/materiales_list",
                parent: 'base',
                templateUrl: "/app/components/materials/listMaterialView.html",
                controller: "listMaterialController"
            });
    }

})();
