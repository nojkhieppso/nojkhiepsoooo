
(function (app) {
    'use strict';

    app.controller('schoolCtrl', schoolsCtrl);

    schoolsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function schoolsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        
        
        $scope.loadingSchools = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Schools = [];
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openSchoolDialog = openSchoolDialog;
        $scope.Active = Active;
        $scope.Delete = Delete;

        function search(page) {
            page = page || 0;

            $scope.loadingSchools = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterSchools
                }
            };

            apiService.get('/api/school/search/', config,
            Schoolsloadcompleted,
            Schoolsloadfailed);
        }


        function Delete(ID) {
            apiService.post('/api/school/delete/' + ID, null,
            schoolsdeletecompleted,
            schoolsdeletefailed);
        }

        function loadSchool(ID) {
            apiService.get('/api/school/details/' + ID, null,
            schoolsloadcompleted,
            schoolsloadfailed);
        }

        function openSchoolDialog(ID) {
            var box = $("#mb-remove-row");
            box.addClass("open");
            loadSchool(ID);
        }

        function Active(ID, bool) {
            
            apiService.post('/api/school/active/' + ID +'/'+ bool, null,
            schoolsactivecompleted,
            schoolsactivefailed);
            
        }

        function closeSchoolDialog() {
            var box = $("#mb-remove-row");
            box.removeClass("open");
            search();
        }
        function Schoolsloadcompleted(response) {
            $scope.Schools = response.data.Items;
            $scope.page = response.data.Page;
            $scope.pagesCount = response.data.TotalPages;
            $scope.totalCount = response.data.TotalCount;
            $scope.loadingSchools = false;

            if ($scope.filterSchools && $scope.filterSchools.length) {
                notificationService.displayInfo(response.data.Items.length + ' schools found');
            }

        }
        function schoolsloadcompleted(response) {
            $scope.school = response.data;
        }
        function schoolsloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function schoolsdeletecompleted(response) {
            closeSchoolDialog();
            notificationService.displaySuccess(response.data);
        }
        function Schoolsloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function schoolsdeletefailed(response) {
            notificationService.displayError(response.data);
        }


        function schoolsactivecompleted(response) {
            search($scope.page);
            notificationService.displaySuccess(response.data);
        }
        function schoolsactivefailed(response) {
            notificationService.displayError(response.data);
        }
        function clearSearch() {
            $scope.filterSchools = '';
            search();
        }

        $scope.search();
    }

})(angular.module('homeCinema'));