
(function () {


    app.controller('user', function ($scope, ngTableParams, $sce, $http, Erpmodal) {


        var current;
        var UserId = 0;
        var state;
        var datas = [];

        $scope.UserInfo = new Object();

        $scope.RoleIdAndName = new Object();
        $scope.Employees = new Object();
        $scope.EmployeeId;
        $scope.RoleId;
        var init = function () {
            $scope.ButtonDisabled = true;
            $scope.UserInfo.Status = true;
            state = 1;
            $http({
                url: '/Security/User/GetUsers',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
                $scope.UserInfo = {};
            });
            $http({
                url: '/Security/Role/GetRoles',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RoleIdAndName = data;
                //$scope.RoleId = 1;
            });


            GetEmployees();
            

        }//end of init

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10           // count per page
        }, {
            total: datas.length, // length of data
            getData: function ($defer, params) {
                $defer.resolve(datas.slice((params.page() - 1) * params.count(), params.page() * params.count()));

            }
        })
        ;
       
        init();//init is called

        function GetEmployees() {

            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employees = data;

            });


        }//end of GetEmployees



        $scope.setFortEdit = function (index) {
            var pid = datas[index];
            current = index;
            state = 1;
            UserId = pid.Id;
            $scope.UserInfo.LoginName = pid.LoginName;
            $scope.EmployeeId = pid.HrmEmployeeId;
            $scope.RoleId = pid.SecRoleId;
            $scope.UserInfo.Status = pid.Status;
            $scope.UserInfo.CreatedBy = pid.CreatedBy;
            $scope.UserInfo.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
        }

        $scope.Save = function (isValid) {
            if (isValid) {
                Erpmodal.Confirm('Save').then(function (result) {
                    if ($scope.UserInfo.Password == $scope.UserInfo.ConfirmPassword) {
                        if (state == 1) {
                            $scope.UserInfo.Id = UserId;
                        }
                        else { $scope.UserInfo.Id = 0; }
                        $scope.UserInfo.Password = md5($scope.UserInfo.Password);
                        $scope.UserInfo.HrmEmployeeId = $scope.EmployeeId;
                        $scope.UserInfo.SecRoleId = $scope.RoleId;
                        $http.post("/Security/User/SaveUser", $scope.UserInfo).success(
                            function (data) {

                                //if (state != 1) {
                                //    $scope.UserInfo.Id = data.OperationId;
                                //    datas.push($scope.UserInfo);
                                //}
                                //else {
                                //    datas[current] = $scope.UserInfo;
                                //}
                                //UserId = 0;
                                //state = 0;
                                //current = {};
                                //$scope.UserInfo = {};
                                Erpmodal.Save(data, "Save");
                                $scope.tableParams.reload();
                                $scope.Reset();
                                init();

                            }
                            ).error(function (error) {
                            });
                    }
                    else {
                        Erpmodal.Warning("Password and Confirm Password must be equal !");
                    }
                });
            }

        };//end of Save

        $scope.Reset = function () {
            $scope.ButtonDisabled = true;
            $scope.RoleId = 0;
            $scope.EmployeeId = 0;
            UserId = 0;
            state = 0;
            $scope.UserInfo = {};
            current = {};
            $scope.UserInfo.Status = true;
            $scope.Users.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = UserId;
                if (UserId != 0) {
                    $http.post("/Security/User/DeleteSecUser", { Id: Id }).success(function (data) {
                        datas.splice(current);
                        //UserId = 0;
                        //state = 0;
                        //current = {};
                        //$scope.UserInfo = {};

                        $scope.tableParams.reload();
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
            });
        };
    });

}).call(this);