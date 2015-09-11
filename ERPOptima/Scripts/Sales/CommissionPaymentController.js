(function () {
   
    app.controller("CommissionPaymentController", function ($scope, $http, Erpmodal) {
        var state = 0;
        var MainId = 0;
        $scope.InitMainObj = function (force) {
            if (force || ($scope.CommissionPayment === undefined)) {
                $scope.CommissionPayment = new Object();

                state = 0;
                MainId = 0;
                $scope.DeleteDisabled = true;
            }
        };//end of InitMainObj

        var init = function () {
            
            $scope.InitMainObj(false);
            
            GetDistributors();

            GetDealers();
            
            //get all commissions
            $http({
                url: '/Sales/CommissionPayment/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            }).error(function () {
                //Erpmodal.Warning("Error!");
            });
        };//end of init

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        var GetDistributors = function () {
            //get distributors
            $http({
                url: '/Sales/Distributor/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Distributors = data;
            }).error(function () {
                //Erpmodal.Warning("Error!");
            });
        };

        var GetDealers = function () {
            //get dealers
            $http({
                url: '/Sales/Dealer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Dealers = data;
            }).error(function () {
                //Erpmodal.Warning("Error!");
            });
        };

        init();

        $scope.DistributorChanged = function () {
            //$scope.InitMainObj();
            if ($scope.CommissionPayment.SlsDitributorId > 0)
            {
                $scope.CommissionPayment.SlsDealerId = 0;
                $scope.CommissionPayment.Party = $scope.CommissionPayment.SlsDitributorId;
                //Party type for distributor - in DB
                $scope.CommissionPayment.PartyType = 1;

                $scope.GetNetSales();
            }
        };//end of DistributorChanged

        $scope.DealerChanged = function () {
            //$scope.InitMainObj();
            if ($scope.CommissionPayment.SlsDealerId > 0)
            {
                $scope.CommissionPayment.SlsDitributorId = 0;
                $scope.CommissionPayment.Party = $scope.CommissionPayment.SlsDealerId;
                //Party type for dealer - in DB
                $scope.CommissionPayment.PartyType = 3;

                $scope.GetNetSales();
            }
        };//end of DealerChanged

        $scope.GetNetSales = function () {
             
            if ($scope.CommissionPayment.From !== undefined &&
                $scope.CommissionPayment.To !== undefined &&
                $scope.CommissionPayment.PartyType !== undefined &&
                $scope.CommissionPayment.Party !== undefined) {
                var fromVal = $scope.CommissionPayment.From;
                var toVal = $scope.CommissionPayment.To;
                var partyTypeVal = $scope.CommissionPayment.PartyType;
                var partyVal = $scope.CommissionPayment.Party;
                $http({
                    url: '/Sales/CommissionPayment/GetNetSales',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        from: fromVal,
                        to: toVal,
                        partyType: partyTypeVal,
                        party: partyVal
                    }
                }).success(function (data) {
                     
                    $scope.CommissionPayment.NetSaleAmount = data.result;
                    $scope.GetCommissionRate();
                }).error(function () {
                    //
                });
            }
            
        };//end of GetNetSales

        $scope.GetCommissionRate = function () {
             
            if($scope.CommissionPayment.NetSaleAmount > 0)
            {
                var fromVal = $scope.CommissionPayment.From;
                var toVal = $scope.CommissionPayment.To;
                var partyTypeVal = $scope.CommissionPayment.PartyType;
                var partyVal = $scope.CommissionPayment.Party;
                var netSaleVal = $scope.CommissionPayment.NetSaleAmount;
                $http({
                    url: '/Sales/CommissionPayment/GetCommissionRate',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        from: fromVal,
                        to: toVal,
                        partyType: partyTypeVal,
                        party: partyVal,
                        netSales: netSaleVal
                    }
                }).success(function (data) {
                     
                    $scope.CommissionPayment.CommissionPercentage = data.result;

                    $scope.GetCommission();

                }).error(function () {
                    //
                });
            }
        };//End of GetCommissionRate

        $scope.GetCommission = function () {
            if($scope.CommissionPayment.CommissionPercentage !== undefined 
                && $scope.CommissionPayment.NetSaleAmount !== undefined)
            {
                var commissionVal = $scope.CommissionPayment.CommissionPercentage * $scope.CommissionPayment.NetSaleAmount / 100;
                 
                var places = Math.pow(10, 2);
                $scope.CommissionPayment.Commission = Math.round(commissionVal * places) / places;

            }
        };//End of GetCommission

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.CommissionPayment.Id = MainId;
                    }
                    else { $scope.CommissionPayment.Id = 0; }
                                      

                    $http.post("/Sales/CommissionPayment/Save", $scope.CommissionPayment).success(
                        function (data) {
                            Erpmodal.Delete(data, "Save");
                            $scope.Reset();
                            init();
                        }
                        ).error(function (error) {

                        });
                });


        };//end of Save

        $scope.Delete = function () {
            if (!$scope.DeleteDisabled)
                Erpmodal.Confirm('Delete').then(function (result) {
                    var Id = MainId;
                    if (Id != 0) {
                        $http.post("/Sales/CommissionPayment/Delete", { Id: Id }).success(function (data) {
                            Erpmodal.Delete(data, "Delete");
                            init();
                        }).error(function () {

                        })
                    }
                    else { Erpmodal.Warning("Delete Failed!"); }
                });


        };//end of Delete

        $scope.setForEdit = function (item) {            
            $scope.InitMainObj(true);
            $scope.DeleteDisabled = false;
            state = 1;
            $scope.CommissionPayment = jQuery.extend(true, {}, item);
            MainId = $scope.CommissionPayment.Id;

            $scope.CommissionPayment.From = ConverttoDateString(item.From);
            $scope.CommissionPayment.To = ConverttoDateString(item.To);

            $scope.CommissionPayment.Date = ConverttoDateString(item.Date);
            $scope.CommissionPayment.CreatedDate = ConverttoDateString(item.CreatedDate);
            if (item.ModifiedDate !== undefined) {
                $scope.CommissionPayment.ModifiedDate = ConverttoDateString(item.ModifiedDate);
            }

            if($scope.CommissionPayment.SlsDitributorId > 0)
            {
                $scope.CommissionPayment.SlsDealerId = 0;
                $scope.CommissionPayment.Party = $scope.CommissionPayment.SlsDitributorId;
                //Party type for distributor - in DB
                $scope.CommissionPayment.PartyType = 1;
            }
            else if ($scope.CommissionPayment.SlsDealerId > 0) {
                $scope.CommissionPayment.SlsDitributorId = 0;
                $scope.CommissionPayment.Party = $scope.CommissionPayment.SlsDealerId;
                //Party type for dealer - in DB
                $scope.CommissionPayment.PartyType = 3;
            }

            
        };//End of setForEdit

        $scope.Reset = function () {
            $scope.InitMainObj(true);
            $scope.Clear();
            GetDistributors();
            GetDealers();
            $scope.CommissionPaymentForm.$setPristine();
        };//end of Reset


    }); //end of controller

}).call(this); //end of function