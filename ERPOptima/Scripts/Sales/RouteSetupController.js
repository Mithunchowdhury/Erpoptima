
(function () {

    app.controller('RouteSetup', function ($scope, $http, Erpmodal) {
                
        $scope.Parties = new Object();
        $scope.PartyDetails = [];
        var RouteSetupId;
        var current;
        var DepartmentId = 0;       
        var datas = [];
        $scope.RouteSetup = new Object();
        $scope.Resources = new Object();
        $scope.Offices = new Object();
        $scope.EmployeeId;
        $scope.OfficeId = 0;
        $scope.typeid = 0;

        var state = 0;
        $scope.ButtonDisabled = true;

        $scope.GetAll = function () {
            $http({
                url: '/Sales/RouteSetup/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {                
                $scope.Resources = data;
            }).error(function (data) {
            });
        };
   
        $scope.LoadOffices = function () {
            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Offices = data;
            }).error(function (data) {
            });
        };

        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;

            $scope.LoadOffices();

            $scope.GetAll();

        };//end of init

        $scope.ChangeType = function () {

            if ($scope.typeid == 1) {
                $http({
                    url: '/Sales/Distributor/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });

            }

            else if ($scope.typeid == 2) {
                $http({
                    url: '/Sales/Retailer/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });


            }
            else if ($scope.typeid == 3) {
                $http({
                    url: '/Sales/Dealer/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });


            }
            else if ($scope.typeid == 4) {
                $http({
                    url: '/Sales/CorporateClient/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });


            }


            else {
                $scope.Parties = new Object();
            }
        };

        $scope.GetPartyDetail = function () {
            if ($scope.typeid == 1) {
                $http({
                    url: '/Sales/Distributor/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    var val = {
                        Id: 0, SlsRouteId: 0, PartyType: 1,
                        SlsDistributorId: data.Id, SlsRetailerId: 0, SlsCorporateClientId: 0, SlsDealerId: 0,
                        Name: data.Name, Code: data.Code
                    };
                    $scope.PartyDetails.push(val);
                });

            }

            else if ($scope.typeid == 2) {
                $http({
                    url: '/Sales/Retailer/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    var val = {
                        Id: 0, SlsRouteId: 0, PartyType: 2,
                        SlsDistributorId: 0, SlsRetailerId: data.Id, SlsCorporateClientId: 0, SlsDealerId: 0,
                        Name: data.Name, Code: data.Code
                    };
                    $scope.PartyDetails.push(val);
                });

            }
            else if ($scope.typeid == 3) {
                $http({
                    url: '/Sales/Dealer/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    var val = {
                        Id: 0, SlsRouteId: 0, PartyType: 3,
                        SlsDistributorId: 0, SlsRetailerId: 0, SlsCorporateClientId: 0, SlsDealerId: data.Id,
                        Name: data.Name, Code: data.Code
                    };
                    $scope.PartyDetails.push(val);
                });

            }
            else if ($scope.typeid == 4) {
                $http({
                    url: '/Sales/CorporateClient/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    var val = {
                        Id: 0, SlsRouteId: 0, PartyType: 4,
                        SlsDistributorId: 0, SlsRetailerId: 0, SlsCorporateClientId: data.Id, SlsDealerId: 0,
                        Name: data.Name, Code: data.Code
                    };
                    $scope.PartyDetails.push(val);
                });

            }

        };
        
        //Save
        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.RouteSetup.Id = RouteSetupId;
                    }
                    else {
                        $scope.RouteSetup.Id = 0;
                    }
                    $scope.RouteSetup.SlsOfficeId = $scope.OfficeId;
                 
                    angular.forEach($scope.PartyDetails, function (value, key) {
                        value.SlsRouteId = $scope.RouteSetup.Id;
                    });
                     
                    $http.post("/Sales/RouteSetup/Save", { record: $scope.RouteSetup, detailRecords: $scope.PartyDetails}).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                        }
                        ).error(function (error) {

                        });
                });

        };//end of Save

        $scope.PartyReset = function () {
            $scope.OfficeId = 0;
            $scop.typeid = 0;

            $scope.partyId = 0;
            $scope.Parties = new Object();
        };//end of Reset

        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            $scope.OfficeId = 0;
            RouteSetupId = 0;
            $scope.partyId = 0;

            $scope.Parties = new Object();
            state = 0;
            $scope.Department = {};
            $scope.typeid = {};
            $scope.PartyDetails = {};
            $scope.RouteSetups.$setPristine();


        };//end of Reset

        

        $scope.setFortEdit = function (record) {
             
            state = 1;

            //  $scope.RouteSetup = jQuery.extend(true, {}, record);

            RouteSetupId = record.Id;

            $scope.OfficeId = record.SlsOfficeId;

            $scope.RouteSetup.CreatedBy = record.CreatedBy;

            $scope.RouteSetup.Code = record.Code;
            $scope.RouteSetup.Name = record.Name;
            $scope.RouteSetup.CreatedDate = ConverttoDateString(record.CreatedDate);
            $scope.ButtonDisabled = false;

            $http({
                url: '/Sales/RouteSetup/GetRouteSetupDetails',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: { routesetupId: RouteSetupId }
            }).success(function (data) {
                $scope.PartyDetails = [];
                $scope.PartyDetails = data;
            }).error(function (data) {
            });


        };
     
        $scope.deleteData = function (item) {
            var index = $scope.PartyDetails.indexOf(item);
            if (index > -1) {
                $scope.PartyDetails.splice(index, 1);
            }
        };


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = DepartmentId;
                if (DepartmentId != 0) {
                    $http.post("/Sales/RouteSetup/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Department !"); }
            });
        };

        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

    });

}).call(this);
