
angular.module('BigData', ['ngResource', 'ngCookies', 'ngRoute'])

.config(['$provide', '$httpProvider', '$routeProvider', '$locationProvider',

    function ($provide, $httpProvider, $routeProvider, $locationProvider
        ) {



        $routeProvider
        .when('/', {
            templateUrl: '/views/pages/home.html',
            controller: 'MainController',
            controllerAs: 'main'
        })


        .when('/edit/:id', {
            templateUrl: '/views/pages/edit.html'//,
            //controller: 'editController'
        })

        .when('/new/:topic', {
            templateUrl: '/views/pages/edit.html'
            //,controller: 'editController'
        });
        $locationProvider.html5Mode(true);

    }
]);





