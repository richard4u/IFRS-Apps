window.Fintrak = {};
(function (se) {
    var rootPath;
    se.rootPath = rootPath;
}(window.Fintrak));
Fintrak.rootPath = '/';

(function (ft) {
    var AccountLoginModel = function () {

        var self = this;

        self.CompanyCode = '';
        self.LoginID = '';
        self.Password = '';
        self.RememberMe = false;
    }
    ft.AccountLoginModel = AccountLoginModel;
}(window.Fintrak));
