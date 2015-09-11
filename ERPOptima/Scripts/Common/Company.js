
(function () {


    app.controller('company', function ($scope, ngTableParams, $http, Erpmodal) {
        var counter = 1;
        var state;
        var datas = [];
        var CompanyId = 0;
        $scope.Company = {};
        $scope.GroupId = 0;
        var init = function () {

            state = 0;

            $scope.ButtonDisabled = true;
            CountryId = 0;
            $http.get("/Common/Company/GetCmnCompanies").success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
            });
            $http({
                url: '/Common/Group/GetGroups',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.SecGroups = data;
                $scope.GroupId = 1;               
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
        
        init();//init is called

        $scope.setFortEdit = function (pid) {
            $scope.mode = true;
            state = 1;
            CompanyId = pid.Id;
            $scope.Company.SecGroupId = pid.SecGroupId;
            $scope.Company.Name = pid.Name;
            $scope.Company.Address = pid.Address;
            $scope.Company.ContactNo = pid.ContactNo;
            $scope.Company.Email = pid.Email;
            $scope.Company.Remarks = pid.Remarks;
            $scope.Company.Web = pid.Web;
            $scope.Company.Prefix = pid.Prefix;
            $scope.Company.Logo = pid.Logo;
            $scope.Company.CreatedBy = pid.CreatedBy;
            $scope.Company.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }

        $scope.Reset = function () {
            CompanyId = 0;
            state = 0;
            $scope.CountryId = 0;
            $scope.Company = {};
            $scope.GroupId = 1;
            $scope.ButtonDisabled = true;

        }

        $scope.Save = function (isActive) {
            if (isActive)
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Company.Id = CompanyId;
                }
                else { $scope.Company.Id = 0; }
                $http.post("/Common/Company/SaveCmnCompany", $scope.Company).success(
                    function (data) {
                        Erpmodal.Save(data, "Save");
                        init();                       
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });
        };

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = CompanyId;
                $http.post("/Common/Company/DeleteCmnCompany", { Id: Id }).success(function (data) {
                    init();
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();

                }).error(function () {

                })
            });
        };
    });

}).call(this);