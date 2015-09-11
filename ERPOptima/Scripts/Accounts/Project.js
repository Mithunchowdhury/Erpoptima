


$(document).ready(function () {
    $('.datetimepicker').datetimepicker();

  
    
});

(function () {


    app.controller('Project', function ($scope, $timeout, ngTableParams, $modal, $http, Erpmodal) {

        var current;
        var projectId = 0;
        var state;
        var datas = [];
        $scope.BusinessIdNName = new Object();
        $scope.EmployeeIdNName = new Object();
        $scope.ChequeBook = new Object();
        $scope.BusinessesId;
        $scope.EmployeeId;
        $scope.Project = {};
        $scope.LoadBusinessInfo = function (bsId) {
            $http({
                url: '/Accounts/ProjectClose/GetBusinesses',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.BusinessIdNName = data;
                $scope.BusinessesId = bsId;
            });
        };
        var init = function () {

            state = 0;
            $scope.LoadBusinessInfo(0);
            $http.get("/Common/Project/GetProjects").success(function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].StartDate != null && data[i].EndDate != null) {
                        data[i].StartDate = ConverttoDateString(data[i].StartDate);
                        data[i].EndDate = ConverttoDateString(data[i].EndDate);
                    }
                    else {
                        data[i].StartDate = "";
                        data[i].EndDate = "";
                    }
                }
                var data = data;
                datas = data;
                $scope.tableParams.reload();
            }).error(function (data) {
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

        init();//init is called
        $scope.LoadProjectInfo = function () {
            if ($scope.BusinessesId == -1) {
                $scope.open();
            }
        };

        $scope.setFortEdit = function (pid) {

            $scope.mode = true;
            state = 1;
            projectId = pid.Id;
            $scope.Project.Code = pid.Code;
            $scope.Project.Name = pid.Name;
            $scope.Project.Location = pid.Location;
            $scope.Project.StartDate = pid.StartDate;
            $scope.Project.EndDate = pid.EndDate;
            $scope.Project.Status = pid.Status;
            $scope.Project.IsDefault = pid.IsDefault;
            $scope.Project.HrmEmployeeId = pid.HrmEmployeeId;
            $scope.Project.Prefix = pid.Prefix;
            $scope.BusinessesId = pid.CmnBusinessesId;
            $scope.Project.Description = pid.Description;
            $scope.Project.CreatedBy = pid.CreatedBy;
            $scope.Project.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Project.Id = projectId;
                }
                else { $scope.Project.Id = 0; }
                $scope.Project.CmnBusinessesId = $scope.BusinessesId;
                $http.post("/Common/Project/SaveProject", $scope.Project).success(
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
            projectId = 0;
            state = 0;
            $scope.Project = {};
            current = 0;
            $scope.BusinessesId = 0;
            $scope.EmployeeId = 0;
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = projectId;
                if (projectId != 0) {
                    $http.post("/Common/Project/DeleteCmnProject", { Id: Id }).success(function (data) {

                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
            });
        };

        //open Modal
        $scope.Businesses = { Id: 0, Name: "", Prefix: "" };

        $scope.open = function (size) {
            var modalInstance = $modal.open({
                templateUrl: 'modal',
                controller: ModalInstanceCtrl,
                size: size,
                resolve: {
                    Businesses: function () {
                        return $scope.Businesses;
                    }
                }
            });

            modalInstance.result.then(function () {
                $http.post("/Common/Business/SaveCmnBusiness", $scope.Businesses).success(
                    function (data) {
                        $scope.LoadBusinessInfo(data.OperationId);
                        $scope.Businesses = {};
                    }
                    ).error(function (error) {
                    });
            }, function () {
            });

            modalInstance.opened.then(function () {
            }, function () {
            });

        };


        var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, Businesses) {
            $scope.Businesses = Businesses;
            $scope.ok = function () {
                $modalInstance.close($scope.Businesses);
            };

            $scope.cancel = function () {
                //$scope.LoadBusinessInfo(0);
                init();
                $modalInstance.dismiss('cancel');
            };
        };
    });

}).call(this);
