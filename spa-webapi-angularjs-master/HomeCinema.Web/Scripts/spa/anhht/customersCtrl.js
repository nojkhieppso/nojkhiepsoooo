(function (app) {
    'use strict';

    app.controller('customersCtrl', customersCtrl);

    customersCtrl.$inject = ['$scope','$modal', 'apiService', 'notificationService'];

    function customersCtrl($scope, $modal, apiService, notificationService) {

        $scope.pageClass = 'page-customers';
        $scope.loadingCustomers = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Customers = [];
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            $scope.loadingCustomers = true;
            apiService.get('/api/customers/search/', config,
            customersLoadCompleted,
            customersLoadFailed);
        }
        function customersLoadCompleted(result) {
            $scope.Customers = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingCustomers = false;

            if ($scope.filterCustomers && $scope.filterCustomers.length) {
                notificationService.displayInfo(result.data.Items.length + ' customers found');
            }

        }
        function customersLoadFailed(response) {
            notificationService.displayError(response.data);
        }
        $scope.search();
    }

})(angular.module('homeCinema'));