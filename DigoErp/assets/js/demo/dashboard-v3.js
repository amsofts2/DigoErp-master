(function () {
    var adminDashboardApp = angular.module("adminDashboardApp", ['ngRoute']);
    adminDashboardApp.controller("DashboardController", function ($scope, $http, $location, $window) {
        var vm = this;
        vm.TotalSales = 0;
        vm.TotalProfit = 0;
        vm.TotalOrderCount = 0;
        vm.TopOneSaleItem = {};
        vm.TopOneSaleItem.Total = 0;

        vm.TopSecondSaleItem = {};
        vm.TopSecondSaleItem.Total = 0;

        vm.TopThirdSaleItem = {};
        vm.TopThirdSaleItem.Total = 0;

        vm.postUrl = $("#appURL").val() + "Home/Index";

        var handleTotalSalesSparkline = function () {
            var options = {
                chart: {
                    id: "total-sales-sparkline",
                    type: 'line',
                    width: 200,
                    height: 36,
                    sparkline: {
                        enabled: true
                    },
                    stacked: true
                },
                stroke: {
                    curve: 'smooth',
                    width: 3
                },
                fill: {
                    type: 'gradient',
                    gradient: {
                        opacityFrom: 1,
                        opacityTo: 1,
                        colorStops: [{
                            offset: 0,
                            color: COLOR_BLUE,
                            opacity: 1
                        },
                        {
                            offset: 100,
                            color: COLOR_INDIGO,
                            opacity: 1
                        }]
                    }
                },
                series: [{
                    data: []
                }],
                tooltip: {
                    theme: 'dark',
                    fixed: {
                        enabled: false
                    },
                    x: {
                        show: false
                    },
                    y: {
                        title: {
                            formatter: function (seriesName) {
                                return '';
                            }
                        },
                        formatter: (value) => { return $("#currency").val() + " " + convertNumberWithCommas(value) }
                    },
                    marker: {
                        show: false
                    }
                },
                responsive: [{
                    breakpoint: 1500,
                    options: {
                        chart: {
                            width: 130
                        }
                    }
                }, {
                    breakpoint: 1300,
                    options: {
                        chart: {
                            width: 100
                        }
                    }
                }, {
                    breakpoint: 1200,
                    options: {
                        chart: {
                            width: 200
                        }
                    }
                }, {
                    breakpoint: 576,
                    options: {
                        chart: {
                            width: 180
                        }
                    }
                }, {
                    breakpoint: 400,
                    options: {
                        chart: {
                            width: 120
                        }
                    }
                }]
            };

            if ($('#total-sales-sparkline').length !== 0) {
                var apexCharts = new ApexCharts(document.querySelector('#total-sales-sparkline'), options);
                apexCharts.render();
            }
        };

        var handleBarChart = function (values) {
            "use strict";
            var barChartData = [{
                key: 'Cumulative Return',
                values: values
            }];

            nv.addGraph(function () {
                var barChart = nv.models.discreteBarChart()
                  .x(function (d) { return d.label })
                  .y(function (d) { return d.value })
                  .showValues(true)
                  .duration(250);

                if ($("#nv-bar-chart").length >= 1) {
                    $("#nv-bar-chart").empty();
                }
                d3.select('#nv-bar-chart').append('svg')
                    .datum(barChartData).call(barChart);
                nv.utils.windowResize(barChart.update);
                return barChart;
            });
        }
        
        function setTopSaleItemValues() {
            try {
                vm.TopFiveItemsSales.sort(function(a, b) {
                    return b.Total - a.Total;
                });
            } catch (e) {

            } 
            try {
                vm.TopOneSaleItem = vm.TopFiveItemsSales[0];
                if (typeof vm.TopOneSaleItem === "undefined") {
                    vm.TopOneSaleItem = {};
                    vm.TopOneSaleItem.Total = 0;
                }
            } catch (e) {
                vm.TopOneSaleItem.Total = 0;
            }
            try {
                vm.TopSecondSaleItem = vm.TopFiveItemsSales[1];
                if (typeof vm.TopSecondSaleItem === "undefined") {
                    vm.TopSecondSaleItem = {};
                    vm.TopSecondSaleItem.Total = 0;
                }
            } catch (e) {
                vm.TopSecondSaleItem.Total = 0;
            }
            try {
                vm.TopThirdSaleItem = vm.TopFiveItemsSales[2];
                if (typeof vm.TopThirdSaleItem === "undefined") {
                    vm.TopThirdSaleItem = {};
                    vm.TopThirdSaleItem.Total = 0;
                }
            } catch (e) {
                vm.TopThirdSaleItem.Total = 0;
            }

            try {
                vm.TopFiveItemsSales.sort(function (a, b) {
                    return a.Total - b.Total;
                });
            } catch (e) {

            }
        }
        function getDashboardResponse(fromDate, toDate, label) {
            $http.post(vm.postUrl, { fromDate: fromDate, toDate: toDate, datelabel: label }).then(function (response) {
                vm.TotalProfit = convertNumberWithCommas(response.data.TotalProfit);
                vm.TotalOrderCount = convertNumberWithCommas(response.data.TotalOrderCount);
                vm.LowStockItems = response.data.LowStockItems;
                vm.TopThreeCategories = response.data.TopThreeCategories;
                vm.SalePayments = response.data.SalePayments;

                vm.TopFiveItemsSales = response.data.TopFiveItemsSales;

                setTopSaleItemValues();

                vm.Sales = response.data.Sales;

                vm.TotalSales = 0;
                var salesData = [];
                angular.forEach(vm.Sales, function (payment) {
                    vm.TotalSales += payment.PaymentAmount;
                    salesData.push(payment.PaymentAmount);
                });

                vm.MinCardHeight = {
                    "height": vm.TotalSales > 0 ? "235px":"207px"
                };
                vm.TotalSales = parseFloat(Math.round(vm.TotalSales * 100) / 100).toFixed(2);
                vm.TotalSales = convertNumberWithCommas(vm.TotalSales);

                vm.SalesPercentage = parseFloat(Math.round(response.data.SalesPercentage * 100) / 100).toFixed(2);
                vm.OrderProgress = parseFloat(Math.round(response.data.OrderProgress * 100) / 100).toFixed(2);
                vm.OrderProgress = parseFloat(vm.OrderProgress);
                vm.OrderProgress = {
                    "width": vm.OrderProgress + "%"
                };

                vm.ProfitProgess = parseFloat(Math.round(response.data.ProfitProgess * 100) / 100).toFixed(2);
                vm.ProfitProgess = parseFloat(vm.ProfitProgess);
                vm.ProfitProgess = {
                    "width": vm.ProfitProgess + "%"
                };

                angular.forEach(vm.SalePayments, function (payment) {
                    payment.Count = convertNumberWithCommas(payment.Count);
                });

                ApexCharts.exec('total-sales-sparkline', 'updateSeries', [{
                    data: salesData
                }], true);

                var values = [];
                var colors = [COLOR_RED, COLOR_ORANGE, COLOR_GREEN, COLOR_AQUA, COLOR_BLUE, COLOR_PURPLE, COLOR_GREY];
                angular.forEach(vm.TopFiveItemsSales, function(sale,key) {
                    values.push(
                    {
                        'label': sale.ItemName,
                        'value': sale.Total,
                        'color': colors[key]
                    });
                });
                handleBarChart(values);
            }, function (error) {
                //$window.location.reload();
            });
        }

        var handleDateRangeFilter = function () {
            //$('#daterange-filter span').html(moment().subtract(7, 'days').format('D MMMM YYYY') + ' - ' + moment().format('D MMMM YYYY'));
            $('#daterange-filter span').html(moment().format('D MMMM YYYY') + ' - ' + moment().format('D MMMM YYYY'));
            $('#daterange-prev-date').html(moment().subtract(15, 'days').format('D MMMM') + ' - ' + moment().subtract(8, 'days').format('D MMMM YYYY'));

            $('#daterange-filter').daterangepicker({
                format: 'MM/DD/YYYY',
                startDate: moment(),
                endDate: moment(),
                dateLimit: { days: 60 },
                showDropdowns: true,
                showWeekNumbers: true,
                timePicker: false,
                timePickerIncrement: 1,
                timePicker12Hour: true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                },
                opens: 'right',
                drops: 'down',
                buttonClasses: ['btn', 'btn-sm'],
                applyClass: 'btn-primary',
                cancelClass: 'btn-default',
                separator: ' to ',
                locale: {
                    applyLabel: 'Submit',
                    cancelLabel: 'Cancel',
                    fromLabel: 'From',
                    toLabel: 'To',
                    customRangeLabel: 'Custom',
                    daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                    monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                    firstDay: 1
                }
            }, function (start, end, label) {
                $('#daterange-filter span').html(start.format('D MMMM YYYY') + ' - ' + end.format('D MMMM YYYY'));

                if (label === "Today") {
                    vm.DateLabel = "yesterday";
                }
                if (label === "Yesterday") {
                    vm.DateLabel = "previous day";
                }
                if (label === "Last 7 Days") {
                    vm.DateLabel = "last week";
                }
                if (label === "Last 30 Days") {
                    vm.DateLabel = "previous month";
                }
                if (label === "This Month") {
                    vm.DateLabel = "previous month";
                }
                if (label === "Last Month") {
                    vm.DateLabel = "previous month";
                }

                var gap = end.diff(start, 'days');
                $('#daterange-prev-date').html(moment(start).subtract(gap, 'days').format('D MMMM') + ' - ' + moment(start).subtract(1, 'days').format('D MMMM YYYY'));
                getDashboardResponse(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'), label);
            });
        };

        function init() {
            handleDateRangeFilter();
            handleBarChart([]);
            handleTotalSalesSparkline();
        }
        init();
        getDashboardResponse(moment().format('YYYY-MM-DD'), moment().format('YYYY-MM-DD'), "Today");
        vm.DateLabel = "yesterday";
    });
})();
