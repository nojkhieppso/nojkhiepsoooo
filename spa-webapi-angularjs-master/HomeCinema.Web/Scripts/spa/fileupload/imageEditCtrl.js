(function (app) {
    'use strict';

    app.controller('imageEditCtrl', imageEditCtrl);

    imageEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService'];

    function imageEditCtrl($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {
        $scope.pageClass = 'page-images';
        $scope.image = {};
        $scope.genres = [];
        $scope.loadingimage = true;
        $scope.isReadOnly = false;
        $scope.Updateimage = Updateimage;
        $scope.prepareFiles = prepareFiles;
        $scope.openDatePicker = openDatePicker;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        var imageImage = null;

        function loadimage() {

            $scope.loadingimage = true;

            apiService.get('/api/images/details/' + $routeParams.id, null,
            imageLoadCompleted,
            imageLoadFailed);
        }

        function imageLoadCompleted(result) {
            $scope.image = result.data;
            $scope.loadingimage = false;

            loadGenres();
        }

        function imageLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function genresLoadCompleted(response) {
            $scope.genres = response.data;
        }

        function genresLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function loadGenres() {
            apiService.get('/api/genres/', null,
            genresLoadCompleted,
            genresLoadFailed);
        }

        function Updateimage() {
            if (imageImage) {
                fileUploadService.uploadImage(imageImage, $scope.image.ID, UpdateimageModel);
            }
            else
                UpdateimageModel();
        }

        function UpdateimageModel() {
            apiService.post('/api/images/update', $scope.image,
            updateimageSucceded,
            updateimageFailed);
        }

        function prepareFiles($files) {
            imageImage = $files;
        }

        function updateimageSucceded(response) {
            
            notificationService.displaySuccess($scope.image.Title + ' has been updated');
            $scope.image = response.data;
            imageImage = null;
        }

        function updateimageFailed(response) {
            notificationService.displayError(response);
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };

        loadimage();
    }

})(angular.module('homeCinema'));