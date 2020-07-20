(function () {
    var digoErpApp = angular.module("digoErpApp", ['ngRoute']);

    digoErpApp.directive("selectFilesNg", function () {
        return {
            require: "ngModel",
            link: function postLink(scope, elem, attrs, ngModel) {
                elem.on("change", function (e) {
                    var files = elem[0].files;
                    ngModel.$setViewValue(files);
                })
            }
        }
    });

    digoErpApp.controller("RoleController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var roleFormParsley = "";
        angular.element(document).ready(function () {
            roleFormParsley = $('#roleForm').parsley();
            var roleId = $("#roleId").val();
            if (roleId > 0) {
                vm.get(roleId);
            }
        });

        vm.get = function (roleId) {
            debugger;
            var req = {
                "url": "/Auth/Roles/GetRoleById?roleId=" + roleId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.Name = response.data.Name;
                vm.Code = response.data.Code;
                vm.Description = response.data.Description;

            }, function (error) {

            });
        };
        vm.save = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id);
            fd.append("Name", vm.Name);
            fd.append("Code", vm.Code);
            fd.append("Description", vm.Description);
            var req = {
                method: 'POST',
                url: "/Auth/Roles/AddOrUpdate",
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
                        window.location.href = "/Auth/Roles";
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
            if (roleFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.save();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Auth/Roles/GetByRoleName?rolename=" + vm.Name
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode == 404) { //Not Found
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
            vm.Code = "";
            vm.Description = "";
           
            roleFormParsley.reset();
        }
    });
})()