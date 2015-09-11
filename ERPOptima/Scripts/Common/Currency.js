
(function () {


    app.controller('currency', function ($scope, ngTableParams, $http, Erpmodal) {
        var counter = 1;
        var state;
        var datas = [];
        var CurrencyId = 0;
        $scope.Currency = {};
        $scope.CountryId = 0;
        var init = function () {

            state = 0;
            $scope.AnFCurrency = [];
            $scope.Currency.Status = true;
            $scope.ButtonDisabled = true;
            CountryId = 0;
            $http.get("/Common/Currency/GetCurrencies").success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
            });

            $http({
                url: '/Common/Country/GetCmnCountriesIdAndName',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.CmnCountries = data;

            });
        }//end of init

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10        // count per page
        }, {
            total: datas.length, // length of data
            getData: function ($defer, params) {
                $defer.resolve(datas.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }

        });

        $scope.next = function () {
            var fraction = datas.length % 10;
            var cc = datas.length / 10;
            if (fraction > 0)
            {
                cc++;
            }
            if (cc >= counter+1) {
                counter++;
                $scope.tableParams.page(counter);
            }
        };

        $scope.previous = function () {
            if (counter > 1)
                counter--;
            $scope.tableParams.page(counter);

        }
        
        init();//init is called

        $scope.setFortEdit = function (pid) {
            $scope.mode = true;
            state = 1;
            CurrencyId = pid.Id;
            $scope.CountryId = pid.CmnCountryId;
            $scope.Currency.Name = pid.Name;
            $scope.Currency.ShortName = pid.ShortName;
            
            $scope.Currency.ExchangeRate = pid.ExchangeRate;
            $scope.Currency.Symbol = pid.Symbol;
            $scope.Currency.Status = pid.Status;
            $scope.Currency.CreatedBy = pid.CreatedBy;
            $scope.Currency.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            CurrencyId = 0;
            state = 0;
            $scope.CountryId = 0;
            $scope.Currency = {};
            $scope.Currency.Status = true;
            $scope.ButtonDisabled = true;

        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Currency.Id = CurrencyId;
                }
                else { $scope.Currency.Id = 0; }
                $scope.Currency.CmnCountryId = $scope.CountryId;
                $http.post("/Common/Currency/SaveCmnCurrency", $scope.Currency).success(
                    function (data) {
                        init();
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });
        };//end of Save


        $scope.Delete = function (cId) {
            Erpmodal.Confirm('Delete').then(function (result) {
                //var Id = CurrencyId;
                var Id = cId;
                $http.post("/Common/Currency/DeleteCmnCurrency", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);