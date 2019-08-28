(function () {
    angular.module('SmartOrder.accounts',
        ['SmartOrder.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('add_account', {
                url: "/add_account",
                parent:'base',
                templateUrl: "/app/components/accounts/addAccountView.html",
                controller: "addAccountController"
            }).state('edits_account', {
                url: "/edits_account/{ID:string}",
                parent: 'base',
                templateUrl: "/app/components/accounts/editsAccountView.html",
                controller: "editsAccountController"
            }).state('list_account', {
                url: "/list_account",
                parent: 'base',
                templateUrl: "/app/components/accounts/listAccountsView.html",
                controller: "listAccountsController"
            });
    }

})();
