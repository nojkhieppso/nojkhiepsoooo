
(function () {
    'use strict';
    angular.module('homeCinema', ['common.core', 'common.ui'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            //.when("/", {
            //    templateUrl: "scripts/spa/home/index.html",

            //    controller: "indexCtrl",
            //    resolve: { isAuthenticated: isAuthenticated }
            //})
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl",
                
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })
            .when("/customers", {
                templateUrl: "scripts/spa/customers/customers.html",
                controller: "customersCtrl"
            })
            .when("/customers/register", {
                templateUrl: "scripts/spa/customers/register.html",
                controller: "customersRegCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/movies", {
                templateUrl: "scripts/spa/movies/movies.html",
                controller: "moviesCtrl"
            })
            .when("/movies/add", {
                templateUrl: "scripts/spa/movies/add.html",
                controller: "movieAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/movies/:id", {
                templateUrl: "scripts/spa/movies/details.html",
                controller: "movieDetailsCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/movies/edit/:id", {
                templateUrl: "scripts/spa/movies/edit.html",
                controller: "movieEditCtrl"
            })
            .when("/images/add", {
                templateUrl: "scripts/spa/fileupload/add.html",
                controller: "imageAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            //Calendar
             .when("/", {
                 templateUrl: "scripts/spa/Calendars/calendars.html",
                 controller: "calendarsCtrl",
                 resolve: { isAuthenticated: isAuthenticated }
             })


            //.when("/calendars", {
            //    templateUrl: "scripts/spa/Calendars/calendars.html",
            //    controller: "calendarsCtrl",
            //    resolve: { isAuthenticated: isAuthenticated }
            //})

            .when("/calendars/:id", {
                templateUrl: "scripts/spa/Calendars/details.html",
                controller: "calendarDetailsCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            //Role
            .when("/roles", {
                templateUrl: "scripts/spa/Roles/roles.html",
                controller: "rolesCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/roles/add", {
                templateUrl: "scripts/spa/Roles/add.html",
                controller: "roleAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
             .when("/roles/edit/:id", {
                 templateUrl: "scripts/spa/Roles/edit.html",
                 controller: "roleEditCtrl",
                 resolve: { isAuthenticated: isAuthenticated }
             })

            //Role
            .when("/userroles/:id", {
                templateUrl: "scripts/spa/userroles/userroles.html",
                controller: "userrolesCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })

            //Group
            .when("/groups", {
                templateUrl: "scripts/spa/Groups/groups.html",
                controller: "groupsCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/groups/add", {
                templateUrl: "scripts/spa/Groups/add.html",
                controller: "groupAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
             .when("/groups/edit/:id", {
                 templateUrl: "scripts/spa/Groups/edit.html",
                 controller: "groupEditCtrl",
                 resolve: { isAuthenticated: isAuthenticated }
             })
            //User
            .when("/users", {
                templateUrl: "scripts/spa/account/users.html",
                controller: "usersCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            //404
            .when("/404", {
                templateUrl: "scripts/spa/404/pages-error-404.html",
                controller:"errorCtrl"
            })
            //lession
            .when("/lessions", {
                templateUrl: "scripts/spa/lessions/lessions.html",
                controller: "lessionCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/lessions/add", {
                templateUrl: "scripts/spa/lessions/add.html",
                controller: "lessionAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
             .when("/lessions/edit/:id", {
                 templateUrl: "scripts/spa/lessions/edit.html",
                 controller: "lessionEditCtrl",
                 resolve: { isAuthenticated: isAuthenticated }
             })

            //class
            .when("/classrooms", {
                templateUrl: "scripts/spa/classrooms/classrooms.html",
                controller: "classroomCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/classrooms/add", {
                templateUrl: "scripts/spa/classrooms/add.html",
                controller: "classroomAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
             .when("/classrooms/edit/:id", {
                 templateUrl: "scripts/spa/classrooms/edit.html",
                 controller: "classroomEditCtrl",
                 resolve: { isAuthenticated: isAuthenticated }
             })
             //school
            .when("/schools", {
                templateUrl: "scripts/spa/schools/schools.html",
                controller: "schoolCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/schools/add", {
                templateUrl: "scripts/spa/schools/add.html",
                controller: "schoolAddCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
             .when("/schools/edit/:id", {
                 templateUrl: "scripts/spa/schools/edit.html",
                 controller: "schoolEditCtrl",
                 resolve: { isAuthenticated: isAuthenticated }
             })
            //rental
            .when("/rental", {
                templateUrl: "scripts/spa/rental/rental.html",
                controller: "rentStatsCtrl"
            }).otherwise({ redirectTo: "/" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }

        //$(document).ready(function () {
        //    $(".fancybox").fancybox({
        //        openEffect: 'none',
        //        closeEffect: 'none'
        //    });

        //    $('.fancybox-media').fancybox({
        //        openEffect: 'none',
        //        closeEffect: 'none',
        //        helpers: {
        //            media: {}
        //        }
        //    });

        //    $('[data-toggle=offcanvas]').click(function () {
        //        $('.row-offcanvas').toggleClass('active');
        //    });
        //});
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }

})();
