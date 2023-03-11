/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FinstatReportViewerTrendController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                FinstatReportViewerTrendController]);

    function FinstatReportViewerTrendController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Finstat';
        vm.view = 'finstat-reportviewertrend-view';
        vm.viewName = 'FinstatReportViewerTrend';

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
        vm.currenciesmodel2 = "NGN";
        vm.budgettypemodel2 = "1";
        vm.durationTypemodel2 = "Monthly";
        vm.durationType = [ "Daily", "Weekly", "Monthly", "Quarterly" ];


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
            vm.currenciesmodel2 = "NGN";
            vm.durationTypemodel2 = "Monthly";
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
            var inputCurrency = document.getElementById("Currency");
            var inputdurationType = document.getElementById("durationType");

            if (inputrunDate.value.length >= 5 && inputCurrency.value.length >= 1 && inputdurationType.value.length >= 3) {
                var stat = true;
                return stat;
            }
            else {
                var stat = false;
                alert('Please ensure the Report Date, Currency and Duration Type are not Empty');
                inputrunDate.focus;
                inputCurrency.focus;
                inputdurationType.focus;
                return stat;
            }

        }

        vm.reportSet004 = function () {
            if (vm.checkinputLength() == true) {
                vm.closedPeriodsmodel2 = vm.closedPeriodsmodel;
                vm.currenciesmodel2 = vm.currenciesmodel;
                vm.durationTypemodel2 = vm.durationTypemodel;
                //vm.budgettypemodel2 = vm.budgettypemodel;
            }
        }

    }
}());
