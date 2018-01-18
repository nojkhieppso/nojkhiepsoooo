(function (app) {
    'use strict';

    app.controller('roleEditCtrl', roleEditCtrl);

    roleEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function roleEditCtrl($scope, $location, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-roles';
        $scope.roleupdate = {};
        $scope.UpdateRole = UpdateRole;
        function loadRole() {
            apiService.get('/api/role/details/' + $routeParams.id, null,
            roleLoadCompleted,
            roleLoadFailed);
        }

        function roleLoadCompleted(result) {
            $scope.roleupdate = result.data;
        }

        function roleLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function UpdateRole() {
            //notificationService.displaySuccess(' has been updated');
            apiService.post('/api/role/update', $scope.roleupdate,
             updateRoleSucceded,
             updateRoleFailed);
        }

        function updateRoleSucceded(response) {
            notificationService.displaySuccess($scope.roleupdate.Name);
            $location.path('/roles');
        }

        function updateRoleFailed(response) {
            notificationService.displayError(response);
        }
        loadRole();
    }

})(angular.module('homeCinema'));