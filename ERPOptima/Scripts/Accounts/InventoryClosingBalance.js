

app.controller("InventoryClosingBalanceController", function ($scope, $http, Erpmodal) {

    $scope.YearId = {};

    $scope.ClosingBalances = [];
    $scope.Years = new Object();
    $scope.YearId = 0;
    var items = new Array();

    function init() {
        $scope.ShowLoading = false;
        $scope.SaveDisabled = true;
        $http({
            url: '/Common/FinancialYear/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Years = data;
            $scope.YearId = 0;
        });
    }//end of init

    init();  // Call of initS
    
    $scope.$watch('YearId', function (newVal, oldVal) {
        //debugger;
        if (newVal != 0 && !angular.equals({}, newVal)) {
            $scope.ShowLoading = true;
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Just a moment...</h1>' });
            $http.get("/Accounts/ClosingBalance/GetClosingBalanceByFinancialYearId?financialYearId=" + newVal).success(function (data) {
                $.unblockUI();
                $scope.ClosingBalances = data;
                $scope.CalculateDebitAndCredit();
                $scope.SaveDisabled = false;
                $scope.ShowLoading = false;
            }).error(function (data) {
                $.unblockUI();
            });
        }
    });//end of watch

    $scope.CalculateDebitAndCredit = function (obj) {
        //debugger;
        var debittotal = 0.0;
        var credittotal = 0.0;
        items.push(obj);
        _.each($scope.ClosingBalances, function (item) {
            debittotal += item.Debit;
            credittotal += item.Credit;
        });

        $scope.TotalDebit = debittotal.toFixed(2);
        $scope.TotalCredit = credittotal.toFixed(2);
    };

    $scope.Save = function () {
        Erpmodal.Confirm("Save").then(function (result) {
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
            $http.post("/Accounts/ClosingBalance/Update",
                {
                    viewModelList: items
                }).success(function (data) {

                    Erpmodal.Save(data, "Save");
                    $.unblockUI();
                    //Code for reload grid when save data
                    items = [];
                    $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Just a moment...</h1>' });
                    $http.get("/Accounts/ClosingBalance/GetClosingBalanceByFinancialYearId?financialYearId=" + $scope.YearId).success(function (data) {
                        $.unblockUI();
                        $scope.ClosingBalances = data;
                        $scope.CalculateDebitAndCredit();
  
                    }).error(function (data) {
                        $.unblockUI();
                    });
                   //
                }).error(function (data) {
                    $.unblockUI();
                });
        });
    };//end of Save

    $scope.Reset = function () {
        $scope.YearId = {};
        items = new Array();
        $scope.ClosingBalances = [];
        $scope.SaveDisabled = true;
        $scope.YearId = 0;
    };//end of Reset
});