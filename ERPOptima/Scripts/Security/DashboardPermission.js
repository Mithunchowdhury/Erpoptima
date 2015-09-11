
(function () {


    app.controller('dashboardPermission', function ($scope, $http, Erpmodal) {


        var datas = [];
        $scope.CompanyUserIdAndName = new Object();
        $scope.CompanyModuleIdNName = new Object();
        $scope.Resources = new Object();
        $scope.RoleId = 0;
        $scope.ModuleId = 0;
        var init = function () {
            $scope.RoleId = 0;
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
            $http({
                url: '/Security/Role/GetRoles',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RoleIdAndName = data;
                $scope.RoleId = 0;
            }).error(function (data) {
            });


        }//end of init       

        init();//init is called     
       
        $scope.ResourceInfo = function () {
            if ($scope.RoleId == '0') {
                $scope.ModuleId = 0;
                Erpmodal.Warning("Select a Role first.");
            }
            else {
                $http({
                    url: '/Security/DashboardPermission/GetDashboardPermissionByRoleId?roleId=' + $scope.RoleId + '&moduleId=' + $scope.ModuleId,
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Resources = data;
                    $scope.ButtonDisabled = false;
                }).error(function (data) {
                });
            }
        };

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {               
                var checkedItems = [];
                angular.forEach($scope.Resources, function (value, key) {                   
                        var obj = new Object();
                        obj.SecRoleId = $scope.RoleId;
                        obj.SecDashboardId = value.Id;
                        obj.IsPermitted = value.IsPermitted;
                        this.push(obj);                  
                },
                checkedItems);
                if (checkedItems.length > 0) {
                    $http.post("/Security/DashboardPermission/Save", { secDashboardPermissionsList: checkedItems, roleId: $scope.RoleId, moduleId: $scope.ModuleId }).success(function (data) {
                        Erpmodal.Save(data, "Save");

                    }).error(function (data) {

                    });
                }
                else { Erpmodal.Warning("Please select a permission !"); }
            });
        };//end of Save
        $scope.Reset = function () {
            $scope.Resources = new Object();
            $scope.RoleId = 0;
            $scope.ModuleId = 0;
            init();
            $scope.ButtonDisabled = true;

        }//end of Reset
    });

}).call(this);