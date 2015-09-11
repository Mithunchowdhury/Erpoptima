


app.controller("AdvanceAdjustmentReportController", function ($scope, ngTableParams, $http, Erpmodal) {

   
    $scope.EmployeeId = 0;
    $scope.advances = [];

    function AdvanceRefNoCombo() {
        $http({
            url: '/Accounts/Advance/GetTransactionalHeadByCompanyId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.advances = data;
        });
    }

    var init = function () {
        AdvanceRefNoCombo();
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Employee = data;
               // $scope.EmployeeId = 0;
            });


        }//end of init


    init();//init is called


    $scope.EmployeeChange = function () {
        //alert("Ok");
        //$scope.showList = function () {
        //    $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
        //    $http.post("/Accounts/Advance/AdjustmentListSearch", { AnFAdvanceId: $scope.RefNo }).success(function (data) {
        //        $scope.AdjustmentLists = data;
        //        $.unblockUI();
        //    }).error(function (error) {
        //        $.unblockUI();
        //    });
        //};
        $http.post("/Accounts/Advance/GetRefNoByEmpId", { employeeID: $scope.EmployeeId }).success(function (data) {
                $scope.advances = data;
             
            }).error(function (error) {
                alert("Server Errors");
            });
    };

    //$scope.EmployeeChange = function () {
    //    //$scope.LoadAllDistributors();
    //    $scope.RefNoByEmployeeId();
    //    //

    //};

    //$scope.RefNoByEmployeeId = function () {
    //    //$scope.OfficeList = new Object();
    //    //$scope.Distributor.OfficeId = 0;
    //    //$scope.Distributor.DistrictId = 0;
    //    //$scope.Distributor.ThanaId = 0;
    //    $http.post("/Accounts/Advance/GetByEmployeeId", { employeeId: $scope.EmployeeId }).success(
    //               function (data) {
    //                  // $scope.OfficeList = data;
    //                   $scope.EmployeeId = 0;
    //               }
    //               ).error(function (error) {
    //               });
    //};

    // Reset
        $scope.Reset = function () {
            

        }


    });
