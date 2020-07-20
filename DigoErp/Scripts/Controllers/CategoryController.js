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

    digoErpApp.controller("CategoryController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var categoryFormParsley = "";
        angular.element(document).ready(function () {
            categoryFormParsley = $('#categoryForm').parsley();
            var categoryId = $("#categoryId").val();
            if (categoryId > 0) {
                vm.get(categoryId);
            }
        });

        vm.get = function (categoryId) {
            var req = {
                "url": "/Settings/Categories/GetCategoryById?categoryId=" + categoryId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Name = response.data.Name;
                vm.Type = response.data.Type;
                vm.Color = response.data.Color;
                vm.Enabled = response.data.Enabled;
                $("#Type").selectpicker('val', vm.Type.toString());
            }, function (error) {

            });
        };
        vm.saveForm = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("Name", vm.Name);
            fd.append("Type", vm.Type);
            fd.append("Enabled", vm.Enabled);
            fd.append("Color", $("#colorpicker").val());
            
            var req = {
                method: 'POST',
                url: "/Settings/Categories/AddOrUpdate",
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
                        window.location.href = "/Settings/Categories";
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
            if (categoryFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.saveForm();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Settings/Categories/GetByName?name=" + vm.Name
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode == 404) { // Not Found
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
            vm.Color = "";
            vm.Enabled = 0;
            categoryFormParsley.reset();
        }
    });
})()