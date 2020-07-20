(function () {
    var digoErpApp = angular.module("digoErpApp", ['ngRoute']);


    digoErpApp.controller("CurrencyController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var currencyFormParsley = "";
        angular.element(document).ready(function () {
            currencyFormParsley = $('#currencyForm').parsley();
            var currencyId = $("#currencyId").val();
            if (currencyId > 0) {
                vm.get(currencyId);
            }
        });

        vm.get = function (currencyId) {
            var req = {
                "url": "/Settings/Currencies/GetById?currencyId=" + currencyId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Name = response.data.Name;
                vm.Code = response.data.Code;
                vm.Rate = response.data.Rate;
                vm.Precision = response.data.Precision;
                vm.Symbol = response.data.Symbol;
                vm.SymbolPosition = response.data.SymbolPosition;
                vm.DecimalMark = response.data.DecimalMark;
                vm.Thousands_Separator = response.data.Thousands_Separator;
                vm.Enabled = response.data.Enabled;
                vm.Default_Currency = response.data.Default_Currency;

                $("#Code").selectpicker('val', vm.Code);
                $("#SymbolPosition").selectpicker('val', vm.SymbolPosition);
            }, function (error) {

            });
        };
        vm.saveForm = function () {
            var req = {
                method: 'POST',
                url: "/Settings/Currencies/AddOrUpdate",
                data: vm
            };
            $http(req).then(function (response) {
                if (response.data.StatusCode === 200) {
                    swal(response.data.MessageAr, {
                        icon: "success"
                    }).then((val) => {
                        window.location.href = "/Settings/Currencies";
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
        vm.save = function () {
            if (currencyFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.saveForm();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Settings/Currencies/GetByName?name=" + vm.Name
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode == 404) { // Not Found
                            vm.saveForm();
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
            vm.Name = "";
            vm.Type = "";
            vm.Rate = "";
            vm.Precision = "";
            vm.Symbol = "";
            vm.Enabled = 0;
            vm.Default_Currency = 0;
            $("#Type").selectpicker('val', vm.Type);
            $("#Code").selectpicker('val', vm.Code);
            $("#SymbolPosition").selectpicker('val', vm.SymbolPosition);
            currencyFormParsley.reset();
        }
    });
})()