(function () {

    app.controller('AreaConfig', function ($scope, $http, Erpmodal) {

        var init = function () {
            $scope.LoadAllEmployee();
            $scope.LoadAllRegion();
            $scope.LoadAllOffice();
            $scope.LoadAllDistrict();
            $scope.LoadAllThana();

        };

        $scope.LoadAllEmployee = function () {
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.AllEmployee = data;
            }).error(function (data) {

            });
        };
        $scope.LoadAllRegion = function () {
            $http({
                url: '/Sales/Region/GetAllRegion',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.AllRegion = data;
            }).error(function (data) {

            });
        };
        $scope.LoadAllOffice = function () {
            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.AllOffice = data;
            }).error(function (data) {

            });
        };
        $scope.LoadAllDistrict = function () {
            $http({
                url: '/Sales/District/GetAllDistrict',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.AllDistrict = data;
            }).error(function (data) {

            });
        };
        $scope.LoadAllThana = function () {
            $http({
                url: '/Sales/Thana/GetAllThana',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.AllThana = data;
            }).error(function (data) {

            });
        };


        $scope.setAllCheckBox = function () {
            angular.forEach($scope.Resources, function (value, key) {
                value.Status = $scope.readCheck;
            });
        };

        $scope.$watch("ShowOrHide", function (newVal, oldVal) {
             
            var dvNo = $scope.ShowOrHide;
            if (dvNo == 1) {
                $scope.RegionId = 0;
                $scope.OfficeId = 0;
                $scope.DistrictId = 0;
                $scope.ThanaId = 0;

                $scope.ListName = "Region";
                $scope.ShowRegion = false;
                $scope.ShowOffice = false;
                $scope.ShowDistrict = false;
                $scope.ShowThana = false;

                $scope.LoadAllAreas();
            }
            if (dvNo == 2) {
                $scope.RegionId = 0;
                $scope.OfficeId = 0;
                $scope.DistrictId = 0;
                $scope.ThanaId = 0;

                $scope.ListName = "Office";
                $scope.ShowRegion = true;
                $scope.ShowOffice = false;
                $scope.ShowDistrict = false;
                $scope.ShowThana = false;

                $scope.LoadAllAreas();
            }
            if (dvNo == 3) {
                $scope.RegionId = 0;
                $scope.OfficeId = 0;
                $scope.DistrictId = 0;
                $scope.ThanaId = 0;

                $scope.ListName = "District";
                $scope.ShowRegion = true;
                $scope.ShowOffice = true;
                $scope.ShowDistrict = false;
                $scope.ShowThana = false;

                $scope.LoadAllAreas();
            }
            if (dvNo == 4) {
                $scope.RegionId = 0;
                $scope.OfficeId = 0;
                $scope.DistrictId = 0;
                $scope.ThanaId = 0;

                $scope.ListName = "Thana";
                $scope.ShowRegion = true;
                $scope.ShowOffice = true;
                $scope.ShowDistrict = true;
                $scope.ShowThana = false;

                $scope.LoadAllAreas();
            }
            if (dvNo == 5) {
                $scope.RegionId = 0;
                $scope.OfficeId = 0;
                $scope.DistrictId = 0;
                $scope.ThanaId = 0;

                $scope.ListName = "Area";
                $scope.ShowRegion = true;
                $scope.ShowOffice = true;
                $scope.ShowDistrict = true;
                $scope.ShowThana = true;

                $scope.LoadAllAreas();
            }
        });

        $scope.LoadOffices = function () {
            var rid = $scope.RegionId;

            $http.post("/Sales/Office/GetByRegionId", { regionId: rid }).success(
                        function (data) {
                             
                            $scope.AllOffice = [];
                            $scope.AllOffice = data;
                            $scope.OfficeId = 0;                           
                        }).error(function (error) {
                        });
            $scope.LoadAllAreas();            
        };
        $scope.LoadDistricts = function () {
            var rid = $scope.RegionId;
            var oid = $scope.OfficeId;

            $http.get("/Sales/District/GetAll?regionId=" + rid + "&officeId=" + oid).success(
                   function (data) {
                        
                       $scope.AllDistrict = [];
                       $scope.AllDistrict = data;                       
                       $scope.DistrictId = 0;                       
                   }).error(function (error) {
                   });
            $scope.LoadAllAreas();            
        };
        $scope.LoadThanas = function () {
            var rid = $scope.RegionId;
            var oid = $scope.OfficeId;
            var did = $scope.DistrictId;

            $http.get("/Sales/Thana/GetAll?regionId=" + rid + "&officeId=" + oid + "&districtId=" + did).success(
                       function (data) {
                            
                           $scope.AllThana = [];
                           $scope.AllThana = data;                           
                           $scope.ThanaId = 0;
                       }).error(function (error) {
                       });
            $scope.LoadAllAreas();
        };
        
        $scope.LoadAllAreas = function () {
             
            var basedOnParam = $scope.ShowOrHide;
            var employeeIdParam = $scope.EmployeeId;
            var selRegionIdParam = $scope.RegionId;
            var selOfficeIdParam = $scope.OfficeId;
            var selDistrictIdParam = $scope.DistrictId;
            var selThanaIdParam = $scope.ThanaId;

            $http({
                url: '/Sales/Area/GetAreaForConfiguration',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    basedOn: basedOnParam, selemployeeId: employeeIdParam, selRegionId: selRegionIdParam,
                    selOfficeId: selOfficeIdParam, selDistrictId: selDistrictIdParam, selThanaId: selThanaIdParam
                }
            }).success(function (data) {
                 
                $scope.Resources = data;
            }).error(function (data) {

            });
        };

        $scope.Save = function () {
             
            Erpmodal.Confirm('Save').then(function (result) {
                 
                $scope.AreaConfiguration = new Object();
                $scope.AreaConfiguration.Id = 0;
                $scope.AreaConfiguration.HrmEmployeeId = $scope.EmployeeId;
                $scope.AreaConfiguration.IsRegionBased = $scope.ShowOrHide == 1 ? true : false;
                $scope.AreaConfiguration.IsOfficeBased = $scope.ShowOrHide == 2 ? true : false;
                $scope.AreaConfiguration.IsDistrictBased = $scope.ShowOrHide == 3 ? true : false;
                $scope.AreaConfiguration.IsThanaBased = $scope.ShowOrHide == 4 ? true : false;
                $scope.AreaConfiguration.IsAreaBased = $scope.ShowOrHide == 5 ? true : false;
                $scope.AreaConfiguration.Remarks = '';

                var checkedItems = [];
                angular.forEach($scope.Resources, function (value, key) {
                    if (value.Status) {
                        var obj = new Object();
                        obj.RefId = value.Id;
                        obj.Id = 0;
                        obj.SlsAreaConfigurationId = 0;
                        obj.BasedOn = $scope.ListName;
                        this.push(obj);
                    }
                },checkedItems);
                if (checkedItems.length > 0) {
                     
                    $http.post("/Sales/Area/SaveAreaConfiguration", { objConfig: $scope.AreaConfiguration, objConfigDetails: checkedItems }).success(function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();

                    }).error(function (data) {

                    });
                }
                else { Erpmodal.Warning("Please select an item from list !"); }
            });
        };//end of Save

        $scope.Reset = function () {
            $scope.ResetId();           
            $scope.Resources = [];
            $scope.Businesses.$setPristine();

        };//end of Reset
        $scope.ResetId = function () {
            $scope.ThanaId = 0;
            $scope.DistrictId = 0;
            $scope.OfficeId = 0;
            $scope.RegionId = 0;
            $scope.EmployeeId = 0;
            $scope.readCheck = false;
        };//end of Reset


        init();
    });

}).call(this);