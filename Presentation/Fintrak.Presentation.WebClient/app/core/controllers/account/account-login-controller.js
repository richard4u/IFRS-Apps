(function ()
{
    "use strict";
    angular
        .module("fintrak.extra")
        .controller("AccountLoginController",
                    ['$http', AccountLoginController]);

    function AccountLoginController($http, $location)
    {

        var vm = this;

        vm.init = false;
        vm.accountModel = new Fintrak.AccountLoginModel();
        vm.returnUrl = '';
        vm.firstlogon = 'OK';    
        //var message = 0;
        vm.enableDefaultCompany = false;

        var initialize = function () {
            if (vm.init == false) {

                initializeView();
                vm.init === true;
            }
        }

        var initializeView = function () {
        }

        vm.login = function (loginId) {
            //login with user's credentials
            ConfirmFirstLogon(loginId);

            if (vm.firstlogon == "OK" && vm.accountModel.Password != "@password")
            {
                $http.post(Fintrak.rootPath + 'api/account/login', vm.accountModel)
                  .then(function (result) {
                      //window.location.href = Fintrak.rootPath;
                      window.location.href = '/home/TemplateSelection';
                  }, function (result) {
                      alert('Unable to login: ' + result.data);
                      toastr.error('Unable to login: ' + result.data, 'Fintrak');
                  })
            }
           
            else if (vm.firstlogon == "OK" && vm.accountModel.Password == "@password")
                {
                    //alert('Please change password ' + result.data);
                    $http.post(Fintrak.rootPath + 'api/account/login', vm.accountModel)
                       .then(function (result) {
                           //window.location.href = Fintrak.rootPath;
                           window.location.href = '/home/TemplateSelection';
                       }, function (result) {
                           alert('Unable to login: ' + result.data);
                           toastr.error('Unable to login: ' + result.data, 'Fintrak');
                       });
                }
            else
            {
                alert('Unable to login: ' + result.data);
                toastr.error('Unable to login: ' + result.data, 'Fintrak');
            }
      
        }

        $("#icontoggle").click(function () {
            $(this).find("i").toggleClass("fa-eye-slash fa-eye");
        });

        vm.seepassword = function () {
            var x = document.getElementById("passwordview");
            if (x.type == "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        vm.changepasswordmodal = function () {
            var modal = document.getElementById("myModal");
            var span = document.getElementsByClassName("close")[0];
            modal.style.display = "block";
            span.onclick = function () {
                modal.style.display = "none";
            }
            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
        }

        var ConfirmFirstLogon = function (loginId) {
            //var url = 'api/account/userexist/' + loginId
            //vm.viewModelHelper.apiGet(url, null,
            $http.get(Fintrak.rootPath + 'api/account/userexist/' + loginId)
            .then(function (result) {
                vm.firstlogon = result.statusText;
            },
            function (result) {
                alert('Unable to login: ' + result.statusText);
                vm.firstlogon = result.statusText;
            });
        }

        initialize();
    }
   
    
}());
