(function () {
    var digoErpApp = angular.module("digoErpApp", ['ngRoute']);

    digoErpApp.controller("CustomersController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var customerFormParsley = "";
        angular.element(document).ready(function () {
            customerFormParsley = $('#customerForm').parsley();
            var customerId = $("#customerId").val();
            if (customerId > 0) {
                vm.get(customerId);
            }
        });

        vm.get = function (customerId) {
            debugger;
            var req = {
                "url": "/Sales/Customers/GetCustomerById?customerId=" + customerId,
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
                vm.Refrence = response.data.Refrence;
                vm.Address = response.data.Address;
                vm.AccountNumber = response.data.AccountNumber;
                vm.SadadNumber = response.data.SadadNumber;
                vm.IsEnabled = response.data.IsEnabled;
                try {
                    vm.CurrencyId = response.data.CurrencyId;
                    $("#CurrencyId").selectpicker('val', vm.CurrencyId.toString());
                } catch (e) {

                }
            }, function (error) {

            });
        };
        vm.save = function () {
            var req = {
                method: 'POST',
                url: "/Sales/Customers/AddOrUpdate",
                data: vm
            };
            $http(req).then(function (response) {
                if (response.data.StatusCode === 200) {
                    swal(response.data.MessageAr, {
                        icon: "success"
                    }).then((val) => {
                        window.location.href = "/sales/customers";
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
            if (customerFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.save();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Sales/Customers/GetByEmail?email=" + vm.Email
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
            vm.Name = "";
            vm.Email = "";
            vm.PhoneNumber = "";
            vm.IsEnabled = 0;
            vm.TaxNumber = "";
            vm.CurrencyId = "";
            vm.PhoneNumber = "";
            vm.Website = "";
            vm.Refrence = "";
            vm.Address = "";
            vm.AccountNumber = "";
            vm.SadadNumber = "";
            customerFormParsley.reset();
        }
    });
})()