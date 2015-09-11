(function () {

    app.controller('RoutePlan', function ($scope, $http, Erpmodal) {
      
        $scope.days = [];

        $scope.Routes = new Object();
       
        $scope.Offices = new Object();
        $scope.Employees = new Object();
        $scope.Weeks = new Object();
        $scope.EmployeeId;
        $scope.Sat = [];
        $scope.Sun = [];
        $scope.Mon = [];
        $scope.Tue = [];
        $scope.Wed = [];
        $scope.Thu = [];
        $scope.Plans = new Object();
        $scope.Plan = new Object();
        $scope.PlanDetails = new Object();
        $scope.CurrentWeekNo = 0;
        $scope.NoofWeeks = 52;
      
        var objId = 0;
        var offc = 0;
        var state = 0;
        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;

            //employee
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employees = data;
                
            });
            $http({
                url: '/Sales/RouteSetup/GetAllWeeks',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Weeks = data;
            });
        }//end of init

        function loadRoutes() {
            $http({
                url: '/Sales/RouteSetup/GetAllByOfficeId',
                method: 'GET',
                params: { officeId: offc },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Routes = data;
            });
        }
        $scope.Reset = function () {
            $scope.Employee = new Object();
            $scope.week = new Object();
            $scope.Routes = new Object();
            $scope.Title = "";
            $scope.days = [];
            $scope.Sat = [];
            $scope.Sun = [];
            $scope.Mon = [];
            $scope.Tue = [];
            $scope.Wed = [];
            $scope.Thu = [];
            var objId = 0;
            var offc = 0;
            var state = 0;
            $scope.RoutePlan.$setPristine();
        }
        function loadPlans() {
            $http({
                url: '/Sales/RouteSetup/GetAllPlansByEmployeeId',
                method: 'GET',
                params: { employeeId: angular.fromJson($scope.Employee).Id },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Plans = data;
            });
        }
        function loadPlanDetails() {
            $http({
                url: '/Sales/RouteSetup/GetDetailsByPlanId',
                method: 'GET',
                params: { employeeId: angular.fromJson($scope.Employee).Id },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.PlanDetails = data;
            });
        }

        init();//init is called
        $scope.EmployeeChanged = function() {
            
            offc = angular.fromJson($scope.Employee).SlsOfficeId;
            loadRoutes();
            loadPlans();
        };
        $scope.WeekChanged = function () {
       
            $scope.days = [];
            var week = new Object();
            week = angular.fromJson($scope.week);
            var startDate = parseDate(week.Start);
            var endDate = parseDate(week.End);
            for (var dt = moment(startDate) ; dt.diff(endDate) <= 0; dt.add('days', 1)) {
                $scope.days.push(dt.format('DD-MMM-YYYY'));
            }
        };
        $scope.Delete = function (record) {
            objId = record.Id;
            Erpmodal.Confirm('Delete').then(function (result) {
                $http.post("/RouteSetup/DeleteRoutePlanById", { Id: objId }).success(function (data) {
                    $scope.Plans.pop(record);
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();
                }).error(function (data) {
                    Erpmodal.Delete(data, "Error");
                });
            });
        }
        $scope.setFortEdit = function (record) {

            state = 1;
            objId = record.Id;
            var weekNo = record.WeekNo;
            var employeeid = record.HrmEmplyeeId;
            $scope.Title = record.Title;
            for (var i = 0; i < $scope.Weeks.length; i++) {
                if ($scope.Weeks[i].WeekNo == weekNo) {
                    $scope.week = angular.toJson($scope.Weeks[i]);
                }
            }
            loadPlans();
            //$scope.Sat[0] = '19';
            angular.forEach($scope.Routes, function (item) {
                $scope.Sat[item.Id] = true;
            });
            //details
            //for (var i = 0; i < $scope.PlanDetails.length; i++) {
                
            //}
            //for (var i = 0; i < $scope.Employees.length; i++) {
            //    if ($scope.Employees[i].Id == employeeid) {
            //        $scope.Employee = $scope.Employees[i];
            //    }
            //}
        };
        $scope.Save = function () {
            Erpmodal.Confirm("Save").then(function (result) {
                if (state == 1) {
                    $scope.Plan.Id = objId;
                }
                else {
                    $scope.Plan.Id = 0;
                }

                var week = new Object();
                week = angular.fromJson($scope.week);
                var startDate = parseDate(week.Start);
                var endDate = parseDate(week.End);
                $scope.Plan.HrmEmployeeId = angular.fromJson($scope.Employee).Id;
                $scope.Plan.WeekNo = week.WeekNo;
                $scope.Plan.DateFrom = startDate;
                $scope.Plan.DateTo = endDate;
                $scope.Plan.Title = $scope.Title;
                $scope.Plan.Status = 0;
                $scope.Plan.SlsRoutePlanDetails = [];
                //Set suterdays
                for (var i = 0; i < $scope.Sat.length; i++) {
                    if ($scope.Sat[i] != "undefined" && $scope.Sat[i] != 0) {
                        var record = new Object();
                        record.Id = 0;
                        record.SlsRoutePlanId = 0;
                        record.Date = parseDate($scope.days[0]);
                        record.SlsRouteId = $scope.Sat[i];
                        $scope.Plan.SlsRoutePlanDetails.push(record);
                    }
                }
                //Set sundays
                for (var i = 0; i < $scope.Sun.length; i++) {
                    if (typeof $scope.Sun[i] != "undefined" && $scope.Sun[i] != 0) {
                        var record = new Object();
                        record.Id = 0;
                        record.SlsRoutePlanId = 0;
                        record.Date = parseDate($scope.days[1]);
                        record.SlsRouteId = $scope.Sun[i];
                        $scope.Plan.SlsRoutePlanDetails.push(record);
                    }
                }
                //Set mondays
                for (var i = 0; i < $scope.Mon.length; i++) {
                    if (typeof $scope.Mon[i] != "undefined" && $scope.Mon[i] != 0) {
                        var record = new Object();
                        record.Id = 0;
                        record.SlsRoutePlanId = 0;
                        record.Date = parseDate($scope.days[2]);
                        record.SlsRouteId = $scope.Mon[i];
                        $scope.Plan.SlsRoutePlanDetails.push(record);
                    }
                }
                //Set tuesdays
                for (var i = 0; i < $scope.Tue.length; i++) {
                    if (typeof $scope.Tue[i] != "undefined" && $scope.Tue[i] != 0) {
                        var record = new Object();
                        record.Id = 0;
                        record.SlsRoutePlanId = 0;
                        record.Date = parseDate($scope.days[3]);
                        record.SlsRouteId = $scope.Tue[i];
                        $scope.Plan.SlsRoutePlanDetails.push(record);
                    }
                }
                //Set wednesdays
                for (var i = 0; i < $scope.Wed.length; i++) {
                    if (typeof $scope.Wed[i] != "undefined" && $scope.Wed[i] != 0) {
                        var record = new Object();
                        record.Id = 0;
                        record.SlsRoutePlanId = 0;
                        record.Date = parseDate($scope.days[4]);
                        record.SlsRouteId = $scope.Wed[i];
                        $scope.Plan.SlsRoutePlanDetails.push(record);
                    }
                }
                //Set thusdays
                for (var i = 0; i < $scope.Thu.length; i++) {
                    if (typeof $scope.Thu[i] != "undefined" && $scope.Thu[i] != 0) {
                        var record = new Object();
                        record.Id = 0;
                        record.SlsRoutePlanId = 0;
                        record.Date = parseDate($scope.days[5]);
                        record.SlsRouteId = $scope.Thu[i];
                        $scope.Plan.SlsRoutePlanDetails.push(record);
                    }
                }
                $http.post("/RouteSetup/SaveRoutePlan",
                        $scope.Plan
                    ).success(function (data) {
                         ;
                        $scope.Reset();
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }).error(function (data) {
                        Erpmodal.Save(data, "Error");
                    });
            });
        };//end of Save
    });

}).call(this);