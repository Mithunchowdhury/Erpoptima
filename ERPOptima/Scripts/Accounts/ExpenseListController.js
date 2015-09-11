//jQuery
$(document).ready(function () { $('.datetimepicker').datetimepicker(); });
//end jQuery

app.controller("ExpenseListController", function ($http, $scope, $location, Erpmodal) {
    var self = this;
    var anFExpensId;
    //$scope.GoToExpense = [];
    $scope.ExpensListEntry = [];
    $scope.Particular = [];
    $scope.Ledger = {};
    $scope.Code = "";
    //$scope.ProjectId = {};
    $scope.Expens = new Object();
    //$scope.BusinessId = 0;
    $scope.CostCenters = [];
    // $scope.CostcenterId = {};
    $scope.TransactionalHeads = [];
    $scope.SumbitShow = true;
    $scope.ExpenseList = [];

    var PopulateCostCenterCombo = function () {
        $http.get("/Accounts/CostCenter/GetCostCenters").success(function (data) {
            $scope.CostCenters = data;
        }).error(function (data) {
        });//end of getrequest
    }

    var PopulateTransactionalHeadCombo = function () {
        $http.get("/Accounts/ChartofAccount/GetTransactionalHeadByCompanyId").success(function (data) {
        $scope.TransactionalHeads = data;
        }).error(function (data) {
        });
        }// end of PopulateTransactionalHeadCombo

    function GetCode() {
        $http({
            url: '/Accounts/Expense/GetCode',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Code = data;
        });
       }//end of GetCode

    var init = function () {
        $scope.Id = 0;
        GetListTag();
        $scope.ButtonDisabled = true;
        $scope.Ledger.Status = 'false';
        $scope.Ledger.Type = 1;
        PopulateCostCenterCombo();
        PopulateTransactionalHeadCombo();
        $http({
            url: '/Accounts/ProjectClose/GetBusinesses',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Businesses = data;
            $scope.BusinessId = 0;
        });

        //get all data for edit
        $http.get('/Accounts/Expense/GetByCurrentFinancialYearId').success(function (data) {
            $scope.ExpensListEntry = data;
        }).error(function (data) {
        });

        if (List != 0) { GetData(); }
        else { GetCode(); }

    };// end of init

    init();

    //search expense list
    $scope.Search = function () {
        $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
        $http.post("/Accounts/Expense/Search", { fromDate: $scope.fromDate, toDate: $scope.toDate, status: $scope.status }).success(function (data) {
            $scope.ExpenseList = data;
            $.unblockUI();

        }).error(function (error) {
            $.unblockUI();
        });
    };

    // -------------------Save---------------------
    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
            $scope.Expens.RefNo = $scope.Code;
            $scope.Expens.AnFCostCenterId = $scope.Ledger.CostcenterId;
            $scope.Expens.AnFChartOfAccountId = $scope.Ledger.AnFChartOfAccountId;

            $http.post('/Accounts/Expense/Save', $scope.Expens).success(function (data) {
                Erpmodal.Save(data, "Save");
                //init();
                $scope.Reset();
                $.unblockUI();
                $scope.ButtonDisabled = true;
                //$route.reload();
            }).error(function (error) {
                $.unblockUI();
            });
        });
    };
    
    //-----------------Edit row----------------------
    $scope.setForEdit = function (rowitem) {
        state = 1;
        //$scope.Id = rowitem.Id;
        $scope.Expens.Id = rowitem.Id;
        $scope.Code = rowitem.RefNo;
        $scope.Ledger.CostcenterId = rowitem.AnFCostCenterId;
        $scope.Ledger.AnFChartOfAccountId = rowitem.AnFChartOfAccountId;
        $scope.Expens.Date = ConverttoDateString(rowitem.Date);
        $scope.Expens.Particular = rowitem.Particular;
        $scope.Expens.BillNo = rowitem.BillNo;
        $scope.Expens.Mode = rowitem.Mode;
        $scope.Expens.Narration = rowitem.Narration;
        $scope.ButtonDisabled = false;
    };

    //go to expense entry form for edit
    $scope.GoToExpense = function (expense) {
        var url = 'Expens=' + JSON.stringify(expense) + '&ListTag=1';
        window.open("/Accounts/Expense/Expense?" + encodeURIComponent(url));
        window.focus();
    }
    function GetListTag() {
        if (window.location.search.split('?').length > 1) {
            List = angular.fromJson(decodeURIComponent(window.location.search.split('?')[1]).split('&')[1].split('=')[1]);
        }
        else {
            List = 0;
        }
    }
    function GetData() {

        if (window.location.search.split('?').length > 1) {
            var url = decodeURIComponent(window.location.search.split('?')[1]).split('&')[0].split('=')[1];
            var expense = angular.fromJson(url);
            $scope.expens = new Object();
            state = 1;
            $scope.Code = expense.RefNo;
            $scope.Expens.Id = expense.Id;
            $scope.Ledger.CostcenterId = expense.AnFCostCenterId;
            $scope.Ledger.AnFChartOfAccountId = expense.AnFChartOfAccountId;
            $scope.Expens.Particular = expense.Particular;
            $scope.Expens.Date = ConverttoDateString(expense.Date);
            $scope.Expens.BillNo = expense.BillNo;
            $scope.Expens.Mode = expense.Mode;
            $scope.Expens.Narration = expense.Narration;
            $scope.Expens.FileLocation = expense.FileLocation;
            $scope.ButtonDisabled = false;
        }
    };
    
    //--------------Delete--------------------
    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Deleting...</h1>' });
            var Id = anFExpensId;
            $scope.Expens.Id = $scope.Expens.Id;
            $scope.Expens.RefNo = $scope.Code; 
            $scope.Expens.AnFCostCenterId = $scope.Ledger.CostcenterId; 
            $scope.Expens.AnFChartOfAccountId = $scope.Ledger.AnFChartOfAccountId; 
            $scope.Expens.Particular = $scope.Expens.Particular;
            $scope.Expens.Date = $scope.Expens.Date;
            $scope.Expens.BillNo = $scope.Expens.BillNo;
            $scope.Expens.Mode = $scope.Expens.Mode;
            $scope.Expens.Narration = $scope.Expens.Narration;
            $scope.Expens.FileLocation = $scope.Expens.FileLocation;
            if (anFExpensId != 0) {
                $http.post('/Accounts/Expense/Delete', $scope.Expens).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    $scope.Expens = {};
                    state = 0;
                    $.unblockUI();
                    init();
                    $scope.Reset();
                    $scope.ButtonDisabled = true;
                }).error(function () {
                    $.unblockUI();
                })
            }
            else { Erpmodal.Warning("Please select one !"); }
        });
    };//end of Delete

    //reset expense form
    $scope.Reset = function () {
        //alert('i m reset');
        $scope.ButtonDisabled = true;
        $scope.Expens.Id = 0;
        $scope.Code = "";
        $scope.Ledger = {};
        $scope.Expens.Particular = "";
        $scope.Expens.Date = null;
        $scope.Expens.BillNo = "";
        $scope.Expens.Mode = "";
        $scope.Expens.Narration = "";
        angular.forEach(angular.element("input[type='file']"),
        function (inputElem) { angular.element(inputElem).val(null); });
        //$scope.Expens.FileLocation = null;
        //init();
        GetCode();
        //$scope.Expens.$setPristine();
    };

    //reset expense list form for listing page
    $scope.ResetList = function () {
        $scope.fromDate = null;
        $scope.toDate = null;
        $scope.status = "";
    };
});