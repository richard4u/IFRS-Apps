/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ConditionalPDEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ConditionalPDEditController]);

    function ConditionalPDEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'conditionalpd-edit-view';
        vm.viewName = 'ConditionalPD';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.conditionalpd = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var conditionalpdRules = [];

        var setupRules = function () {

            conditionalpdRules.push(new validator.PropertyRule("AssetDescription", {
                required: { message: "Asset_Description is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("AssetType", {
                required: { message: "Asset_Type is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("Counterparty", {
                required: { message: "Counter_party is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("RatingAgency", {
                required: { message: "Rating_Agency is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("CreditRating", {
                required: { message: "Credit_Rating is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("SandPRating", {
                required: { message: "S_and_PRating is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD1", {
                required: { message: "PD1 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD2", {
                required: { message: "PD2 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD3", {
                required: { message: "PD3 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD4", {
                required: { message: "PD4 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD5", {
                required: { message: "PD5 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD6", {
                required: { message: "PD6 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD7", {
                required: { message: "PD7 is required" }
            }));
            conditionalpdRules.push(new validator.PropertyRule("PD8", {
                required: { message: "PD8 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD9", {
                required: { message: "PD9 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD10", {
                required: { message: "PD10 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD11", {
                required: { message: "PD11 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD12", {
                required: { message: "PD12 is required" }
            }));

            conditionalpdRules.push(new validator.PropertyRule("PD13", {
                required: { message: "PD13 is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ConditionalPD_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/conditionalpd/getconditionalpd/' + $stateParams.ConditionalPD_Id, null,
                   function (result) {
                       vm.conditionalpd = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.conditionalpd = { AssetDescription: '', AssetType: '', Counterparty: '', RatingAgency: '', PD1: '', PD2: '', PD3: '', PD4: '', PD5: '', PD6: '', PD7: '', PD8: '', PD9: '', PD10: '', PD11: '', PD12: '', PD13: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.conditionalpd, conditionalpdRules);
            vm.viewModelHelper.modelIsValid = vm.conditionalpd.isValid;
            vm.viewModelHelper.modelErrors = vm.conditionalpd.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/conditionalpd/updateconditionalpd', vm.conditionalpd,
               function (result) {
                   
                   $state.go('ifrs9-conditionalpd-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.conditionalpd.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/conditionalpd/deleteconditionalpd', vm.conditionalpd.ConditionalPD_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-conditionalpd-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-conditionalpd-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
