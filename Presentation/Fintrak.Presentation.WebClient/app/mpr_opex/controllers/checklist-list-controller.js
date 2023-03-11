/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CheckListListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        CheckListListController]);

    function CheckListListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'mpr-Opex';
        vm.view = 'checklist-list-view';
        vm.viewName = 'CheckList';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.checkLists = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/checklist/availablechecklist', null,
                   function (result) {
                       vm.checkLists = result.data;
                       InitialView();
                       vm.init = true;
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#checkListTable').length > 0) {
                    var exportTable = $('#checkListTable').DataTable({
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

        vm.getRunCheckList = function () {
            vm.viewModelHelper.apiGet('api/checklist/runchecklist', null,
                  function (result) {
                      vm.checkLists = result.data;                     
                  },
                  function (result) {
                      toastr.error(result.data, 'Fintrak');
                  }, null);
        }
        initialize(); 
    }
}());
