(function () {
    app.controller('AdvanceEntryController', function ($scope, $http, Erpmodal) {
        var state;
        var AdvanceEntryId=0;
       // $scope.Employee = new Object();
       
        $scope.Resources = new Object();
        //$scope.Advance = {};

        // Add by Bably
        $scope.AnFAdvance = new Object();
        $scope.RefNo = "";
        $scope.AdvanceList = [];
        $scope.EmployeeId = 0;
        $scope.Employee = new Object();


        // To automatically generate RefNo  by Bably
        function GetRefNo() {

            $http({
                url: '/Accounts/Advance/GetRefNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RefNo = data;

            });

        }//end of GetRefNo

        var init = function () {
            GetRefNo();
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.EmployeeId = 0;
            

            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Employee = data;
                $scope.EmployeeId = 0;
            });
            //$http.get("/Accounts/Advance/GetAllAdvance").success(function (data) {
            //    //datas = [];
            //    //datas = data;
            //    //$scope.tableParams.reload();
            //    $scope.Resources = data;

            //});

            // Get record for Grid View
            $http.get('/Accounts/Advance/GetByCurrentFinancialYearId').success(function (data) {
                $scope.Resources = data;
                $scope.EmployeeId = 0;

            }).error(function (data) {
            });

        }//end of init

       

        init();//init is called

        //-------------------------------Edit-------------------------------------
        $scope.setFortEdit = function (rowitem) {
            debugger;
            state = 1;
            $scope.EmployeeId = 0;
            AdvanceEntryId = rowitem.Id;
            $scope.RefNo = rowitem.RefNo;
            $scope.EmployeeId = rowitem.HrmEmployeeId;
            $scope.AnFAdvance.Date = ConverttoDateString(rowitem.Date);
            $scope.AnFAdvance.ProposedReturnDate = ConverttoDateString(rowitem.ProposedReturnDate);
            $scope.AnFAdvance.Advance = rowitem.Advance;
            $scope.AnFAdvance.Purpose = rowitem.Purpose;
            $scope.ButtonDisabled = false;

        };  //End of Edit


        //-------------------------------Reset--------------------------------------
        $scope.Reset = function () {
            init();
            state = 0;   
            EmployeeId = 0;
            AdvanceEntryId = 0;
            $scope.AnFAdvance = {};           
            $scope.ButtonDisabled = true;
            $scope.WebForm.$setPristine();

        }  //End of Reset

        //----------------------------------Save------------------------------------
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
              
                $scope.AnFAdvance.RefNo = $scope.RefNo;
                $scope.AnFAdvance.HrmEmployeeId = $scope.EmployeeId;
                $http.post('/Accounts/Advance/Save', $scope.AnFAdvance).success(function (data) {
                    Erpmodal.Save(data, "Save");
                    init();
                    $scope.Reset();
                    $.unblockUI();
                    $scope.ButtonDisabled = true;

                }).error(function (error) {
                    $.unblockUI();
                });
            });

        };//end of Save


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {

            });
        };

        //------------------------Delete--------------------
        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
                var Id = AdvanceEntryId;
                if (AdvanceEntryId != 0) {
                    $http.post('/Accounts/Advance/Delete', {Id : Id}).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.AnFAdvance = {};
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
}).call(this);