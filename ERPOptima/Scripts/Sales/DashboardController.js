//inject the $scope service into the controller 

app.controller('ChartController', function ($scope, $http) {

    $scope.dashboards = [];

    //Daily Target Sales Company
    $scope.dataDTSalesCompany = [];
    var dataDTSalesCompanyTargets = [];
    var dataDTSalesCompanySales = [];

    //Daily Target Collections Company
    $scope.dataDTCollectionCompany = [];
    var dataDTCollectionCompanyTargets = [];
    var dataDTCollectionCompanyCollections = [];

    $scope.dataRegionalSales = [];

    //Monthly Sales & Collection Company
    $scope.dataMSalesCompany = [];
    var dataMSalesCompanySales = [];
    var dataMSalesCompanyCollections = [];

    //Monthly Sales & Collection Branch
    $scope.dataMSalesBranch = [];
    var dataMSalesBranchSales = [];
    var dataMSalesBranchCollections = [];

    //Current Target Sales Employee
    $scope.dataCTSalesEmployee = [];
    var dataCTSalesEmployeeTargets = [];
    var dataCTSalesEmployeeSales = [];

    //Current Target Collections Employee
    $scope.dataCTCollectionEmployee = [];
    var dataCTCollectionEmployeeTargets = [];
    var dataCTCollectionEmployeeCollections = [];

    $scope.regionalSales = [];
    var regionalSalesPer = [];

    var init = function () {        
        GetPermittedDashboards();
    };
    init();
    function GetPermittedDashboards() {
        $http({
            url: '/Security/DashboardPermission/GetPermittedDashBoard',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.dashboards = data;
        });
    };

    var GetDailySalesTargetCompany = function() {
        $http({
            url: '/Sales/Dashboard/GetDataDTSalesCompany',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.dataDTSalesCompany = data;
            dataDTSalesCompanyTargets = [];
            dataDTSalesCompanySales = [];
            for (var i = 0; i < data.Targets.length; i++) {
                var newData = { label: data.Targets[i].label, y: data.Targets[i].y };
                dataDTSalesCompanyTargets.push(newData);
            }
            for (var i = 0; i < data.Sales.length; i++) {
                var newData = { label: data.Sales[i].label, y: data.Sales[i].y };
                dataDTSalesCompanySales.push(newData);
            }
            DailySalesTargetCompany();
        });
    };

    var GetDailyCollectionTargetCompany = function () {
        $http({
            url: '/Sales/Dashboard/GetDataDTCollectionCompany',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.dataDTCollectionCompany = data;
            dataDTCollectionCompanyTargets = [];
            dataDTCollectionCompanyCollections = [];
            for (var i = 0; i < data.Targets.length; i++) {
                var newData = { label: data.Targets[i].label, y: data.Targets[i].y };
                dataDTCollectionCompanyTargets.push(newData);
            }
            for (var i = 0; i < data.Collections.length; i++) {
                var newData = { label: data.Collections[i].label, y: data.Collections[i].y };
                dataDTCollectionCompanyCollections.push(newData);
            }
            DailyCollectionTargetCompany();
        });
    };

    var GetCurrentSalesTargetEmployee = function () {
        $http({
            url: '/Sales/Dashboard/GetDataCTSalesEmployee',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.dataCTSalesEmployee = data;
            dataCTSalesEmployeeTargets = [];
            dataCTSalesEmployeeSales = [];
            for (var i = 0; i < data.Targets.length; i++) {
                var newData = { label: data.Targets[i].label, y: data.Targets[i].y };
                dataCTSalesEmployeeTargets.push(newData);
            }
            for (var i = 0; i < data.Sales.length; i++) {
                var newData = { label: data.Sales[i].label, y: data.Sales[i].y };
                dataCTSalesEmployeeSales.push(newData);
            }
            CurrentSalesTargetEmployee();
        });
    };

    var GetCurrentCollectionTargetEmployee = function () {
        $http({
            url: '/Sales/Dashboard/GetDataCTCollectionEmployee',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.dataCTCollectionEmployee = data;
            dataCTCollectionEmployeeTargets = [];
            dataCTCollectionEmployeeCollections = [];
            for (var i = 0; i < data.Targets.length; i++) {
                var newData = { label: data.Targets[i].label, y: data.Targets[i].y };
                dataCTCollectionEmployeeTargets.push(newData);
            }
            for (var i = 0; i < data.Collections.length; i++) {
                var newData = { label: data.Collections[i].label, y: data.Collections[i].y };
                dataCTCollectionEmployeeCollections.push(newData);
            }
            CurrentCollectionTargetEmployee();
        });
    };

    var GetMonthlySalesCompany = function () {
        $http({
            url: '/Sales/Dashboard/GetDataMonthlySCCompany',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.dataMSalesCompany = data;
            dataMSalesCompanySales = [];
            dataMSalesCompanyCollections = [];
            for (var i = 0; i < data.Sales.length; i++) {
                var newData = { label: data.Sales[i].label, y: data.Sales[i].y };
                dataMSalesCompanySales.push(newData);
            }
            for (var i = 0; i < data.Collections.length; i++) {
                var newData = { label: data.Collections[i].label, y: data.Collections[i].y };
                dataMSalesCompanyCollections.push(newData);
            }
            MonthlySalesCompany();
        });
    };

    var GetMonthlySalesBranch = function () {
        $http({
            url: '/Sales/Dashboard/GetDataMonthlySCBranch',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.dataMSalesBranch = data;
            dataMSalesBranchSales = [];
            dataMSalesBranchCollections = [];
            for (var i = 0; i < data.Sales.length; i++) {
                var newData = { label: data.Sales[i].label, y: data.Sales[i].y };
                dataMSalesBranchSales.push(newData);
            }
            for (var i = 0; i < data.Collections.length; i++) {
                var newData = { label: data.Collections[i].label, y: data.Collections[i].y };
                dataMSalesBranchCollections.push(newData);
            }
            MonthlySalesBranch();
        });
    };

    var GetRegionalSales = function () {
        $http({
            url: '/Sales/Dashboard/GetRegionWiseSales',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.regionalSales = data;
            regionalSalesPer = [];
            for (var i = 0; i < data.Sales.length; i++) {
                var newData = { label: data.Sales[i].label, y: data.Sales[i].y, legendText: data.Sales[i].legendText, per: data.Sales[i].per };
                regionalSalesPer.push(newData);
            }
            
            RegionalSales();
        });
    };

    $scope.changeDashboard = function (name) {
         ;
        if (name === "DailySalesTargetCompany") {
            GetDailySalesTargetCompany();            
        }
        else if (name === "DailyCollectionTargetCompany") {
            GetDailyCollectionTargetCompany();
        }
        else if (name === "RegionalSales") {
            GetRegionalSales();
        }
        else if (name === "MonthlySalesCompany") {
            GetMonthlySalesCompany();
        }
        else if (name === "MonthlySalesBranch") {
            GetMonthlySalesBranch();
        }
        else if (name === "CurrentSalesTargetEmployee") {
            GetCurrentSalesTargetEmployee();
        }
        else if (name === "CurrentCollectionTargetEmployee") {
            GetCurrentCollectionTargetEmployee();
        }
    };

    var DailySalesTargetCompany = function () {
        var chartDailySalesTargetCompany = new CanvasJS.Chart("chartContainer",
        {
            title: {
                text: "Daily Sales Target & Sales (Company)"
            },
            animationEnabled: true,
            legend: {
                cursor: "pointer",
                itemclick: function (e) {
                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                        e.dataSeries.visible = false;
                    }
                    else {
                        e.dataSeries.visible = true;
                    }
                    chartDailySalesTargetCompany.render();
                }
            },
            axisY: {
                title: "Amount (Tk.)"
            },
            toolTip: {
                shared: true,
                content: function (e) {
                     
                    var str = '';
                    var total = 0;
                    var pending = 0;
                    var str3;
                    var str2;
                    for (var i = 0; i < e.entries.length; i++) {
                        var str1 = "<span style= 'color:" + e.entries[i].dataSeries.color + "'> " + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
                        total = e.entries[i].dataPoint.y + total;
                        str = str.concat(str1);
                    }
                    //pending = target - sales;
                    if (e.entries[0].dataPoint.y !== undefined) {
                        if (e.entries[1].dataPoint.y === undefined) {
                            e.entries[1].dataPoint.y = 0;
                        }
                        pending = e.entries[0].dataPoint.y - e.entries[1].dataPoint.y;
                    }
                    str2 = "<span style = 'color:DodgerBlue; '><strong>" + e.entries[0].dataPoint.label + "</strong></span><br/>";
                    //str3 = "<span style = 'color:Tomato '>Total: </span><strong>" + total + "</strong><br/>";
                    str3 = "<span style = 'color:Tomato '>Pending: </span><strong>" + pending + "</strong><br/>";

                    return (str2.concat(str)).concat(str3);
                }

            },
            data: [
            {
                type: "bar",
                showInLegend: true,
                name: "Target",
                color: "#F88F22",
                dataPoints: dataDTSalesCompanyTargets                
            },
            {
                type: "bar",
                showInLegend: true,
                name: "Sales",
                color: "#7BC95A",
                dataPoints: dataDTSalesCompanySales                
            }

            ]
        });
        //chartDailySalesTargetCompany.options.data[0].dataPoints = JSON.stringify($scope.dataDTSalesCompany.Targets);
        //chartDailySalesTargetCompany.options.data[1].dataPoints = JSON.stringify$scope.dataDTSalesCompany.Sales);
        chartDailySalesTargetCompany.render();
    };

    var DailyCollectionTargetCompany = function () {
        var chartDailyCollectionTargetCompany = new CanvasJS.Chart("chartContainer",
        {
            title: {
                text: "Daily Collection Target & Collection (Company)"
            },
            animationEnabled: true,
            legend: {
                cursor: "pointer",
                itemclick: function (e) {
                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                        e.dataSeries.visible = false;
                    }
                    else {
                        e.dataSeries.visible = true;
                    }
                    chartDailyCollectionTargetCompany.render();
                }
            },
            axisY: {
                title: "Amount (Tk.)"
            },
            toolTip: {
                shared: true,
                content: function (e) {
                     
                    var str = '';
                    var total = 0;
                    var pending = 0;
                    var str3;
                    var str2;
                    for (var i = 0; i < e.entries.length; i++) {
                        var str1 = "<span style= 'color:" + e.entries[i].dataSeries.color + "'> " + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
                        total = e.entries[i].dataPoint.y + total;
                        str = str.concat(str1);
                    }
                    //pending = target - sales;
                    if (e.entries[0].dataPoint.y !== undefined) {
                        if (e.entries[1].dataPoint.y === undefined) {
                            e.entries[1].dataPoint.y = 0;
                        }
                        pending = e.entries[0].dataPoint.y - e.entries[1].dataPoint.y;
                    }
                    str2 = "<span style = 'color:DodgerBlue; '><strong>" + e.entries[0].dataPoint.label + "</strong></span><br/>";
                    //str3 = "<span style = 'color:Tomato '>Total: </span><strong>" + total + "</strong><br/>";
                    str3 = "<span style = 'color:Tomato '>Pending: </span><strong>" + pending + "</strong><br/>";

                    return (str2.concat(str)).concat(str3);
                }

            },
            data: [
            {
                type: "bar",
                showInLegend: true,
                name: "Target",
                color: "#F88F22",
                dataPoints: dataDTCollectionCompanyTargets

            },
            {
                type: "bar",
                showInLegend: true,
                name: "Collection",
                color: "#7BC95A",
                dataPoints: dataDTCollectionCompanyCollections
            }

            ]
        });

        chartDailyCollectionTargetCompany.render();
    };

    var RegionalSales = function () {
        var chartRegionalSales = new CanvasJS.Chart("chartContainer",
	    {
	        title: {
	            text: "Regional Sales"
	        },
	        animationEnabled: true,
	        legend: {
	            verticalAlign: "bottom",
	            horizontalAlign: "center"
	        },
	        data: [
            {
                indexLabelFontSize: 20,
                indexLabelFontFamily: "Monospace",
                indexLabelFontColor: "darkgrey",
                indexLabelLineColor: "darkgrey",
                indexLabelPlacement: "outside",
                type: "pie",
                showInLegend: true,
                indexLabel: "{label} - {per} %",
                toolTipContent: "{y} - {per} %",
                dataPoints: regionalSalesPer
            }
	        ]
	    });
        chartRegionalSales.render();
    };

    var MonthlySalesCompany = function () {
        var chartMonthlySalesCompany = new CanvasJS.Chart("chartContainer",
		{
		    theme: "theme3",
		    animationEnabled: true,
		    title: {
		        text: "Monthly Sales & Collection(Company)",
		        fontSize: 30
		    },
		    toolTip: {
		        shared: true
		    },
		    axisY: {
		        title: "Amount (Tk.)"
		    },
		    axisY2: {
		        title: ""
		    },

		    legend: {
		        verticalAlign: "top",
		        horizontalAlign: "center"
		    },
		    data: [
			{
			    type: "column",
			    name: "Sales",
			    legendText: "Sales",
			    showInLegend: true,
			    dataPoints: dataMSalesCompanySales
			},
			{
			    type: "column",
			    name: "Collection",
			    legendText: "Collection",
			    axisYType: "secondary",
			    showInLegend: true,
			    dataPoints: dataMSalesCompanyCollections
			}

		    ],
		    legend: {
		        cursor: "pointer",
		        itemclick: function (e) {
		            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
		                e.dataSeries.visible = false;
		            }
		            else {
		                e.dataSeries.visible = true;
		            }
		            chartMonthlySalesCompany.render();
		        }
		    },
		});

        chartMonthlySalesCompany.render();
    };

    var MonthlySalesBranch = function () {
        var chartMonthlySalesBranch = new CanvasJS.Chart("chartContainer",
		{
		    theme: "theme3",
		    animationEnabled: true,
		    title: {
		        text: "Monthly Sales & Collection(Branch)",
		        fontSize: 30
		    },
		    toolTip: {
		        shared: true
		    },
		    axisY: {
		        title: "Amount (Tk.)"
		    },
		    axisY2: {
		        title: ""
		    },

		    legend: {
		        verticalAlign: "top",
		        horizontalAlign: "center"
		    },
		    data: [
			{
			    type: "column",
			    name: "Sales",
			    legendText: "Sales",
			    showInLegend: true,
			    dataPoints: dataMSalesBranchSales
			},
			{
			    type: "column",
			    name: "Collection",
			    legendText: "Collection",
			    axisYType: "secondary",
			    showInLegend: true,
			    dataPoints: dataMSalesBranchCollections
			}

		    ],
		    legend: {
		        cursor: "pointer",
		        itemclick: function (e) {
		            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
		                e.dataSeries.visible = false;
		            }
		            else {
		                e.dataSeries.visible = true;
		            }
		            chartMonthlySalesBranch.render();
		        }
		    },
		});

        chartMonthlySalesBranch.render();
    };

    var CurrentSalesTargetEmployee = function () {
        var chartCurrentSalesTargetEmployee = new CanvasJS.Chart("chartContainer",
        {
            title: {
                text: "Current Sales Target & Sales (Employee)"
            },
            animationEnabled: true,
            legend: {
                cursor: "pointer",
                itemclick: function (e) {
                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                        e.dataSeries.visible = false;
                    }
                    else {
                        e.dataSeries.visible = true;
                    }
                    chartCurrentSalesTargetEmployee.render();
                }
            },
            axisY: {
                title: "Amount (Tk.)"
            },
            toolTip: {
                shared: true,
                content: function (e) {
                     
                    var str = '';
                    var total = 0;
                    var pending = 0;
                    var str3;
                    var str2;
                    for (var i = 0; i < e.entries.length; i++) {
                        var str1 = "<span style= 'color:" + e.entries[i].dataSeries.color + "'> " + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
                        total = e.entries[i].dataPoint.y + total;
                        str = str.concat(str1);
                    }
                    //pending = target - sales;
                    if (e.entries[0].dataPoint.y !== undefined) {
                        if (e.entries[1].dataPoint.y === undefined) {
                            e.entries[1].dataPoint.y = 0;
                        }
                        pending = e.entries[0].dataPoint.y - e.entries[1].dataPoint.y;
                    }
                    str2 = "<span style = 'color:DodgerBlue; '><strong>" + e.entries[0].dataPoint.label + "</strong></span><br/>";
                    //str3 = "<span style = 'color:Tomato '>Total: </span><strong>" + total + "</strong><br/>";
                    str3 = "<span style = 'color:Tomato '>Pending: </span><strong>" + pending + "</strong><br/>";

                    return (str2.concat(str)).concat(str3);
                }

            },
            data: [
            {
                type: "bar",
                showInLegend: true,
                name: "Target",
                color: "#F88F22",
                dataPoints: dataCTSalesEmployeeTargets
            },
            {
                type: "bar",
                showInLegend: true,
                name: "Sales",
                color: "#7BC95A",
                dataPoints: dataCTSalesEmployeeSales
            }

            ]
        });

        chartCurrentSalesTargetEmployee.render();
    };

    var CurrentCollectionTargetEmployee = function () {
        var chartCurrentCollectionTargetEmployee = new CanvasJS.Chart("chartContainer",
        {
            title: {
                text: "Current Collection Target & Collection (Employee)"
            },
            animationEnabled: true,
            legend: {
                cursor: "pointer",
                itemclick: function (e) {
                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                        e.dataSeries.visible = false;
                    }
                    else {
                        e.dataSeries.visible = true;
                    }
                    chartCurrentCollectionTargetEmployee.render();
                }
            },
            axisY: {
                title: "Amount (Tk.)"
            },
            toolTip: {
                shared: true,
                content: function (e) {
                     
                    var str = '';
                    var total = 0;
                    var pending = 0;
                    var str3;
                    var str2;
                    for (var i = 0; i < e.entries.length; i++) {
                        var str1 = "<span style= 'color:" + e.entries[i].dataSeries.color + "'> " + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
                        total = e.entries[i].dataPoint.y + total;
                        str = str.concat(str1);
                    }
                    //pending = target - sales;
                    if (e.entries[0].dataPoint.y !== undefined) {
                        if (e.entries[1].dataPoint.y === undefined) {
                            e.entries[1].dataPoint.y = 0;
                        }
                        pending = e.entries[0].dataPoint.y - e.entries[1].dataPoint.y;
                    }
                    str2 = "<span style = 'color:DodgerBlue; '><strong>" + e.entries[0].dataPoint.label + "</strong></span><br/>";
                    //str3 = "<span style = 'color:Tomato '>Total: </span><strong>" + total + "</strong><br/>";
                    str3 = "<span style = 'color:Tomato '>Pending: </span><strong>" + pending + "</strong><br/>";

                    return (str2.concat(str)).concat(str3);
                }

            },
            data: [
            {
                type: "bar",
                showInLegend: true,
                name: "Target",
                color: "#F88F22",
                dataPoints: dataCTCollectionEmployeeTargets
            },
            {
                type: "bar",
                showInLegend: true,
                name: "Collection",
                color: "#7BC95A",
                dataPoints: dataCTCollectionEmployeeCollections
            }

            ]
        });

        chartCurrentCollectionTargetEmployee.render();
    };


});


