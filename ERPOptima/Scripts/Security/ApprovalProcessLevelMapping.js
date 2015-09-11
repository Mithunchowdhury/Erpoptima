app.controller("ApprovalProcessLevelMappingController", function ($scope, $http, Erpmodal) {

    var sortableEle;
    $scope.Modules = [];
    $scope.SelectedModule = {};
    $scope.ApprovalProcesses = [];
    $scope.SelectedApprovalProcess = {};
    $scope.ApprovalProcessMapping = [];
    $scope.SaveDisabled = true;

    function init() {

        $http.get("/Security/Module/GetByCompanyId").success(function (data) {
            $scope.Modules = data;
        }).error(function (data) {

        });
    }//end of init

    init();

    $scope.GetApprovalProcesses = function () {

        $http.get("/Common/ApprovalProcess/GetByModuleId?SecModuleId=" + $scope.SelectedModule).success(function (data) {

            $scope.ApprovalProcesses = data;


        }).error(function (data) {

        });



    }// end of GetApprovalProcesses

    $scope.GetProcessLevel = function () {

        
        var url = "/Common/ProcessLevel/GetByModuleId";
        url = url + "?approvalProcessId=" + $scope.SelectedApprovalProcess;

        $http.get(url).success(function (data) {

            $scope.ApprovalProcessMapping = data;
            $scope.SaveDisabled = false;

        }).error(function (data) {

        });

    };

    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            var checkedItems = [];
            var i = 1;
            angular.forEach($scope.ApprovalProcessMapping, function (value, key) {
                if (value.Mapped == true || value.Id != 0) {
                    var obj = new Object();
                    obj.Id = value.Id;
                    obj.CmnApprovalProcessId = $scope.SelectedApprovalProcess;
                    obj.CmnProcessLevelId = value.ProcessLevelId;
                    obj.Mapped = value.Mapped;
                    obj.Priority = i;
                    this.push(obj);
                }

                i++;
            }, checkedItems);
            $http.post("/Security/Approval/SaveApprovalProcessMapping", { objApprovalProcessLevels: checkedItems }).success(function (data) {

                Erpmodal.Save(data, "Save");
                $scope.Reset();

            }).error(function (data) {

            });

        });

    };//end of Save

    $scope.Reset = function () {

        $scope.SelectedModule = {};
        $scope.ApprovalProcesses = [];
        $scope.SelectedApprovalProcess = {};
        $scope.ApprovalProcessMapping = [];
        $scope.SaveDisabled = true;

    }

    $scope.dragStart = function (e, ui) {
        ui.item.data('start', ui.item.index());
    }
    $scope.dragEnd = function (e, ui) {
        var start = ui.item.data('start'),
            end = ui.item.index();

        $scope.ApprovalProcessMapping.splice(end, 0,
            $scope.ApprovalProcessMapping.splice(start, 1)[0]);

        $scope.$apply();
    }
    sortableEle = $('#sortable').sortable({
        start: $scope.dragStart,
        update: $scope.dragEnd
    });


});