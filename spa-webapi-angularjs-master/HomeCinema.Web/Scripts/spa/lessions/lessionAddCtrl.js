(function (app) {
    'use strict';

    app.controller('lessionAddCtrl', lessionAddCtrl);

    lessionAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function lessionAddCtrl($scope, $location, $routeParams, apiService, notificationService) {

        $scope.pageClass = 'page-lessions';
        $scope.lession = {};
        $scope.AddLession = AddLession;

        function AddLession() {

            var StartAt = $('#StartAt').val() || null;
            var EndAt = $('#EndAt').val() || null;
            $scope.lession.StartAt = StartAt
            $scope.lession.EndAt = EndAt
            apiService.post('/api/lession/add', $scope.lession,
             addlessionsucceded,
             addLessionFailed);
        }

        function addlessionsucceded(response) {
            notificationService.displaySuccess($scope.lession.Name + ' successful');
            $scope.lession = response.data;
        }

        function addLessionFailed(response) {
           
            notificationService.displayError(response.statusText);
        }

    }

})(angular.module('homeCinema'));