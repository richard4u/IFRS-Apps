
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsloanmisspaymentEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
            IfrsloanmisspaymentEditController]);

    function IfrsloanmisspaymentEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsloanmisspayment-edit-view';
        vm.viewName = 'Ifrs loan misspayment';
        vm.showChildren = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.Ifrsloanmisspayment = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var IfrsloanmisspaymentRules = [];

        var setupRules = function () {

            IfrsloanmisspaymentRules.push(new validator.PropertyRule("SICR_Param", {
                required: { message: "SICR Parameter required" }
            }));

            IfrsloanmisspaymentRules.push(new validator.PropertyRule("SICR_Desc", {
                required: { message: "SICR Description required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/Ifrsloanmisspayment/getIfrsloanmisspayment/' + $stateParams.ID, null,
                        function (result) {
                            vm.Ifrsloanmisspayment = result.data;
                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else

                    vm.MissedPaymentResults = { DefaultParam: '', DefaultType: '', DaysPastDue: '', Active: true };
                   // vm.Ifrsloanmisspayment = { Threshold: 0, Deteroriation_Level: 0, Classification_Type: 0, Active: true };
            }
        }

        var intializeLookUp = function () {

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.Ifrsloanmisspayment, IfrsloanmisspaymentRules);
            vm.viewModelHelper.modelIsValid = vm.Ifrsloanmisspayment.isValid;
            vm.viewModelHelper.modelErrors = vm.Ifrsloanmisspayment.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/Ifrsloanmisspayment/updateIfrsloanmisspayment', vm.Ifrsloanmisspayment,
                    function (result) {

                        $state.go('Ifrsloanmisspayment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.Ifrsloanmisspayment.errors;

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
                vm.viewModelHelper.apiPost('api/Ifrsloanmisspayment/deleteIfrsloanmisspayment', vm.Ifrsloanmisspayment.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('Ifrsloanmisspayment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        vm.cancel = function () {
            $state.go('Ifrsloanmisspayment-list');
        };


        //  IfrsloanmisspaymentRules();
        initialize();
    }
}());

