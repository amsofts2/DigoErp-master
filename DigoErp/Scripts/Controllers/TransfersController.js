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

    digoErpApp.controller("TransfersController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var transferFormParsley = "";
        angular.element(document).ready(function () {
            transferFormParsley = $('#transferForm').parsley();
            var transferId = $("#transferId").val();
            if (transferId > 0) {
                vm.get(transferId);
            }
        });

        vm.get = function (transferId) {
            debugger;
            var req = {
                "url": "/Banking/Transfers/GetTransferById?transferId=" + transferId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.FromAccount = response.data.FromAccount;
                vm.ToAccount = response.data.ToAccount;
                vm.Amount = response.data.Amount;
                vm.Date = convertDbDateToClientDate(response.data.Date);
                vm.Description = response.data.Description;
                vm.PaymentMethod = response.data.PaymentMethod;
                vm.Reference = response.data.Reference;
                $("#FromAccount").selectpicker('val', vm.FromAccount);
                $("#ToAccount").selectpicker('val', vm.ToAccount);
                $("#PaymentMethod").selectpicker('val', vm.PaymentMethod);
               
            }, function (error) {

            });
        };
        vm.save = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("FromAccount", vm.FromAccount);
            fd.append("ToAccount", vm.ToAccount);
            fd.append("Amount", vm.Amount);
            fd.append("Date", $("#Date").val());
            fd.append("Description", vm.Description);
            fd.append("PaymentMethod", vm.PaymentMethod);
            fd.append("Reference", vm.Reference);

            var req = {
                method: 'POST',
                url: "/Banking/Transfers/AddOrUpdate",
                headers: {
                    'Content-Type': undefined
                },
                data: fd
            };
            $http(req).then(function (response) {
                if (response.data.StatusCode == 200) {
                    swal(response.data.MessageAr, {
                        icon: "success"
                    }).then((val) => {
                        window.location.href = "/Banking/Transfers";
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
            if (transferFormParsley.isValid()) {
                vm.save();
            }
        };
        vm.cancelClick = function () {
            vm.whenViewOnly = false;
            this.clearFormData();
        };
        vm.clearFormData = function () {
            vm.Id = "";
            vm.FromAccount = "";
            vm.ToAccount = "";
            vm.Amount = "";
            vm.Date = "";
            vm.Description = "";
            vm.PaymentMethod = 0;
            vm.Reference = "";
            transferFormParsley.reset();
        }
    });
})()