

app.controller("TrialBalanceDetailsController", function ($scope, $http, $timeout) {
    $scope.Report = { Type: "1" }
      
  
    $scope.CostCenters = new Object();


    
    function init() {
       
        $http({
            url: '/Accounts/CostCenter/GetCostCenters',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
           
            $timeout(function () {
                $scope.CostCenters = data;
                $scope.$apply();
            }, 0);
        }).error(function (data) {
        });

    }
    init();   

    $scope.GetChartOfAccountReport = function () {        

        window.location = "../Report/GetAnFChartOfAccountReport"
    };



    $scope.GetAnFChartOfAccountWIthOpeningbalanceReport = function () {
        // ;
        //$http({
        //    url: '/Accounts/Report/GetAnFChartOfAccountWIthOpeningbalanceReport',
        //    headers: { 'Content-Type': 'application/json' },
        //    data: $scope.Parmeters
        //}).success(function (data) {
        //}).error(function (data) {
        //});

       // window.location = "../Report/GetAnFChartOfAccountWIthOpeningbalanceReport?obj=" + $scope.Parmeters
    };
});