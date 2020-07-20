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

    digoErpApp.controller("BillController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var billFormParsley = "";
        angular.element(document).ready(function () {
            billFormParsley = $('#billForm').parsley();
            vm.currencySymbol = $("#currencySymbol").val();
            var billId = $("#billId").val();
            if (billId > 0) {
                vm.get(billId);
            }
        });

        vm.get = function (billId) {
            var req = {
                "url": "/Purchases/Bills/GetBillById?billId=" + billId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.VendorId = response.data.VendorId;
                vm.CategoryId = response.data.CategoryId;
                vm.CurrencyId = response.data.CurrencyId;
                vm.Recurring = response.data.Recurring;
                vm.Number = response.data.Number;
                vm.OrderNumber = response.data.OrderNumber;
                vm.Notes = response.data.Notes;
                vm.Amount = response.data.Amount;
                vm.BillDate = response.data.BillDate;
                vm.DueDate = response.data.DueDate;
                vm.Status = response.data.Status;
                vm.Attachment = response.data.Attachment;

                $("#Recurring").selectpicker('val', vm.Recurring);
                $("#VendorId").selectpicker('val', vm.VendorId);
                $("#CategoryId").selectpicker('val', vm.CategoryId);
                $("#CurrencyId").selectpicker('val', vm.CurrencyId);
                
            }, function (error) {

            });
        };
        vm.saveForm = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("Number", vm.Id ? vm.Number : $("#Number").val());
            fd.append("VendorId", vm.VendorId);
            fd.append("CategoryId", vm.CategoryId);
            fd.append("CurrencyId", vm.Id ? vm.CurrencyId : $("#CurrencyId").val());
            fd.append("OrderNumber", vm.OrderNumber);
            fd.append("Notes", vm.Notes);
            fd.append("Recurring", vm.Recurring);
            fd.append("Amount", vm.Amount);
            fd.append("BillDate", $("#BillDate").val());
            fd.append("DueDate", $("#DueDate").val());
            fd.append("Status", vm.Status);
            fd.append("Subtotal", Number($("#sub-total").text().replace(vm.currencySymbol, "")).toFixed(2));
            fd.append("Discount", Number($("#discount-total").text().replace(vm.currencySymbol, "")).toFixed(2));
            fd.append("Discount_Percentage", $("#discount_on_sub_total").val());
            fd.append("Tax", Number($("#tax-total").text().replace(vm.currencySymbol, "")).toFixed(2));
            fd.append("GrandTotal", Number($("#grand-total").text().replace(vm.currencySymbol, "")).toFixed(2));

            var billItems = [];
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

                    billItems.push({
                        Id: typeof $(this).attr("data-Bill_ItemId") !== "undefined" ? $(this).attr("data-Bill_ItemId") : 0,
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
                fd.append("Bill_Items", JSON.stringify(billItems));
            } catch (e) {

            }

            try {
                fd.append("file", $scope.fileArray[0]);
            } catch (e) {

            }
            var req = {
                method: 'POST',
                url: "/Purchases/Bills/AddOrUpdate",
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
                        window.location.href = "/purchases/bills";
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
            if (billFormParsley.isValid()) {
                vm.saveForm();
            }
        };
        vm.cancelClick = function () {
            vm.whenViewOnly = false;
            this.clearFormData();
        };
        vm.clearFormData = function () {
            vm.Id = "";
            vm.Number = "";
            vm.VendorId = "";
            vm.Amount = "";
            vm.BillDate = "";
            vm.DueDate = "";
            vm.CurrencyId = "";
            vm.CategoryId = "";
            vm.Status = 0;
            billFormParsley.reset();
        }
    });
})()