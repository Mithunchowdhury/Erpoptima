
app.controller("MonthLockController", function ($scope, $http, Erpmodal) {

    var state;
    var anFMonthLockId;

    var financialYear = {};
    $scope.Months = [];
    $scope.AnFMonthLock = new Object();
    $scope.currentMonth = {};
    $scope.Years = [];
    $scope.SaveDisabled = true;
  
    var currentIndex;

    $scope.GetStatusText = function (status) {
        if (status) {
            return "Locked";
        }
        else {
            return "Unlocked";
        }
    };//end of GetStatus

    //---------------Get Month Name------------------

    $scope.GetMonthname = function (monthNumber) {
        var monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"];

        return monthNames[monthNumber - 1];
    };



    var init = function () {
        $scope.ButtonDisabled = true;
        state = 0;
        //Get Years from C#
        $http.get('/Accounts/MonthLock/GetYears').success(function (data) {
            $scope.Years = data;
        }).error(function (data) {

        });

        $http.get('/Common/FinancialYear/GetCurrentFinancialYear').success(function (data) {
            $scope.CurrentFinancialYearName = data.Name;
        }).error(function (data) {
        }).then(function () {
            $http.get('/Accounts/MonthLock/GetByCurrentFinancialYearId').success(function (data) {
                $scope.Months = data;
            }).error(function (data) {
            });
        });
        
    };//end of init

    $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
    init();  //call of init()
    $.unblockUI();

   
    //-----------------Edit row----------------------
    $scope.setFortEdit = function (rowitem) {
       
        state = 1;
        $scope.AnFMonthLock.Id = rowitem.Id;
        $scope.AnFMonthLock.MonthNo = rowitem.MonthNo;
        $scope.AnFMonthLock.Year = rowitem.Year;
        $scope.AnFMonthLock.LockingStatus = rowitem.LockingStatus;
        $scope.ButtonDisabled = false;
        //currentIndex = index;
        //$scope.currentMonth = $scope.Months[index];
    };

    //--------------Reset----------------------
    $scope.reset = function () {       
        $scope.AnFMonthLock.Year = "";
        $scope.AnFMonthLock.MonthNo = "";
        $scope.monthlocks.$setPristine();
    };

    // -------------------Save---------------------
    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
            $http.post('/Accounts/MonthLock/Save',$scope.AnFMonthLock).success(function (data) {
                Erpmodal.Save(data, "Save");
                init();
                $scope.reset();
                $.unblockUI();
                $scope.ButtonDisabled = true;
               
            }).error(function (error) {
                $.unblockUI();
            });
        });
    };

    //--------------Delete--------------------
    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
            var Id = anFMonthLockId;
            if (anFMonthLockId != 0) {
                $http.post('/Accounts/MonthLock/Delete', $scope.AnFMonthLock).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    $scope.AnFMonthLock = {};
                    state = 0;
                    $.unblockUI();
                    init();
                    $scope.ButtonDisabled = true;
                    //$scope.reset()
                }).error(function () {
                    $.unblockUI();
                })
            }
            else { Erpmodal.Warning("Please select one !"); }
        });
    };//end of Delete
   
});