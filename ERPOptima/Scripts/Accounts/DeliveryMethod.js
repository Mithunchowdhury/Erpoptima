
(function () {


    app.controller('deliveryMethod', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var DeliveryMethodId = 0;
        $scope.DeliveryMethod = {};


        var init = function () {

            state = 0;
            $scope.DeliveryMethod.Status = true;
            $scope.ButtonDisabled = true;

            $http.get("/Accounts/DeliveryMethod/GetAnFDeliveryMethods").success(function (data) {
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
            DeliveryMethodId = pid.Id;
            $scope.DeliveryMethod.Name = pid.Name;
            $scope.DeliveryMethod.Status = pid.Status;
            $scope.DeliveryMethod.CreatedBy = pid.CreatedBy;
            $scope.DeliveryMethod.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            DeliveryMethodId = 0;
            state = 0;
            $scope.DeliveryMethod = {};
            $scope.DeliveryMethod.Status = true;
            $scope.ButtonDisabled = true;

        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.DeliveryMethod.Id = DeliveryMethodId;
                }
                else { $scope.DeliveryMethod.Id = 0; }

                $http.post("/Accounts/DeliveryMethod/SaveDeliveryMethod", $scope.DeliveryMethod).success(
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
                var Id = DeliveryMethodId;
                $http.post("/Accounts/DeliveryMethod/DeleteAnFDeliveryMethod", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);