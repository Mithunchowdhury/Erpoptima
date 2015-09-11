app.controller("SalesTargetController", function ($scope, $http, $timeout) {

    $scope.Employee = [];    
    $scope.EmployeeId = 0;
   
    function init() {
        $http({
            url: '/Hrm/Employee/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.Employee = [];
            $scope.Employee = data;
            $scope.EmployeeId = 0;
        });      
               

    }
    //cal init
    init();

    
    //call reset function

    $scope.Reset = function () {
        //init();
        $scope.EmployeeId = 0;
        $scope.SalesTargetReport.$setPristine();
        //$scope.Employee = 0;
       
    }

    //end of Reset


    
});