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

    digoErpApp.controller("UserController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var userFormParsley = "";
        angular.element(document).ready(function () {
            userFormParsley = $('#userForm').parsley();
            var userId = $("#userId").val();
            if (userId > 0) {
                vm.whenEdit = true;
                vm.get(userId);
            }
        });

        vm.get = function (userId) {
            var req = {
                "url": "/Auth/Users/GetUserById?userId=" + userId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.whenEdit = true;
                vm.Id = response.data.Id;
                vm.FullName = response.data.FullName;
                vm.Email = response.data.Email;
                vm.Phone = response.data.Phone;
                vm.Password = response.data.Password;
                vm.ConfirmPassword = response.data.Password;
                vm.Language = response.data.Language;
                vm.LandingPage = response.data.LandingPage;

                $("#Language").selectpicker('val', vm.Language);
                $("#LandingPage").selectpicker('val', vm.LandingPage);

                try {
                    vm.BranchId = response.data.BranchId.toString();
                    $("#BranchId").selectpicker('val', vm.BranchId);
                } catch (e) {

                }
                try {
                    vm.RoleId = response.data.RoleId.toString();
                    $("#RoleId").selectpicker('val', vm.RoleId);
                } catch (e) {

                }

                vm.IsEnabled = response.data.IsEnabled;
                vm.Photo = response.data.Photo;
            }, function (error) {

            });
        };
        vm.save = function () {
            var fd = new FormData();
            fd.append("Id", vm.Id > 0  ? vm.Id :  0);
            fd.append("FullName", vm.FullName);
            fd.append("Email", vm.Email);
            fd.append("Password", vm.Password);
            fd.append("ConfirmPassword", vm.ConfirmPassword);
            fd.append("BranchId", vm.BranchId);
            fd.append("RoleId", vm.RoleId);
            fd.append("IsEnabled", vm.IsEnabled);
            fd.append("Photo", vm.Photo);
            fd.append("Language", vm.Language);
            fd.append("LandingPage", vm.LandingPage);
            try {
                fd.append("file", $scope.fileArray[0]);
            } catch (e) {

            }

            var req = {
                method: 'POST',
                url: "/Auth/Users/AddOrUpdate",
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
                        window.location.href = "/auth/users";
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
            if (userFormParsley.isValid()) {
                if (vm.whenEdit) {
                    vm.save();
                } else {
                    var reqCheck = {
                        method: 'GET',
                        url: "/Auth/Users/GetByEmail?email=" + vm.Email
                    };
                    $http(reqCheck).then(function (response) {
                        if (response.data.StatusCode === 404) { // not found
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
            vm.FullName = "";
            vm.Email = "";
            vm.Phone = "";
            vm.Password = "";
            vm.ConfirmPassword = "";
            vm.ConfirmPassword = "";
            vm.BranchId = "";
            vm.RoleId = "";
            vm.IsEnabled = 0;
            vm.Language = "";
            vm.LandingPage = "";
            userFormParsley.reset();
        }
    });
})()