/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ConditionalPDListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ConditionalPDListController]);

    function ConditionalPDListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'conditionalpd-list-view';
        vm.viewName = 'ConditionalPD';

        var tabID = 'conditionalpdTable'
        var exportTable;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.defaultCount = 1000;
        vm.conditionalpd = [];

        vm.processDate = null;
        vm.endDate = new Date();
        vm.init = false;
        vm.searchParam = '';
        vm.showInstruction = true;

        vm.uniqueAssetDescription = [];
        vm.found = false;
        vm.count = 0;
        vm.assetType = [];
        vm.assetTypeVal = '';

        vm.instruction = 'Only top ' + vm.defaultCount + ' records loaded. Use the export button to download all record for the selected date';

        var initialize = function () {
            if (vm.init === false) {
                intializeLookUp();

                vm.viewModelHelper.apiGet('api/conditionalpd/availableconditionalpds/' + vm.defaultCount, null,
                   function (result) {
                       vm.conditionalpd = result.data;
                       InitialView('conditionalpdTable');
                       vm.searchParam = '';

                       intializeUnique();

                       if (vm.init === true) {
                           exportTable.destroy();
                       } else vm.init = true;
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var intializeUnique = function () {
            for (var i = 0; i < vm.conditionalpd.length; i++) {
                for (var y = 0; y < vm.uniqueAssetDescription.length; y++) {
                    if (vm.conditionalpd[i]['AssetDescription'] == vm.uniqueAssetDescription[y]) {
                        vm.found = true;
                    }
                }
                vm.count++;
                if (vm.count == 1 && vm.found == false) {
                    vm.uniqueAssetDescription.push(vm.conditionalpd[i]['AssetDescription']);
                }
                vm.count = 0;
                vm.found = false;
            }
            console.log(vm.uniqueAssetDescription);
        }

        var intializeLookUp = function () {
            getdistinctassettype();
        }

        var InitialView = function (tableId) {
            InitialGrid(tableId);
        };

        var InitialGrid = function () {

            setTimeout(function () {
                // data export
                if ($('#conditionalpdTable').length > 0) {
                exportTable = $('#conditionalpdTable').DataTable({
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
            },
                50);
        };


        vm.exportAllData = function () {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                vm.loading = false;
            }
            xhr.open('GET', 'api/conditionalpd/availableconditionalpds/0', true);
            xhr.responseType = 'arraybuffer';
            xhr.onload = function () {
                if (this.status === 200) {
                    var filename = "";
                    var disposition = xhr.getResponseHeader('Content-Disposition');
                    if (disposition && disposition.indexOf('attachment') !== -1) {
                        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                        var matches = filenameRegex.exec(disposition);
                        if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                    }
                    var type = xhr.getResponseHeader('Content-Type');

                    var blob = typeof File === 'function'
                        ? new File([this.response], filename, { type: type })
                        : new Blob([this.response], { type: type });
                    if (typeof window.navigator.msSaveBlob !== 'undefined') {
                        // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                        window.navigator.msSaveBlob(blob, filename);
                    } else {
                        var URL = window.URL || window.webkitURL;
                        var downloadUrl = URL.createObjectURL(blob);

                        if (filename) {
                            // use HTML5 a[download] attribute to specify filename
                            var a = document.createElement("a");
                            // safari doesn't support this yet
                            if (typeof a.download === 'undefined') {
                                window.location = downloadUrl;
                            } else {
                                a.href = downloadUrl;
                                a.download = filename;
                                document.body.appendChild(a);
                                a.click();
                            }
                        } else {
                            window.location = downloadUrl;
                        }

                        setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
                    }
                }
            };
            xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            vm.loading = true;
            xhr.send()
        }


        vm.loadConditionalPDBySearch = function () {

            if (vm.searchParam === '') {
                toastr.warning('Please input an Asset Description ', 'Empty Search');
                return
            } else {

                vm.viewModelHelper.apiGet('api/conditionalpd/getconditionalpdbysearch/' + vm.searchParam, null,
                    function (result) {
                        vm.conditionalpd = result.data;
                        console.log(vm.conditionalpd);
                        InitialView('conditionalpdTable');
                        if (vm.init === true) {
                            exportTable.destroy();
                        } else vm.init = true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.loadConditionalPDByAssetType = function () {

            if (vm.assetTypeVal === '') {
                toastr.warning('Please input an Asset Type ', 'Empty');
                return
            } else {

                vm.viewModelHelper.apiGet('api/conditionalpd/getconditionalpdbyassettype/' + vm.assetTypeVal, null,
                    function (result) {
                        vm.conditionalpd = result.data;
                        console.log(vm.conditionalpd);
                        InitialView('conditionalpdTable');
                        if (vm.init === true) {
                            exportTable.destroy();
                        } else vm.init = true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.storeConditionalPDProcess = function () {
            var _date = vm.endDate.toISOString().split("T")[0];
            vm.viewModelHelper.apiGet('api/conditionalpd/conditionalpdstoreprocess/' + _date, null,
                function (result) {
                    //  reload();      
                    vm.conditionalpd = result.data;
                    InitialView(conditionalpd);
                    if (vm.init === true) {
                        exportTable.destroy();
                    } else vm.init = true;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        var getdistinctassettype = function () {
            vm.viewModelHelper.apiGet('api/conditionalpd/distinctassettype', null,
                function (result) {
                    vm.assetType = result.data;
                    vm.init === true;

                },
                function (result) {
                    toastr.error('Fail to load Asset Type.', 'Fintrak');
                }, null);
        }



        vm.refresh = function () {
            vm.init = false;
            vm.searchParam = '';
            initialize();
            exportTable.destroy();
        }

        initialize();
    }
}());
