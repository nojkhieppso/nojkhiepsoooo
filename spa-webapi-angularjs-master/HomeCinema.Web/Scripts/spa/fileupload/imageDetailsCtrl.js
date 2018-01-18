(function (app) {
    'use strict';

    app.controller('imageDetailsCtrl', imageDetailsCtrl);

    imageDetailsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function imageDetailsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-images';
        $scope.image = {};
        $scope.loadingimage = true;
        $scope.loadingRentals = true;
        $scope.isReadOnly = true;
        $scope.openRentDialog = openRentDialog;
        $scope.returnimage = returnimage;
        $scope.rentalHistory = [];
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;
        $scope.isBorrowed = isBorrowed;

        function loadimage() {

            $scope.loadingimage = true;

            apiService.get('/api/images/details/' + $routeParams.id, null,
            imageLoadCompleted,
            imageLoadFailed);
        }

        function loadRentalHistory() {
            $scope.loadingRentals = true;

            apiService.get('/api/rentals/' + $routeParams.id + '/rentalhistory', null,
            rentalHistoryLoadCompleted,
            rentalHistoryLoadFailed);
        }

        function loadimageDetails() {
            loadimage();
            loadRentalHistory();
        }

        function returnimage(rentalID) {
            apiService.post('/api/rentals/return/' + rentalID, null,
            returnimageSucceeded,
            returnimageFailed);
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

        function imageLoadCompleted(result) {
            $scope.image = result.data;
            $scope.loadingimage = false;
        }

        function imageLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function rentalHistoryLoadCompleted(result) {
            
            $scope.rentalHistory = result.data;
            $scope.loadingRentals = false;
        }

        function rentalHistoryLoadFailed(response) {
            notificationService.displayError(response);
        }

        function returnimageSucceeded(response) {
            notificationService.displaySuccess('image returned to HomeCinema succeesfully');
            loadimageDetails();
        }

        function returnimageFailed(response) {
            notificationService.displayError(response.data);
        }

        function openRentDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/rental/rentimageModal.html',
                controller: 'rentimageCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                loadimageDetails();
            }, function () {
            });
        }

        loadimageDetails();
    }

})(angular.module('homeCinema'));