
(function () {
    angular.module('SmartOrder',
        ['SmartOrder.dishes', 'SmartOrder.materials', 'SmartOrder.restaurant', 'SmartOrder.services',
            'SmartOrder.cashier', 'SmartOrder.common'])
        .config(config)
     .config(configAuthentication);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: "../app/shared/views/baseView.html",
                controller: "baseController",
                abstract: true
            })
            .state('login', {
                url: "/login",
                templateUrl: "../app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('register', {
                url: "/register",
                templateUrl: "/app/components/login/registerView.html",
                controller: "registerController"
            })
            .state('home', {
                url: "/admin",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            })
            .state('history', {
                url: "/history",
                parent: 'base',
                templateUrl: "/app/components/home/listHistoryView.html",
                controller: "listHistoryController"
            })
            .state('profile', {
                url: "/profile",
                //parent: 'base',
                templateUrl: "/app/components/home/profileView.html",
                controller: "profileController"
            });
        $urlRouterProvider.otherwise('/login');
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }

})();