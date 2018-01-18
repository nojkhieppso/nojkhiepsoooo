(function (app) {
    'use strict';

    app.controller('errorCtrl', errorCtrl);

    errorCtrl.$inject = ['$scope','apiService', 'notificationService'];

    function errorCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-error';
    }

})(angular.module('homeCinema'));