
(function () {

    app.controller('DistributorController', function ($scope, $http, Erpmodal, crudservice) {

        var state;
        var DistributorId;

        //NEW to use later: this is using crud service
        //crudservice.GetAllURL = '/Sales/Distributor/GetAll';
        //crudservice.SaveURL = '/Sales/Distributor/Save';

        $scope.DistributorList = new Object();
        $scope.Distributor = new Object();
        $scope.CodeState = false;

        $scope.RegionList = new Object();
        $scope.OfficeList = new Object();
        $scope.DistrictList = new Object();
        $scope.ThanaList = new Object();

        $scope.RegionId = 0;
        $scope.OfficeId = 0;
        $scope.DistrictId = 0;
        $scope.ThanaId = 0;
        $scope.DistHeadOfficeId = null;

        var init = function () {
            $scope.Distributor = new Object();
            $scope.Distributor.IsAllCompany = true;
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.CodeState = false;

            //Earlier - this has been used
            // For Load Grid
            $http({
                url: '/Sales/Distributor/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DistributorList = data;

            });
            
            //NEW to use later: this is using crud service
            //crudservice.getAll().then(function (data) {
            //     
            //    $scope.DistributorList = data;

            //});

            // To Load Region Combo
            $http({
                url: '/Sales/Region/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RegionList = data;
            });

            // To Load Office Combo
            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.OfficeList = data;
            });

            // To Load District Combo

            $http({
                url: '/Sales/District/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DistrictList = data;
            });

            // To Load Thana Combo
            $http({
                url: '/Sales/Thana/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.ThanaList = data;
            });

            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.HeadOfficeList = data;
            });


        }//end of init

        init();//init is called


        // To select grid row 
        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.RegionChangeHandler = function () {
            $scope.LoadAllDistributors();
            $scope.OfficeListByRegionId();
            //

        };
        $scope.OfficeChangeHandler = function () {
            $scope.LoadAllDistributors();
            $scope.DistrictListByOfficeId();
            //

        };
        $scope.DistrictChangeHandler = function () {
            $scope.LoadAllDistributors();
            $scope.ThanaListByDistrictId();
            //

        };
        $scope.ThanaChangeHandler = function () {
            $scope.LoadAllDistributors();            
            //

        };
        
        $scope.LoadAllDistributors = function () {
            $http({
                url: '/Sales/Thana/GetAllIds',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    regionId: $scope.Distributor.RegionId,
                    officeId: $scope.Distributor.OfficeId,
                    districtId: $scope.Distributor.DistrictId,
                    thanaId: $scope.Distributor.ThanaId
                }
            }).success(function (data) {
                var thanaIds = data;
                 ;
                //get all - distributors
                $http({
                    url: '/Sales/Distributor/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        thanaIdList: thanaIds,
                        filter: 1
                    }
                }).success(function (data) {
                    $scope.DistributorList = data;

                });
                //end of get all - fistributors
            });
        };

        $scope.OfficeListByRegionId = function () {
            $scope.OfficeList = new Object();
            $scope.Distributor.OfficeId = 0;
            $scope.Distributor.DistrictId = 0;
            $scope.Distributor.ThanaId = 0;
            $http.post("/Sales/Office/GetByRegionId", { regionId: $scope.Distributor.RegionId }).success(
                       function (data) {
                           $scope.OfficeList = data;
                           $scope.Distributor.OfficeId = 0;
                       }
                       ).error(function (error) {
                       });
        };

        $scope.DistrictListByOfficeId = function () {
            $scope.DistrictList = new Object();
            $scope.Distributor.DistrictId = 0;
            $scope.Distributor.ThanaId = 0;
            $http.post("/Sales/District/GetByOfficeId", { officeId: $scope.Distributor.OfficeId }).success(
                       function (data) {
                           $scope.DistrictList = data;
                           $scope.Distributor.DistrictId = 0;
                       }
                       ).error(function (error) {
                       });
        };

        $scope.ThanaListByDistrictId = function () {
            $scope.ThanaList = new Object();
            $scope.Distributor.ThanaId = 0;
            $http.post("/Sales/Thana/GetByDistrictId", { districtId: $scope.Distributor.DistrictId }).success(
                       function (data) {
                           $scope.ThanaList = data;
                           $scope.Distributor.ThanaId = 0;
                       }
                       ).error(function (error) {
                       });
        };

        $scope.setFortEdit = function (rowitem) {
            state = 1;
            DistributorId = rowitem.Id;
            
            $scope.Distributor.RegionId = 0;
            var rowRegionId = 0;
            var rowOfficeId = 0;
            var rowDistrictId = 0;
            var rowThanaId = rowitem.SlsThanaId;
            $http.post("/Sales/Thana/DistrictIdOfThisThana", { thanaId: rowThanaId }).success(
                       function (data) {
                           if (data.status == true) {
                               rowDistrictId = data.result;

                               //get office by district id
                               $http.post("/Sales/District/OfficeIdOfThisDistrict", { distId: rowDistrictId }).success(
                                function (data) {
                                        if (data.status == true) {
                                            rowOfficeId = data.result;

                                            //get region by office id
                                            $http.post("/Sales/Office/RegionIdOfThisOffice", { officeId: rowOfficeId }).success(
                                            function (data) {
                                                if (data.status == true) {
                                                    rowRegionId = data.result;

                                                    $scope.Distributor.RegionId = rowRegionId;
                                                    $scope.Distributor.OfficeId = rowOfficeId;
                                                    $scope.Distributor.DistrictId = rowDistrictId;
                                                    $scope.Distributor.ThanaId = rowThanaId;
                                                }
                                            }).error(function (error) {
                                            }); //end of region by office
                                        }
                                }).error(function (error) {
                                });//end of office by district                               
                           }
                       }).error(function (error) {
                       });//end of district by thana

            
            $scope.Distributor.Code = rowitem.Code;
            $scope.Distributor.Name = rowitem.Name;
            $scope.Distributor.Description = rowitem.Description;
            //$scope.Distributor.DistHeadOfficeId = rowitem.HeadOfficeId;
            $scope.Distributor.Address = rowitem.Address;
            $scope.Distributor.Phone = rowitem.Phone;
            $scope.Distributor.ResponsiblePerson = rowitem.ResponsiblePerson;
            $scope.Distributor.BankName = rowitem.BankName;
            $scope.Distributor.BankAccount = rowitem.BankAccount;
            $scope.Distributor.CreditLimit = rowitem.CreditLimit;
            $scope.Distributor.SecurityDeposit = rowitem.SecurityDeposit;
            $scope.Distributor.SalesTarget = rowitem.SalesTarget;
            $scope.Distributor.RateOfCommission = rowitem.RateOfCommission;
            $scope.Distributor.Remarks = rowitem.Remarks;
            $scope.Distributor.IsAllCompany = rowitem.IsAllCompany;

            $scope.Distributor.CreatedBy = rowitem.CreatedBy;
            $scope.Distributor.CreatedDate = ConverttoDateString(rowitem.CreatedDate);
            
            $scope.ButtonDisabled = false;
            $scope.CodeState = true;
        }


        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            DistributorId = 0;
            state = 0;
            $scope.Distributor = {};
            $scope.DistributorForm.$setPristine();
        }//end of Reset

        $scope.Save = function (isValid) {
            if (isValid)
            {
                //Earlier - this has been used
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {                        
                        $scope.Distributor.Id = DistributorId;
                    }
                    else { $scope.Distributor.Id = 0; }

                    $scope.Distributor.SlsThanaId = $scope.Distributor.ThanaId;
                    //$scope.Distributor.HeadOfficeId = $scope.Distributor.DistHeadOfficeId;                    
                    $scope.Distributor.HeadOfficeId =null;
                    $http.post("/Sales/Distributor/Save", $scope.Distributor).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                        }
                        ).error(function (error) {

                        });
                });


                //NEW to use later: this is using crud service
                //if (state == 1) {
                //    $scope.Distributor.Id = DistributorId;
                //}
                //else { $scope.Distributor.Id = 0; }

                //$scope.Distributor.SlsThanaId = $scope.Distributor.ThanaId;                                 
                //$scope.Distributor.HeadOfficeId = null;

                //crudservice.save($scope.Distributor).then(function (data) {
                //     
                //    $scope.Reset();
                //});

            }


        };//end of Save
        
        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = DistributorId;
                if (DistributorId != 0) {
                    $http.post("/Sales/Distributor/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select an Office !"); }
            });
        };//end of Delete
        

    });

    app.directive('ngUnique', ['$http', function (async) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keyup', function (evt) {
                    scope.$apply(function () {
                        var val = elem.val();

                        var req = { "distCode": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/Distributor/IsCodeAvailable', data: req };
                        async(ajaxConfiguration)
                            .success(function (data, status, headers, config) {
                                ctrl.$setValidity('unique', data.result);
                            });

                    });
                });
            }
        }
    }]);

    app.directive('dnUnique', ['$http', function (async) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keyup', function (evt) {
                    scope.$apply(function () {
                        var val = elem.val();

                        var req = { "distName": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/Distributor/IsNameAvailable', data: req };
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