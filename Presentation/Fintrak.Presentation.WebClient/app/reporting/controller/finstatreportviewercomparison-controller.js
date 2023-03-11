/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FinstatReportViewerComparisonController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                FinstatReportViewerComparisonController]);

    function FinstatReportViewerComparisonController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Finstat';
        vm.view = 'finstat-reportviewercomparison-view';
        vm.viewName = 'FinstatReportViewerComparison';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;

        vm.found = false;
        vm.count = 0;
        vm.defaultCount = 10;

        vm.currency = {};
        vm.currencies = [];
        vm.closedPeriods = [];
        vm.selectedSolutionId = 2;
        vm.selectedYear = {};
        //vm.solutionRunDate;
        vm.closedPeriodsmodel2 = "1900-01-01";
        vm.prevclosedPeriodsmodel2 = "1900-01-01";
        vm.currenciesmodel2 = "NGN";
        vm.budgettypemodel2 = "1";
        vm.budgettype = [{ id: "1", name: "Board Budget" }, { id: "0", name: "Stretch Budget" }];


        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/currencyRate/getcurrencybyDate', null,
                    function (result) {
                        vm.currencies = result.data;
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);

                vm.viewModelHelper.apiGet('api/closedperiod/closedPeriodsCount/' + vm.defaultCount, null,

                    function (result) {
                        vm.closedPeriods = result.data;
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);

                vm.viewModelHelper.apiGet('api/solutionrundate/availablesolutionrundates', null,
                    function (result) {
                        vm.solutionRunDate = result.data[0].RunDate;
                        vm.selectedYear = vm.solutionRunDate.substring(0, 4);
                        //vm.closedPeriodsmodel2 = vm.solutionRunDate;
                        InitialView();
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);

                vm.init === true;

            }

        }

        initialize();

        //vm.closedPeriodsmodel2 = vm.solutionRunDate;

        var InitialView = function () {
            vm.closedPeriodsmodel2 = vm.solutionRunDate;
            vm.prevclosedPeriodsmodel2 = vm.solutionRunDate;
            vm.currenciesmodel2 = "NGN";
            //vm.budgettypemodel2 = '1';
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

        //var defaultreportSet004 = function () {
        //    vm.closedPeriodsmodel2 = vm.solutionRunDate;
        //    vm.currenciesmodel2 = "NGN";
        //    vm.budgettypemodel2 = '1';
        //}

        vm.checkinputLength = function () {
            var inputrunDate = document.getElementById("runDate");
            var inputprevrunDate = document.getElementById("prevrunDate");
            var inputCurrency = document.getElementById("Currency");

            if (inputrunDate.value.length >= 9 && inputCurrency.value.length >= 1 && inputprevrunDate.value.length >= 9) {
                var stat = true;
                return stat;
            }
            else {
                var stat = false;
                alert('Please ensure the Report Date, Previous Report Date and Currency are not Empty');
                inputrunDate.focus;
                inputCurrency.focus;
                inputprevrunDate.focus;
                return stat;
            }

        }

        vm.reportSet004 = function () {
            if (vm.checkinputLength() == true) {
                vm.closedPeriodsmodel2 = vm.closedPeriodsmodel;
                vm.prevclosedPeriodsmodel2 = vm.prevclosedPeriodsmodel;
                vm.currenciesmodel2 = vm.currenciesmodel;
                //vm.budgettypemodel2 = vm.budgettypemodel;
            }
        }

    }
}());
