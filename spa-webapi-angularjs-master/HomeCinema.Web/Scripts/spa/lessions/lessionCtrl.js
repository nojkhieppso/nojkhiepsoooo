
(function (app) {
    'use strict';

    app.controller('lessionCtrl', lessionsCtrl);

    lessionsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function lessionsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        
        
        $scope.loadingLessions = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Lessions = [];
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openLessionDialog = openLessionDialog;
        $scope.Active = Active;
        $scope.Delete = Delete;

        function search(page) {
            page = page || 0;

            $scope.loadingLessions = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterLessions
                }
            };

            apiService.get('/api/lession/search/', config,
            Lessionsloadcompleted,
            Lessionsloadfailed);
        }


        function Delete(ID) {
            apiService.post('/api/lession/delete/' + ID, null,
            lessionsdeletecompleted,
            lessionsdeletefailed);
        }

        function loadLession(ID) {
            apiService.get('/api/lession/details/' + ID, null,
            lessionsloadcompleted,
            lessionsloadfailed);
        }

        function openLessionDialog(ID) {
            var box = $("#mb-remove-row");
            box.addClass("open");
            loadLession(ID);
        }

        function Active(ID, bool) {
            
            apiService.post('/api/lession/active/' + ID +'/'+ bool, null,
            lessionsactivecompleted,
            lessionsactivefailed);
            
        }

        function closeLessionDialog() {
            var box = $("#mb-remove-row");
            box.removeClass("open");
            search();
        }
        function Lessionsloadcompleted(response) {
            $scope.Lessions = response.data.Items;
            $scope.page = response.data.Page;
            $scope.pagesCount = response.data.TotalPages;
            $scope.totalCount = response.data.TotalCount;
            $scope.loadingLessions = false;

            if ($scope.filterLessions && $scope.filterLessions.length) {
                notificationService.displayInfo(response.data.Items.length + ' lessions found');
            }

        }
        function lessionsloadcompleted(response) {
            $scope.lession = response.data;
        }
        function lessionsloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function lessionsdeletecompleted(response) {
            closeLessionDialog();
            notificationService.displaySuccess(response.data);
        }
        function Lessionsloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function lessionsdeletefailed(response) {
            notificationService.displayError(response.data);
        }


        function lessionsactivecompleted(response) {
            search($scope.page);
            notificationService.displaySuccess(response.data);
        }
        function lessionsactivefailed(response) {
            notificationService.displayError(response.data);
        }
        function clearSearch() {
            $scope.filterLessions = '';
            search();
        }

        $scope.search();
    }

})(angular.module('homeCinema'));