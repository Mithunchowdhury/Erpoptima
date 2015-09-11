
(function () {

    app.controller('department', function ($scope, $http, Erpmodal) {


        var current;
        var DepartmentId = 0;
        var state;
        var datas = [];
        $scope.Department = new Object();
        $scope.Resources = new Object();
        $scope.Employee = new Object();
        $scope.EmployeeId;
        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
           
            //employee
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employee = data;
            });
            $http({
                url: '/Hrm/Department/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 ;
                $scope.Resources = data;
            });

        }//end of init

       

        init();//init is called

        $scope.setFortEdit = function (pid) {
            state = 1;
            DepartmentId = pid.Id;
            pid.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.Department = pid;
            //$scope.Department.ShortName = pid.ShortName;
            $scope.EmployeeId = pid.InCharge;
            //$scope.Department.Description = pid.Description;

            $scope.ButtonDisabled = false;
        }

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.Department.Id = DepartmentId;
                    }
                    else { $scope.Department.Id = 0; }
                    $scope.Department.InCharge = $scope.EmployeeId;
                    $http.post("/Hrm/Department/Save", $scope.Department).success(
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
            DepartmentId = 0;
            $scope.EmployeeId = 0;
            state = 0;
            $scope.Department = {};
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = DepartmentId;
                if (DepartmentId != 0) {
                    $http.post("/Hrm/Department/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Department !"); }
            });
        };

    });

}).call(this);