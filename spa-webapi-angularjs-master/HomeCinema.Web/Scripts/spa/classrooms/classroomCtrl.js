
(function (app) {
    'use strict';

    app.controller('classroomCtrl', classroomsCtrl);

    classroomsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function classroomsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        
        
        $scope.loadingClassrooms = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Classrooms = [];
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openClassroomDialog = openClassroomDialog;
        $scope.Active = Active;
        $scope.Delete = Delete;

        function search(page) {
            page = page || 0;

            $scope.loadingClassrooms = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterClassrooms
                }
            };

            apiService.get('/api/classroom/search/', config,
            Classroomsloadcompleted,
            Classroomsloadfailed);
        }


        function Delete(ID) {
            apiService.post('/api/classroom/delete/' + ID, null,
            classroomsdeletecompleted,
            classroomsdeletefailed);
        }

        function loadClassroom(ID) {
            apiService.get('/api/classroom/details/' + ID, null,
            classroomsloadcompleted,
            classroomsloadfailed);
        }

        function openClassroomDialog(ID) {
            var box = $("#mb-remove-row");
            box.addClass("open");
            loadClassroom(ID);
        }

        function Active(ID, bool) {
            apiService.post('/api/classroom/active/' + ID +'/'+ bool, null,
            classroomsactivecompleted,
            classroomsactivefailed);
            
        }

        function closeClassroomDialog() {
            var box = $("#mb-remove-row");
            box.removeClass("open");
            search();
        }
        function Classroomsloadcompleted(response) {
            $scope.Classrooms = response.data.Items;
            $scope.page = response.data.Page;
            $scope.pagesCount = response.data.TotalPages;
            $scope.totalCount = response.data.TotalCount;
            $scope.loadingClassrooms = false;

            if ($scope.filterClassrooms && $scope.filterClassrooms.length) {
                notificationService.displayInfo(response.data.Items.length + ' classrooms found');
            }

        }
        function classroomsloadcompleted(response) {
            $scope.classroom = response.data;
        }
        function classroomsloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function classroomsdeletecompleted(response) {
            closeClassroomDialog();
            notificationService.displaySuccess(response.data);
        }
        function Classroomsloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function classroomsdeletefailed(response) {
            notificationService.displayError(response.data);
        }
        function classroomsactivecompleted(response) {
            search($scope.page);
            notificationService.displaySuccess(response.data);
        }
        function classroomsactivefailed(response) {
            notificationService.displayError(response.data);
        }
        function clearSearch() {
            $scope.filterClassrooms = '';
            search();
        }

        $scope.search();
    }

})(angular.module('homeCinema'));