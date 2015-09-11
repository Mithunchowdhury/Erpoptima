
(function () {


    app.controller('costCenter', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var CostCenterId = 0;
        $scope.CostCenter = {};
        $scope.Resources = new Object();

        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.AnFCostCenter = [];

           
            $http.get("/Accounts/CostCenter/GetCostCenters").success(function (data) {
                //datas = [];
                //datas = data;
                //$scope.tableParams.reload();
                $scope.Resources = data;

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
            CostCenterId = pid.Id;
            $scope.CostCenter.Name = pid.Name;
            $scope.CostCenter.Location = pid.Location;
            $scope.CostCenter.ContactPerson = pid.ContactPerson;
            $scope.CostCenter.ContactNo = pid.ContactNo;
            $scope.CostCenter.Remarks = pid.Remarks;
            $scope.CostCenter.Status = pid.Status;
            $scope.CostCenter.IsDefault = pid.IsDefault;
            $scope.CostCenter.CreatedBy = pid.CreatedBy;
            $scope.CostCenter.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            
            init();
            CostCenterId = 0;
            state = 0;
            $scope.CostCenter = {};
            $scope.ButtonDisabled = true;
            $scope.CostCenters.$setPristine();
        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.CostCenter.Id = CostCenterId;
                }
                else { $scope.CostCenter.Id = 0; }

                $http.post("/Accounts/CostCenter/SaveCostCenter", $scope.CostCenter).success(

                    function (data) {
                        Erpmodal.Save(data, "Save");
                        //init();
                        //CostCenterId = 0;
                        //state = 0;
                        $scope.Reset();

                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        
        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = CostCenterId;
                $http.post("/Accounts/CostCenter/DeleteAnFCostCenter", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    
                    $scope.Reset();
                   
                }).error(function () {

                })
            });
        };
    });

}).call(this);