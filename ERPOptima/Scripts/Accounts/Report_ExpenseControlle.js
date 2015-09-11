//jQuery

$(document).ready(function () {
    $('.datetimepicker').datetimepicker();

});



    app.controller("ExpenseReportController", function ($http, $scope, Erpmodal ) {

        $scope.CostCenters = [];
        $scope.CostcenterId = {};
        $scope.Expens = new Object();
       

        var PopulateCostCenterCombo = function () {
            $http.get("/Accounts/CostCenter/GetCostCenters").success(function (data) {
                $scope.CostCenters = data;
            }).error(function (data) {

            });//end of getrequest
        }

        var init = function () {

            PopulateCostCenterCombo();
        };  //end of init


        init();//init is called

   
    });
