(function (app) {
    'use strict';

    app.controller('imagesCtrl', imagesCtrl);

    imagesCtrl.$inject = ['$scope', 'apiService','notificationService'];

    function imagesCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-images';
        $scope.loadingimages = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        
        $scope.images = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        function search(page) {
            page = page || 0;

            $scope.loadingimages = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 6,
                    filter: $scope.filterimages
                }
            };

            apiService.get('/api/images/', config,
            imagesLoadCompleted,
            imagesLoadFailed);
        }

        function imagesLoadCompleted(result) {
            $scope.images = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingimages = false;

            if ($scope.filterimages && $scope.filterimages.length)
            {
                notificationService.displayInfo(result.data.Items.length + ' images found');
            }
            
        }

        function imagesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterimages = '';
            search();
        }

        $scope.search();
    }

})(angular.module('homeCinema'));