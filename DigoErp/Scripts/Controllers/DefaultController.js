(function () {
    var digoErpApp = angular.module("digoErpApp", ['ngRoute']);

    digoErpApp.directive("selectFilesNg", function () {
        return {
            require: "ngModel",
            link: function postLink(scope, elem, attrs, ngModel) {
                elem.on("change", function (e) {
                    var files = elem[0].files;
                    ngModel.$setViewValue(files);
                });
            }
        }
    });

    digoErpApp.controller("DefaultController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var defaultFormParsley = "";
        angular.element(document).ready(function () {
            defaultFormParsley = $('#defaultForm').parsley();
            vm.get();
        });

        vm.get = function () {
            var req = {
                "url": "/Settings/Default/GetDefaultSettings",
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.Id = response.data.Id;
                vm.AccountId = response.data.AccountId;
                vm.CurrencyId = response.data.CurrencyId;
                vm.TaxId = response.data.TaxId;
                vm.PaymentMethod = response.data.PaymentMethod;
                vm.Language = response.data.Language;
                vm.RecordsPerPage = response.data.RecordsPerPage;
                vm.Logo = response.data.Logo;

                $("#AccountId").selectpicker('val', vm.AccountId);
                $("#CurrencyId").selectpicker('val', vm.CurrencyId);
                $("#TaxId").selectpicker('val', vm.TaxId);
                $("#PaymentMethod").selectpicker('val', vm.PaymentMethod);
                $("#Language").selectpicker('val', vm.Language);
                $("#RecordsPerPage").selectpicker('val', vm.RecordsPerPage);
            }, function (error) {

            });
        };

        vm.save = function () {
            if (defaultFormParsley.isValid()) {
                var fd = new FormData();
                fd.append("Id", vm.Id);
                fd.append("AccountId", vm.AccountId);
                fd.append("CurrencyId", vm.CurrencyId);
                fd.append("TaxId", vm.TaxId);
                fd.append("PaymentMethod", vm.PaymentMethod);
                fd.append("Language", vm.Language);
                fd.append("RecordsPerPage", vm.RecordsPerPage);
                fd.append("Logo", vm.Logo);
                try {
                    fd.append("file", $scope.fileArray[0]);
                } catch (e) {

                }

                var req = {
                    method: 'POST',
                    url: "/Settings/Default/Index",
                    headers: {
                        'Content-Type': undefined
                    },
                    data: fd
                };

                $http(req).then(function (response) {
                    if (response.data.StatusCode === 200) {
                        swal(response.data.MessageAr, {
                            icon: "success"
                        }).then((val) => {
                            window.location.href = "/settings/settings";
                        });
                    } else {
                        swal(response.data.MessageAr, {
                            icon: "error"
                        }).then((val) => {
                        });
                    }
                }, function (error) {

                });
            }
        };
        vm.cancelClick = function () {
            vm.whenViewOnly = false;
            this.clearFormData();
        };
        vm.clearFormData = function () {
            vm.Id = "";
            vm.AccountId = "";
            vm.CurrencyId = "";
            vm.TaxId = "";
            vm.PaymentMethod = "";
            vm.Language = "";
            vm.RecordsPerPage = "";

            $("#AccountId").selectpicker('val', vm.AccountId);
            $("#CurrencyId").selectpicker('val', vm.CurrencyId);
            $("#TaxId").selectpicker('val', vm.TaxId);
            $("#PaymentMethod").selectpicker('val', vm.PaymentMethod);
            $("#Language").selectpicker('val', vm.Language);
            $("#RecordsPerPage").selectpicker('val', vm.RecordsPerPage);
            defaultFormParsley.reset();
        }
    });
})()