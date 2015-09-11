app.controller("PartyListReportController", function ($http, $scope, $location) {
    
    $scope.PartyList = [];
    $scope.type = 0;
 

   
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

    
    $scope.$watch("Voucher.ReportType", function (newVal, oldVal) {
        if (newVal == 1) {
            $scope.SumbitShow = false;
        }
        else {
            $scope.SumbitShow = true;
        }
    });

    $scope.GetPartyList = function () {
        $http.post("/Sales/SalesReport/GetPartyListReport", $scope.type).success(function (data) {

            $scope.PartyList = data;

        }).error(function (error) {



        });
    };
});