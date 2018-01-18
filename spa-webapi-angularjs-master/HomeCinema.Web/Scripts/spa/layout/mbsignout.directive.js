(function(app) {
    'use strict';

    app.directive('mbSignout', mbSignout);

    function mbSignout() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/scripts/spa/layout/mbSignout.html'
        }
    }

})(angular.module('common.ui'));