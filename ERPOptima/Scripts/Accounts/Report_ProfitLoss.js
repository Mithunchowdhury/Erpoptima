//jQuery
$(document).ready(function () { $('.datetimepicker').datetimepicker(); });
//end jQuery

app.controller("NoteScheduleReportController", function ($http, $scope, $timeout) {

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
    $scope.ProjectInfo = function () {
        $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.BusinessId).success(function (data) {
            $scope.Projects = data;
        }).error(function (data) {
        });
    };


});