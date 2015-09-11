
(function () {


    app.controller('business', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var BusinessId = 0;
        $scope.Business = {};


        var init = function () {

            state = 0;
            $scope.AnFBusiness = [];
            $scope.Business.Status = true;
            $scope.ButtonDisabled = true;

            $http.get("/Common/Business/GetCmnBusinesses").success(function (data) {
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
            BusinessId = pid.Id;
            $scope.Business.Name = pid.Name;
            $scope.Business.Type = pid.Type;
            $scope.Business.Remarks = pid.Remarks;
            $scope.Business.Prefix = pid.Prefix;
            $scope.Business.Status = pid.Status;
            $scope.Business.CreatedBy = pid.CreatedBy;
            $scope.Business.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            BusinessId = 0;
            state = 0;
            $scope.Business = {};
            $scope.Business.Status = true;
            $scope.ButtonDisabled = true;

        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Business.Id = BusinessId;
                }
                else { $scope.Business.Id = 0; }

                $http.post("/Common/Business/SaveCmnBusiness", $scope.Business).success(
                    function (data) {
                        //if (data.Success) {
                            init();
                            $scope.Reset();
                            Erpmodal.Save(data, "Save");
                        //}
                        //else { Erpmodal.Warning('Prefix already exists!'); }
                    }
                    ).error(function (error) {
                    });
            });
        };//end of Save


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = BusinessId;
                $http.post("/Common/Business/DeleteAnFCostCenter", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);