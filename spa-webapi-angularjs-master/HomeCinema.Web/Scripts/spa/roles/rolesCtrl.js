
(function (app) {
    'use strict';

    app.controller('rolesCtrl', rolesCtrl);

    rolesCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function rolesCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        
        
        $scope.loadingRoles = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Roles = [];
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openRoleDialog = openRoleDialog;
        $scope.Active = Active;
        $scope.Delete = Delete;

        function search(page) {
            page = page || 0;

            $scope.loadingRoles = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterRoles
                }
            };

            apiService.get('/api/role/search/', config,
            Rolesloadcompleted,
            Rolesloadfailed);
        }


        function Delete(ID) {
            apiService.post('/api/role/delete/' + ID, null,
            rolesdeletecompleted,
            rolesdeletefailed);
        }

        function loadRole(ID) {
            apiService.get('/api/role/details/' + ID, null,
            rolesloadcompleted,
            rolesloadfailed);
        }

        function openRoleDialog(ID) {
            var box = $("#mb-remove-row");
            box.addClass("open");
            loadRole(ID);
        }

        function Active(ID, bool) {
            
            apiService.post('/api/role/active/' + ID +'/'+ bool, null,
            rolesactivecompleted,
            rolesactivefailed);
            
        }

        function closeRoleDialog() {
            var box = $("#mb-remove-row");
            box.removeClass("open");
            search();
        }
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
        function rolesloadcompleted(response) {
            $scope.role = response.data;
        }
        function rolesloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function rolesdeletecompleted(response) {
            closeRoleDialog();
            notificationService.displaySuccess(response.data);
        }
        function Rolesloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function rolesdeletefailed(response) {
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