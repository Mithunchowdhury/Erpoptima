//window.location = "../Report/GetAnFChartOfAccountReport";

app.controller("ChartOfAccountReportController", function ($scope, $http, $timeout) {
    $scope.Report = { Type: "1" }

    $scope.Parmeters = { CmnProjectId: null }

    //$scope.Projects = new Object();
    //$scope.Businesses = new Object();
    //$scope.BusinessId = 0;
    $scope.Years = new Object(); // add by muna
    $scope.YearId = 0;

    //$http({
    //    url: '/Common/Project/GetProjectsByCompanyId',
    //    method: 'GET',
    //    headers: { 'Content-Type': 'application/json' }
    //}).success(function (data) {
    //   
    //    $timeout(function () {
    //        $scope.Projects = data;
    //        $scope.$apply();
    //    }, 0);
    //}).error(function (data) {
    //});
    function init() {
        //$http({
        //    url: '/Accounts/ProjectClose/GetBusinesses',
        //    method: 'GET',
        //    headers: { 'Content-Type': 'application/json' }
        //}).success(function (data) {

        //    $scope.Businesses = data;
        //    $scope.BusinessId = 0;
        //});
        $http({
            url: '/Common/FinancialYear/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }//add by muna
        }).success(function (data) {

            $scope.Years = data;
            $scope.YearId = 0;
        });

    }
    init();
    //$scope.ProjectInfo = function () {
    //    $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.BusinessId).success(function (data) {
    //        $scope.Projects = data;
    //    }).error(function (data) {
    //    });
    //};
    $scope.GetChartOfAccountReport = function () {
        //$http({
        //    url: '/Accounts/Report/GetAnFChartOfAccountReport',
        //    method: 'GET',
        //}).success(function (data) {
        //}).error(function (data) {
        //});
        //alert('Hi')
        //window.location = "../Report/GetAnFChartOfAccountReport"
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

        window.location = "../Report/GetAnFChartOfAccountWIthOpeningbalanceReport?obj=" + $scope.Parmeters
    };
});