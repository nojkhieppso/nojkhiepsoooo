(function (app) {
    'use strict';

    app.controller('classroomAddCtrl', classroomAddCtrl);

    classroomAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function classroomAddCtrl($scope, $location, $routeParams, apiService, notificationService) {

        $scope.pageClass = 'page-classrooms';
        $scope.classroom = {};
        $scope.AddClassroom = AddClassroom;

        function AddClassroom() {

            var StartAt = $('#StartAt').val() || null;
            var EndAt = $('#EndAt').val() || null;
            $scope.classroom.StartAt = StartAt
            $scope.classroom.EndAt = EndAt
            apiService.post('/api/classroom/add', $scope.classroom,
             addclassroomsucceded,
             addClassroomFailed);
        }

        function addclassroomsucceded(response) {
            notificationService.displaySuccess($scope.classroom.Name + ' successful');
            $scope.classroom = response.data;
        }

        function addClassroomFailed(response) {
            
            notificationService.displayError(response.statusText);
        }

    }

})(angular.module('homeCinema'));