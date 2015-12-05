(function () {
    'use strict';
    var app = angular.module('BigData');
    app.controller('MainController',
    [
        '$rootScope', '$location',
        function ($rootScope, $location) {
            var vm = this;
            $rootScope.models = {
                helloAngular: 'I work!'
            };
        }]);

}());