
(function () {

    app.controller('AreaConfig', function ($scope, $http, Erpmodal) {


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
        $scope.Employee = new Object();
        $scope.AreaConfiguration = new Object();
        $scope.RegionId = 0;
        $scope.OfficeId = 0;
        $scope.DistrictId = 0;
        $scope.ThanaId = 0;

        $scope.ShowRegion = false;
        $scope.ShowOffice = false;
        $scope.ShowDistrict = false;
        $scope.ShowThana = false;
        $scope.ShowArea = false;
        $scope.ShowOrHide = 0;
        $scope.ListName = "";
        $scope.EmployeeId = 0;
        $scope.readCheck = false;
        var init = function () {
            $scope.ListName = "";
            $scope.ShowRegion = false;
            $scope.ShowOffice = false;
            $scope.ShowDistrict = false;
            $scope.ShowThana = false;
            $scope.ShowArea = false;
            $scope.ShowOrHide = 0;

            $scope.EmployeeId=0;
            $scope.RegionId = 0;
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.ThanaId = 0;
            $scope.readCheck = false;
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employee = data;
            });
            $http({
                url: '/Sales/Region/GetAllRegion',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Region = data;
                $scope.RegionId = 0;
            });
            //$http({
            //    url: '/Sales/Office/GetAll',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.Office = data;
            //    $scope.OfficeId = 0;
            //});
            //$http({
            //    url: '/Sales/District/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId,
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.District = data;
            //    $scope.DistrictId = 0;
            //});
            //$http({
            //    url: '/Sales/Thana/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId,
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.Thana = data;
            //    $scope.ThanaId = 0;
            //});           

        }//end of init

        init();//init is called

        $scope.LoadInfo = function () {
            $http({
                url: '/Sales/Area/GetAreaConfigurationByEmployee?employeeId=' + $scope.EmployeeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 ;
                if (data.length > 0) {
                    if (data[0].IsRegionBased != undefined && data[0].IsRegionBased==true) {
                        $scope.ShowOrHide = 1;
                       // $scope.Resources = data;
                        //$scope.RegionInfo();
                    }
                    else if (data[0].IsOfficeBased != undefined && data[0].IsOfficeBased) {
                        $scope.RegionId = data[0].SlsRegionId;
                        $scope.ShowOrHide = 2;
                        //$scope.Resources = data;
                       // $scope.OfficeInfo();
                    }
                    else if (data[0].IsDistrictBased != undefined && data[0].IsDistrictBased) {
                        $scope.RegionId = 1;
                        $scope.OfficeId = data[0].SlsOfficeId;

                        $scope.ShowOrHide = 3;
                        //$scope.Resources = data;
                        //$scope.DistrictInfo();
                    }
                    else if (data[0].IsThanaBased != undefined && data[0].IsThanaBased) {
                        $scope.ShowOrHide = 4;
                        $scope.Resources = data;
                        $scope.DistrictId = data[0].SlsDistrictId;

                        //$scope.ThanaInfo();
                    }
                    else if (data[0].IsAreaBased != undefined && data[0].IsAreaBased) {
                        $scope.ShowOrHide = 5;
                        $scope.Resources = data;
                        $scope.ThanaId = data[0].SlsThanaId;
                        //$scope.AreaInfo();
                    }
                }
                else {
                    $scope.ListName = "";
                    $scope.Resources = {};
                }
            });
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
        //$scope.LoadInfo = function () {
        //    $scope.AreaInfo();
        //};
        $scope.RegionInfo = function () {
            $http({
                url: '/Sales/Region/GetRegionByEmployee?employeeId='+$scope.EmployeeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Resources = data;
            });
        };
        $scope.OfficeInfo = function () {
            var rid = $scope.RegionId;
            $scope.Office = {};
            if ($scope.ShowOrHide == 2) {
                $http.post("/Sales/Office/GetOfficeByEmployee", { employeeId: $scope.EmployeeId, regionId: rid }).success(
                            function (data) {
                                 
                                $scope.Resources = data;
                            }
                            ).error(function (error) {
                            });
            }
            else {
                $http.post("/Sales/Office/GetByRegionId", { regionId: rid }).success(
                            function (data) {
                                 
                                $scope.Resources = {};
                                $scope.Office = data;
                            }
                            ).error(function (error) {
                            });
            }

        };
        $scope.DistrictInfo = function () {
            $scope.District = {};
            if ($scope.ShowOrHide == 3) {
                $http.post("/Sales/District/GetDistrictByEmployee", { employeeId: $scope.EmployeeId, officeId: $scope.OfficeId }).success(
                            function (data) {
                                 
                                $scope.Resources = data;
                            }
                            ).error(function (error) {
                            });
            }
            else {               
                $http.get("/Sales/District/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId).success(
                       function (data) {
                            
                           $scope.Resources = {};
                           $scope.District = data;
                       }
                       ).error(function (error) {
                       });
            }
           
        };
        $scope.ThanaInfo = function () {
            $scope.Thana = {};
            if ($scope.ShowOrHide == 4) {
                $http.post("/Sales/Thana/GetThanaByEmployee", { employeeId: $scope.EmployeeId, districtId: $scope.DistrictId }).success(
                            function (data) {
                                 
                                $scope.Resources = data;
                            }
                            ).error(function (error) {
                            });
            }
            else {
                $http.get("/Sales/Thana/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId).success(
                           function (data) {
                                
                           $scope.Resources = {};
                           $scope.Thana = data;
                           }
                       ).error(function (error) {
                       });
            }           
        };
        $scope.AreaInfo = function () {
             
            $scope.Area = {};
            if ($scope.ShowOrHide == 5) {
                $http.post("/Sales/Area/GetAreaByEmployee", { employeeId: $scope.EmployeeId, thanaId: $scope.ThanaId }).success(
                            function (data) {
                                 
                                $scope.Resources = data;
                            }
                            ).error(function (error) {
                            });
            }
            else {
                $http.get("/Sales/Area/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId + "&thanaId=" + $scope.ThanaId).success(
                          function (data) {
                               
                              $scope.Area = data;
                          }
                          ).error(function (error) {
                          });
            }
        };
        $scope.$watch("ShowOrHide", function (newVal, oldVal) {
             
                var dvNo = $scope.ShowOrHide;
                if (dvNo == 1) {
                    $scope.ListName = "Region";
                    $scope.RegionInfo();
                    $scope.ShowRegion = false;
                    $scope.ShowOffice = false;
                    $scope.ShowDistrict = false;
                    $scope.ShowThana = false;
                    //$scope.ResetId();
                }
                if (dvNo == 2) {
                    $scope.ListName = "Office";
                    //$scope.Resources = {};
                    $scope.OfficeInfo();
                    $scope.ShowRegion = true; 
                    $scope.ShowOffice = false;
                    $scope.ShowDistrict = false;
                    $scope.ShowThana = false;
                   // $scope.ResetId();
                }
                if (dvNo == 3) {
                    $scope.ListName = "District";
                    //$scope.Resources = {};
                    //$scope.OfficeInfo();
                    $scope.DistrictInfo();
                    $scope.ShowRegion = true;
                    $scope.ShowOffice = true;
                    $scope.ShowDistrict = false;
                    $scope.ShowThana = false;
                    //$scope.ResetId();
                }
                if (dvNo == 4) {
                    $scope.ListName = "Thana";
                    //$scope.Resources = {};
                   // $scope.OfficeInfo();
                    //$scope.DistrictInfo();
                    $scope.ThanaInfo();
                    $scope.ShowRegion = true;
                    $scope.ShowOffice = true;
                    $scope.ShowDistrict = true;
                    $scope.ShowThana = false;
                    //$scope.ResetId();
                }
                if (dvNo == 5) {
                    $scope.ListName = "Area";
                    //$scope.Resources = {};
                    //$scope.OfficeInfo();
                    //$scope.DistrictInfo();
                    //$scope.ThanaInfo();
                    $scope.AreaInfo();
                    $scope.ShowRegion = true;
                    $scope.ShowOffice = true;
                    $scope.ShowDistrict = true;
                    $scope.ShowThana = true;
                    //$scope.ResetId();
                }            
        });
        $scope.setAllCheckBox = function () {
            angular.forEach($scope.Resources, function (value, key) {
                value.Status = $scope.readCheck;
            });
        };
       
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                $scope.AreaConfiguration.Id = 0;
                $scope.AreaConfiguration.HrmEmployeeId = $scope.EmployeeId;
                $scope.AreaConfiguration.IsRegionBased = $scope.ShowOrHide == 1 ? true : false;
                $scope.AreaConfiguration.IsOfficeBased = $scope.ShowOrHide == 2 ? true : false;
                $scope.AreaConfiguration.IsDistrictBased = $scope.ShowOrHide == 3 ? true : false;
                $scope.AreaConfiguration.IsThanaBased = $scope.ShowOrHide == 4 ? true : false;
                $scope.AreaConfiguration.IsAreaBased = $scope.ShowOrHide == 5 ? true : false;
              
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
                },
                checkedItems);
                if (checkedItems.length > 0) {
                     
                    $http.post("/Sales/Area/SaveAreaConfiguration", { objConfig: $scope.AreaConfiguration, objConfigDetails: checkedItems }).success(function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();

                    }).error(function (data) {

                    });
                }
                else { Erpmodal.Warning("Please an item from list !"); }
            });
        };//end of Save

        $scope.Reset = function () {
            $scope.ResetId();
            init();
            $scope.ButtonDisabled = true;
            AreaId = 0;
            state = 0;
            $scope.Resources = {};
            $scope.Businesses.$setPristine();

        }//end of Reset
        $scope.ResetId = function () {
            $scope.RegionId = 0;
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.ThanaId = 0;
            $scope.readCheck = false;
           
        }//end of Reset      

    });

}).call(this);