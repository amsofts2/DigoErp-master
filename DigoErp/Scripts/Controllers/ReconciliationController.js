(function () {
    var digoErpApp = angular.module("digoErpApp", ['ngRoute']);

    digoErpApp.controller("ReconciliationController", function ($scope, $http, $location, $window, $timeout) {
        var vm = this;
        vm.StartDate = moment().startOf('month').format("DD-MM-YYYY");
        vm.EndDate = moment().endOf('month').format("DD-MM-YYYY");

        angular.element(document).ready(function () {
            $('#ReconciliationSearchForm').parsley();
        });

        vm.searchTransactions = function () {
            vm.ClosingBalance = 0.0;
            var req = {
                "method": 'POST',
                "url": "/Banking/Transactions/GetTransactionsForReconciliation",
                data: vm
            };
            $http(req).then(function (response) {
                vm.StartDate = response.data.StartDate;
                if (!vm.StartDate) {
                    vm.StartDate = moment().startOf('month').format("DD-MM-YYYY");
                    $('.datepicker').datepicker("setDate", vm.StartDate).datepicker('update');
                }
                vm.EndDate = response.data.EndDate;
                if (!vm.EndDate) {
                    vm.EndDate = moment().endOf('month').format("DD-MM-YYYY");
                    $("#EndDate").val(vm.EndDate);
                    $('.datepicker').datepicker("setDate", vm.EndDate);
                }
                vm.AccountId = response.data.AccountId;
                vm.ClosingBalance = response.data.ClosingBalance;
                vm.ClosingBalance = !vm.ClosingBalance ? 0.0 : vm.ClosingBalance;
                vm.Transactions = response.data.Transactions;

                try {
                    $("#AccountId").selectpicker('val', vm.AccountId.toString());
                } catch (e) {

                }
            }, function (error) {

            });
        };
        vm.Save = function () {
            vm.DisabledSubBtn = true;
            var req = {
                method: 'POST',
                url: "/Banking/Reconciliations/AddOrUpdate",
                data: vm
            };
            $http(req).then(function (response) {
                if (response.data.StatusCode === 200) {
                    swal(response.data.MessageAr, {
                        icon: "success"
                    }).then((val) => {
                        window.location.href = "/banking/reconciliations";
                    });
                } else {
                    vm.DisabledSubBtn = false;
                    swal(response.data.MessageAr, {
                        icon: "error"
                    }).then((val) => {
                    });
                }
            }, function (error) {

            });
        };
        vm.Cancel = function () {
            window.location.href = "/banking/reconciliations";
        };
        vm.searchTransactions();
    });
})()
