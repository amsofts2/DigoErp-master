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

    digoErpApp.controller("InvoiceController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;
        var invoiceFormParsley = "";
        angular.element(document).ready(function () {
            vm.currencySymbol = $("#currencySymbol").val();
            invoiceFormParsley = $('#invoiceForm').parsley();

            var invoiceId = $("#invoiceId").val();
            if (invoiceId > 0) {
                vm.get(invoiceId);
            }
        });

        vm.get = function (invoiceId) {
            var req = {
                "url": "/Sales/Invoices/GetInvoiceById?invoiceId=" + invoiceId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.CustomerId = response.data.CustomerId;
                vm.CategoryId = response.data.CategoryId;
                vm.CurrencyId = response.data.CurrencyId;
                vm.Recurring = response.data.Recurring;
                vm.InvoiceDate = response.data.InvoiceDate;
                vm.DueDate = response.data.DueDate;
                vm.InvoiceNumber = response.data.InvoiceNumber;
                vm.OrderNumber = response.data.OrderNumber;
                vm.Notes = response.data.Notes;
                vm.Footer = response.data.Footer;
                vm.Amount = response.data.Amount;
                vm.Recurring = response.data.Recurring.toString();
                vm.Attachment = response.data.Attachment;
                vm.Status = response.data.Status;

                $("#Recurring").selectpicker('val', vm.Recurring);
                $("#CustomerId").selectpicker('val', vm.CustomerId);
                $("#CategoryId").selectpicker('val', vm.CategoryId);
                $("#CurrencyId").selectpicker('val', vm.CurrencyId);
            }, function (error) {

            });
        };

        vm.saveForm = function () {

            var fd = new FormData();
            fd.append("Id", $("#invoiceId").val());
            fd.append("CustomerId", vm.CustomerId);
            fd.append("CategoryId", vm.CategoryId);
            fd.append("CurrencyId", $("#invoiceId").val() > 0 ? vm.CurrencyId : $("#CurrencyId").val());
            fd.append("InvoiceDate", $("#InvoiceDate").val());
            fd.append("DueDate", $("#DueDate").val());
            fd.append("InvoiceNumber", $("#InvoiceNumber").val());
            fd.append("OrderNumber", vm.OrderNumber);
            fd.append("Notes", vm.Notes);
            fd.append("Footer", vm.Footer);
            fd.append("Recurring", vm.Recurring);
            fd.append("Attachment", vm.Attachment);
            fd.append("Status", vm.Status);
            fd.append("Subtotal", Number($("#sub-total").text().replace(vm.currencySymbol, "")).toFixed(2));
            fd.append("Discount", Number($("#discount-total").text().replace(vm.currencySymbol, "")).toFixed(2));
            fd.append("Discount_Percentage", $("#discount_on_sub_total").val());
            fd.append("Tax", Number($("#tax-total").text().replace(vm.currencySymbol, "")).toFixed(2));
            fd.append("GrandTotal", Number($("#grand-total").text().replace(vm.currencySymbol, "")).toFixed(2));
            var invoiceItems = [];
            var itemRows = $("#invoice-item-rows tr");
            $.each(itemRows, function (key, value) {
                var itemId = $(this).find("#ItemId").val();
                var quantity = $(this).find("#Quantity").val();
                if (typeof itemId !== "undefined" && itemId > 0) {
                    var price = 0;
                    try {
                        price = Number($(this).find("#Price").val().replace($("#currencySymbol").val(), "")).toFixed(2);
                    } catch (ex) {

                    }
                    var taxId = $(this).find("#TaxId").val();
                    var taxRate = $(this).find("#TaxId").find("option:selected").attr("data-rate");
                    var itemName = $(this).find("#ItemId").find("option:selected").text().trim();

                    invoiceItems.push({
                        Id: typeof $(this).attr("data-InvoiceItemId") !== "undefined" ? $(this).attr("data-InvoiceItemId") : 0,
                        ItemId: itemId,
                        Name: itemName,
                        Quantity: quantity,
                        Price: price,
                        TaxId: taxId,
                        Tax: taxRate
                    });
                }
            });
            try {
                fd.append("InvoiceItems", JSON.stringify(invoiceItems));
            } catch (e) {

            }
            try {
                fd.append("file", $scope.fileArray[0]);
            } catch (e) {

            }
            vm.DisbaleSubmitBtn = true;
            var req = {
                method: 'POST',
                url: "/Sales/Invoices/AddOrUpdate",
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
                        window.location.href = "/Sales/Invoices";
                    });
                } else {
                    swal(response.data.MessageAr, {
                        icon: "error"
                    }).then((val) => {
                        vm.DisbaleSubmitBtn = false;
                    });
                }
            }, function (error) {

            });
        };

        vm.save = function () {
            if (invoiceFormParsley.isValid()) {
                vm.DisbaleSubmitBtn = true;
                if (vm.whenEdit) {
                    vm.saveForm();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Sales/Invoices/GetByInvoiceNumber?invoicenumber=" + vm.InvoiceNumber
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode === 404) { //Not Found
                            vm.saveForm();
                        } else {
                            vm.DisbaleSubmitBtn = false;
                            swal(response.data.MessageAr, {
                                icon: "error"
                            }).then((val) => {
                                vm.DisbaleSubmitBtn = false;
                            });
                        }
                    }, function (error) {
                        vm.DisbaleSubmitBtn = false;
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
            vm.CustomerId = "";
            vm.InvoiceDate = "";
            vm.DueDate = "";
            vm.InvoiceNumber = "";
            vm.OrderNumber = "";
            vm.Notes = "";
            vm.Footer = "";
            vm.Recurring = "";
            vm.Attachment = "";
            vm.CurrencyId = "";
            vm.CategoryId = "";
            vm.Status = 0;
            invoiceFormParsley.reset();
        }
    });
})()