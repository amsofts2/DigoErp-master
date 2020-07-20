(function () {
    var digoErpApp = angular.module("digoErpApp", ['ngRoute']);

    digoErpApp.directive("selectFilesNg", function () {
        return {
            require: "ngModel",
            link: function postLink(scope, elem, attrs, ngModel) {
                elem.on("change", function(e) {
                    var files = elem[0].files;
                    ngModel.$setViewValue(files);
                });
            }
        }
    });

    digoErpApp.controller("AccountController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var accountFormParsley = "";
        angular.element(document).ready(function () {
            accountFormParsley = $('#accountForm').parsley();
            var accountId = $("#accountId").val();
            if (accountId > 0) {
                vm.get(accountId);
            }
        });

        vm.get = function (accountId) {
            debugger;
            var req = {
                "url": "/Banking/Accounts/GetAccountById?accountId=" + accountId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.AccountName = response.data.AccountName;
                vm.Number = response.data.Number;
                vm.IBANNumber = response.data.IBANNumber;

                try {
                    vm.CurrencyId = response.data.CurrencyId;
                    $("#CurrencyId").selectpicker('val', vm.CurrencyId.toString());
                } catch (e) {

                } 

                vm.OpeningBalance = response.data.OpeningBalance;
                vm.BankName = response.data.BankName;
                vm.BankPhone = response.data.BankPhone;
                vm.BankAddress = response.data.BankAddress;
                vm.DefaultAccount = response.data.DefaultAccount;
                vm.IsEnabled = response.data.IsEnabled;
                
            }, function (error) {

            });
        };
        vm.save = function () {
            var req = {
                method: 'POST',
                url: "/Banking/Accounts/AddOrUpdate",
                data: vm
            };
            $http(req).then(function (response) {
                if (response.data.StatusCode === 200) {
                    swal(response.data.MessageAr, {
                        icon: "success"
                    }).then((val) => {
                        window.location.href = "/Banking/Accounts";
                    });
                } else {
                    swal(response.data.MessageAr, {
                        icon: "error"
                    }).then((val) => {
                    });
                }
            }, function (error) {

            });
        };
        vm.saveForm = function () {
            if (accountFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.save();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Banking/Accounts/GetByAccountName?accountname=" + vm.AccountName
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode === 404) { //Not Found
                            vm.save();
                        } else {
                            swal(response.data.MessageAr, {
                                icon: "error"
                            }).then((val) => {

                            });
                        }
                    }, function (error) {

                    });
                }
            }
        };
        vm.cancelClick = function () {
            vm.whenViewOnly = false;
            this.clearFormData();
        };
        vm.clearFormData = function () {
            vm.Id = "";
            vm.AccountName = "";
            vm.Number = "";
            vm.IBANNumber = "";
            vm.BankPhone = "";
            vm.BankName = "";
            vm.IsEnabled = 0;
            vm.DefaultAccount = 0;
            vm.CurrencyId = "";
            $("#CurrencyId").selectpicker('val', vm.CurrencyId.toString());
            accountFormParsley.reset();
        }
    });
})()