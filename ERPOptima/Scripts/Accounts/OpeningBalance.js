

app.controller("mappingWithProjectController", function ($scope, $http, Erpmodal) {
    var localOpeningBalance = [];

    //$scope.Projects = [];
    $scope.YearId = {};

    $scope.OpeningBalances = [];
    $scope.Years = new Object();
    $scope.YearId = 0;
    var items = new Array();

    function init() {
        $scope.ShowLoading = false;
        $scope.SaveDisabled = true;
        $http({
            url: '/Common/FinancialYear/GetAll',//Add by muna
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Years = data;
            $scope.YearId = 0;
        });
    }//end of init
    init();
  
    $scope.$watch('YearId', function (newVal, oldVal) {
        //debugger;
        if (newVal != 0 && !angular.equals({}, newVal)) {
            $scope.ShowLoading = true;
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Just a moment...</h1>' });
            $http.get("/Accounts/OpeningBalance/GetByFinancialYearId?financialYearId=" + newVal).success(function (data) {
                $.unblockUI();
                $scope.OpeningBalances = data;
                $scope.CalculateDebitAndCredit();
                $scope.SaveDisabled = false;
                $scope.ShowLoading = false;
            }).error(function (data) {
                $.unblockUI();
            });
        }
    });//end of watch

    $scope.CalculateDebitAndCredit = function (obj) {
        debugger;
        var debittotal = 0.0;
        var credittotal = 0.0;
        items.push(obj);
        _.each($scope.OpeningBalances, function (item) {
            debittotal += item.Debit;
            credittotal += item.Credit;
        });

        $scope.TotalDebit = debittotal.toFixed(2);
        $scope.TotalCredit = credittotal.toFixed(2);
    };

    $scope.Save = function () {
        Erpmodal.Confirm("Save").then(function (result) {
            //var updatedList = _.where($scope.OpeningBalances, { IsEditable: true });
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
            $http.post("/Accounts/OpeningBalance/Update",
                {
                    //viewModelList: updatedList
                    viewModelList: items
                }).success(function (data) {

                    Erpmodal.Save(data, "Save");
                    $.unblockUI();

                    //Code for reload grid when save data
                    items = [];
                    $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Just a moment...</h1>' });
                    $http.get("/Accounts/OpeningBalance/GetByFinancialYearId?financialYearId=" + $scope.YearId).success(function (data) {
                        $.unblockUI();
                        $scope.OpeningBalances = data;
                        $scope.CalculateDebitAndCredit();

                    }).error(function (data) {
                        $.unblockUI();
                    });
                    //
                   // $scope.Reset();
                }).error(function (data) {
                    $.unblockUI();
                });
        });
    };//end of Save

    $scope.Reset = function () {
        $scope.YearId = {};
        items = new Array();
        $scope.OpeningBalances = [];
        $scope.SaveDisabled = true;
       // $scope.TotalDebit = 0.0;
        //$scope.TotalCredit = 0.0;
        $scope.YearId = 0;
    };//end of Reset
});