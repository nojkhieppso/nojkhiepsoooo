(function (app) {
    'use strict';

    app.controller('schoolAddCtrl', schoolAddCtrl);

    schoolAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function schoolAddCtrl($scope, $location, $routeParams, apiService, notificationService) {

        $scope.pageClass = 'page-schools';
        $scope.school = {};
        $scope.AddSchool = AddSchool;

        function AddSchool() {
            apiService.post('/api/school/add', $scope.school,
             addschoolsucceded,
             addSchoolFailed);
        }

        function addschoolsucceded(response) {
            notificationService.displaySuccess($scope.school.Name + ' successful');
            $scope.school = response.data;
        }

        function addSchoolFailed(response) {
            
            notificationService.displayError(response.statusText);
        }
    }

})(angular.module('homeCinema'));