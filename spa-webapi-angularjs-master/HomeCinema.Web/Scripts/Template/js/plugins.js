$(function () {


    var formElements = function () {
        // Bootstrap datepicker
        var feDatepicker = function () {
            if ($(".datepicker").length > 0) {
                $(".datepicker").datepicker({ format: 'yyyy-mm-dd' });
                $("#dp-2,#dp-3,#dp-4").datepicker(); // Sample
            }

        }// END Bootstrap datepicker

        //Bootstrap timepicker
        var feTimepicker = function () {
            // Default timepicker
            if ($(".timepicker").length > 0)
                $('.timepicker').timepicker();

            // 24 hours mode timepicker
            if ($(".timepicker24").length > 0)
                $(".timepicker24").timepicker({ minuteStep: 30, showSeconds: false, showMeridian: false });

        }// END Bootstrap timepicker

        //Daterangepicker 
        var feDaterangepicker = function () {
            if ($(".daterange").length > 0)
                $(".daterange").daterangepicker({ format: 'YYYY-MM-DD', startDate: '2013-01-01', endDate: '2013-12-31' });
        }
        // END Daterangepicker

        //Bootstrap colopicker        
        var feColorpicker = function () {
            // Default colorpicker hex
            if ($(".colorpicker").length > 0)
                $(".colorpicker").colorpicker({ format: 'hex' });

            // RGBA mode
            if ($(".colorpicker_rgba").length > 0)
                $(".colorpicker_rgba").colorpicker({ format: 'rgba' });

            // Sample
            if ($("#colorpicker").length > 0)
                $("#colorpicker").colorpicker();

        }// END Bootstrap colorpicker

        //Bootstrap select
        var feSelect = function () {
            if ($(".select").length > 0) {
                $(".select").selectpicker();

                $(".select").on("change", function () {
                    if ($(this).val() == "" || null === $(this).val()) {
                        if (!$(this).attr("multiple"))
                            $(this).val("").find("option").removeAttr("selected").prop("selected", false);
                    } else {
                        $(this).find("option[value=" + $(this).val() + "]").attr("selected", true);
                    }
                });
            }
        }//END Bootstrap select


        //Validation Engine
        var feValidation = function () {
            if ($("form[id^='validate']").length > 0) {

                // Validation prefix for custom form elements
                var prefix = "valPref_";

                //Add prefix to Bootstrap select plugin
                $("form[id^='validate'] .select").each(function () {
                    $(this).next("div.bootstrap-select").attr("id", prefix + $(this).attr("id")).removeClass("validate[required]");
                });

                // Validation Engine init
                $("form[id^='validate']").validationEngine('attach', {
                    promptPosition: "bottomLeft", scroll: false,
                    onValidationComplete: function (form, status) {
                        form.validationEngine("updatePromptsPosition");
                    },
                    prettySelect: true,
                    usePrefix: prefix
                });
            }
        }//END Validation Engine

        //Masked Inputs
        var feMasked = function () {
            if ($("input[class^='mask_']").length > 0) {
                $("input.mask_tin").mask('99-9999999');
                $("input.mask_ssn").mask('999-99-9999');
                $("input.mask_date").mask('9999-99-99');
                $("input.mask_product").mask('a*-999-a999');
                $("input.mask_phone").mask('99 (999) 999-99-99');
                $("input.mask_phone_ext").mask('99 (999) 999-9999? x99999');
                $("input.mask_credit").mask('9999-9999-9999-9999');
                $("input.mask_percent").mask('99%');
            }
        }//END Masked Inputs

        //Bootstrap tooltip
        var feTooltips = function () {
            $("body").tooltip({ selector: '[data-toggle="tooltip"]', container: "body" });
        }//END Bootstrap tooltip

        //Bootstrap Popover
        var fePopover = function () {
            $("[data-toggle=popover]").popover();
            $(".popover-dismiss").popover({ trigger: 'focus' });
        }//END Bootstrap Popover

        //Tagsinput
        var feTagsinput = function () {
            if ($(".tagsinput").length > 0) {

                $(".tagsinput").each(function () {

                    if ($(this).data("placeholder") != '') {
                        var dt = $(this).data("placeholder");
                    } else
                        var dt = 'add a tag';

                    $(this).tagsInput({ width: '100%', height: 'auto', defaultText: dt });
                });

            }
        }// END Tagsinput

        //iCheckbox and iRadion - custom elements
        var feiCheckbox = function () {
            if ($(".icheckbox").length > 0) {
                $(".icheckbox,.iradio").iCheck({ checkboxClass: 'icheckbox_minimal-grey', radioClass: 'iradio_minimal-grey' });
            }
        }
        // END iCheckbox

        //Bootstrap file input
        var feBsFileInput = function () {

            if ($("input.fileinput").length > 0)
                $("input.fileinput").bootstrapFileInput();

        }
        //END Bootstrap file input

        return {// Init all form element features
            init: function () {
                feDatepicker();
                feTimepicker();
                feColorpicker();
                feSelect();
                feValidation();
                feMasked();
                feTooltips();
                fePopover();
                feTagsinput();
                feiCheckbox();
                feBsFileInput();
                feDaterangepicker();
            }
        }
    }();

    var uiElements = function () {

        //Datatables
        var uiDatatable = function () {
            if ($(".datatable").length > 0) {
                $(".datatable").dataTable();
                $(".datatable").on('page.dt', function () {
                    onresize(100);
                });
            }

            if ($(".datatable_simple").length > 0) {
                $(".datatable_simple").dataTable({ "ordering": false, "info": false, "lengthChange": false, "searching": false });
                $(".datatable_simple").on('page.dt', function () {
                    onresize(100);
                });
            }
        }//END Datatable        

        //RangeSlider // This function can be removed or cleared.
        var uiRangeSlider = function () {

            //Default Slider with start value
            if ($(".defaultSlider").length > 0) {
                $(".defaultSlider").each(function () {
                    var rsMin = $(this).data("min");
                    var rsMax = $(this).data("max");

                    $(this).rangeSlider({
                        bounds: { min: 1, max: 200 },
                        defaultValues: { min: rsMin, max: rsMax }
                    });
                });
            }//End Default

            //Date range slider
            if ($(".dateSlider").length > 0) {
                $(".dateSlider").each(function () {
                    $(this).dateRangeSlider({
                        bounds: { min: new Date(2012, 1, 1), max: new Date(2015, 12, 31) },
                        defaultValues: { min: new Date(2012, 10, 15), max: new Date(2014, 12, 15) }
                    });
                });
            }//End date range slider

            //Range slider with predefinde range            
            if ($(".rangeSlider").length > 0) {
                $(".rangeSlider").each(function () {
                    var rsMin = $(this).data("min");
                    var rsMax = $(this).data("max");

                    $(this).rangeSlider({
                        bounds: { min: 1, max: 200 },
                        range: { min: 20, max: 40 },
                        defaultValues: { min: rsMin, max: rsMax }
                    });
                });
            }//End

            //Range Slider with custom step
            if ($(".stepSlider").length > 0) {
                $(".stepSlider").each(function () {
                    var rsMin = $(this).data("min");
                    var rsMax = $(this).data("max");

                    $(this).rangeSlider({
                        bounds: { min: 1, max: 200 },
                        defaultValues: { min: rsMin, max: rsMax },
                        step: 10
                    });
                });
            }//End

        }//END RangeSlider

        //Start Knob Plugin
        var uiKnob = function () {

            if ($(".knob").length > 0) {
                $(".knob").knob();
            }

        }//End Knob

        // Start Smart Wizard
        var uiSmartWizard = function () {

            if ($(".wizard").length > 0) {

                //Check count of steps in each wizard
                $(".wizard > ul").each(function () {
                    $(this).addClass("steps_" + $(this).children("li").length);
                });//end

                // This par of code used for example
                if ($("#wizard-validation").length > 0) {

                    var validator = $("#wizard-validation").validate({
                        rules: {
                            login: {
                                required: true,
                                minlength: 2,
                                maxlength: 8
                            },
                            password: {
                                required: true,
                                minlength: 5,
                                maxlength: 10
                            },
                            repassword: {
                                required: true,
                                minlength: 5,
                                maxlength: 10,
                                equalTo: "#password"
                            },
                            email: {
                                required: true,
                                email: true
                            },
                            name: {
                                required: true,
                                maxlength: 10
                            },
                            adress: {
                                required: true
                            }
                        }
                    });

                }// End of example

                $(".wizard").smartWizard({
                    // This part of code can be removed FROM
                    onLeaveStep: function (obj) {
                        var wizard = obj.parents(".wizard");

                        if (wizard.hasClass("wizard-validation")) {

                            var valid = true;

                            $('input,textarea', $(obj.attr("href"))).each(function (i, v) {
                                valid = validator.element(v) && valid;
                            });

                            if (!valid) {
                                wizard.find(".stepContainer").removeAttr("style");
                                validator.focusInvalid();
                                return false;
                            }

                        }

                        return true;
                    },// <-- TO

                    //This is important part of wizard init
                    onShowStep: function (obj) {
                        var wizard = obj.parents(".wizard");

                        if (wizard.hasClass("show-submit")) {

                            var step_num = obj.attr('rel');
                            var step_max = obj.parents(".anchor").find("li").length;

                            if (step_num == step_max) {
                                obj.parents(".wizard").find(".actionBar .btn-primary").css("display", "block");
                            }
                        }
                        return true;
                    }//End
                });
            }

        }// End Smart Wizard

        //OWL Carousel
        var uiOwlCarousel = function () {

            if ($(".owl-carousel").length > 0) {
                $(".owl-carousel").owlCarousel({ mouseDrag: false, touchDrag: true, slideSpeed: 300, paginationSpeed: 400, singleItem: true, navigation: false, autoPlay: true });
            }

        }//End OWL Carousel

        // Summernote 
        var uiSummernote = function () {
            /* Extended summernote editor */
            if ($(".summernote").length > 0) {
                $(".summernote").summernote({
                    height: 250,
                    codemirror: {
                        mode: 'text/html',
                        htmlMode: true,
                        lineNumbers: true,
                        theme: 'default'
                    }
                });
            }
            /* END Extended summernote editor */

            /* Lite summernote editor */
            if ($(".summernote_lite").length > 0) {

                $(".summernote_lite").on("focus", function () {

                    $(".summernote_lite").summernote({
                        height: 100, focus: true,
                        toolbar: [
                            ["style", ["bold", "italic", "underline", "clear"]],
                            ["insert", ["link", "picture", "video"]]
                        ]
                    });
                });
            }
            /* END Lite summernote editor */

            /* Email summernote editor */
            if ($(".summernote_email").length > 0) {

                $(".summernote_email").summernote({
                    height: 400, focus: true,
                    toolbar: [
                        ['style', ['bold', 'italic', 'underline', 'clear']],
                        ['font', ['strikethrough']],
                        ['fontsize', ['fontsize']],
                        ['color', ['color']],
                        ['para', ['ul', 'ol', 'paragraph']],
                        ['height', ['height']]
                    ]
                });

            }
            /* END Email summernote editor */

        }// END Summernote 

        // Custom Content Scroller
        var uiScroller = function () {

            if ($(".scroll").length > 0) {
                $(".scroll").mCustomScrollbar({ axis: "y", autoHideScrollbar: true, scrollInertia: 20, advanced: { autoScrollOnFocus: false } });
            }

        }// END Custom Content Scroller

        // Sparkline
        var uiSparkline = function () {

            if ($(".sparkline").length > 0)
                $(".sparkline").sparkline('html', { enableTagOptions: true, disableHiddenCheck: true });

        }// End sparkline              

        $(window).resize(function () {
            if ($(".owl-carousel").length > 0) {
                $(".owl-carousel").data('owlCarousel').destroy();
                uiOwlCarousel();
            }
        });

        return {
            init: function () {
                uiDatatable();
                uiRangeSlider();
                uiKnob();
                uiSmartWizard();
                uiOwlCarousel();
                uiSummernote();
                uiScroller();
                uiSparkline();
            }
        }

    }();

    var templatePlugins = function () {

        var tp_clock = function () {

            function tp_clock_time() {
                var now = new Date();
                var hour = now.getHours();
                var minutes = now.getMinutes();

                hour = hour < 10 ? '0' + hour : hour;
                minutes = minutes < 10 ? '0' + minutes : minutes;

                $(".plugin-clock").html(hour + "<span>:</span>" + minutes);
            }
            if ($(".plugin-clock").length > 0) {

                tp_clock_time();

                window.setInterval(function () {
                    tp_clock_time();
                }, 10000);

            }
        }

        var tp_date = function () {

            if ($(".plugin-date").length > 0) {

                var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

                var now = new Date();
                var day = days[now.getDay()];
                var date = now.getDate();
                var month = months[now.getMonth()];
                var year = now.getFullYear();

                $(".plugin-date").html(day + ", " + month + " " + date + ", " + year);
            }

        }

        return {
            init: function () {
                tp_clock();
                tp_date();
            }
        }
    }();

    var fullCalendar = function () {

        var calendar = function () {
            if ($("#calendar").length > 0) {
                function prepare_external_list() {
                    $('#external-events div.external-event').each(function () {
                        var eventObject = { title: $.trim($(this).text()) };
                        $(this).data('eventObject', eventObject);
                        $(this).draggable({
                            zIndex: 9999,
                            revert: true,
                            revertDuration: 0
                        });
                    });
                }
                var date = new Date();
                var d = date.getDate();
                var m = date.getMonth();
                var y = date.getFullYear();

                
                if ($.cookie("repository") != null) {
                    var data = JSON.parse($.cookie("repository"));
                    var authdata = data.loggedUser.authdata;
                    $.ajax({
                        url: '/api/lession/all',
                        beforeSend: function (request) {
                            request.withCredentials = true;
                            request.setRequestHeader("Authorization", authdata);
                        },
                        success: function (resultData) {
                            lessionList = resultData;
                        }
                    });

                    $.ajax({
                        url: 'api/Account/Getuserbyleader',
                        beforeSend: function (request) {
                            request.withCredentials = true;
                            request.setRequestHeader("Authorization", authdata);
                        },
                        success: function (resultData) {
                            EmployeeList = resultData;
                            prepare_external_list();
                        }
                    });

                    $.ajax({
                        url: '/api/school/all',
                        beforeSend: function (request) {
                            request.withCredentials = true;
                            request.setRequestHeader("Authorization", authdata);
                        },
                        success: function (resultData) {
                            classroom = resultData;
                        }
                    });
                }

                var EmployeeList = [];
                var lessionList = [];
                var classroom = [];
                var calendar = $('#calendar').fullCalendar({
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,agendaWeek,agendaDay'
                    },
                    eventRender: function (event, element) {
                        $(element).tooltip({ title: event.title, container: "body" });
                    },
                    eventLimit: true,
                    editable: true,
                    eventSources: {

                        beforeSend: function (request) {
                            request.withCredentials = true;
                            request.setRequestHeader("Authorization", authdata);
                        },
                        url: "/api/calendars/searchr"
                    },
                    droppable: true,
                    selectHelper: true,
                    //select: function (start, end, allDay) {
                    //    var title = prompt('Event Title:');
                    //    if (title) {
                    //        calendar.fullCalendar('renderEvent',
                    //        {
                    //            title: title,
                    //            start: start,
                    //            end: end,
                    //            allDay: allDay
                    //        },
                    //        true
                    //        );
                    //    }
                    //    calendar.fullCalendar('unselect');
                    //},
                    drop: function (date, allDay, ID) {

                        var originalEventObject = $(this).data('eventObject');
                        var copiedEventObject = $.extend({}, originalEventObject);
                        var calendar = {
                            "title": originalEventObject.title,
                            "Description": originalEventObject.title,
                            "start": date._d,
                            "end": date._d,
                            "allDay": 0
                        };
                        copiedEventObject.start = date;
                        copiedEventObject.allDay = allDay;
                        copiedEventObject.backgroundColor = $(this).css("background-color");
                        copiedEventObject.borderColor = $(this).css("border-color");
                        jQuery.ajax({
                            type: 'POST',
                            dataType: 'json',
                            beforeSend: function (request) {
                                request.withCredentials = true;
                                request.setRequestHeader("Authorization", authdata);
                            },
                            url: 'api/calendars/Adddrag',
                            contentType: 'application/json',
                            data: JSON.stringify(calendar),
                            success: function (data) {
                                copiedEventObject.ID = data.ID;

                                $('#editModal').modal();
                                $('#editcalendar').val(copiedEventObject.ID);
                                $('#edittearcher').val(copiedEventObject.title);
                                $('#editlession').empty();
                                $('#editdatetime').val(getFormattedDate2(data.start));
                                $('#edittoday').val(data.start);
                                //var optionlessionedit = '<option value="0">Chọn tiết</option>';
                                var optionlessionedit = '';
                                lessionList.forEach(function (item) {
                                    optionlessionedit += '<option value="' + item.Id + '">' + item.Description + '</option>';
                                })
                                $('#editlession').append(optionlessionedit);

                                $('#editcboclass').empty();
                                //var optionclassedit = '<option value="0">Chọn lớp</option>';
                                var optionclassedit = '';
                                classroom.forEach(function (item) {
                                    optionclassedit += '<option value="' + item.Id + '">' + item.Name + '(' + item.Description + ')</option>';
                                })
                                $('#editcboclass').append(optionclassedit);
                                //$('#calendar').fullCalendar('renderEvent', copiedEventObject, true);
                            },
                            error: function () {
                                alert("error");
                            },
                        });

                    },
                    eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {

                        var calendar = {
                            "title": event.title,
                            "Description": event.title,
                            "start": event.start._d,
                            "end": event.start._d,
                            "allDay": 0,
                            "CalendarId": event.ID,
                            "today": event.start._d,
                        };
                        jQuery.ajax({
                            type: 'POST',
                            beforeSend: function (request) {
                                request.withCredentials = true;
                                request.setRequestHeader("Authorization", authdata);
                            },
                            dataType: 'json',
                            url: 'api/calendars/updatedrag',
                            contentType: 'application/json',
                            data: JSON.stringify(calendar),
                            success: function (data) {
                                $('#calendar').fullCalendar('refetchEvents');
                            },
                            error: function () {
                                revertFunc();
                            },
                        });


                    },
                    eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {

                        jQuery.ajax({
                            type: 'POST',
                            beforeSend: function (request) {
                                request.withCredentials = true;
                                request.setRequestHeader("Authorization", authdata);
                            },
                            dataType: 'json',
                            url: 'api/calendars/update',
                            contentType: 'application/json',
                            data: JSON.stringify(event),
                            success: function (data) {

                            },
                            error: function () {
                                revertFunc();
                            },
                        });



                    },
                    eventClick: function (calEvent, jsEvent, view) {

                        var eventID = calEvent.ID;
                        var today = calEvent.start._i;

                        $('#editModal').modal();
                        $('#editcalendar').val(eventID);
                        $('#edittearcher').val(calEvent.title);
                        $('#edittoday').val(today);
                        $('#editlession').empty();
                        $('#editdatetime').val(getFormattedDate2(today));
                        //var optionlessionedit = '<option value="0">Chọn tiết</option>';
                        var optionlessionedit = '';
                        lessionList.forEach(function (item) {
                            optionlessionedit += '<option value="' + item.Id + '">' + item.Description + '</option>';
                        })
                        $('#editlession').append(optionlessionedit);

                        $('#editcboclass').empty();
                        //var optionclassedit = '<option value="0">Chọn lớp</option>';
                        var optionclassedit = '';
                        classroom.forEach(function (item) {
                            optionclassedit += '<option value="' + item.Id + '">' + item.Name + '(' + item.Description + ')</option>';
                        })
                        $('#editcboclass').append(optionclassedit);



                    },
                    dayClick: function (date, allDay, jsEvent, view) {
                        $('#newcboEmployees').empty();
                        //var option = '<option value="0">Chọn giáo viên</option>';
                        var option = '';
                        EmployeeList.forEach(function (item) {
                            option += '<option value="' + item.Id + '">' + item.Username + '</option>';
                        })
                        $('#newcboEmployees').append(option);

                        $('#newcbolession').empty();
                        //var optionlession = '<option value="0">Chọn tiết</option>';
                        var optionlession = '';
                        lessionList.forEach(function (item) {
                            optionlession += '<option value="' + item.Id + '">' + item.Description + '</option>';
                        })
                        $('#newcbolession').append(optionlession);

                        $('#newcboclass').empty();
                        //var optionclass = '<option value="0">Chọn lớp</option>';
                        var optionclass = '';
                        classroom.forEach(function (item) {
                            optionclass += '<option value="' + item.Id + '">' + item.Name + '(' + item.Description + ')</option>';
                        })
                        $('#newcboclass').append(optionclass);


                        $('#newSelectedModal').modal();

                        $('#newdatescheduled').val(getFormattedDate(date));
                        $('#datetime').val(getFormattedDate2(date));


                    }
                });
                prepare_external_list();
                //$("#new-event").on("click",function(){
                //    var et = $("#new-event-text").val();
                //    if(et != ''){
                //        $("#external-events").prepend('<a class="list-group-item external-event">'+et+'</a>');
                //        prepare_external_list();
                //    }
                //});
            }
            $('#newsubmitButton').on('click', function (e) {
                var dayclick = new Date($('#newdatescheduled').val()).toISOString();

                var UserID = $('#newcboEmployees').val();
                var lesstion = $('#newcbolession').val();
                var classID = $('#newcboclass').val();
                var soluongchau = $('#newsoluongchau').val() || 0;
                $('#newSelectedModal').modal('hide');
                var calendar = {
                    "soluongchau": soluongchau,
                    "Description": '',
                    "lessionID": lesstion,
                    "SchoolId": classID,
                    "allDay": 0,
                    "UserID": UserID,
                    "today": dayclick,
                };

                jQuery.ajax({
                    type: 'POST',
                    beforeSend: function (request) {
                        request.withCredentials = true;
                        request.setRequestHeader("Authorization", authdata);
                    },
                    dataType: 'json',
                    url: 'api/calendars/add',
                    contentType: 'application/json',
                    data: JSON.stringify(calendar),
                    success: function (data) {
                        $('#calendar').fullCalendar('refetchEvents');
                    },
                    error: function () {

                    },
                });
            });


            $('#editsubmitButton').on('click', function (e) {
                var ID = $('#editcalendar').val();
                var editclassid = $('#editcboclass').val();
                var editlessionid = $('#editlession').val();
                var today = $('#edittoday').val();
                var soluongchau = $('#soluongchau').val() || 0;
                var calendar = {
                    "CalendarId": ID,
                    "soluongchau": soluongchau,
                    "start": '',
                    "end": '',
                    "allDay": 0,
                    "UserID": '',
                    "today": today,
                    "lessionID": editlessionid,
                    "SchoolId": editclassid
                };

                jQuery.ajax({
                    type: 'POST',
                    beforeSend: function (request) {
                        request.withCredentials = true;
                        request.setRequestHeader("Authorization", authdata);
                    },
                    dataType: 'json',
                    url: 'api/calendars/update',
                    contentType: 'application/json',
                    data: JSON.stringify(calendar),
                    success: function (data) {
                        $('#editModal').modal('hide');
                        $('#calendar').fullCalendar('refetchEvents');

                    },
                    error: function () {

                    },
                });
            });


            $('#delete').on('click', function (e) {
                var ID = $('#editcalendar').val();
                jQuery.ajax({
                    type: 'POST',
                    beforeSend: function (request) {
                        request.withCredentials = true;
                        request.setRequestHeader("Authorization", authdata);
                    },
                    dataType: 'json',
                    url: 'api/calendars/delete/' + ID + '',
                    contentType: 'application/json',
                    data: JSON.stringify(calendar),
                    success: function (data) {
                        $('#calendar').fullCalendar('refetchEvents');
                    },
                    error: function () {

                    },
                });
            });
        }
        return {
            init: function () {
                calendar();
            }
        }
    }();

    formElements.init();
    uiElements.init();
    templatePlugins.init();
    fullCalendar.init();
    /* My Custom Progressbar */
    //$.mpb = function (action, options) {

    //    var settings = $.extend({
    //        state: '',
    //        value: [0, 0],
    //        position: '',
    //        speed: 20,
    //        complete: null
    //    }, options);

    //    if (action == 'show' || action == 'update') {

    //        if (action == 'show') {
    //            $(".mpb").remove();
    //            var mpb = '<div class="mpb ' + settings.position + '">\n\
    //                           <div class="mpb-progress'+ (settings.state != '' ? ' mpb-' + settings.state : '') + '" style="width:' + settings.value[0] + '%;"></div>\n\
    //                       </div>';
    //            $('body').append(mpb);
    //        }

    //        var i = $.isArray(settings.value) ? settings.value[0] : $(".mpb .mpb-progress").width();
    //        var to = $.isArray(settings.value) ? settings.value[1] : settings.value;

    //        var timer = setInterval(function () {
    //            $(".mpb .mpb-progress").css('width', i + '%'); i++;

    //            if (i > to) {
    //                clearInterval(timer);
    //                if ($.isFunction(settings.complete)) {
    //                    settings.complete.call(this);
    //                }
    //            }
    //        }, settings.speed);

    //    }

    //    if (action == 'destroy') {
    //        $(".mpb").remove();
    //    }

    //}
    /* Eof My Custom Progressbar */


    // New selector case insensivity        
    $.expr[':'].containsi = function (a, i, m) {
        return jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
    };
    function getFormattedDate(date) {
        var today = new Date(date);
        var dd = today.getDate();
        var mm = today.getMonth(); //January is 0!
        var hh = today.getHours();
        var min = today.getMinutes();
        if (hh == 0)
            hh = '00';
        if (min == 0)
            min = '00';
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }

        var month = new Array();
        month[0] = "Jan";
        month[1] = "Feb";
        month[2] = "Mar";
        month[3] = "Apr";
        month[4] = "May";
        month[5] = "Jun";
        month[6] = "Jul";
        month[7] = "Aug";
        month[8] = "Sep";
        month[9] = "Oct";
        month[10] = "Nov";
        month[11] = "Dec";

        var today = dd + '/' + month[mm] + '/' + yyyy + ' ' + hh + ':' + min;
        return today;
    };
    function getFormattedDate2(date) {
        var today = new Date(date);
        var dd = today.getDate();
        var mm = today.getMonth(); //January is 0!
        var hh = today.getHours();
        var min = today.getMinutes();
        if (hh == 0)
            hh = '00';
        if (min == 0)
            min = '00';
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }

        var month = new Array();
        month[0] = "Jan";
        month[1] = "Feb";
        month[2] = "Mar";
        month[3] = "Apr";
        month[4] = "May";
        month[5] = "Jun";
        month[6] = "Jul";
        month[7] = "Aug";
        month[8] = "Sep";
        month[9] = "Oct";
        month[10] = "Nov";
        month[11] = "Dec";

        var today = dd + '-' + month[mm] + '-' + yyyy;
        return today;
    };
});

Object.size = function (obj) {
    var size = 0, key;
    for (key in obj) {
        if (obj.hasOwnProperty(key)) size++;
    }
    return size;
};
