
(function () {

    app.controller('RetailerController', function ($scope, $http, Erpmodal) {

        var state;
        var RetailerId;

        $scope.RetailerList = new Object();
        $scope.Retailer = new Object();
        $scope.CodeState = false;

        $scope.DistributorList = new Object();
        $scope.OfficeList = new Object();       

        $scope.DistributorId = 0;
        $scope.OfficeId = 0;        
        
        var init = function () {
            $scope.Retailer = new Object();
            $scope.Retailer.IsAllCompany = true;
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.CodeState = false;
            $http({
                url: '/Sales/Retailer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RetailerList = data;

            });

            $http({
                url: '/Sales/Distributor/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DistributorList = data;
            });

            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.OfficeList = data;
            });


        }//end of init

        init();//init is called
        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (rowitem) {
            state = 1;
            RetailerId = rowitem.Id;
                        
            $scope.Retailer.DistributorId = rowitem.SlsDistributorId;
            $scope.Retailer.OfficeId = rowitem.SlsOfficeId;
            $scope.Retailer.Code = rowitem.Code;
            $scope.Retailer.Name = rowitem.Name;
            $scope.Retailer.Description = rowitem.Description;            
            $scope.Retailer.Address = rowitem.Address;
            $scope.Retailer.Phone = rowitem.Phone;
            $scope.Retailer.ResponsiblePerson = rowitem.ResponsiblePerson;
            $scope.Retailer.BankName = rowitem.BankName;
            $scope.Retailer.BankAccount = rowitem.BankAccount;
            $scope.Retailer.CreditLimit = rowitem.CreditLimit;            
            $scope.Retailer.Remarks = rowitem.Remarks;
            $scope.Retailer.IsAllCompany = rowitem.IsAllCompany;

            $scope.Retailer.CreatedBy = rowitem.CreatedBy;
            $scope.Retailer.CreatedDate = ConverttoDateString(rowitem.CreatedDate);

            $scope.ButtonDisabled = false;
            $scope.CodeState = true;
        }


        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            RetailerId = 0;
            state = 0;
            $scope.Retailer = {};
            $scope.RetailerForm.$setPristine();
        }//end of Reset

        $scope.Save = function (isValid) {
            $scope.Retailer.SlsDistributorId = $scope.Retailer.DistributorId;
            $scope.Retailer.SlsOfficeId = $scope.Retailer.OfficeId;

            if ((angular.isUndefined($scope.Retailer.SlsDistributorId) || $scope.Retailer.SlsDistributorId == 0) && (angular.isUndefined($scope.Retailer.SlsOfficeId) || $scope.Retailer.SlsOfficeId == 0)) {
                Erpmodal.Warning("You have to select either distribbutor or Office");
            }
            else if (!angular.isUndefined($scope.Retailer.SlsDistributorId) && $scope.Retailer.SlsDistributorId != 0 && !angular.isUndefined($scope.Retailer.SlsOfficeId) && $scope.Retailer.SlsOfficeId != 0) {
                Erpmodal.Warning("You have to select either distribbutor or Office");
            }
            else {
                if (isValid)
                    Erpmodal.Confirm('Save').then(function (result) {
                        if (state == 1) {
                            $scope.Retailer.Id = RetailerId;
                        }
                        else { $scope.Retailer.Id = 0; }

                        //TODO: need to discuss about SeCompany - as it is missing in BRD.
                        $scope.Retailer.SecCompanyId = 1;

                        $http.post("/Sales/Retailer/Save", $scope.Retailer).success(
                            function (data) {
                                Erpmodal.Save(data, "Save");
                                $scope.Reset();
                            }
                            ).error(function (error) {
                                 
                            });
                    });
            }
        };//end of Save

        //
       

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = RetailerId;
                if (RetailerId != 0) {
                    $http.post("/Sales/Retailer/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select an Office !"); }
            });
        };//end of Delete

        //////

    });

    app.directive('ngUnique', ['$http', function (async) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keyup', function (evt) {
                    scope.$apply(function () {
                        var val = elem.val();

                        var req = { "retailerCode": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/Retailer/IsCodeAvailable', data: req };
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

                        var req = { "retailerName": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/Retailer/IsNameAvailable', data: req };
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