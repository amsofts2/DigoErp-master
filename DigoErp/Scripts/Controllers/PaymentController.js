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

    digoErpApp.controller("PaymentController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var paymentFormParsley = "";
        angular.element(document).ready(function () {
            paymentFormParsley = $('#PaymentForm').parsley();
            var paymentId = $("#paymentId").val();
            if (paymentId > 0) {
                vm.get(paymentId);
            }
        });

        vm.get = function (paymentId) {
            var req = {
                "url": "/Purchases/Payments/GetPaymentById?paymentId=" + paymentId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Date = response.data.Date;
                vm.Amount = response.data.Amount;
                try {
                    vm.AccountId = response.data.AccountId.toString();
                } catch (e) {

                }
                try {
                    vm.VendorId = response.data.VendorId.toString();
                    $("#VendorId").selectpicker('val', vm.VendorId);
                } catch (e) {

                }
                vm.Description = response.data.Description;
                try {
                    vm.CategoryId = response.data.CategoryId.toString();
                    $("#CategoryId").selectpicker('val', vm.CategoryId);
                } catch (e) {

                }
               
                vm.Refrence = response.data.Refrence;
                vm.Attachment = response.data.Attachment;
                try {
                    vm.AccountId = response.data.AccountId.toString();
                    $("#AccountId").selectpicker('val', vm.AccountId);
                } catch (e) {

                }

                try {
                    vm.BillId = response.data.BillId.toString();
                    $("#BillId").selectpicker('val', vm.BillId);
                } catch (e) {

                }

                try {
                    vm.Recurring = response.data.Recurring.toString();
                    $("#Recurring").selectpicker('val', vm.Recurring);
                } catch (e) {

                }
                try {
                    vm.PaymentMethod = response.data.PaymentMethod.toString();
                    $("#PaymentMethod").selectpicker('val', vm.PaymentMethod);
                } catch (e) {

                } 

            }, function (error) {

            });
        };
        vm.saveForm = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("Date", $("#Date").val());
            fd.append("Amount", vm.Amount);
            fd.append("AccountId", vm.AccountId);
            fd.append("VendorId", vm.VendorId);
            fd.append("Description", vm.Description);
            fd.append("CategoryId", vm.CategoryId);
            fd.append("Recurring", vm.Recurring);
            fd.append("PaymentMethod", vm.PaymentMethod);
            fd.append("Refrence", vm.Refrence);
            fd.append("Attachment", vm.Attachment);
            try {
                fd.append("file", $scope.fileArray[0]);
            } catch (e) {

            }
            fd.append("BillId", vm.BillId);
            var req = {
                method: 'POST',
                url: "/Purchases/Payments/AddOrUpdate",
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
                        window.location.href = "/purchases/payments";
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
            if (paymentFormParsley.isValid()) {
                vm.saveForm();
            }
        };
        vm.cancelClick = function () {
            vm.whenViewOnly = false;
            this.clearFormData();
        };
        vm.clearFormData = function () {
            vm.Id = "";
            vm.Date = "";
            vm.Amount = "";
            vm.AccountId = "";
            vm.CategoryId = "";
            vm.BillId = "";
            vm.VendorId = "";
            vm.Description = "";
            vm.Recurring = 0;
            vm.PaymentMethod = 0;
            vm.Refrence = 0;
            vm.Attacment = 0;
            paymentFormParsley.reset();
        }
    });
})()