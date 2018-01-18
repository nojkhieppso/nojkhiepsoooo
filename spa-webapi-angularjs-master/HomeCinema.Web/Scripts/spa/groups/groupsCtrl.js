
(function (app) {
    'use strict';

    app.controller('groupsCtrl', groupsCtrl);

    groupsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function groupsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.Groups = [];
        $scope.openGroupDialog = openGroupDialog;
        $scope.Delete = Delete;
        function loadGroups() {
            apiService.get('api/group/latest', null,
            Groupsloadcompleted,
            Groupsloadfailed);
        }
        function Groupsloadcompleted(response) {
            $scope.Groups = response.data;
        }
        function Groupsloadfailed(response) {
            notificationService.displayError(response.data);
        }
        function openGroupDialog(ID) {
            var box = $("#mb-remove-row");
            box.addClass("open");
            loadGroup(ID);
        }

        function loadGroup(ID) {
            apiService.get('/api/group/details/' + ID, null,
            grouploadcompleted,
            grouploadfailed);
        }
        function grouploadcompleted(response) {
            $scope.groupdelete = response.data;
        }
        function grouploadfailed(response) {
            notificationService.displayError(response.data);
        }
        function Delete(ID) {
            apiService.post('/api/group/delete/' + ID, null,
            groupsdeletecompleted,
            groupsdeletefailed);
        }
        function groupsdeletecompleted(response) {
            notificationService.displaySuccess(response.data);
            closeDialog();
        }
        function groupsdeletefailed(response) {
            notificationService.displayError(response.data);
        }
        function closeDialog() {
            var box = $("#mb-remove-row");
            box.removeClass("open");
            loadGroups();
        }
        loadGroups();
    }

})(angular.module('homeCinema'));