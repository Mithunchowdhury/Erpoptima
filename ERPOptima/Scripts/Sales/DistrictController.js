(function () {

    app.controller('District', function ($scope, $http, Erpmodal) {


        var current;
        var DistrictId = 0;
        $scope.RegionId = 0;
        $scope.OfficeId = 0;
        var state;
        var datas = [];
        $scope.District = new Object();
        $scope.Resources = new Object();
        $scope.Office = new Object();
        $scope.Region = new Object();

        var init = function () {
            $scope.RegionId = 0;
            $scope.OfficeId = 0;

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
                $scope.Resources = data;
                $scope.OfficeId = 0;
                $scope.RegionId = 0;

            });
                    
        }//end of init

       

        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (pid) {
            state = 1;
            DistrictId = pid.Id;
            $scope.District.Code = pid.Code;
            $scope.District.Name = pid.Name;
            $scope.District.Remarks = pid.Remarks;
            $scope.District.CreatedBy = pid.CreatedBy;
            $scope.District.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
            $scope.RegionId = pid.SlsRegionId;
            $scope.OfficeId = pid.SlsOfficeId;

        }
        $scope.OfficeInfoByRegionId = function () {
            $scope.OfficeId = 0;
            $scope.DistrictInfo();
            $http.post("/Sales/Office/GetByRegionId", { regionId: $scope.RegionId }).success(
                       function (data) {
                           $scope.Office = data;
                           $scope.OfficeId = 0;
                       }
                       ).error(function (error) {
                       });
        };
        $scope.DistrictInfo = function () {
            $http.get("/Sales/District/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId).success(
                      function (data) {
                           ;
                          $scope.Resources = data;
                      }
                      ).error(function (error) {
                      });
        };
        $scope.Save = function (isValid) {
            var cLength=$scope.District.Code;
            if (cLength.length <= 2) {
                if (isValid)
                    Erpmodal.Confirm('Save').then(function (result) {
                        if (state == 1) {
                            $scope.District.Id = DistrictId;
                        }
                        else { $scope.District.Id = 0; }
                        $scope.District.SlsOfficeId = $scope.OfficeId;
                        $http.post("/Sales/District/Save", $scope.District).success(
                            function (data) {
                                Erpmodal.Save(data, "Save");
                                $scope.Reset();
                            }
                            ).error(function (error) {
                            });
                    });
            }
            else { Erpmodal.Warning("Code length not more then 2.");}

        };//end of Save

        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            DistrictId = 0;
            state = 0;
            $scope.District = {};
            OfficeId = 0;
            RegionId = 0;
            $scope.Districts.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = DistrictId;
                if (DistrictId != 0) {
                    $http.post("/Sales/District/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a District !"); }
            });
        };

    });

}).call(this);