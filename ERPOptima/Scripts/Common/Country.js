
(function () {


    app.controller('country', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var CountryId = 0;
        $scope.Country = {};


        var init = function () {

            state = 0;
            $scope.Country.Status = true;
            $scope.ButtonDisabled = true;

            $http.get("/Common/Country/GetCmnCountries").success(function (data) {
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
            CountryId = pid.Id;
            $scope.Country.Name = pid.Name;
            $scope.Country.ShortName = pid.ShortName;
            $scope.Country.Status = pid.Status;
            $scope.Country.CreatedBy = pid.CreatedBy;
            $scope.Country.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            CountryId = 0;
            state = 0;
            $scope.Country = {};
            $scope.Country.Status = true;
            $scope.ButtonDisabled = true;

        }

        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Country.Id = CountryId;
                }
                else { $scope.Country.Id = 0; }

                $http.post("/Common/Country/SaveCmnCountry", $scope.Country).success(
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
                var Id = CountryId;
                $http.post("/Common/Country/DeleteCmnCountry", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);