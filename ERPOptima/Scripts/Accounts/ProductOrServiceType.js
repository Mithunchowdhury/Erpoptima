
(function () {


    app.controller('ProductServiceType', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var ProductServiceTypeId = 0;
        $scope.ProductServiceType = {};


        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.AnFProductServiceType = [];
            $scope.ProductServiceType.Status = true;

            $http.get("/Accounts/ProductOrServiceType/GetProductOrServiceTypes").success(function (data) {
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
            ProductServiceTypeId = pid.Id;
            $scope.ProductServiceType.Description = pid.Description;
            $scope.ProductServiceType.Status = pid.Status;
            $scope.ProductServiceType.CreatedBy = pid.CreatedBy;
            $scope.ProductServiceType.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {

            init();
            ProductServiceTypeId = 0;
            state = 0;
            $scope.ProductServiceType = {};
            $scope.ButtonDisabled = true;
            $scope.ProductServiceType.Status = true;
            $scope.ProductServiceTypes.$setPristine();
        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.ProductServiceType.Id = ProductServiceTypeId;
                }
                else { $scope.ProductServiceType.Id = 0; }

                $http.post("/Accounts/ProductOrServiceType/SaveProductOrServiceType", $scope.ProductServiceType).success(

                    function (data) {
                        Erpmodal.Save(data, "Save");

                        $scope.Reset();

                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = ProductServiceTypeId;
                $http.post("/Accounts/ProductOrServiceType/DeleteAnFProductOrServiceType", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");

                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);