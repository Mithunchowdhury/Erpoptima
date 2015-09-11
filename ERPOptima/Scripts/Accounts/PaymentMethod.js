
(function () {


    app.controller('paymentMethod', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var PaymentMethodId = 0;
        $scope.PaymentMethod = {};


        var init = function () {

            state = 0;
            $scope.PaymentMethod.Status = true;
            $scope.ButtonDisabled = true;

            $http.get("/Accounts/PaymentMethod/GetPaymentMethods").success(function (data) {
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
            PaymentMethodId = pid.Id;
            $scope.PaymentMethod.Name = pid.Name;
            $scope.PaymentMethod.Status = pid.Status;
            $scope.PaymentMethod.CreatedBy = pid.CreatedBy;
            $scope.PaymentMethod.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            PaymentMethodId = 0;
            state = 0;
            $scope.PaymentMethod = {};
            $scope.PaymentMethod.Status = true;
            $scope.ButtonDisabled = true;
            $scope.PaymentMethods.$setPristine();

        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.PaymentMethod.Id = PaymentMethodId;
                }
                else { $scope.PaymentMethod.Id = 0; }

                $http.post("/Accounts/PaymentMethod/SavePaymentMethod", $scope.PaymentMethod).success(
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
                var Id = PaymentMethodId;
                $http.post("/Accounts/PaymentMethod/DeletePaymentMethod", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);