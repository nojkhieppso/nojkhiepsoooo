(function (app) {
    'use strict';

    app.controller('calendarDetailsCtrl', calendarDetailsCtrl);

    calendarDetailsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function calendarDetailsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-calendars';
        $scope.calendars = [];
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        
        $scope.loadingCalendar = true;
        function search(page) {
        var result=$scope.date2;
        if (result == "undefined" ||result==""||result==null) {
            $scope.date2 = {
                startDate: null,
                endDate: null
            };
        }
        var datatime = JSON.parse(JSON.stringify($scope.date2));
            
            page = page || 0;
            $scope.loadingUsers = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    userid: $routeParams.id,
                    StartAt: datatime.startDate||'',
                    EndAt: datatime.endDate||''
                }
            };

            apiService.get('api/calendars/Searchr/', config,
            calendarLoadCompleted,
            calendarLoadFailed);
        }

        function calendarLoadCompleted(result) {
            $scope.calendars = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.Totalcurrency = result.data.Totalcurrency;
            $scope.Totalmonth = result.data.Totalmonth;
            $scope.loadingCalendar = false;
        }

        function calendarLoadFailed(response) {
            notificationService.displayError(response.data);
        }
        function clearSearch() {
            $scope.date2 = {
                startDate: null,
                endDate: null
            };
            search();
        }
        $scope.date2 = {
            startDate: null,
            endDate: null
        };

        $scope.opts = {
            locale: {
                format: 'DD/MM/YYYY',
                applyClass: 'btn-green',
                applyLabel: "Apply",
                fromLabel: "From",
                toLabel: "To",
                cancelLabel: 'Cancel',
                customRangeLabel: 'Custom',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                firstDay: 1,
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'
                ]
            },
        };
        //Watch for date changes
        $scope.search();
    }

})
(angular.module('homeCinema'));

