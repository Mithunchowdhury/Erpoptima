app.controller("ProductPriceReportController", function ($scope, $http, $timeout) {
   
    $scope.Product = new Object();
    $scope.ProductId = 0;   


    function init() {

        $http({
            url: '/Sales/ChartOfProduct/GetProducts',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Product = data;
            $scope.ProductId = 0;
            

        });     

    }
    //cal init
    init();

    //call reset function

    $scope.Reset = function () {
        $scope.ProductPriceReport.$setPristine();
        $scope.ProductId = 0;

    }

    //end of Reset


    //$scope.ProjectInfo = function () {
    //    $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.BusinessId).success(function (data) {
    //        $scope.Projects = data;
    //    }).error(function (data) {
    //    });
    //};

   // $scope.GetChartOfAccountReport = function () {
        //$http({
        //    url: '/Accounts/Report/GetAnFChartOfAccountReport',
        //    method: 'GET',
        //}).success(function (data) {
        //}).error(function (data) {
        //});

        // window.location = "../Report/GetAnFChartOfAccountReport"
   // };



    //$scope.GetAnFChartOfAccountWIthOpeningbalanceReport = function () {
        // ;
        //$http({
        //    url: '/Accounts/Report/GetAnFChartOfAccountWIthOpeningbalanceReport',
        //    headers: { 'Content-Type': 'application/json' },
        //    data: $scope.Parmeters
        //}).success(function (data) {
        //}).error(function (data) {
        //});

        // window.location = "../Report/GetAnFChartOfAccountWIthOpeningbalanceReport?obj=" + $scope.Parmeters
    //};
});