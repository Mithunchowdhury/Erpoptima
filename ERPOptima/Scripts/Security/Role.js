
(function () {


    app.controller('role', function ($scope, ngTableParams, $http, Erpmodal) {


        var current;
        var RoleId = 0;
        var state;
        var datas = [];
        $scope.Role = new Object();
        $scope.EmployeeId;
        $scope.CompanyId;
        var init = function () {
            $scope.ButtonDisabled = true;
            $scope.Role.Status = true;
            state = 0;
            $http({
                url: '/Security/Role/GetRoles',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
            });

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

        $scope.setFortEdit = function (index) {
            var pid = datas[index];
            current = index;
            state = 1;
            RoleId = pid.Id;
            $scope.Role.Name = pid.Name;
            $scope.Role.Status = pid.Status;
            $scope.Role.CreatedBy = pid.CreatedBy;
            $scope.Role.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;

        }

        $scope.Save = function (isValid) {
            if (isValid)
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Role.Id = RoleId;
                }
                else { $scope.Role.Id = 0; }

                $http.post("/Security/Role/SaveRole", $scope.Role).success(
                    function (data) {
                        // init();
                        //if (state != 1) {
                        //     ;
                        //    $scope.Role.Id = data.OperationId;
                        //    datas.push($scope.Role);
                        //}
                        //else {
                        //     ;
                        //    datas[current] = $scope.Role;
                        //}
                        init();
                        Erpmodal.Save(data, "Save");
                        $scope.tableParams.reload();
                        $scope.Reset();

                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        $scope.Reset = function () {
            $scope.ButtonDisabled = true;
            RoleId = 0;
            state = 0;
            $scope.Role = {};
            current = {};
            $scope.Role.Status = true;
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = RoleId;
                if (RoleId != 0) {
                    $http.post("/Security/Role/DeleteSecRole", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                       // datas.splice(current);
                        //RoleId = 0;
                        //state = 0;
                        //current = {};
                        // $scope.Role = {};
                        init();
                        $scope.tableParams.reload();
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a role !"); }
            });
        };

    });

}).call(this);
