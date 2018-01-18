(function (app) {
    'use strict';

    app.controller('lessionEditCtrl', lessionEditCtrl);

    lessionEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function lessionEditCtrl($scope, $location, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-lessions';
        $scope.lessionupdate = {};
        $scope.UpdateLession = UpdateLession;
        function loadLession() {
            apiService.get('/api/lession/details/' + $routeParams.id, null,
            lessionLoadCompleted,
            lessionLoadFailed);
        }

        function lessionLoadCompleted(result) {
            $scope.lessionupdate = result.data;
            $('#StartAtEdit').val($scope.lessionupdate.StartAt);
            $('#EndAtEdit').val($scope.lessionupdate.EndAt);
        }

        function lessionLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function UpdateLession() {
            //notificationService.displaySuccess(' has been updated');
            var StartAt = $('#StartAtEdit').val() || null;
            var EndAt = $('#EndAtEdit').val() || null;
            $scope.lessionupdate.StartAt = StartAt
            $scope.lessionupdate.EndAt = EndAt
            apiService.post('/api/lession/update', $scope.lessionupdate,
             updateLessionSucceded,
             updateLessionFailed);
        }

        function updateLessionSucceded(response) {
            notificationService.displaySuccess($scope.lessionupdate.Name);
            $location.path('/lessions');
        }

        function updateLessionFailed(response) {
            notificationService.displayError(response);
        }
        loadLession();
    }

})(angular.module('homeCinema'));