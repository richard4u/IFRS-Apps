/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CurrencyRateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CurrencyRateEditController]);

    function CurrencyRateEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'currencyrate-edit-view';
        vm.viewName = 'Currency Rate';

        vm.currencyRate = {};

        vm.openedDate = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        var exportTable;
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.showChildren = false;

        vm.rateTypes = [];

        vm.frequencies = [
          { Id: 'M', Name: 'Monthly' },
          { Id: 'Q', Name: 'Quaterly' },
          { Id: 'Y', Name: 'Yearly' }
        ];

        var currencyRateRules = [];

        var setupRules = function () {
           
            currencyRateRules.push(new validator.PropertyRule("RateTypeId", {
                notZero: { message: "Rate Type is required" }
            }));

            currencyRateRules.push(new validator.PropertyRule("Date", {
                mustBeDate: { message: "Please enter a valid date" }
            }));

            currencyRateRules.push(new validator.PropertyRule("CurrencyId", {
                notZero: { message: "Currency is required" }
            }));
        }

        var initialize = function () {
            if (vm.init == false) {
                initializeLookUp();

                if ($stateParams.currencyrateId != 0) {
                    vm.viewModelHelper.apiGet('api/currencyrate/getcurrencyrate/' + $stateParams.currencyrateId, null,
                   function (result) {
                       vm.currencyRate = result.data;
                       initialView();
                       vm.init === true;
                       window.scrollTo(0, 0);
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.currencyRate = { RateTypeId: 0, Frequency: '', Date: new Date(), Rundate: new Date(), CurrencyId: $stateParams.currencyId, Active: true };

                if ($stateParams.currencyId != 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/currency/getcurrencywithchildrens/' + $stateParams.currencyId, null,
                        function (result) {
                            vm.currency = result.data.Currency;
                            vm.currencyRates = result.data.CurrencyRates;
                            //initialView();
                            //vm.init = true;
                            //
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.currency = { Name: '', Symbol: '', Active: true };
            }
        }

        var initializeLookUp = function () {
            getRateTypes();
        }

        var initialView = function () {
            InitialGrid();
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.currencyRate, currencyRateRules);
            vm.viewModelHelper.modelIsValid = vm.currencyRate.isValid;
            vm.viewModelHelper.modelErrors = vm.currencyRate.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/currencyrate/updatecurrencyrate', vm.currencyRate,
               function (result) {

                   $state.go('core-currencyrate-edit', { currencyId: vm.currencyRate.CurrencyId, currencyrateId: 0 });
                   vm.refresh();
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.currencyRate.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function (CurrencyRateId) {
            var deleteFlag = $window.confirm('Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/currencyrate/deletecurrencyrate', CurrencyRateId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-currencyrate-edit', { currencyId: $stateParams.currencyId, currencyrateId: 0 });
                  vm.refresh();
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-currency-list');
        }

        vm.openDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate = true;
        }

        var getRateTypes = function () {
            vm.viewModelHelper.apiGet('api/ratetype/availableratetypes', null,
                 function (result) {
                     vm.rateTypes = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load rate types.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#currencyRateTable').length > 0) {
                    exportTable = $('#currencyRateTable').DataTable({
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

        vm.refresh = function () {
            vm.init = false;
            //vm.searchParam = '';
            initialize();
            //exportTable.destroy();
        }
    }
}());
