(function (app) {
    'use strict';

    app.controller('schoolEditCtrl', schoolEditCtrl);

    schoolEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function schoolEditCtrl($scope, $location, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-schools';
        $scope.schoolupdate = {};
        $scope.UpdateSchool = UpdateSchool;
        function loadSchool() {
            apiService.get('/api/school/details/' + $routeParams.id, null,
            schoolLoadCompleted,
            schoolLoadFailed);
        }

        function schoolLoadCompleted(result) {
            $scope.schoolupdate = result.data;
        }

        function schoolLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function UpdateSchool() {
            //notificationService.displaySuccess(' has been updated');
            apiService.post('/api/school/update', $scope.schoolupdate,
             updateSchoolSucceded,
             updateSchoolFailed);
        }

        function updateSchoolSucceded(response) {
            notificationService.displaySuccess($scope.schoolupdate.Name);
            $location.path('/schools');
        }

        function updateSchoolFailed(response) {
            notificationService.displayError(response);
        }
        loadSchool();
    }

})(angular.module('homeCinema'));