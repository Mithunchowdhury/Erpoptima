app.controller("SalesCommissionReportController", function ($http, $scope, $location) {

    $scope.DateFrom = new Date();
    $scope.DateTo = null;
    $scope.StartDate = "";
    $scope.EndDate = "";


    var init = function () {
        $scope.StartDate = "";
        $scope.EndDate = "";

       


    };// end of init

    init();


    $scope.Reset=function () {

        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.SalesCommissionReport.$setPristine();



    }












});