(function (app) {
    'use strict';

    app.controller('groupEditCtrl', groupEditCtrl);

    groupEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function groupEditCtrl($scope, $location, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-groups';
        $scope.groupupdate = {};
        $scope.UpdateGroup = UpdateGroup;
        function loadGroup() {
            apiService.get('/api/group/details/' + $routeParams.id, null,
            groupLoadCompleted,
            groupLoadFailed);
        }
        
        function groupLoadCompleted(result) {
            $scope.groupupdate = result.data;
        }

        function groupLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function UpdateGroup() {
        //notificationService.displaySuccess(' has been updated');
            apiService.post('/api/group/update', $scope.groupupdate,
             updateGroupSucceded,
             updateGroupFailed);
        }

        function updateGroupSucceded(response) {
            notificationService.displaySuccess($scope.groupupdate.Name);
            $location.path('/groups');
        }

        function updateGroupFailed(response) {
            notificationService.displayError(response);
        }
        loadGroup();
    }

})(angular.module('homeCinema'));