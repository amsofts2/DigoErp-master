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

    digoErpApp.controller("BranchController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;

        var branchFormParsley = "";
        angular.element(document).ready(function () {
            branchFormParsley = $('#branchForm').parsley();
            var branchId = $("#branchId").val();
            if (branchId > 0) {
                vm.whenEdit = true;
                vm.get(branchId);
            }
        });

        vm.get = function (branchId) {
            var req = {
                "url": "/Common/Branches/GetById?branchId=" + branchId,
                "type": "GET"
            };
            $http(req).then(function (response) {
                vm.Id = response.data.Id;
                vm.Name = response.data.Name;
                vm.Email = response.data.Email;
                vm.TaxNumber = response.data.TaxNumber;
                vm.Phone = response.data.Phone;
                vm.Address = response.data.Address;
                vm.Logo = response.data.Logo;
            }, function (error) {

            });
        };
        vm.saveForm = function () {
            if (branchFormParsley.isValid()) {
                var fd = new FormData();
                fd.append("Id", vm.Id);
                fd.append("Name", vm.Name);
                fd.append("Email", vm.Email);
                fd.append("TaxNumber", vm.TaxNumber);
                fd.append("Phone", vm.Phone);
                fd.append("Address", vm.Address);
                fd.append("Logo", vm.Logo);
                try {
                    fd.append("file", $scope.fileArray[0]);
                } catch (e) {

                } 
                var req = {
                    method: 'POST',
                    url: "/Common/Branches/AddOrUpdate",
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
                            window.location.href = "/common/branches";
                        });
                    } else {
                        swal(response.data.MessageAr, {
                            icon: "error"
                        }).then((val) => {
                        });
                    }
                }, function (error) {

                });
            }
        };
        vm.cancelClick = function () {
            vm.whenViewOnly = false;
            this.clearFormData();
        };
        vm.clearFormData = function () {
            vm.Id = "";
            vm.Name = "";
            vm.Email = "";
            vm.Phone = "";
            vm.Address = "";
            vm.TaxNumber = "";
            branchFormParsley.reset();
        }
    });
})()