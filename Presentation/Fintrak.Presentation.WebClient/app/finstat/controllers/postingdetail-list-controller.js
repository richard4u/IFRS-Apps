/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PostingDetailListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        PostingDetailListController]);

    function PostingDetailListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'postingdetail-list-view';
        vm.viewName = 'Posting Details';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.postingDetails = [];
        vm.reportType = 2;

        vm.reportTypes = [
         { Id: 0, Name: 'All' },
         { Id: 1, Name: 'IAS39' },
         { Id: 2, Name: 'IFRS9' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/postingdetail/getpostingdetail/' + 0 , null,
                   function (result) {
                       vm.postingDetails = result.data;                       

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
                if ($('#postingDetailTable').length > 0) {
                    var exportTable = $('#postingDetailTable').DataTable({
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


        vm.load = function () {

            vm.viewModelHelper.apiGet('api/postingdetail/getpostingdetail/' + vm.reportType, null,
               function (result) {
                   vm.postingDetails = result.data;
                   toastr.success('Postings loaded successfully.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load Postings', 'Fintrak Error');
               }, null);
        }


        initialize(); 
    }
}());
