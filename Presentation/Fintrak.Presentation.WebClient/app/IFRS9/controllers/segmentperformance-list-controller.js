/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SegmentPerformanceListController",
                    ['$scope', '$state','$window', 'viewModelHelper', 'validator',
                        SegmentPerformanceListController]);

    function SegmentPerformanceListController($scope,$state,$window, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'segmentperformance-list-view';
        vm.viewName = 'Segment Performance';
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.segmentperformance = [];
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
 

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/segmentperformance/availablesegmentperformances', null,
                   function (result) {
                       vm.segmentperformance = result.data;
                       InitialView();
                       vm.init === true;                      
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
          //  InitialGrid2();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#SegmentPerformanceTable').length > 0) {
                    var exportTable = $('#SegmentPerformanceTable').DataTable({
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
    }
}());
