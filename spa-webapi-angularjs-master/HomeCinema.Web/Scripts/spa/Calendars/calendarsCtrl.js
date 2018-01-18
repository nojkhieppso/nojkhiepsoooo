(function (app) {
    'use strict';

    app.controller('calendarsCtrl', calendarsCtrl);

    calendarsCtrl.$inject = ['$scope', 'apiService','notificationService'];

    function calendarsCtrl($scope, apiService, notificationService) {
        $scope.Users = [];

        function loadUsers() {
            apiService.get('api/Account/Getuserbyleader', null,
            Usersloadcompleted,
            Usersloadfailed);
        }
        function Usersloadcompleted(response) {
            $scope.Users = response.data;
        }

        function Usersloadfailed(response) {
            notificationService.displayError(response.data);
        }
        loadUsers();
    }

})(angular.module('homeCinema'));