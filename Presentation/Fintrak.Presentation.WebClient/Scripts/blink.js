﻿(function () {
    "use strict";
    angular
        .module("fintrak")
         .directive('blink', ['$interval', function ($interval) {
             return function (scope, element, attrs) {
                 var timeoutId;

                 var blink = function () {
                     element.css('visibility') === 'hidden' ? element.css('visibility', 'inherit') : element.css('visibility', 'hidden');
                 }

                 timeoutId = $interval(function () {
                     blink();
                 }, 1000);

                 element.css({
                     'display': 'inline-block'
                 });

                 element.on('$destroy', function () {
                     $interval.cancel(timeoutId);
                 });
             };
         }])
        .controller("FairValuationModelListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        FairValuationModelListController]);

    function FairValuationModelListController($scope, $state, viewModelHelper, validator) {

        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'fairvaluationmodel-list-view';
        vm.viewName = 'Fair Valuation Models Inventory';

        //vm.viewModelHelper.modelIsValid = true;
        //vm.viewModelHelper.modelErrors = [];

        //vm.fairvaluationmodels = [];

        //vm.init = false;
        //vm.showInstruction = false;
        //vm.instruction = '';

        //var initialize = function () {

        //    if (vm.init === false) {
        //        vm.viewModelHelper.apiGet('api/fairvaluationmodel/availablefairvaluationmodels', null,
        //           function (result) {
        //               vm.fairvaluationmodels = result.data;
        //               InitialView();
        //               vm.init === true;

        //           },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //    }
        //}

        //var InitialView = function () {
        //    InitialGrid();
        //}

        //var InitialGrid = function () {
        //    setTimeout(function () {

        //        // data export
        //        if ($('#fairvaluationmodelTable').length > 0) {
        //            var exportTable = $('#fairvaluationmodelTable').DataTable({
        //                "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
        //                sDom: "T<'clearfix'>" +
        //                    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
        //                    "t" +
        //                    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
        //                "tableTools": {
        //                    "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
        //                }
        //            });
        //        }
        //    }, 50);
        //}

        //initialize();




    }
}());



//angular.module('blink', [])
//   .directive('blink', ['$interval', function ($interval) {
//       return function (scope, element, attrs) {
//           var timeoutId;

//           var blink = function () {
//               element.css('visibility') === 'hidden' ? element.css('visibility', 'inherit') : element.css('visibility', 'hidden');
//           }

//           timeoutId = $interval(function () {
//               blink();
//           }, 1000);

//           element.css({
//               'display': 'inline-block'
//           });

//           element.on('$destroy', function () {
//               $interval.cancel(timeoutId);
//           });
//       };
//   }]);