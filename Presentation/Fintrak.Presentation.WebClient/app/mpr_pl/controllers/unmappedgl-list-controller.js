/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UnMappedPLGLListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        UnMappedPLGLListController]);

    function UnMappedPLGLListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'unmappedgl-list-view';
        vm.viewName = 'Un-Mapped P&L GLs';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.unMappedGLs = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/mprglmapping/getunmappedgl', null,
                   function (result) {
                       vm.unMappedGLs = result.data;
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
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#unMappedGLTable').length > 0) {
                    var exportTable = $('#unMappedGLTable').DataTable({
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

        vm.getUnMappedGLs = function () {
            vm.viewModelHelper.apiGet('api/mprglmapping/getunmappedgl', null,
                  function (result) {
                      vm.unMappedGLs = result.data;
                      
                  },
                  function (result) {
                      toastr.error(result.data, 'Fintrak');
                  }, null);
        }
        

        initialize();
    }
}());