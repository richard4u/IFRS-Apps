/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroeconomicForecastEditController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        MacroeconomicForecastEditController]);

    function MacroeconomicForecastEditController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomicforecast-edit-view';
        vm.viewName = 'MacroEconomic ForeCast';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

 
        vm.MacroResults = {};
        vm.init = false;
      

       

        var macroeconomicforecast = [];
  
        var setupRules = function () {

            macroeconomicforecast.push(new validator.PropertyRule("Coefficient", {
                required: { message: "Coefficient is required" }
            }));

            macroeconomicforecast.push(new validator.PropertyRule("Baseline_InflationRate", {
                required: { message: "Baseline_InflationRate" }
            }));

            //macroeconomicforecast.push(new validator.PropertyRule("Variable1", {
            //    required: { message: "lst Variable is required" }
            //}));

            //macroeconomicforecast.push(new validator.PropertyRule("Variable2", {
            //    required: { message: "2nd Variable is required" }
            //}));



        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macroeconomicforecast/getmacroeconomicforecast/' + $stateParams.Id, null,
                        function (result) {
                            vm.MacroResults= result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                  // var macroeconomicforecast  = { Year: 0, Sector_Code: '', Variable1: 0, Variable2: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            // getSectors()

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.MacroResults, macroeconomicforecast);
            vm.viewModelHelper.modelIsValid = vm.MacroResults.isValid;
            vm.viewModelHelper.modelErrors = vm.MacroResults.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/macroeconomicforecast/updatemacroeconomicforecast' + vm.MacroResults.Id,
                    function (result) {

                        $state.go('ifrs9-macroeconomicforecast-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.MacroResultserrors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/macroeconomicforecast/deletemacroeconomicforecast', vm.MacroResults.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('ifrs9-macroeconomicforecast-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-macroeconomicforecast-list');
        };


       
        setupRules();

        initialize();
    }
}());
