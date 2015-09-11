
(function () {

    app.controller('Areas', function ($scope, $http, Erpmodal) {


        var current;
        var AreaId = 0;
        var state;
        var datas = [];
        $scope.Area = new Object();
        $scope.Thana = new Object();
        $scope.District = new Object();
        $scope.Resources = new Object();
        $scope.Office = new Object();
        $scope.Region = new Object();
        $scope.RegionId = 0;
        $scope.OfficeId = 0;
        $scope.DistrictId = 0;
        $scope.ThanaId = 0;

        var init = function () {
            $scope.RegionId = 0;
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.ThanaId = 0;

            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Sales/Region/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Region = data;
            });
            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Office = data;
            });           
            $http({
                url: '/Sales/District/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.District = data;
            });

            $http({
                url: '/Sales/Thana/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Thana = data;

            });
            $http({
                url: '/Sales/Area/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId + "&thanaId=" + $scope.ThanaId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
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
            AreaId = rowData.Id;
            $scope.Area.Region = rowData.Region;
            $scope.Area.Code = rowData.Code;
            $scope.Area.Name = rowData.Name;
            $scope.Area.Remarks = rowData.Remarks;
            $scope.Area.CreatedBy = rowData.CreatedBy;
            $scope.Area.CreatedDate = ConverttoDateString(rowData.CreatedDate);
            $scope.ButtonDisabled = false;
            $scope.RegionId = rowData.SlsRegionId;
            $scope.OfficeId = rowData.SlsOfficeId;
            $scope.DistrictId = rowData.SlsDistrictId;
            $scope.ThanaId = rowData.SlsThanaId;
            $scope.OfficeInfo();
            $scope.DistrictInfo();
            $scope.ThanaInfo();

        }
        $scope.LoadOfficeInfo = function () {
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.ThanaId = 0;
            $scope.OfficeInfo();
            $scope.DistrictInfo();
            $scope.ThanaInfo();
            $scope.AreaInfo();
        };
        $scope.LoadDistrictInfo = function () {
            $scope.DistrictId = 0;
            $scope.ThanaId = 0;
            $scope.DistrictInfo();
            $scope.ThanaInfo();
            $scope.AreaInfo();
        };
        $scope.LoadThanaInfo = function () {
            $scope.ThanaId = 0;
            $scope.ThanaInfo();
            $scope.AreaInfo();
        };
        $scope.LoadInfo = function () {
            $scope.AreaInfo();
        };
        $scope.OfficeInfo = function () {
            $http.post("/Sales/Office/GetByRegionId", { regionId: $scope.RegionId }).success(
                        function (data) {
                            $scope.Office = data;
                        }
                        ).error(function (error) {
                        });
        };
        $scope.DistrictInfo = function () {
            $http.get("/Sales/District/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId ).success(
                        function (data) {
                            $scope.District = data;
                        }
                        ).error(function (error) {
                        });
        };
        $scope.ThanaInfo = function () {
            $http.get("/Sales/Thana/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId).success(
                      function (data) {                         
                          $scope.Thana = data;
                      }
                      ).error(function (error) {
                      });
        };
        $scope.AreaInfo = function () {
            $http.get("/Sales/Area/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId + "&thanaId=" + $scope.ThanaId).success(
                      function (data) {

                          $scope.Resources = data;
                      }
                      ).error(function (error) {
                      });
        };
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Area.Id = AreaId;
                }
                else { $scope.Area.Id = 0; }
                $scope.Area.SlsThanaId = $scope.ThanaId;
                $http.post("/Sales/Area/Save", $scope.Area).success(
                    function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        $scope.Reset = function () {
            $scope.Area = {};
            $scope.RegionId = 0;
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.ThanaId = 0;
            AreaId = 0;
            state = 0;

            init();
            $scope.ButtonDisabled = true;            
            $scope.Areas.$setPristine();
           
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = AreaId;
                if (AreaId != 0) {
                    $http.post("/Sales/Area/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Area !"); }
            });
        };

    });

}).call(this);