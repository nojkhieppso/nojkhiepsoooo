(function (app) {
    'use strict';

    app.controller('classroomEditCtrl', classroomEditCtrl);

    classroomEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function classroomEditCtrl($scope, $location, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-classrooms';
        $scope.classroomupdate = {};
        $scope.UpdateClassroom = UpdateClassroom;
        function loadClassroom() {
            apiService.get('/api/classroom/details/' + $routeParams.id, null,
            classroomLoadCompleted,
            classroomLoadFailed);
        }

        function classroomLoadCompleted(result) {
            $scope.classroomupdate = result.data;
            $('#StartAtEdit').val($scope.classroomupdate.StartAt);
            $('#EndAtEdit').val($scope.classroomupdate.EndAt);
        }

        function classroomLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function UpdateClassroom() {
            //notificationService.displaySuccess(' has been updated');
            var StartAt = $('#StartAtEdit').val() || null;
            var EndAt = $('#EndAtEdit').val() || null;
            $scope.classroomupdate.StartAt = StartAt
            $scope.classroomupdate.EndAt = EndAt
            apiService.post('/api/classroom/update', $scope.classroomupdate,
             updateClassroomSucceded,
             updateClassroomFailed);
        }

        function updateClassroomSucceded(response) {
            notificationService.displaySuccess($scope.classroomupdate.Name);
            $location.path('/classrooms');
        }

        function updateClassroomFailed(response) {
            notificationService.displayError(response);
        }
        loadClassroom();
    }

})(angular.module('homeCinema'));