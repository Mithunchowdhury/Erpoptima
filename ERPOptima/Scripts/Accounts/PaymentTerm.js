
(function () {


    app.controller('paymentTerm', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var PaymentTermId = 0;
        $scope.PaymentTerm = {};


        var init = function () {

            state = 0;
            $scope.PaymentTerm.Status = true;
            $scope.ButtonDisabled = true;

            $http.get("/Accounts/PaymentTerm/GetPaymentTerms").success(function (data) {
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
            PaymentTermId = pid.Id;
            $scope.PaymentTerm.Name = pid.Name;
            $scope.PaymentTerm.Status = pid.Status;
            $scope.PaymentTerm.CreatedBy = pid.CreatedBy;
            $scope.PaymentTerm.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            PaymentTermId = 0;
            state = 0;
            $scope.PaymentTerm = {};
            $scope.PaymentTerm.Status = true;
            $scope.ButtonDisabled = true;
            $scope.PaymentTerms.$setPristine();

        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.PaymentTerm.Id = PaymentTermId;
                }
                else { $scope.PaymentTerm.Id = 0; }

                $http.post("/Accounts/PaymentTerm/SavePaymentTerm", $scope.PaymentTerm).success(
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
                var Id = PaymentTermId;
                $http.post("/Accounts/PaymentTerm/DeletePaymentTerm", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);