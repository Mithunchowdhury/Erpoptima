//jQuery
$(document).ready(function () { $('.datetimepicker').datetimepicker(); });
//end jQuery


app.controller("COGSController", function ($scope, $http, $timeout) {
    var self = this;
    $scope.Ledger = {};
    $scope.Projects = [];
    $scope.ProjectId = {};
    $scope.Businesses = new Object();
    $scope.BusinessId = 0;

    var CompanyInfoCombo = function () {
        $http.get("/Accounts/Report/GetCompany").success(function (data) {
            $scope.CompanyInfoCombo = data;
        }).error(function (data) {
        });//end of getrequest
    }

    function init() {
        CompanyInfoCombo();
        $http({
            url: '/Accounts/ProjectClose/GetBusinesses',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Businesses = data;
            $scope.BusinessId = 0;
        });

    }
    init();
    //$scope.ProjectInfo = function () {
    //    $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.BusinessId).success(function (data) {
    //        $scope.Projects = data;
    //    }).error(function (data) {
    //    });
    //};
    //$scope.GetChartOfAccountReport = function () {
    //    //$http({
    //    //    url: '/Accounts/Report/GetAnFChartOfAccountReport',
    //    //    method: 'GET',
    //    //}).success(function (data) {
    //    //}).error(function (data) {
    //    //});

    //    window.location = "../Report/GetAnFChartOfAccountReport"
    //};



    //$scope.GetAnFChartOfAccountWIthOpeningbalanceReport = function () {
    //    // ;
    //    //$http({
    //    //    url: '/Accounts/Report/GetAnFChartOfAccountWIthOpeningbalanceReport',
    //    //    headers: { 'Content-Type': 'application/json' },
    //    //    data: $scope.Parmeters
    //    //}).success(function (data) {
    //    //}).error(function (data) {
    //    //});

    //   // window.location = "../Report/GetAnFChartOfAccountWIthOpeningbalanceReport?obj=" + $scope.Parmeters
    //};
});