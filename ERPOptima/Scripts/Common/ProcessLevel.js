
(function () {

    app.controller("ProcessLevelController", function ($scope, $http, Erpmodal, ngTableParams) {


        var state;
        $scope.processLevels = [];
        var id = 0;
        $scope.ProcessLevel = {};
        var current;
        var datas = [];

        $scope.GetStatusText = function (status) {

            if (status) {
                return "Active";
            }
            else {
                return "Inactive";
            }

        };//end of GetStatus
       
        var init = function () {
            $scope.ButtonDisabled = true;
            $scope.ProcessLevel.Status = true;

            state = 0;

            $http.get("/Common/ProcessLevel/GetProcessLevels").success(function (data) {
                //$scope.processLevels = [];
                //$scope.processLevels = data;
                datas = [];
                datas = data;
                $scope.tableParams.reload();

            });


        };//end of init

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

        $scope.setFortEdit = function (data) {
            state = 1;
            id = data.Id;
            $scope.ProcessLevel.Name = data.Name;
            $scope.ProcessLevel.Status = data.Status;
            $scope.ProcessLevel.CreatedBy = data.CreatedBy;
            $scope.ProcessLevel.CreatedDate = ConverttoDateString(data.CreatedDate);
            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            init();
            id = 0;
            $scope.ProcessLevel = {};
            $scope.ButtonDisabled = true;
            state = 0;
            current = {};
            $scope.ProcessLevel.Status = true;

        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.ProcessLevel.Id = id;
                }
                else {
                    $scope.ProcessLevel.Id = 0;
                }

                $http.post("/Common/ProcessLevel/Save", $scope.ProcessLevel).success(function (data) {
                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                }
                    ).error(function (error) {
                    });
            });
        };//end of Save


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = id;
                $http.post("/Common/ProcessLevel/Delete", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();
                }).error(function (error) {

                })
            });
        };





    });

}).call(this);