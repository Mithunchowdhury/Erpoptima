
//jQuery

$(document).ready(function () {
    $('.datetimepicker').datetimepicker();


    //$("#SearchButton").click(

    //    function () {
    //        $("#Search").submit(function (event) {
    //            event.preventDefault();
    //            $(this).unbind(event);
    //        });
    //    }

    //    );
});


//end jQuery

app.controller("GeneralLedgerReportController", function ($http, $scope) {

    var self = this;

    $scope.Ledger = {};


    //$scope.Projects = [];
    //$scope.ProjectId = {};

    $scope.CostCenters = [];
    $scope.CostcenterId = {};

    $scope.TransactionalHeads = [];

    $scope.AnFChartOfAccountId = {};
    //$scope.Businesses = new Object();
    //$scope.BusinessId = 0;


    //var PopulateProjectCombo = function () {
    //    $http.get("/Common/Project/GetProjectsByCompanyId").success(function (data) {
    //        $scope.Projects = data;
    //    }).error(function (data) {

    //    });//end of getrequest
    //}// end of PopulateProjectCombo

    var PopulateCostCenterCombo = function () {
        $http.get("/Accounts/CostCenter/GetCostCenters").success(function (data) {
            $scope.CostCenters = data;
        }).error(function (data) {
        });//end of getrequest
    }// end of PopulateCostCenterCombo


    var PopulateTransactionalHeadCombo = function () {


        $http.get("/Accounts/ChartofAccount/GetTransactionalHeadByCompanyId").success(function (data) {
            $scope.TransactionalHeads = data;
        }).error(function (data) {
        });//end of getrequest



    }// end of PopulateTransactionalHeadCombo

    var init = function () {
        $scope.Ledger.Status = 'false';
        //$scope.Ledger.Type = 1;
        PopulateCostCenterCombo();
        PopulateTransactionalHeadCombo();
        //$http({
        //    url: '/Accounts/ProjectClose/GetBusinesses',
        //    method: 'GET',
        //    headers: { 'Content-Type': 'application/json' }
        //}).success(function (data) {

        //    $scope.Businesses = data;
        //    $scope.BusinessId = 0;
        //});
    };// end of init

    init();
   
    $scope.ProjectInfo = function () {
        $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.BusinessId).success(function (data) {
            $scope.Projects = data;
        }).error(function (data) {
        });
    };


});