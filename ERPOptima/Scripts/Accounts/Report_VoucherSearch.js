//jQuery

$(document).ready(function () {
  //  $('.datepicker').datepicker();

    $("#SearchButton").click(

        function () {
            $("#Search").submit(function (event) {
                event.preventDefault();
                $(this).unbind(event);
            });
        }

        );//end of click

    //Default datetimepicer
    $(function () {
        $("#startDate").datepicker({
            dateFormat: "dd-mm-yy"
        })//.datepicker("setDate", "0");
    });
    $(function () {
        $("#endDate").datepicker({
            dateFormat: "dd-mm-yy"
        })//.datepicker("setDate", "0");
    });

});

//end jQuery

app.controller("VoucherReportController", function ($http, $scope, $location) {
    var self = this;

    $scope.VoucherList = [];

    $scope.Voucher = {};

    $scope.Projects = [];
    $scope.ProjectId = {};
    $scope.Businesses = new Object();
    $scope.BusinessId = 0;

    $scope.CostCenters = [];
    $scope.CostcenterId = {};

    $scope.SumbitShow = true;

    var dt=new Date();
    $scope.Voucher.DateFrom = $.datepicker.formatDate('dd-mm-yy', dt);
    $scope.Voucher.ToDate = $.datepicker.formatDate('dd-mm-yy', dt);

   var PopulateCostCenterCombo = function () {
        $http.get("/Accounts/CostCenter/GetCostCenters").success(function (data) {
            $scope.CostCenters = data;
        }).error(function (data) {
        });//end of getrequest
    }// end of PopulateCostCenterCombo


    var PopulateTransactionalHeadCombo = function () {


        $http.get("/Accounts/ChartofAccount/GetTransactionalHeadByCompanyId").success(function (data) {
            $scope.CostCenters = data;
        }).error(function (data) {
        });//end of getrequest



    }// end of PopulateTransactionalHeadCombo

   
    var init = function () {
        PopulateCostCenterCombo();
        $http({
            url: '/Accounts/ProjectClose/GetBusinesses',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Businesses = data;

        });
    };// end of init

    init();

    $scope.ProjectInfo = function () {
        $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.Voucher.BusinessId).success(function (data) {
            $scope.Projects = data;
        }).error(function (data) {
        });
    };

    $scope.$watch("Voucher.ReportType", function (newVal, oldVal) {
        if (newVal == 1) {
            $scope.SumbitShow = false;
        }
        else {
            $scope.SumbitShow = true;
        }
    });

    $scope.SearchVoucher = function () {
        $http.post("/Accounts/Voucher/AnFVoucherSearch", $scope.Voucher).success(function (data) {

            $scope.VoucherList = data;

        }).error(function (error) {

        });
    };
});