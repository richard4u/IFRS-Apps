/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SegmentPerformanceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SegmentPerformanceEditController]);

    function SegmentPerformanceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'segmentperformance-edit-view';
        vm.viewName = 'Segment Performance';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.segmentperformance = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var segmentperformanceRules = [];


        var setupRules = function () {

            segmentperformanceRules.push(new validator.PropertyRule("SegmentName", {
                required: { message: "Segment Name is required" }
            }));

            segmentperformanceRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.macroeconomicnplId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/segmentperformance/getsegmentperformance/' + $stateParams.segmentId, null,
                   function (result) {
                       vm.segmentperformance = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.segmentperformance = { SegmentCode: 0, SegmentName: 0, Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.segmentperformance, segmentperformanceRules);
            vm.viewModelHelper.modelIsValid = vm.segmentperformance.isValid;
            vm.viewModelHelper.modelErrors = vm.segmentperformance.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/segmentperformance/updatesegmentperformance', vm.segmentperformance,
               function (result) {

                   $state.go('ifrs9-segmentperformance-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.segmentperformance.errors;

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
                vm.viewModelHelper.apiPost('api/segmentperformance/deletesegmentperformance', vm.segmentperformance.SegmentId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-segmentperformance-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-segmentperformance-list');
        };

        setupRules();
        initialize();
    }
}());
