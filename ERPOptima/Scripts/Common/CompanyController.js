//------------Log In Controller------------
app.controller("CompanyController", function ($http, $scope) {
    
    $http({
        url: '/Common/Company/CompaniesForUser',
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data) {
        $scope.companies = data;

        $scope.companyId = 1;
    }).error(function (data) {
    });
    
    $scope.setCompanyId = function () {
       
        $http({
            url: '/Common/Company/SetCompanyId',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: { Id: $scope.companyId }
        }).success(function () {
            window.location.href = '/Home/Index';
        }).error(function (data) {
        });
    };
});