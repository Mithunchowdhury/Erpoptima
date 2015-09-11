
(function () {

    app.controller('Thana', function ($scope, $http, Erpmodal) {


        var current;
        var ThanaId = 0;
        var state;
        var datas = [];
        $scope.Thana = new Object();
        $scope.District = new Object();
        $scope.Resources = new Object();
        $scope.Office = new Object();
        $scope.Region = new Object();
        $scope.RegionId = 0;
        $scope.OfficeId = 0;
        $scope.DistrictId = 0;

        var init = function () {
            $scope.RegionId = 0;
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Sales/Region/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Region = data;
                $scope.RegionId = 0;
            });
            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Office = data;
                $scope.OfficeId = 0;
            });           
            $http({
                url: '/Sales/District/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.District = data;
                $scope.DistrictId = 0;
            });
            $http({
                url: '/Sales/Thana/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 ;
                $scope.Resources = data;
            });

        }//end of init

       

        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (rowData) {
             ;
            state = 1;
            ThanaId = rowData.Id;
            $scope.Thana.Region = rowData.Region;
            $scope.Thana.Code = rowData.Code;
            $scope.Thana.Name = rowData.Name;
            $scope.Thana.Remarks = rowData.Remarks;
            $scope.Thana.CreatedBy = rowData.CreatedBy;
            $scope.Thana.CreatedDate = ConverttoDateString(rowData.CreatedDate);
            $scope.ButtonDisabled = false;
            $scope.RegionId = rowData.SlsRegionId;
            $scope.OfficeId = rowData.SlsOfficeId;
            $scope.DistrictId = rowData.SlsDistrictId;
        }
        $scope.LoadOfficeInfo = function () {
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.OfficeInfo();
            $scope.DistrictInfo();
            $scope.ThanaInfo();
        };
        $scope.LoadDistrictInfo = function () {
            $scope.DistrictId = 0;
            $scope.DistrictInfo();
            $scope.ThanaInfo();
        };
        $scope.LoadInfo = function () {
            $scope.ThanaInfo();
        };
        $scope.OfficeInfo = function () {
            $http.post("/Sales/Office/GetByRegionId", { regionId: $scope.RegionId }).success(
                        function (data) {
                            $scope.Office = data;
                            $scope.OfficeId = 0;
                        }
                        ).error(function (error) {
                        });
        };
        $scope.DistrictInfo = function () {
            $http.get("/Sales/District/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId ).success(
                        function (data) {
                            $scope.District = data;
                            $scope.DistrictId = 0;
                        }
                        ).error(function (error) {
                        });
        };
        $scope.ThanaInfo = function () {
            var did = $scope.DistrictId;
             ;
            $http.get("/Sales/Thana/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId).success(
                      function (data) {
                         
                          $scope.Resources = data;
                      }
                      ).error(function (error) {
                      });
        };
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Thana.Id = ThanaId;
                }
                else { $scope.Thana.Id = 0; }
                $scope.Thana.SlsDistrictId = $scope.DistrictId;
                $http.post("/Sales/Thana/Save", $scope.Thana).success(
                    function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            ThanaId = 0;
            state = 0;
            $scope.Thana = {};
            $scope.Thanas.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = ThanaId;
                if (ThanaId != 0) {
                    $http.post("/Sales/Thana/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Thana !"); }
            });
        };

    });

}).call(this);