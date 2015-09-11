//window.location = "../Report/GetAnFChartOfAccountReport";

app.controller("CostCenterReport", function ($scope, $http, $timeout) {
    $scope.GetAnFCostCenterReport = function () {
        $http({
            url: '/Accounts/CostCenter/GetAnFCostCenterReport',
            method: 'GET',
        }).success(function (data) {
        }).error(function (data) {
        });
    };
});