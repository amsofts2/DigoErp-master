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

    digoErpApp.controller("ItemController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var itemFormParsley = "";
        angular.element(document).ready(function () {
            itemFormParsley = $('#itemForm').parsley();
            var itemId = $("#itemId").val();
            if (itemId > 0) {
                vm.get(itemId);
            }
        });

        vm.get = function (itemId) {
            var req = {
                "url": "/Common/Items/GetItemById?itemId=" + itemId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Name = response.data.Name;
                vm.CategoryId = response.data.CategoryId;
                vm.TaxId = response.data.TaxId;
                vm.SalePrice = response.data.SalePrice;
                vm.PurchasePrice = response.data.PurchasePrice;
                vm.IsEnabled = response.data.IsEnabled;
                vm.Description = response.data.Description;
                vm.Picture = response.data.Picture;

                try {
                    $("#TaxId").selectpicker('val', vm.TaxId.toString());
                } catch (e) {

                }
                try {
                    $("#CategoryId").selectpicker('val', vm.CategoryId.toString());
                } catch (e) {

                }

            }, function (error) {

            });
        };
        vm.saveForm = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("Name", vm.Name);
            fd.append("CategoryId", vm.CategoryId);
            fd.append("TaxId", vm.TaxId);
            fd.append("SalePrice", vm.SalePrice);
            fd.append("PurchasePrice", vm.PurchasePrice);
            fd.append("Description", vm.Description);
            fd.append("Picture", vm.Picture);
            try {
                fd.append("file", $scope.fileArray[0]);
            } catch (e) {

            }
            fd.append("IsEnabled", vm.IsEnabled);
            var req = {
                method: 'POST',
                url: "/Common/Items/AddOrUpdate",
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
                        window.location.href = "/common/items";
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
            if (itemFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.saveForm();
                } else {
                    var reqCheck = {
                        method: 'Get',
                        url: "/Common/Items/GetByName?name=" + vm.Name
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode === 404) { //not found
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
            vm.CategoryId = "";
            vm.SalePrice = "";
            vm.PurchasePrice = "";
            vm.IsEnabled = 0;
            vm.TaxId = "";
            vm.Description = "";
            vm.file = "";
            itemFormParsley.reset();
        }
    });
})()