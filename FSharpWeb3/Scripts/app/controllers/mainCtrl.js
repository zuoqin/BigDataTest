(function () {
    'use strict';
    var app = angular.module('BigData');
    app.controller('MainController',
    [
        '$rootScope', '$location', '$scope', '$resource', '$http','$sce',
        function ($rootScope, $location, $scope, $resource, $http, $sce) {
            var vm = this;
            $rootScope.models = {
                helloAngular: 'I work!'
            };


            function getPage(pagenumber) {
                //var stories = $resource('/api2/story/:page', { page: "@page" });

                $http.get("/api2/story/" + pagenumber).success(function (data) {
                    $rootScope.stories = [];
                    data.forEach(function (item) {
                        $rootScope.stories.push({
                            id: item.id,
                            title: $sce.trustAsHtml(item.title),
                            body: $sce.trustAsHtml(item.content)
                        });
                        //if (persistenceService.getAction() === 0) {
                        //persistenceService.action.save(item);

                        //}
                    });
                }).error(function (error) {
                    $scope.title = "Oops... something went wrong";
                    $scope.working = false;
                });
                return $scope.options;
            }


            getPage(4);

            $scope.deleteStory = function (index) {
                getPage(4);
            };
        }]);

}());