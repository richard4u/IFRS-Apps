/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RegressionCofficientListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        RegressionCofficientListController]);

    function RegressionCofficientListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'regressioncofficient-list-view';
        vm.viewName = 'RegressionCofficient';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        var exportTable;
        var tabID = 'RegTable';
 
        vm.RegressionCofficient = [];
        vm.init = false;
        vm.serachParam = '';
        vm.defaultCount = 2000;
        vm.showInstruction = true;
        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the main search fuctionality to find a specific record by RefNo or Account No.';

        var initialize = function () {

            if (vm.init === false) {
                vm.loaddefault();               
            }
        }


        vm.loaddefault = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/RegressionCofficient/availableRegressionCofficient', null,
                   function (result) {
                       vm.RegressionCofficient= result.data;
                       InitialView('RegTable');
                       vm.searchParam = '';
                       if (vm.init === true) {
                           exportTable.destroy();
                       } else vm.init = true;
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function (tableID) {
            InitialGrid(tableID);
        }

        var InitialGrid = function (tableID) {
            tabID = '#' + tableID
            setTimeout(function () {
                // data export
                if ($(tabID).length > 0) {
                    exportTable = $(tabID).DataTable({
                        "lengthMenu": [[50, 75, 100, -1], [50, 75, 100, "All"]],
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
            vm.serachParam = '';
            initialize();
            exportTable.destroy();
        }

      

        initialize();
    }
}());
