/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpenClosedPeriodListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator', 'closedPeriodSearchService',
                        OpenClosedPeriodListController]);

    function OpenClosedPeriodListController($scope, $state, viewModelHelper, validator, closedPeriodSearchService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'OpenClosedPeriod-list-view';
        vm.viewName = 'Open Closed Period';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.closedPeriods = [];
        vm.selectedSolutionId = 2;
        vm.selectedYear = {};
        vm.solutionRunDate;
        var exportTable;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var intializeLookUp = function () {
            vm.viewModelHelper.apiGet('api/solutionrundate/availablesolutionrundates',
                null,
                (result) => {
                    vm.solutionRunDate = result.data[0].RunDate;                    
                    getYears();
                    vm.selectedYear = vm.solutionRunDate.substring(0, 4);
                    if (vm.init === false) {
                        vm.viewModelHelper.apiGet('api/closedperiod/getclosedperiodtobeopenbylogin/' + vm.selectedSolutionId + '/' + vm.selectedYear, null,
                            
                            function (result) {
                                vm.closedPeriods = result.data;
                                InitialView();
                                vm.init === true;

                            },
                            function (result) {
                                toastr.error(result.data, 'Fintrak');
                            }, null);
                        vm.init === true;
                    }
                },
                (error) => {
                    toastr.error(error, 'Unable to fetch branches');
                },
                null);
        };




        var initialize = function(){
            //vm.selectedSolutionId = closedPeriodSearchService.getSearchModel().solutionId;
           // vm.selectedYear = closedPeriodSearchService.getSearchModel().year;
          //  getActiveYear();
            //if (vm.init === false) {
            //    //getSolutions();
            //    getYears();
            //    vm.currentperiod();
            //    vm.viewModelHelper.apiGet('api/OpenOpenClosedPeriod/getclosedperiodbylogin/' + vm.selectedSolutionId + '/' + vm.selectedYear, null,
            //       function (result) {
            //           vm.closedPeriods = result.data;
            //           InitialView();
            //           vm.init === true;
                       
            //       },
            //     function (result) {
            //         toastr.error(result.data, 'Fintrak');
            //     }, null);
            //}
            intializeLookUp();
        }

        var InitialView = function () {            
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#closedPeriodTable').length > 0) {
                    exportTable = $('#closedPeriodTable').DataTable({
                        "lengthMenu": [[10, 20, 50, 100, -1], [10, 20, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        vm.load = function () {
            closedPeriodSearchService.setSearchModel({ solutionId: vm.selectedSolutionId,year:vm.selectedYear })
            vm.viewModelHelper.apiGet('api/closedperiod/getclosedperiodtobeopenbylogin/' + vm.selectedSolutionId + '/' + vm.selectedYear, null,
                   function (result) {
                       vm.closedPeriods = result.data;
                       InitialView();
                       exportTable.destroy();
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        vm.getStatus = function (status) {
            if (status)
                return 'Active';
            else
                return 'In-Active';
        }

        vm.openDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate = true;
        }

        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getYears = function () {
            vm.viewModelHelper.apiGet('api/fiscalYear/getyears', null,
                 function (result) {
                     vm.years = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load years.', 'Fintrak');
                 }, null);
        }

        vm.currentperiod = function () {
                vm.viewModelHelper.apiGet('api/solutionrundate/availablesolutionrundates', null,
                    function (result) {
                        vm.solutionRunDate = result.data[0].RunDate;
                        vm.selectedYear = vm.solutionRunDate.substring(0, 4);                    
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
        }

        var getActiveYear = function () {
            vm.viewModelHelper.apiGet('api/solutionrundate/availablesolutionrundates',
                null,
                function (result) {
                    vm.solutionRunDate = result.data[0].RunDate;
                    vm.selectedYear = vm.solutionRunDate.substring(0, 4);
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        initialize(); 
    }
}());
