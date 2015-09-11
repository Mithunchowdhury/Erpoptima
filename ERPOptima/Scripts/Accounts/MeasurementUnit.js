
(function () {


    app.controller('MeasurementUnit', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var MeasurementUnitId = 0;
        $scope.MeasurementUnit = {};


        var init = function () {

            state = 0;
            $scope.MeasurementUnit.Status = true;
            $scope.ButtonDisabled = true;

            $http.get("/Accounts/MeasurementUnit/GetMeasurementUnits").success(function (data) {
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
        init();//init is called

        $scope.setFortEdit = function (pid) {
            $scope.mode = true;
            state = 1;
            MeasurementUnitId = pid.Id;
            $scope.MeasurementUnit.Name = pid.Name;
            $scope.MeasurementUnit.ShortName = pid.ShortName;
            $scope.MeasurementUnit.Status = pid.Status;
            $scope.MeasurementUnit.CreatedBy = pid.CreatedBy;
            $scope.MeasurementUnit.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            MeasurementUnitId = 0;
            state = 0;
            $scope.MeasurementUnit = {};
            $scope.MeasurementUnit.Status = true;
            $scope.ButtonDisabled = true;
            $scope.MeasurementUnits.$setPristine();


        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.MeasurementUnit.Id = MeasurementUnitId;
                }
                else { $scope.MeasurementUnit.Id = 0; }

                $http.post("/Accounts/MeasurementUnit/SaveMeasurementUnit", $scope.MeasurementUnit).success(
                    function (data) {
                        init();
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });
        };//end of Save


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = MeasurementUnitId;
                $http.post("/Accounts/MeasurementUnit/DeleteMeasurementUnit", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);