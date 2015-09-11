app.controller("ApprovalPermissionController", function ($scope, $http, Erpmodal) {


    $scope.Modules = [];
    $scope.SelectedModule = {};
    $scope.Users = [];
    $scope.SelectedUser = {};
    $scope.Permissions = [];

    $scope.SaveDisabled = true;

    $scope.ApprovalProcessesPermission = [];

    function init() {
        $http.get("/Security/User/GetSecUsersForCombo").success(function (data) {
            $scope.Users = data;
        }).error(function (data) {


        });

        $http.get("/Security/Module/GetByCompanyId").success(function (data) {
            $scope.Modules = data;
        }).error(function (data) {

        });

    }//end of init

    init();
    $scope.GetApprovalProcessesPermission = function () {
        var url = "/Security/Approval/GetApprovalPermission?SecModuleId=" + $scope.SelectedModule;
        url = url + "&SecUserId=" + $scope.SelectedUser;
        $http.get(url).success(function (data) {

            $scope.Permissions = data;
            $scope.ApprovalProcessesPermission = _.groupBy($scope.Permissions, 'ApprovalProcessName');
            $scope.SaveDisabled = false;

        }).error(function (data) {

        });



    }// end of GetApprovalProcesses

    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            var checkedItems = [];
            var i = 1;
            angular.forEach($scope.Permissions, function (value, key) {
                if (value.Id == 0 && value.Mapped == true || (value.Id != 0 && value.Mapped == false)) {

                    var obj = new Object();
                    obj.Id = value.Id;
                    obj.CmnApprovalProcessLevelId = value.ApprovalProcessLevelId;
                    obj.SecUserId = $scope.SelectedUser;
                    obj.Mapped = value.Mapped;
                    this.push(obj);
                }

                i++;
            }, checkedItems);
            $http.post("/Security/Approval/SaveApprovalPermission", { obj: checkedItems }).success(function (data) {

                Erpmodal.Save(data, "Save");
                $scope.Reset();

            }).error(function (data) {

            });
        });
    };//end of Save

    $scope.Reset = function () {

        $scope.SelectedModule = {};
        $scope.SelectedUser = {};
        $scope.Permissions = [];

        $scope.SaveDisabled = true;

        $scope.ApprovalProcessesPermission = [];
    }
});