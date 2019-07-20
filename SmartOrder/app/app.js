﻿
(function () {
    angular.module('SmartOrder',
        ['SmartOrder.dishes', 'SmartOrder.materials', 'SmartOrder.restaurant','SmartOrder.services','SmartOrder.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: "/app/shared/views/baseView.html",
                controller: "baseController",
                abstract: true
            })
            .state('login', {
                url: "/login",
                templateUrl: "../app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('home', {
                url: "/admin",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }

})();