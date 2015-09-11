app.controller("DailySalesReport", function ($scope, $http, $timeout) {

    $scope.Office = new Object();
    $scope.Category = new Object();    
    $scope.SubCategory = new Object();
    $scope.OfficeId = 0;
    $scope.CategoryId = 0;
    $scope.SubCategoryId = 0;


    function init() {
        $http({
            url: '/Sales/Office/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Office = data;
            $scope.OfficeId = 0;

        });

        $http({
            url: '/Sales/ChartOfProduct/GetCategories',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            // 
            $scope.Category = data;           
            $scope.CategoryId = 0;


        });
              

    }
    //cal init
    init();

    $scope.GetSubCategoryByCategoryId = function () {        
        $http.post('/Sales/ChartOfProduct/GetSubCategories?categoryId=' + $scope.CategoryId).success(
                   function (data) {
                       $scope.SubCategory = data;                       
                       $scope.SubCategoryId = 0;
                   }
                   ).error(function (error) {
                   });

    };

    //call reset function

    $scope.Reset = function () {
        init();       
        $scope.OfficeId = 0;
        $scope.CategoryId = 0;
        $scope.SubCategoryId = 0;
        $scope.DailySalesReport.$setPristine();

    }

    //end of Reset


    //$scope.ProjectInfo = function () {
    //    $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.BusinessId).success(function (data) {
    //        $scope.Projects = data;
    //    }).error(function (data) {
    //    });
    //};

   // $scope.GetSubCategoryByCategoryId = function () {
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