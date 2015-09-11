
$(document).ready(function () {
    $('.datetimepicker').datetimepicker();
});

app.controller("TrialBalanceProjectWiseController", function ($http, $scope) {

    var self = this;


    $scope.Projects = [];
    $scope.ProjectId = {};
    $scope.Businesses = new Object();
    $scope.BusinessId = 0;

    function init() {
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