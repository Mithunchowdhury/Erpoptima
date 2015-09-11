
(function () {

    app.controller('employeeController', function ($scope, $http, Erpmodal) {

        var current;
        var EmployeeId = 0;
        var state;
        var datas = [];
        $scope.Employee = new Object();
        $scope.Resources = new Object();
        $scope.EmployeeIdNName = new Object();
        $scope.DesignationIdNName = new Object();
        $scope.DepartmentIdNName = new Object();
        $scope.OfficeIdNName = new Object();
        $scope.EmployeeId;
        var init = function () {
            $scope.EmployeeId=0;

            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
                $scope.EmployeeIdNName = data;
            });
            $http({
                url: '/Hrm/Department/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DepartmentIdNName = data;
            });
            $http({
                url: '/Hrm/Designation/GetByCompanyId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DesignationIdNName = data;
            });           
            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.OfficeIdNName = data;
            });

        }//end of init

       

        init();//init is called

        $scope.setFortEdit = function (pid) {
            state = 1;
            $scope.EmployeeId = pid.Id;
            $scope.Employee.Name = pid.Name;
            $scope.Employee.Phone = pid.Phone;
            $scope.Employee.Email = pid.Email;
            $scope.Employee.Address = pid.Address;
            $scope.Employee.LineManager = pid.LineManager;
            $scope.Employee.HrmDepartmentId = pid.HrmDepartmentId;
            $scope.Employee.HrmDesignationId = pid.HrmDesignationId;
            $scope.Employee.SlsOfficeId = pid.SlsOfficeId;
            $scope.Employee.CreatedBy = pid.CreatedBy;
            $scope.Employee.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.Employee.Id = $scope.EmployeeId;
                    }
                    else { $scope.Employee.Id = 0; }
                    $http.post("/Hrm/Employee/Save", $scope.Employee).success(
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
            $scope.EmployeeId = 0;
            state = 0;
            $scope.Employee = {};
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = $scope.EmployeeId;
                if ($scope.EmployeeId != 0) {
                    $http.post("/Hrm/Employee/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select an Employee !"); }
            });
        };

    });

}).call(this);