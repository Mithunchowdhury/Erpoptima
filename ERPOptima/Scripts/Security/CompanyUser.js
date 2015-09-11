
(function () {


    app.controller('companyUser', function ($scope, ngTableParams, $http, Erpmodal) {


        var datas = [];
        $scope.CompanyUserIdAndName = new Object();
        $scope.User = new Object();
        $scope.UserId = 0;
        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Security/User/GetSecUsersByCompanyId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.CompanyUserIdAndName = data;
                $scope.UserId = 0;
            });

            var userId = $scope.UserId;
            $http({
                url: '/Security/CompanyUser/GetCompanyUsers?userId=' + userId,
                method: 'POST',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
            }).error(function (data) {
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

       
        $scope.CompanyInfo = function () {           
            $http({
                url: '/Security/CompanyUser/GetCompanyUsers?userId=' + $scope.UserId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
                $scope.ButtonDisabled = false;
            }).error(function (data) {
            });
        };
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                
                var checkedItems = [];
                angular.forEach(datas, function (value, key) {
                    if (value.Status == true) {
                        var obj = new Object();
                        obj.SecCompanyId = value.Id;
                        obj.SecUserId = $scope.UserId;
                        obj.Status = value.Status;
                        this.push(obj);
                    }
                },
                checkedItems);
                $http.post("/Security/CompanyUser/SaveSecCompanyUser", { companyUserList: checkedItems }).success(function (data) {
                    Erpmodal.Save(data, "Save");

                }).error(function (data) {

                });
            });
        };//end of Save
       
    });

}).call(this);