
(function () {


    app.controller('BankReconciliationItem', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var BankReconciliationItemId = 0;
        $scope.BankReconciliationItem = {};
        $scope.AccountNameAndCode = new Object();
        $scope.accountName;


        var init = function () {

            state = 0;
            $scope.BankReconciliationItem.Status = true;
            $scope.BankReconciliationItem.AddOrLess = true;
            $scope.ButtonDisabled = true;

            $http.get("/Accounts/BankReconciliationItem/GetBankReconciliationItems").success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
            });
            $http({
                url: '/Accounts/ChequeBook/GetTransactionalHeadForChequeBook',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.AccountNameAndCode = data;
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

            BankReconciliationItemId = pid.Id;
            $scope.BankReconciliationItem.Name = pid.Name;
            $scope.BankReconciliationItem.Status = pid.Status;
            $scope.accountName = pid.AnFChartOfAccountId;
            $scope.BankReconciliationItem.AddOrLess = pid.AddOrLess;
            $scope.BankReconciliationItem.CreatedBy = pid.CreatedBy;
            $scope.BankReconciliationItem.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            BankReconciliationItemId = 0;
            state = 0;
            $scope.BankReconciliationItem = {};
            $scope.BankReconciliationItem.Status = true;
            $scope.ButtonDisabled = true;
            $scope.accountName = 0;
            $scope.BankReconciliationItem.AddOrLess = true;

        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.BankReconciliationItem.Id = BankReconciliationItemId;
                }
                else { $scope.BankReconciliationItem.Id = 0; }
                $scope.BankReconciliationItem.AnFChartOfAccountId = $scope.accountName;
                $http.post("/Accounts/BankReconciliationItem/SaveBankReconciliationItem", $scope.BankReconciliationItem).success(
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
                var Id = BankReconciliationItemId;
                $http.post("/Accounts/BankReconciliationItem/DeleteBankReconciliationItem", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);