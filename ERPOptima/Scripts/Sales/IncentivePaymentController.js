app.controller("IncentivePayment", function ($scope, $http, Erpmodal) {
    var state = 0;
    var MainId = 0;
    $scope.Resources = new Object();


    function init() {
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.GetSRs();
              
        $scope.Find();

    };//end of init

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    $scope.Find = function () {        
        var year = 0;
        var month = 0;
        var sr = 0;

        if ($scope.MainObj !== undefined && $scope.MainObj.Year !== undefined)
            year = $scope.MainObj.Year;
        if ($scope.MainObj != undefined && $scope.MainObj.Month !== undefined)
            month = $scope.MainObj.Month;
        if ($scope.MainObj != undefined && $scope.MainObj.HrmEmployeeId !== undefined)
            sr = $scope.MainObj.HrmEmployeeId;

        $http({
            url: '/Sales/SalesIncentiveSettings/GetAllIncentivePayment',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: { year: year, month: month, salesPersonId: sr }
        }).success(function (data) {            
            $scope.Resources = data;

            //When year, month and employee entry given, if this resources is empty, calculate due 
            //commission and display in UI.
            if ($scope.Resources !== undefined && $scope.Resources.length > 0 && 
                year > 0 && month > 0 && sr > 0) {
                var commissionAmnt = 0;
                var commissionPaid = 0;
                
                for (var i = 0; i < $scope.Resources.length; i++)
                {
                    commissionAmnt += $scope.Resources[i].Commission;
                    commissionPaid += $scope.Resources[i].AmountPaid;
                }
                if(commissionAmnt >= commissionPaid)
                {
                    $scope.MainObj.Commission = commissionAmnt - commissionPaid;
                }
            }
            else if((($scope.Resources !== undefined && $scope.Resources.length <= 0) || ($scope.Resources === undefined)) &&
                (year > 0 && month > 0 && sr > 0))
            {
                $http({
                    url: '/Sales/SalesIncentiveSettings/GetDueIncentives',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { year: year, month: month, salesPersonId: sr }
                }).success(function (data) {
                     
                    $scope.MainObj.Commission = data.result;
                }).error(function (err) {
                     
                });
            }

        }).error(function(err){
        });
    };

    $scope.GetSRs = function () {
        $http({
            url: '/Sales/SalesIncentiveSettings/GetAllSalesReprasentatives',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.Employees = data;
        }).error(function (err) {
             
        });
    };


    init();

    $scope.setForEdit = function (rowitem) {
         
        state = 1;
        $scope.ButtonDisabled = false;

        $scope.MainObj = {};
        $scope.MainObj = jQuery.extend(true, {}, rowitem);
        MainId = rowitem.Id;

        $scope.MainObj.PaymentDate = ConverttoDateString(rowitem.PaymentDate);
        $scope.MainObj.CreatedDate = ConverttoDateString(rowitem.CreatedDate);
        if (rowitem.ModifiedDate !== undefined)
            $scope.MainObj.ModifiedDate = ConverttoDateString(rowitem.ModifiedDate);
    };

    $scope.ResetForm = function () {
        $scope.MainObj = new Object();

        $scope.Resources = [];
    };

    $scope.Reset = function () {
        //variable for form state
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.InsentivePayment.$setPristine();
        $scope.ResetForm();
        $scope.Find();
    };

    $scope.Save = function () {

        Erpmodal.Confirm('Save').then(function (result) {
            if (state == 1) {
                $scope.MainObj.Id = MainId;
            }
            else { $scope.MainObj.Id = 0; }

            $http.post("/Sales/SalesIncentiveSettings/SavePayment", $scope.MainObj).success(
                function (data) {
                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                    $scope.Find();
                }
                ).error(function (error) {

                });
        });


    };//end of Save
  
    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            var Id = MainId;
            if (MainId != 0) {
                $http.post("/Sales/SalesIncentiveSettings/DeletePayment", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();
                    $scope.Find();
                }).error(function () {

                })
            }
            else { Erpmodal.Warning("Please select payment!"); }
        });
    };//end of Delete


    $scope.ResetReport = function () {
        $scope.SearchObj = new Object();
    };

    $scope.ShowReport = function () {
        var year = 0;
        var month = 0;
        var sr = 0;

        if ($scope.SearchObj !== undefined && $scope.SearchObj.Year !== undefined)
            year = $scope.SearchObj.Year;
        if ($scope.SearchObj != undefined && $scope.SearchObj.Month !== undefined)
            month = $scope.SearchObj.Month;
        if ($scope.SearchObj != undefined && $scope.SearchObj.HrmEmployeeId !== undefined)
            sr = $scope.SearchObj.HrmEmployeeId;
        $http({
            url: '/Sales/SalesIncentiveSettings/ShowIncentiveReport',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: { year: year, month: month, salesPersonId: sr }
        }).success(function (data) {
             
            
            var file = new Blob([data], { type: 'application/pdf' });
            var fileURL = URL.createObjectURL(file);
            window.open(fileURL);
        }).error(function (err) {
             
        });
    };


});
