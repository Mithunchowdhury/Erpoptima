(function () {

    app.controller('CollectionTarget', function ($scope, $http, Erpmodal) {
        
        var CollectionTargetId = 0;
        $scope.MonthId = 0;
        $scope.YearId = 0;
        $scope.EmployeeId = 0;
        var state;       
        $scope.CollectionTarget = new Object();
        $scope.Resources = new Object();
        $scope.Month = new Object();
        $scope.Year = new Object();
        $scope.Employee = new Object();

        var init = function () {           
            $scope.MonthId = 0;
            $scope.YearId = 0;
            $scope.EmployeeId = 0;
            $scope.ButtonDisabled = true;
            state = 0;
            $http({                
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employee = data;
                $scope.EmployeeId = 0;               

            });

            $http({
                url: '/Sales/CollectionTarget/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    MonthId: $scope.MonthId,
                    YearId: $scope.YearId,
                    EmployeeId:$scope.EmployeeId
                }
            }).success(function (data) {
                $scope.Resources = data;
                $scope.MonthId = 0;
                $scope.YearId = 0;
                $scope.EmployeeId = 0;

            });
            
        }//end of init      


        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        //for edit item
        $scope.setFortEdit = function (ctid) {
            state = 1;            
            CollectionTargetId = ctid.Id;
            $scope.CollectionTarget.TargetCollectionAmount = ctid.TargetCollectionAmount;
            $scope.CollectionTarget.Remarks = ctid.Remarks;
            $scope.CollectionTarget.SecCompanyId = ctid.SecCompanyId;
            $scope.CollectionTarget.CreatedBy = ctid.CreatedBy;
            $scope.CollectionTarget.CreatedDate = ConverttoDateString(ctid.CreatedDate);
            $scope.ButtonDisabled = false;

            $scope.YearId = 0;
            $scope.MonthId = 0;
            $scope.EmployeeId = 0;

            $scope.YearId = ctid.Year;           
            $scope.MonthId = ctid.Month;
            $scope.EmployeeId = ctid.HrmEmployeeId;

        };
        //end edit

     // get collectionTarget by month id
        $scope.TargetInfobyMonth = function () {
            $scope.Resources = {};
            $http({
                url: '/Sales/CollectionTarget/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    MonthId: $scope.MonthId,
                    YearId: $scope.YearId,
                    EmployeeId: $scope.EmployeeId
                }
            }).success(function (data) {
                $scope.Resources = data;

            });
        };
        //end

        // get collectionTarget by year id
        $scope.TargetInfobyYear = function () {
            $scope.Resources = {};
            $http({
                url: '/Sales/CollectionTarget/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    MonthId: $scope.MonthId,
                    YearId: $scope.YearId,
                    EmployeeId: $scope.EmployeeId
                }
            }).success(function (data) {
                $scope.Resources = data;

            });
        };
        //end

        // get collectionTarget by employee id
        $scope.TargetInfobyEmployee = function () {
            $scope.Resources = {};
            $http({
                url: '/Sales/CollectionTarget/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    MonthId: $scope.MonthId,
                    YearId: $scope.YearId,
                    EmployeeId: $scope.EmployeeId
                }
            }).success(function (data) {
                $scope.Resources = data;

            });
        };
        //end

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.CollectionTarget.Id = CollectionTargetId;
                    }
                    else { $scope.CollectionTarget.Id = 0; }
                   
                    $scope.CollectionTarget.Month = $scope.MonthId;
                    $scope.CollectionTarget.Year = $scope.YearId;
                    $scope.CollectionTarget.HrmEmployeeId = $scope.EmployeeId;
                   //  
                    $http.post("/Sales/CollectionTarget/Save", $scope.CollectionTarget).success(
                        function (data) {                            
                            Erpmodal.Save(data, "Save");                                                        
                            $scope.Reset();
                        }
                        ).error(function (error) {

                        });
                });

        };//end of Save

        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            CollectionTargetId = 0;
            state = 0;
            $scope.CollectionTarget = {};
            $scope.CollectionTargets.$setPristine();
            YearId = 0;
            MonthId = 0;
            EmployeeId = 0;

        }//end of Reset

        $scope.remove = function (ctid) {
           //  
            var index = $scope.Resources.indexOf(ctid);
            if (index > -1) {
                $scope.Resources.splice(index, 1);
            }
        };

    });

}).call(this);