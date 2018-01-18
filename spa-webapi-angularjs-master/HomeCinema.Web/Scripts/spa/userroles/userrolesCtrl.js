
(function (app) {
    'use strict';

    app.controller('userrolesCtrl', userrolesCtrl);

    userrolesCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function userrolesCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {


        $scope.loadingRoles = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Roles = [];
        $scope.roleupdate = {};
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        //$scope.openRoleDialog = openRoleDialog;
        $scope.Active = Active;

        function search(page) {
            page = page || 0;

            $scope.loadingRoles = true;

            var config = {
                params: {
                    userid: $routeParams.id,
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterRoles
                }
            };

            apiService.get('/api/userroles/search/', config,
            Rolesloadcompleted,
            Rolesloadfailed);
        }
        function Active(userid, roleid, active) {
            $scope.role = {};
            $scope.role.UserID = userid;
            $scope.role.RoleID = roleid;
            $scope.role.Active = active;
            
            apiService.post('/api/userroles/add', $scope.role,
             rolesactivecompleted,
             rolesactivefailed);
        }
        //function closeRoleDialog() {
        //    var box = $("#mb-remove-row");
        //    box.removeClass("open");
        //    search();
        //}
        function Rolesloadcompleted(response) {
            $scope.Roles = response.data.Items;
            $scope.page = response.data.Page;
            $scope.pagesCount = response.data.TotalPages;
            $scope.totalCount = response.data.TotalCount;
            $scope.loadingRoles = false;
            if ($scope.filterRoles && $scope.filterRoles.length) {
                notificationService.displayInfo(response.data.Items.length + ' roles found');
            }

        }

        function rolesloadfailed(response) {
            notificationService.displayError(response.data);
        }

        function Rolesloadfailed(response) {
            notificationService.displayError(response.data);
        }



        function rolesactivecompleted(response) {
            search($scope.page);
            notificationService.displaySuccess(response.data);
        }
        function rolesactivefailed(response) {
            notificationService.displayError(response.data);
        }
        function clearSearch() {
            $scope.filterRoles = '';
            search();
        }

        $scope.search();
    }

})(angular.module('homeCinema'));