using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HomeCinema.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/Vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/Vendors/jquery.js",
                "~/Scripts/Vendors/jquery.cookie.js",
                "~/Scripts/Vendors/bootstrap.js",
                "~/Scripts/Vendors/toastr.js",
                "~/Scripts/Vendors/jquery.raty.js",
                "~/Scripts/Vendors/respond.src.js",
                "~/Scripts/Vendors/angular.js",
                "~/Scripts/vendors/angular-messages.js",
                "~/Scripts/Vendors/angular-route.js",
                "~/Scripts/Vendors/angular-cookies.js",
                "~/Scripts/Vendors/angular-validator.js",
                "~/Scripts/Vendors/angular-base64.js",
                "~/Scripts/Vendors/angular-file-upload.js",
                "~/Scripts/Vendors/angucomplete-alt.min.js",
                "~/Scripts/Template/js/plugins/daterangepicker/moment.js",
                "~/Scripts/Template/js/plugins/daterangepicker/daterangepicker.js",
                "~/Scripts/vendors/angular-daterangepicker.js",
                "~/Scripts/Vendors/ui-bootstrap-tpls-0.13.1.js",
                "~/Scripts/Vendors/underscore.js",
                "~/Scripts/Vendors/raphael.js",
                "~/Scripts/Vendors/morris.js",
                //"~/Scripts/Vendors/jquery.fancybox.js",
                //"~/Scripts/Vendors/jquery.fancybox-media.js",
                "~/Scripts/Vendors/loading-bar.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/membershipService.js",
                "~/Scripts/spa/services/fileUploadService.js",
                "~/Scripts/spa/layout/topBar.directive.js",
                "~/Scripts/spa/layout/sideBar.directive.js",
                "~/Scripts/spa/layout/mbSignout.directive.js",
                "~/Scripts/spa/layout/customPager.directive.js",
                "~/Scripts/spa/directives/rating.directive.js",
                "~/Scripts/spa/directives/availableMovie.directive.js",
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js",
                "~/Scripts/spa/customers/customersCtrl.js",
                "~/Scripts/spa/customers/customersRegCtrl.js",
                "~/Scripts/spa/customers/customerEditCtrl.js",
                "~/Scripts/spa/movies/moviesCtrl.js",
                "~/Scripts/spa/movies/movieAddCtrl.js",
                "~/Scripts/spa/movies/movieDetailsCtrl.js",
                "~/Scripts/spa/movies/movieEditCtrl.js",
                "~/Scripts/spa/controllers/rentalCtrl.js",
                "~/Scripts/spa/rental/rentMovieCtrl.js",
                "~/Scripts/spa/rental/rentStatsCtrl.js",
                "~/Scripts/spa/fileupload/imageAddCtrl.js",
                //calendar
                "~/Scripts/spa/Calendars/calendarAddCtrl.js",
                "~/Scripts/spa/Calendars/calendarDetailsCtrl.js",
                "~/Scripts/spa/Calendars/calendarEditCtrl.js",
                "~/Scripts/spa/Calendars/calendarsCtrl.js",

                //Role
                "~/Scripts/spa/roles/roleAddCtrl.js",
                "~/Scripts/spa/roles/roleDetailsCtrl.js",
                "~/Scripts/spa/roles/roleEditCtrl.js",
                "~/Scripts/spa/roles/rolesCtrl.js",
                //Group
                "~/Scripts/spa/groups/groupAddCtrl.js",
                "~/Scripts/spa/groups/groupDetailsCtrl.js",
                "~/Scripts/spa/groups/groupEditCtrl.js",
                "~/Scripts/spa/groups/groupsCtrl.js",
                //Users
                "~/Scripts/spa/account/usersCtrl.js",
                //UserRoles
                "~/Scripts/spa/userroles/userrolesCtrl.js",
                //Lessions
                "~/Scripts/spa/lessions/lessionAddCtrl.js",
                "~/Scripts/spa/lessions/lessionDetailsCtrl.js",
                "~/Scripts/spa/lessions/lessionEditCtrl.js",
                "~/Scripts/spa/lessions/lessionCtrl.js",
                //Class
                "~/Scripts/spa/classrooms/classroomAddCtrl.js",
                "~/Scripts/spa/classrooms/classroomDetailsCtrl.js",
                "~/Scripts/spa/classrooms/classroomEditCtrl.js",
                "~/Scripts/spa/classrooms/classroomCtrl.js",
                //school
                "~/Scripts/spa/schools/schoolAddCtrl.js",
                "~/Scripts/spa/schools/schoolDetailsCtrl.js",
                "~/Scripts/spa/schools/schoolEditCtrl.js",
                "~/Scripts/spa/schools/schoolCtrl.js",
                "~/Scripts/spa/404/errorCtrl.js"

                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/content/css/toastr.css",
                "~/Content/css/loading-bar.css",
                "~/Scripts/Template/css/bootstrap/bootstrap.css",
                "~/Scripts/Template/css/theme-default.css",
                "~/Scripts/Template/css/bootstrap-daterangepicker/daterangepicker.css"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}