app.controller("TargetNAchievementReport", function ($scope, $http, $timeout) {

    $scope.Employee = new Object();    
    $scope.EmployeeId = 0;
    $scope.MonthId = 0;
    $scope.YearId = 0;  
    function init() {
        $http({
            url: '/Hrm/Employee/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Employee = data;
            $scope.EmployeeId = 0;
        });      
               

    }
    //cal init
    init();

    
    //call reset function

    $scope.Reset = function () {
        init();
        $scope.EmployeeId = 0;
        $scope.MonthId = 0;
        $scope.YearId = 0;
        $scope.TargetNAchievementReport.$setPristine();
    }

    //end of Reset


    
});