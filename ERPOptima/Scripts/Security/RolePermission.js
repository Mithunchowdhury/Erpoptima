
(function () {


    app.controller('rolePermission', function ($scope, $http, Erpmodal) {


        var datas = [];
        $scope.CompanyUserIdAndName = new Object();
        $scope.CompanyModuleIdNName = new Object();

        $scope.Resources = new Object();
        $scope.Roles = new Object();
        $scope.UserId = 0;
        $scope.RoleId = 0;
        $scope.UserOrRole = '';
        $scope.userRadio;
        $scope.roleRadio;
        $scope.ModuleId = 0;
        var init = function () {

            $scope.UserOrRole = 'Role Name';
            $scope.roleRadio = '1';
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Security/Module/GetByCompanyId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.CompanyModuleIdNName = data;
                $scope.ModuleId = 0;
            }).error(function (data) {
            });
            //$http({
            //    url: '/Security/User/GetSecUsersByCompanyId',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.RoleIdAndName = data;
            //    $scope.RoleId = 0;
            //    $scope.UserId = 0;
            //}).error(function (data) {
            //});
            $http({
                url: '/Security/Role/GetRoles',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RoleIdAndName = '';
                $scope.Roles = data;
                $scope.RoleId = 0;
                $scope.UserId = 0;
                $scope.ModuleId = 0;

            }).error(function (data) {
            });
            
        }//end of init       
       
        init();//init is called
        $scope.RoleInfo = function () {
            $scope.UserOrRole = 'Role Name';
            $http({
                url: '/Security/Role/GetRoles',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RoleIdAndName = '';
                $scope.Roles = data;
                $scope.RoleId = 0;
                $scope.UserId = 0;
                $scope.ModuleId = 0;

            }).error(function (data) {
            });
        };
        $scope.UserInfo = function () {
            $scope.ButtonDisabled = true;
            $scope.UserOrRole = 'User Name';
            
            $http({
                url: '/Security/User/GetSecUsersByCompanyId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RoleIdAndName = '';
                $scope.RoleIdAndName = data;
                $scope.RoleId = 0;
                $scope.UserId = 0;
                $scope.ModuleId = 0;
            }).error(function (data) {
            });
        };
        $scope.setReadonlyCheckBox = function () {

            angular.forEach($scope.Resources, function (value, key) {
                value.Read = $scope.readCheck;
            });
        };
        $scope.setAddCheckBox = function () {

            angular.forEach($scope.Resources, function (value, key) {
                value.Add = $scope.addCheck;
            });
        };
        $scope.setEditCheckBox = function () {

            angular.forEach($scope.Resources, function (value, key) {
                value.Edit = $scope.editCheck;
            });
        };
        $scope.setDeleteCheckBox = function () {

            angular.forEach($scope.Resources, function (value, key) {
                value.Delete = $scope.deleteCheck;
            });
        };
        $scope.setPrintCheckBox = function () {

            angular.forEach($scope.Resources, function (value, key) {
                value.Print = $scope.printCheck;
            });
        };
        $scope.ResourceInfo = function () {
             ;
            var roleid = $scope.RoleId;;
            var userid = 0;
            //if ($scope.roleRadio == '1') {
            //    roleid = $scope.RoleId;
            //}
            //else { userid = $scope.RoleId; }
            $http({
                url: '/Security/RolePermission/GetResourcePermissionByRoleId?roleId=' + roleid + '&moduleId=' + $scope.ModuleId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
                $scope.ButtonDisabled = false;
            }).error(function (data) {
            });
        };
       
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                var roleid = null;
                var userid = null;
                roleid=$scope.RoleId;
 
                //if ($scope.roleRadio == '1') {
                //    roleid = $scope.RoleId;
                //}
                //else { userid = $scope.RoleId; }
                var checkedItems = [];
                angular.forEach($scope.Resources, function (value, key) {
                    if (value.Read == true || value.Add == true || value.Edit == true || value.Delete == true || value.Print == true) {
                        var obj = new Object();
                        obj.SecResourceId = value.Id;
                        obj.SecRoleId = roleid;
                        obj.SecUserId = userid;
                        obj.ReadOnly = value.Read;
                        obj.Add = value.Add;
                        obj.Edit = value.Edit;
                        obj.Delete = value.Delete;
                        obj.Print = value.Print;
                        this.push(obj);
                    }
                },
                checkedItems);
                $http.post("/Security/RolePermission/SaveSecRolePermissions", { secRolePermissionsList: checkedItems, roleId: roleid, moduleId: $scope.ModuleId }).success(function (data) {
                    Erpmodal.Save(data, "Save");

                }).error(function (data) {

                });
                //if (checkedItems.length > 0) {
                //    $http.post("/Security/RolePermission/SaveSecRolePermissions", { secRolePermissionsList: checkedItems, moduleId: $scope.ModuleId }).success(function (data) {
                //        Erpmodal.Save(data, "Save");

                //    }).error(function (data) {

                //    });
                //}
                //else { Erpmodal.Warning("Please select a permission !");}
            });
        };//end of Save
        $scope.Reset = function () {
            $scope.Resources = new Object();
            $scope.Roles = new Object();
            $scope.UserId = 0;
            $scope.RoleId = 0;
            init();
            $scope.ButtonDisabled = true;

        }//end of Reset
    });

}).call(this);