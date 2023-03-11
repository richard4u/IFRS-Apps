/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("QualitativeNoteListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        QualitativeNoteListController]);

    function QualitativeNoteListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'qualitativenote-list-view';
        vm.viewName = 'Qualitative Note';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.qualitativeNotes = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/qualitativeNote/availablequalitativenotes', null,
                   function (result) {
                       vm.qualitativeNotes = result.data;

                       InitialView();
                       vm.init === true;
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        vm.flipFlagToDescription = function (flag) {

            if (flag === 1) {
                return 'Conventional';
            } else if (flag === 2) {
                return 'Interim';
            } else if (flag === 3) {
                return 'NIB';
            }
        };

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#qualitativeNoteTable').length > 0) {
                    var exportTable = $('#qualitativeNoteTable').DataTable({
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
