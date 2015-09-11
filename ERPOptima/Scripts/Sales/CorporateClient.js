
(function () {

    app.controller('CorporateClient', function ($scope, $http, Erpmodal) {


        var current;
        var CorporateClientId = 0;
        var state;
        var datas = [];
        $scope.CorporateClient = new Object();
        $scope.District = new Object();
        $scope.Resources = new Object();
        $scope.Office = new Object();
        $scope.Region = new Object();
        $scope.CorporateType = new Object();
        $scope.Employee = new Object();
        $scope.RegionId = 0;
        $scope.OfficeId = 0;
        $scope.DistrictId = 0;
        $scope.CorporateTypeId = 0;
        $scope.EmployeeId = 0;
        

        var init = function () {
            $scope.RegionId = 0;
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.CorporateTypeId = 0;
            $scope.EmployeeId = 0;

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
                url: '/Sales/CorporateIndustryType/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.CorporateType = data;
                $scope.CorporateTypeId = 0;
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
                url: '/Sales/CorporateClient/GetAll?regionId="' + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId + "&corporateTypeId=" + $scope.CorporateTypeId + "&employeeId=" + $scope.EmployeeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employee = data;
                $scope.EmployeeId = 0;
            });
        }//end of init

       

        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

      
        $scope.LoadOfficeInfo = function () {
            $scope.OfficeId = 0;
            $scope.DistrictId = 0;
            $scope.OfficeInfo();
            $scope.DistrictInfo();
            
        };
        $scope.LoadDistrictInfo = function () {
            $scope.DistrictId = 0;
            $scope.DistrictInfo();
            
        };
       
        $scope.LoadInfo = function () {
            $scope.CorporateClientInfo();
            $scope.CorporateTypeInfo();
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
       
        $scope.CorporateClientInfo = function () {
            $http.get("/Sales/CorporateClient/GetAll?regionId=" + $scope.RegionId + "&officeId=" + $scope.OfficeId + "&districtId=" + $scope.DistrictId + "&corporateTypeId=" + $scope.CorporateTypeId + "&employeeId=" + $scope.EmployeeId).success(
                      function (data) {

                          $scope.Resources = data;
                      }
                      ).error(function (error) {
                      });
        };

        $scope.setFortEdit = function (rowData) {
             ;
            state = 1;
            CorporateClientId = rowData.Id;
            $scope.CorporateClient.Region = rowData.Region;
            $scope.CorporateClient.Name = rowData.Name;
            $scope.CorporateClient.Type = rowData.Type;
            $scope.CorporateClient.Code = rowData.Code;
            $scope.CorporateClient.BusinessType = rowData.BusinessType;
            $scope.CorporateClient.Email = rowData.Email;
            $scope.CorporateClient.Phone = rowData.Phone;
            $scope.CorporateClient.ContactPerson = rowData.ContactPerson;
            $scope.CorporateClient.ContactPersonEmail = rowData.ContactPersonEmail;
            $scope.CorporateClient.ContactPersonPhone = rowData.ContactPersonPhone;
            $scope.CorporateClient.ExecutiveName = rowData.ExecutiveName;
            $scope.CorporateClient.DefaultCreditLimit = rowData.DefaultCreditLimit;
            $scope.CorporateClient.CreatedBy = rowData.CreatedBy;
            $scope.CorporateClient.CreatedDate = ConverttoDateString(rowData.CreatedDate);
            $scope.ButtonDisabled = false;
            $scope.RegionId = rowData.SlsRegionId;
            $scope.OfficeId = rowData.SlsOfficeId;
            $scope.DistrictId = rowData.SlsDistrictId;
            $scope.EmployeeId = rowData.ExecutiveName;
            $scope.CorporateTypeId = rowData.SlsCorporateTypeId;
        }
        
        $scope.LoadInfo = function () {
            $scope.CorporateClientInfo();
        };
        
        $scope.CorporateClientInfo = function () {
            var did = $scope.EmployeeId;
            $http.get("/Sales/CorporateClient/GetAll").success(
                      function (data) {
                         
                          $scope.Resources = data;
                      }
                      ).error(function (error) {
                      });
        };
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.CorporateClient.Id = CorporateClientId;
                }
                else { $scope.CorporateClient.Id = 0; }
                $scope.CorporateClient.ExecutiveName = $scope.EmployeeId;
                $scope.CorporateClient.SlsDistrictId = $scope.DistrictId;
                $scope.CorporateClient.SlsCorporateTypeId = $scope.CorporateTypeId;
                $http.post("/Sales/CorporateClient/Save", $scope.CorporateClient).success(
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
            CorporateClientId = 0;
            state = 0;
            $scope.CorporateClient = {};
            $scope.CorporateClients.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = CorporateClientId;
                if (CorporateClientId != 0) {
                    $http.post("/Sales/CorporateClient/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a CorporateClient !"); }
            });
        };
       
    });


    app.directive('ngUnique', ['$http', function (async) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keyup', function (evt) {
                    scope.$apply(function () {
                        var val = elem.val();

                        var req = { "name": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/CorporateClient/IsNameAvailable', data: req };
                        async(ajaxConfiguration)
                            .success(function (data, status, headers, config) {
                                ctrl.$setValidity('unique', data.result);
                            });

                    });
                });
            }
        }
    }]);

    app.directive('ccUnique', ['$http', function (async) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keyup', function (evt) {
                    scope.$apply(function () {
                        var val = elem.val();

                        var req = { "code": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/CorporateClient/IsCodeAvailable', data: req };
                        async(ajaxConfiguration)
                            .success(function (data, status, headers, config) {
                                ctrl.$setValidity('unique', data.result);
                            });

                    });
                });
            }
        }
    }]);

}).call(this);