(function (app) {
    'use strict';

    app.controller('groupAddCtrl', groupAddCtrl);

    groupAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function groupAddCtrl($scope, $location, $routeParams, apiService, notificationService) {

        $scope.pageClass = 'page-groups';
        $scope.group = {};
        $scope.AddGroup = AddGroup;

        function AddGroup() {
            apiService.post('/api/group/add', $scope.group,
           addgroupsucceded,
           addGroupFailed);
        }
        function addgroupsucceded(response) {
            notificationService.displaySuccess($scope.group.Name + ' successful');
            $scope.group = response.data;
        }

        function addGroupFailed(response) {
            
            notificationService.displayError(response.statusText);
        }

    }

})(angular.module('homeCinema'));