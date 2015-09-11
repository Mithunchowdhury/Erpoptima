


    app.controller('RptReceivableController', function ($scope, ngTableParams, $http, Erpmodal) {

        $scope.ReportReceivableAndPayableViewModel = {};

        $scope.CostCenters = [];
        $scope.CostcenterId = {};

        var PopulateCostCenterCombo = function () {
            $http.get("/Accounts/CostCenter/GetCostCenters").success(function (data) {
                $scope.CostCenters = data;
            }).error(function (data) {
            });//end of getrequest
        }// end of PopulateCostCenterCombo

        var init = function () {

            PopulateCostCenterCombo();

        }//end of init


        init();//init is called

     
        $scope.Reset = function () {

            $scope.WebForm.$setPristine();
        }

    });
