(function () {
    app.controller('DepreciationProcess', function ($scope, ngTableParams, $http, Erpmodal) {
        var state;
        $scope.depreciationProcess = new Object();
        $scope.FnYears = [];
        var init = function () {           
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Common/FinancialYear/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.FnYears = data;
            });

        }//end of init


        init();//init is called

        $scope.setFortEdit = function (pid) {

        }
        $scope.Reset = function () {
            $scope.WebForm.$setPristine();

        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
                //$scope.AnFAdjustment.Id = $scope.Id;
                //$scope.AnFAdjustment.AnFAdvanceId = $scope.AnFAdvanceId;
                //$scope.AnFAdjustment.Date = $scope.Date;
                //$scope.AnFAdjustment.AdjustedAmount = $scope.AdjustedAmount;
                //$http.post('/Accounts/Advance/SaveAdjustment', $scope.AnFAdjustment).success(function (data) {
                //    Erpmodal.Save(data, "Save");
                //    init();
                //    $scope.Reset();
                //    $.unblockUI();
                //    $scope.ButtonDisabled = true;

                //}).error(function (error) {
                //    $.unblockUI();
                //});
            });

        };//end of Save


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {

            });
        };
    });
}).call(this);