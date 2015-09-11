
(function () {

    app.controller('DealerController', function ($scope, $http, Erpmodal) {

        var state;
        var DealerId;

        $scope.DealerList = new Object();
        $scope.Dealer = new Object();
        $scope.CodeState = false;

        $scope.RegionList = new Object();
        $scope.OfficeList = new Object();
        $scope.DistrictList = new Object();
        $scope.ThanaList = new Object();

        $scope.RegionId = 0;
        $scope.OfficeId = 0;
        $scope.DistrictId = 0;
        $scope.ThanaId = 0;
        $scope.DistHeadOfficeId = 0;

        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.CodeState = false;
            $http({
                url: '/Sales/Dealer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DealerList = data;

            });

            $http({
                url: '/Sales/Region/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RegionList = data;
            });

            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.OfficeList = data;
            });

            $http({
                url: '/Sales/District/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DistrictList = data;
            });

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

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.RegionChangeHandler = function () {
            $scope.LoadAllDealers();
            $scope.OfficeListByRegionId();
            //

        };
        $scope.OfficeChangeHandler = function () {
            $scope.LoadAllDealers();
            $scope.DistrictListByOfficeId();
            //

        };
        $scope.DistrictChangeHandler = function () {
            $scope.LoadAllDealers();
            $scope.ThanaListByDistrictId();
            //

        };
        $scope.ThanaChangeHandler = function () {
            $scope.LoadAllDealers();
            //

        };

        $scope.LoadAllDealers = function () {
            $http({
                url: '/Sales/Thana/GetAllIds',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    regionId: $scope.Dealer.RegionId,
                    officeId: $scope.Dealer.OfficeId,
                    districtId: $scope.Dealer.DistrictId,
                    thanaId: $scope.Dealer.ThanaId
                }
            }).success(function (data) {
                var thanaIds = data;
                //get all - distributors
                $http({
                    url: '/Sales/Dealer/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        thanaIdList: thanaIds,
                        filter: 1
                    }
                }).success(function (data) {
                    $scope.DealerList = data;

                });
                //end of get all - fistributors
            });
        };

        $scope.OfficeListByRegionId = function () {
            $scope.OfficeList = new Object();
            $scope.Dealer.OfficeId = 0;
            $scope.Dealer.DistrictId = 0;
            $scope.Dealer.ThanaId = 0;
            $http.post("/Sales/Office/GetByRegionId", { regionId: $scope.Dealer.RegionId }).success(
                       function (data) {
                           $scope.OfficeList = data;
                           $scope.Dealer.OfficeId = 0;
                       }
                       ).error(function (error) {
                       });
        };

        $scope.DistrictListByOfficeId = function () {
            $scope.DistrictList = new Object();
            $scope.Dealer.DistrictId = 0;
            $scope.Dealer.ThanaId = 0;
            $http.post("/Sales/District/GetByOfficeId", { officeId: $scope.Dealer.OfficeId }).success(
                       function (data) {
                           $scope.DistrictList = data;
                           $scope.Dealer.DistrictId = 0;
                       }
                       ).error(function (error) {
                       });
        };

        $scope.ThanaListByDistrictId = function () {
            $scope.ThanaList = new Object();
            $scope.Dealer.ThanaId = 0;
            $http.post("/Sales/Thana/GetByDistrictId", { districtId: $scope.Dealer.DistrictId }).success(
                       function (data) {
                           $scope.ThanaList = data;
                           $scope.Dealer.ThanaId = 0;
                       }
                       ).error(function (error) {
                       });
        };

        $scope.setFortEdit = function (rowitem) {
            state = 1;
            DealerId = rowitem.Id;

            $scope.Dealer.RegionId = 0;
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

                                                $scope.Dealer.RegionId = rowRegionId;
                                                $scope.Dealer.OfficeId = rowOfficeId;
                                                $scope.Dealer.DistrictId = rowDistrictId;
                                                $scope.Dealer.ThanaId = rowThanaId;
                                            }
                                        }).error(function (error) {
                                        }); //end of region by office
                                    }
                                }).error(function (error) {
                                });//end of office by district                               
                           }
                       }).error(function (error) {
                       });//end of district by thana


            $scope.Dealer.Code = rowitem.Code;
            $scope.Dealer.Name = rowitem.Name;
            $scope.Dealer.Description = rowitem.Description;
            $scope.Dealer.DistHeadOfficeId = rowitem.HeadOfficeId;
            $scope.Dealer.Address = rowitem.Address;
            $scope.Dealer.Phone = rowitem.Phone;
            $scope.Dealer.ResponsiblePerson = rowitem.ResponsiblePerson;
            $scope.Dealer.BankName = rowitem.BankName;
            $scope.Dealer.BankAccount = rowitem.BankAccount;
            $scope.Dealer.CreditLimit = rowitem.CreditLimit;
            $scope.Dealer.SalesTarget = rowitem.SalesTarget;
            $scope.Dealer.RateOfCommission = rowitem.RateOfCommission;
            $scope.Dealer.Remarks = rowitem.Remarks;
            $scope.Dealer.IsAllCompany = rowitem.IsAllCompany;

            $scope.Dealer.CreatedBy = rowitem.CreatedBy;
            $scope.Dealer.CreatedDate = ConverttoDateString(rowitem.CreatedDate);

            $scope.ButtonDisabled = false;
            $scope.CodeState = true;
        }


        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            DealerId = 0;
            state = 0;
            $scope.Dealer = {};
            $scope.DealerForm.$setPristine();
        }//end of Reset

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.Dealer.Id = DealerId;
                    }
                    else { $scope.Dealer.Id = 0; }

                    $scope.Dealer.SlsThanaId = $scope.Dealer.ThanaId;
                    $scope.Dealer.HeadOfficeId = $scope.Dealer.DistHeadOfficeId;
                    //TODO: need to discuss about SeCompany - as it is missing in BRD.
                    $scope.Dealer.SecCompanyId = 1;

                    $http.post("/Sales/Dealer/Save", $scope.Dealer).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                        }
                        ).error(function (error) {

                        });
                });


        };//end of Save

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = DealerId;
                if (DealerId != 0) {
                    $http.post("/Sales/Dealer/Delete", { Id: Id }).success(function (data) {
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

                        var req = { "dealerCode": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/Dealer/IsCodeAvailable', data: req };
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

                        var req = { "dealerName": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/Dealer/IsNameAvailable', data: req };
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