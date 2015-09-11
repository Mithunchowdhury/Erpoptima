
(function () {


    app.controller('approvalProcess', function ($scope, ngTableParams, $http, Erpmodal) {


        var current;
        var ApprovalProcessId=0;
        var state;
        var datas = [];
    
        $scope.ApprovalProcess = new Object();
        var init = function () {
            //------------get approval processes------------
            state = 0;
            $scope.ButtonDisabled = true;
            $scope.ApprovalProcess.Status = true;
            $scope.AnFBusiness = [];
            $http.get("/Common/ApprovalProcess/GetApprovalProcesses").success(function (data) {
               
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
        });
        init();

        $scope.setFortEdit = function (index) {
           
            var pid = datas[index];
            current = index;
            state = 1;
            ApprovalProcessId = pid.Id;
            $scope.ApprovalProcess.Name = pid.Name;
            $scope.ApprovalProcess.ShortName = pid.ShortName;
            $scope.ApprovalProcess.Status = pid.Status;
            $scope.ApprovalProcess.CreatedBy = pid.CreatedBy;
            $scope.ApprovalProcess.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;

        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.ApprovalProcess.Id = ApprovalProcessId;
                }
                else { $scope.ApprovalProcess.Id = 0; }
                $scope.ApprovalProcess.SecModuleId = 2;
                $http.post("/Common/ApprovalProcess/SaveApprovalProcess", $scope.ApprovalProcess).success(
                    function (data) {
                        // init();
                        //ApprovalProcessId = 0;
                        // $scope.loadCheckBookInfo();
                        if (state != 1) {
                            $scope.ApprovalProcess.Id = data.OperationId;
                            datas.push($scope.ApprovalProcess);
                        }
                        else {
                            datas[current] = $scope.ApprovalProcess;
                        }
                        //state = 0;
                        //current = {};
                        $scope.tableParams.reload();
                        //$scope.ApprovalProcess = {};
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        $scope.Reset = function () {
            ApprovalProcessId = 0;
            state = 0;
            $scope.ApprovalProcess = {};
            current = {};
            $scope.ButtonDisabled = true;
            $scope.ApprovalProcess.Status = true;
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = ApprovalProcessId;
                if (ApprovalProcessId != 0) {
                    $http.post("/Common/ApprovalProcess/DeleteApprovalProcess", { Id: Id }).success(function (data) {
                        datas.splice(current);
                        //ApprovalProcessId = 0;
                        //state = 0;
                        //current = {};
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