(function (app) {
    'use strict';

    app.controller('roleAddCtrl', roleAddCtrl);

    roleAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function roleAddCtrl($scope, $location, $routeParams, apiService, notificationService) {

        $scope.pageClass = 'page-roles';
        $scope.role = {};
        $scope.AddRole = AddRole;

        function AddRole() {
            apiService.post('/api/role/add', $scope.role,
             addrolesucceded,
             addRoleFailed);
        }

        function addrolesucceded(response) {
            notificationService.displaySuccess($scope.role.Name + ' successful');
            $scope.role = response.data;
        }

        function addRoleFailed(response) {
            
            notificationService.displayError(response.statusText);
        }

    }

})(angular.module('homeCinema'));