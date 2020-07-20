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

    digoErpApp.controller("RevenuesController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var revenueFormParsley = "";
        angular.element(document).ready(function () {
            revenueFormParsley = $('#revenueForm').parsley();
            var revenueId = $("#revenueId").val();
            if (revenueId > 0) {
                vm.get(revenueId);
            }
        });

        vm.get = function (revenueId) {
            debugger;
            var req = {
                "url": "/Sales/Revenues/GetRevenueById?revenueId=" + revenueId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Amount = response.data.Amount;
                vm.Date = response.data.Date;
                vm.AccountId = response.data.AccountId;
                vm.CustomerId = response.data.CustomerId;
                vm.Description = response.data.Description;
                vm.CategoryId = response.data.CategoryId;
                vm.Recurring = response.data.Recurring;
                vm.PaymentMethod = response.data.PaymentMethod;
                vm.Reference = response.data.Reference;
                vm.Attachment = response.data.Attachment;

                $("#AccountId").selectpicker('val', vm.AccountId);
                $("#CustomerId").selectpicker('val', vm.CustomerId);
                $("#CategoryId").selectpicker('val', vm.CategoryId);
                $("#Recurring").selectpicker('val', vm.Recurring > 0 ? vm.Recurring.toString() :"0");
                $("#PaymentMethod").selectpicker('val', vm.PaymentMethod);

            }, function (error) {

            });
        };
        vm.save = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("Date", $("#Date").val());
            fd.append("Amount", vm.Amount);
            fd.append("AccountId", vm.AccountId);
            fd.append("CustomerId", vm.CustomerId);
            fd.append("Description", vm.Description);
            fd.append("CategoryId", vm.CategoryId);
            fd.append("Recurring", vm.Recurring);
            fd.append("PaymentMethod", vm.PaymentMethod);
            fd.append("Reference", vm.Reference);
            fd.append("Attachment", vm.Attachment);
            try {
                fd.append("file", $scope.fileArray[0]);
            } catch (e) {

            }
            var req = {
                method: 'POST',
                url: "/Sales/Revenues/AddOrUpdate",
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
                        window.location.href = "/sales/revenues";
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
            if (revenueFormParsley.isValid()) {
                vm.save();
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
            vm.AccountId = 0;
            vm.CategoryId = 0;
            vm.Description = "";
            vm.PaymentMethod = 0;
            vm.Reference = "";
            vm.Recurring = 0;
            vm.CustomerId = 0;
            revenueFormParsley.reset();
        }
    });
})()