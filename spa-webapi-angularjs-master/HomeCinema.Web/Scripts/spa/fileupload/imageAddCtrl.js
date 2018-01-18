(function (app) {
    'use strict';

    app.controller('imageAddCtrl', imageAddCtrl);

    imageAddCtrl.$inject = ['$scope', '$http', '$timeout', '$upload', '$location', '$routeParams', 'apiService', 'notificationService'];

    function imageAddCtrl($scope, $http, $timeout, $upload, $location, $routeParams, apiService, notificationService) {

        $scope.upload = [];
        $scope.UploadedFiles = [];

        $scope.startUploading = function ($files) {
            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file =$files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "/api/image/images/upload", // webapi url
                        method: "POST",
                        file: $file
                    }).progress(function (evt) {
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.UploadedFiles.push({ FileName: data.FileName, FilePath: data.LocalFilePath, FileLength: data.FileLength });
                    }).error(function (data, status, headers, config) {
                       
                    });
                })(i);
            }
        }

        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        }
    }

})(angular.module('homeCinema'));