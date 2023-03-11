/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProcessHistoryListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                ProcessHistoryListController]);

    function ProcessHistoryListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'processhistory-list-view';
        vm.viewName = 'ProcessHistory';

        var tabID = 'processhistoryTable'
        var exportTable;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.defaultCount = 10;
        vm.ProcessHistory = [];
        vm.startDate = new Date();
        vm.endDate = new Date();
        vm.runTime = new Date();
        vm.processhistoryrun = [];

        vm.processDate = null;
        vm.endDate = new Date();
        vm.init = false;
        vm.showInstruction = true;

        vm.found = false;
        vm.count = 0;

        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded.';


        var initializeUploads = function () {
            getUploads();

        }

        vm.checkAll = function () {

            if (checkall.checked == true) {
                angular.forEach(vm.processhistoryrun, function (process) {
                    process.CanRun = true;

                });
            }
            else {
                angular.forEach(vm.processhistoryrun, function (process) {
                    process.CanRun = false;

                });

            }
        }

        //vm.checkall = function () {
        //    $('#select-all').click(function (event) {
        //        if (this.checked) {
        //            // Iterate each checkbox
        //            $(':checkbox').each(function () {
        //                this.checked = true;
        //            });
        //        } else {
        //            $(':checkbox').each(function () {
        //                this.checked = false;
        //            });
        //        }
        //    });
        //}

        var initialize = function () {
            if (vm.init == false) {
                soluDate();
                initializeUploads();

                vm.viewModelHelper.apiGet('api/processhistory/availableprocesshistorys/' + vm.defaultCount, null,
                    function (result) {
                        vm.ProcessHistory = result.data;
                        //InitialView('processhistoryTable');
                        vm.searchParam = '';

                        if (vm.init == true) {
                            //exportTable.destroy();
                        } else vm.init = true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        var getUploads = function () {
            vm.viewModelHelper.apiGet('api/processhistory/getprocesshistoryrun', null,
                function (result) {
                    vm.processhistoryrun = result.data;
                    vm.status = 'false';
                },
                function (result) {
                    toastr.error('Unable to load upload Process', 'Fintrak Error');
                }, null);
        }

        //vm.runprocess = function () {
        //    vm.viewModelHelper.apiPost('api/processhistory/runprocesshistory/', 1,
        //            function (result) {
        //                vm.uploadResults = result.data;
        //                toastr.success(' uploaded successfully.', 'Fintrak Upload');
        //            },
        //            function (result) {
        //                toastr.error('Fail to run upload', 'Fintrak Error');
        //            }, null);
        //}

        vm.runprocess = function () {

            var processIds = [];
            for (var i = 0; i < vm.processhistoryrun.length; i++) {
                //alert(vm.processhistoryrun[i].ProcessTitle);
                if (vm.processhistoryrun[i].ProcessHistoryRunId) {

                    if (vm.processhistoryrun[i].ProcessTitle == '1- Process Financials') {
                        alert("Process Financials");
                        if (vm.unMappedGLs.length > 0) {
                            toastr.warning('Could not process "Financials". Map the GLs to continue!<br/><br/> Click to view the GLs', 'Unmapped GLs exist', {
                                timeOut: 0, extendedTimeOut: 0, closeButton: true, tapToDismiss: false, onclick: function () {
                                    $state.go('finstat-unmappedgl-list');
                                }
                            });

                            return;
                        }
                    }
                    processIds.push(vm.processhistoryrun[i].ProcessHistoryRunId);
                }
            };

            //Test 
            vm.viewModelHelper.modelIsValid = true;
            vm.viewModelHelper.modelErrors = [];

            vm.viewModelHelper.apiPost('api/processhistory/runprocesshistory', processIds,
                function (result) {
                    vm.uploadResults = result.data;
                    toastr.success('Process Run Succesfully.', 'Fintrak Process');
                    vm.refresh();
                    //$state.go('core-processhistory-list');
                },
                function (result) {
                    toastr.error('Fail to run some processes.', 'Fintrak');
                }, null);

            //  check();
        }

        vm.loadProcessHistory = function () {
            vm.viewModelHelper.apiGet('api/processhistory/availableprocesshistorys/' + vm.defaultCount, null,
                function (result) {
                    vm.ProcessHistory = result.data;
                    toastr.success('Process History loaded successfully.', 'Fintrak');
                    InitialGrid();
                    //exportTable.destroy();
                },
                function (result) {
                    vm.ProcessHistory = [];
                    toastr.error('Fail to load triggers.', 'Fintrak');
                }, null);
        }

        var InitialView = function (tableId) {
            InitialGrid(tableId);
        };

        ////////
        //onload = function () {
        //    setFormatFromInput();
        //    function setFormatFromInput() {
        //        outputDiv.innerHTML = new Date().format(input.value);
        //    }
        //};

        function formatDate(date, format, utc) {
            var MMMM = ["\x00", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            var MMM = ["\x01", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var dddd = ["\x02", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            var ddd = ["\x03", "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
            function ii(i, len) { var s = i + ""; len = len || 2; while (s.length < len) s = "0" + s; return s; }

            var y = utc ? date.getUTCFullYear() : date.getFullYear();
            format = format.replace(/(^|[^\\])yyyy+/g, "$1" + y);
            format = format.replace(/(^|[^\\])yy/g, "$1" + y.toString().substr(2, 2));
            format = format.replace(/(^|[^\\])y/g, "$1" + y);

            var M = (utc ? date.getUTCMonth() : date.getMonth()) + 1;
            format = format.replace(/(^|[^\\])MMMM+/g, "$1" + MMMM[0]);
            format = format.replace(/(^|[^\\])MMM/g, "$1" + MMM[0]);
            format = format.replace(/(^|[^\\])MM/g, "$1" + ii(M));
            format = format.replace(/(^|[^\\])M/g, "$1" + M);

            var d = utc ? date.getUTCDate() : date.getDate();
            format = format.replace(/(^|[^\\])dddd+/g, "$1" + dddd[0]);
            format = format.replace(/(^|[^\\])ddd/g, "$1" + ddd[0]);
            format = format.replace(/(^|[^\\])dd/g, "$1" + ii(d));
            format = format.replace(/(^|[^\\])d/g, "$1" + d);

            var H = utc ? date.getUTCHours() : date.getHours();
            format = format.replace(/(^|[^\\])HH+/g, "$1" + ii(H));
            format = format.replace(/(^|[^\\])H/g, "$1" + H);

            var h = H > 12 ? H - 12 : H == 0 ? 12 : H;
            format = format.replace(/(^|[^\\])hh+/g, "$1" + ii(h));
            format = format.replace(/(^|[^\\])h/g, "$1" + h);

            var m = utc ? date.getUTCMinutes() : date.getMinutes();
            format = format.replace(/(^|[^\\])mm+/g, "$1" + ii(m));
            format = format.replace(/(^|[^\\])m/g, "$1" + m);

            var s = utc ? date.getUTCSeconds() : date.getSeconds();
            format = format.replace(/(^|[^\\])ss+/g, "$1" + ii(s));
            format = format.replace(/(^|[^\\])s/g, "$1" + s);

            var f = utc ? date.getUTCMilliseconds() : date.getMilliseconds();
            format = format.replace(/(^|[^\\])fff+/g, "$1" + ii(f, 3));
            f = Math.round(f / 10);
            format = format.replace(/(^|[^\\])ff/g, "$1" + ii(f));
            f = Math.round(f / 10);
            format = format.replace(/(^|[^\\])f/g, "$1" + f);

            var T = H < 12 ? "AM" : "PM";
            format = format.replace(/(^|[^\\])TT+/g, "$1" + T);
            format = format.replace(/(^|[^\\])T/g, "$1" + T.charAt(0));

            var t = T.toLowerCase();
            format = format.replace(/(^|[^\\])tt+/g, "$1" + t);
            format = format.replace(/(^|[^\\])t/g, "$1" + t.charAt(0));

            var tz = -date.getTimezoneOffset();
            var K = utc || !tz ? "Z" : tz > 0 ? "+" : "-";
            if (!utc) {
                tz = Math.abs(tz);
                var tzHrs = Math.floor(tz / 60);
                var tzMin = tz % 60;
                K += ii(tzHrs) + ":" + ii(tzMin);
            }
            format = format.replace(/(^|[^\\])K/g, "$1" + K);

            var day = (utc ? date.getUTCDay() : date.getDay()) + 1;
            format = format.replace(new RegExp(dddd[0], "g"), dddd[day]);
            format = format.replace(new RegExp(ddd[0], "g"), ddd[day]);

            format = format.replace(new RegExp(MMMM[0], "g"), MMMM[M]);
            format = format.replace(new RegExp(MMM[0], "g"), MMM[M]);

            format = format.replace(/\\(.)/g, "$1");

            return format;
        };
        /////

        var InitialGrid = function () {

            setTimeout(function () {
                // data export
                if ($('#processhistoryTable').length > 0) {
                    exportTable = $('#processhistoryTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            },
                50);
        };

        vm.openStartDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedStartDate = true;
        }

        vm.openEndDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedEndDate = true;
        }

        vm.openRunTime = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunTime = true;
        }

        var soluDate = function () {
            vm.viewModelHelper.apiGet('api/solutionrundate/availablesolutionrundates', null,
                function (result) {
                    vm.solutionRunDate = result.data[0].RunDate;

                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.refresh = function () {
            vm.init = false;
            vm.searchParam = '';
            initialize();
            //exportTable.destroy();
        }

        initialize();
    }
}());
