app.controller("MngtDashboard", function ($scope, $http) {
    
    $scope.companyDashboards = [];


    var init = function () {

        $http.get("/Sales/Dashboard/GetCompanyStatus").success(function (data) {

            $scope.companyDashboards = data;

        }).error(function (data) {

        });

    };// end of init

    init();

});