(function () {
    var digoErpApp = angular.module("digoErpApp", ['ngRoute']);

    digoErpApp.controller("TaxController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var taxFormParsley = "";
        angular.element(document).ready(function () {
            taxFormParsley = $('#taxForm').parsley();
            var taxId = $("#taxId").val();
            if (taxId > 0) {
                vm.get(taxId);
            }
        });

        vm.get = function (taxId) {
            var req = {
                "url": "/Settings/Taxes/GetById?taxId=" + taxId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Name = response.data.Name;
                vm.Type = response.data.Type;
                vm.Rate = response.data.Rate;
                vm.Enabled = response.data.Enabled;

                $("#Type").selectpicker('val', vm.Type);
            }, function (error) {

            });
        };
        vm.saveForm = function () {
            var req = {
                method: 'POST',
                url: "/Settings/Taxes/AddOrUpdate",
                data: vm
            };
            $http(req).then(function (response) {
                if (response.data.StatusCode === 200) {
                    swal(response.data.MessageAr, {
                        icon: "success"
                    }).then((val) => {
                        window.location.href = "/Settings/Taxes";
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
            if (taxFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.saveForm();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Settings/Taxes/GetByName?name=" + vm.Name
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
            vm.Type = "";
            vm.Enabled = 0;
            $("#Type").selectpicker('val', vm.Type);
            taxFormParsley.reset();
        }
    });
})()