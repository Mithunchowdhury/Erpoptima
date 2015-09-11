

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

app.controller("NoteScheduleReportController", function ($http, $scope) {

    var self = this;

    $scope.NoteScheduleSearchViewModel = {};

    //$scope.Businesses = new Object();
    //$scope.BusinessId = 0;

    //$scope.Projects = [];
    //$scope.ProjectId = {};

    $scope.CostCenters = [];
    $scope.CostcenterId = {};

    $scope.TransactionalHeads = [];

    $scope.AnFChartOfAccountId = {};


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


        $http.get("/Accounts/ChartofAccount/GetThirdLevel").success(function (data) {
            $scope.TransactionalHeads = data;
        }).error(function (data) {
        });//end of getrequest



    }// end of PopulateTransactionalHeadCombo

    

    
    var init = function () {
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