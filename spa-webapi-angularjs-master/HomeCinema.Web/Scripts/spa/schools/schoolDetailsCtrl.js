(function (app) {
    'use strict';

    app.controller('schoolDetailsCtrl', schoolDetailsCtrl);

    schoolDetailsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function schoolDetailsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-schools';
        $scope.school = {};
        $scope.loadingMovie = true;
        $scope.loadingRentals = true;
        $scope.isReadOnly = true;
        $scope.openRentDialog = openRentDialog;
        $scope.returnMovie = returnMovie;
        $scope.rentalHistory = [];
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;
        $scope.isBorrowed = isBorrowed;

        function loadMovie() {

            $scope.loadingMovie = true;

            apiService.get('/api/schools/details/' + $routeParams.id, null,
            schoolLoadCompleted,
            schoolLoadFailed);
        }

        function loadRentalHistory() {
            $scope.loadingRentals = true;

            apiService.get('/api/rentals/' + $routeParams.id + '/rentalhistory', null,
            rentalHistoryLoadCompleted,
            rentalHistoryLoadFailed);
        }

        function loadMovieDetails() {
            loadMovie();
            loadRentalHistory();
        }

        function returnMovie(rentalID) {
            apiService.post('/api/rentals/return/' + rentalID, null,
            returnMovieSucceeded,
            returnMovieFailed);
        }

        function isBorrowed(rental)
        {
            return rental.Status == 'Borrowed';
        }

        function getStatusColor(status) {
            if (status == 'Borrowed')
                return 'red'
            else {
                return 'green';
            }
        }

        function clearSearch()
        {
            $scope.filterRentals = '';
        }

        function schoolLoadCompleted(result) {
            $scope.school = result.data;
            $scope.loadingMovie = false;
        }

        function schoolLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function rentalHistoryLoadCompleted(result) {
            
            $scope.rentalHistory = result.data;
            $scope.loadingRentals = false;
        }

        function rentalHistoryLoadFailed(response) {
            notificationService.displayError(response);
        }

        function returnMovieSucceeded(response) {
            notificationService.displaySuccess('Movie returned to HomeCinema succeesfully');
            loadMovieDetails();
        }

        function returnMovieFailed(response) {
            notificationService.displayError(response.data);
        }

        function openRentDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/rental/rentMovieModal.html',
                controller: 'rentMovieCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                loadMovieDetails();
            }, function () {
            });
        }

        loadMovieDetails();
    }

})(angular.module('homeCinema'));