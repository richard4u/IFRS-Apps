/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CurrencyListController",
            ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        CurrencyListController]);

    function CurrencyListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'currency-list-view';
        vm.viewName = 'Currencies';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        var exportTable;
        vm.currency = {};
        vm.currencies = [];
        vm.init = false;
        vm.showInstruction = true;
        vm.instruction = 'After Adding a Currency, you will have to Add a Rate to the Currency before using it.';

        var currencyRules = [];

        var setupRules = function () {

            currencyRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            currencyRules.push(new validator.PropertyRule("Symbol", {
                required: { message: "Symbol is required" }
            }));
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.currency, currencyRules);
            vm.viewModelHelper.modelIsValid = vm.currency.isValid;
            vm.viewModelHelper.modelErrors = vm.currency.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/currency/updatecurrency', vm.currency,
                    function (result) {

                        $state.go('core-currency-list');
                        vm.refresh();
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.currency.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.refreshcode = function () {
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/currency/availablecurrencies', null,
                    function (result) {
                        vm.currencies = result.data;
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);

                vm.currency = { Name: '', Symbol: '', Active: true };
            }
        }


        var initialize = function () {
            vm.refreshcode();
            InitialView();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#currencyTable').length > 0) {
                    exportTable = $('#currencyTable').DataTable({
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
            }, 50);
        }

        initialize(); 

        vm.refresh = function () {
            vm.init = false;
            //vm.searchParam = '';
            vm.refreshcode();
            //exportTable.destroy();
        }

        vm.delete = function (CurrencyId) {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/currency/deletecurrency', CurrencyId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('core-currency-list');
                        vm.refresh();
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }
    }
}());
