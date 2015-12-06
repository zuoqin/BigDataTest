(function () {
    'use strict';
    var app = angular.module('BigData');
    app.controller('MainController',
    [
        '$rootScope', '$location', '$scope', '$resource', '$http',
        function ($rootScope, $location, $scope, $resource, $http) {
            var vm = this;
            $rootScope.models = {
                helloAngular: 'I work!'
            };


            function getPage(pagenumber) {
                //var stories = $resource('/api2/story/:page', { page: "@page" });

                $http.get("/api2/story/5").success(function (data) {
                    $scope.options = data;
                }).error(function (error) {
                    $scope.title = "Oops... something went wrong";
                    $scope.working = false;
                });
                return $scope.options;
            }


            //getPage(4);

            $scope.deleteStory = function (index) {
                getPage(4);
            };
        }]);

}());