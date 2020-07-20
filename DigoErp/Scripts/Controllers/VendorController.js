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

    digoErpApp.controller("VendorController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var vendorFormParsley = "";
        angular.element(document).ready(function () {
            vendorFormParsley = $('#vendorForm').parsley();
            var vendorId = $("#vendorId").val();
            if (vendorId > 0) {
                vm.get(vendorId);
            }
        });

        vm.get = function (vendorId) {
            var req = {
                "url": "/Purchases/Vendors/GetVendorById?vendorId=" + vendorId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Name = response.data.Name;
                vm.Email = response.data.Email;
                vm.TaxNumber = response.data.TaxNumber;
                vm.PhoneNumber = response.data.PhoneNumber;
                vm.Website = response.data.Website;
                vm.Address = response.data.Address;
                vm.Refrence = response.data.Refrence;
                vm.AccountNumber = response.data.AccountNumber;
                vm.SadadNumber = response.data.SadadNumber;
                vm.Photo = response.data.Photo;
                vm.IsEnabled = response.data.IsEnabled;
               
                try {
                    vm.CurrencyId = response.data.CurrencyId.toString();
                    $("#CurrencyId").selectpicker('val', vm.CurrencyId);
                } catch (e) {

                }
            }, function (error) {

            });
        };
        vm.saveForm = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("Name", vm.Name);
            fd.append("Email", vm.Email);
            fd.append("TaxNumber", vm.TaxNumber);
            fd.append("PhoneNumber", vm.PhoneNumber);
            fd.append("AccountNumber", vm.AccountNumber);
            fd.append("SadadNumber", vm.SadadNumber);
            fd.append("Website", vm.Website);
            fd.append("Address", vm.Address);
            fd.append("Refrence", vm.Refrence);
            fd.append("Photo", vm.Photo);
            try {
                fd.append("file", $scope.fileArray[0]);
            } catch (e) {

            }
            fd.append("IsEnabled", vm.IsEnabled);
            fd.append("CurrencyId", vm.CurrencyId);
            var req = {
                method: 'POST',
                url: "/Purchases/Vendors/AddOrUpdate",
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
                        window.location.href = "/Purchases/Vendors";
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
            if (vendorFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.saveForm();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Purchases/Vendors/GetByEmail?email=" + vm.Email
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode === 404) { // Not Found
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
            vm.CurrencyId = "";
            vm.Email = "";
            vm.TaxNumber = "";
            vm.PhoneNumber = "";
            vm.Address= "";
            vm.Website = "";
            vm.Refrence = 0;
            vm.AccountNumber = "";
            vm.SadadNumber = "";
            vendorFormParsley.reset();
        }
    });
})()