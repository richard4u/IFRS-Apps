/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UserProfileListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        UserProfileListController]);

    function UserProfileListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'userprofile-list-view';
        vm.viewName = 'User Profile';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.userProfile = {};
        vm.userRoles = [];
        vm.solutionRunDates = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.changePassword = {};
        vm.oldPassword = [];
        vm.newPassword = [];
        vm.confirmPassword = [];
        vm.showRoles = true;
        var message = '';
        $scope.customStyle = {};
        vm.strength = ''
        vm.status = true;
        $scope.disabled = true;

        var changePasswordRules = [];

        var setupRules = function () {

            changePasswordRules.push(new validator.PropertyRule("OldPassword", {
                required: { message: "OldPassword is required" }
            }));

            changePasswordRules.push(new validator.PropertyRule("NewPassword", {
                required: { message: "NewPassword is required" }
            }));

            changePasswordRules.push(new validator.PropertyRule("ConfirmedPassword", {
                required: { message: "ConfirmedPassword is required" }
            }));

        }

        var initialize = function(){

            if (vm.init === false) {
                getUserProfile();
                getUserSolutionRunDates();
                getUserRoles();
                changepassword();
            }
        }

   
        var InitialView = function () {
            //InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#userProfileTable').length > 0) {
                    var exportTable = $('#userProfileTable').DataTable({
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

        var changepassword = function () {
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('', null,
                    function (result) {
                        vm.changePassword = result.data;
                        if (vm.changePassword === 'null')
                            initialView();
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        vm.changePasswordModel = function (opass, newp, comp) {
            validate(opass, newp, comp);
            if (message === 0) {
                if (vm.viewModelHelper.modelIsValid) {
                    var changePasswordModel = { LoginID: '', OldPassword: vm.changePasswordModel.OldPassword, NewPassword: vm.changePasswordModel.NewPassword };
                    vm.viewModelHelper.apiPost('api/account/changepw', changePasswordModel,
                        function (result) {
                            toastr.success('Successfully Changed Password', 'Fintrak');
                            window.location.href = '/Account/Login';
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else {
                    vm.viewModelHelper.modelErrors = vm.changePassword.errors;
                    var errorList = '';
                    angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                        errorList += error + '<br>';
                    });
                    toastr.error(errorList, 'Fintrak');
                }
            }
            else if (message === 2) {
                alert('Password Mismatch')
            }
            else {
                alert('New Password cannot be same as Old Password')
            }
        }

        var validate = function (OldPassword, NewPassword, Confirmpassword) {

            if (NewPassword === Confirmpassword && OldPassword !== NewPassword) {
                message = 0
            }

            else if (OldPassword === NewPassword) {
                message = 1
            }
            else {
                message = 2
            }
        }

        vm.checkpw = function scorePassword(pass) {
            var score = 0;
            if (!pass)
                return

            // award every unique letter until 5 repetitions
            var letters = new Object();
            for (var i = 0; i < pass.length; i++) {
                letters[pass[i]] = (letters[pass[i]] || 0) + 1;
                score += 5.0 / letters[pass[i]];
            }

            // bonus points for mixing it up
            var variations = {
                digits: /\d/.test(pass),
                lower: /[a-z]/.test(pass),
                upper: /[A-Z]/.test(pass),
                nonWords: /\W/.test(pass),
            }

            var variationCount = 0;
            for (var check in variations) {
                variationCount += (variations[check] == true) ? 1 : 0;
            }
            score += (variationCount - 1) * 10;

            if (score > 80)
                turnGreen();
            if (score >= 30 && score <= 60)
                turnBlue();
            if (score >= 10 && score <= 30)
                turnYellow();
            if (score >= 0 && score <= 10)
                turnRed();
        }

        var turnGreen = function () {
            $scope.customStyle.style = { "color": "green" };
            vm.strength = 'Very Strong Password'
            vm.status = false;
            $scope.disabled = false;
        }

        var turnBlue = function () {
            $scope.customStyle.style = { "color": "blue" }
            vm.strength = 'Average Password'
            vm.status = false;
            $scope.disabled = false;
        }

        var turnYellow = function () {
            $scope.customStyle.style = { "color": "purple" }
            vm.strength = 'Weak Password'
            vm.status = true;
            $scope.disabled = true;
        }

        var turnRed = function () {
            $scope.customStyle.style = { "color": "red" }
            vm.strength = 'Very Weak Password'
            vm.status = true;
            $scope.disabled = true;
        }
        setupRules();

        var getUserProfile = function () {
            vm.viewModelHelper.apiGet('api/account/getuserprofile', null,
                  function (result) {
                      vm.userProfile = result.data;

                      //updateaccount();

                      //toastr.success('User data loaded, ready for modifiation.', 'Fintrak');
                  },
                   function (result) {
                       toastr.error('Fail to load user data', 'Fintrak');
                   }, null);
        }

        //View Password
        $("#icontoggle").click(function () {
            $(this).find("i").toggleClass("fa-eye-slash fa-eye");
        });

        vm.seepassword = function () {
            var x = document.getElementById("passwordview");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        $("#iconnewtoggle").click(function () {
            $(this).find("i").toggleClass("fa-eye-slash fa-eye");
        });

        vm.seenewpassword = function () {
            var x = document.getElementById("passwordnewview");
            var y = document.getElementById("passwordnewview2");
            if (x.type === "password") {
                x.type = "text";
                y.type = "text";
            } else {
                x.type = "password";
                y.type = "password";
            }
        }

        var getUserSolutionRunDates = function () {
            vm.viewModelHelper.apiGet('api/solutionrundate/getsolutionrundatebylogin', null,
                  function (result) {
                      vm.solutionRunDates = result.data;
                     
                  },
                   function (result) {
                       toastr.error('Fail to load solution run dates', 'Fintrak');
                   }, null);
        }

        var getUserRoles = function () {
            vm.viewModelHelper.apiGet('api/userrole/getuserrolebylogin', null,
                  function (result) {
                      vm.userRoles = result.data;
                  },
                  function (result) {
                      toastr.error('Fail to load roles', 'Fintrak');
                  }, null);
        }

        var updateaccount = function () {
            vm.viewModelHelper.apiPost('api/account/updateaccount', vm.userProfile,
                  function (result) {
                  },
                   function (result) {
                       toastr.error('Fail to load user data', 'Fintrak');
                   }, null);
        }

        vm.uploadPhoto = function (input, control) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    //Sets the Old Image to new New Image
                    //$('#photo-id').attr('src', e.target.result);
                    $('#' + control).attr('src', e.target.result);

                    //Create a canvas and draw image on Client Side to get the byte[] equivalent
                    var canvas = document.createElement("canvas");
                    var imageElement = document.createElement("img");

                    imageElement.setAttribute('src', e.target.result);
                    canvas.width = 150; //imageElement.width;
                    canvas.height = canvas.width * (imageElement.height / imageElement.width); //imageElement.height;
                    var context = canvas.getContext("2d");
                    context.drawImage(imageElement, 0, 0, canvas.width, canvas.height);
                    var base64Image = canvas.toDataURL("image/jpeg");

                    //Removes the Data Type Prefix 
                    //And set the view model to the new value /data:image\/jpeg;base64,/g,
                    vm.userProfile.Photo = base64Image.replace(/^data:image\/[a-z]+;base64,/, '');

                    vm.viewModelHelper.apiPost('api/account/updateusersetupprofile', vm.userProfile,
                     function (result) {
                         toastr.success('Successfully Changed Profile Picture', 'Fintrak');
                     },
                     function (result) {
                         toastr.error(result.data, 'Fintrak');
                     }, null);
                }

                //Renders Image on Page
                reader.readAsDataURL(input.files[0]);
            }
        };

        initialize();
       
    }
}());
