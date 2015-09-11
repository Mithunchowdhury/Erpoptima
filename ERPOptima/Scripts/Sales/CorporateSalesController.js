(function () {

    app.controller('CorporateSales', function ($scope, $http, Erpmodal) {


        

        var state = 0;  //0- for insert, 1-update
        var isValid = true;
        
        $scope.CorporateSalesApplication = new Object();
        SlsProductId = 0;
        $scope.Products = new Object();
        $scope.CorporateNames = new Object();
        $scope.Product = new Object();
        $scope.ButtonDisabled = true;   // True for disable, False for enable

        $scope.rows = [];
 
        var init = function () {
           
            $scope.ButtonDisabled = true;
            state = 0;
            SlsProductId = 0;
            //Get ref no

            $http({
                url: '/Sales/ChartOfProduct/GetProductsConfigured',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Products = data;
                SlsProductId = 0;


            });
            $http({
                url: '/Sales/CorporateSales/GetRef',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.CorporateSalesApplication.RefNo = data;
            });

            $http({
                url: '/Sales/CorporateClient/GetCorporateName',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.CorporateNames = data;
               


            });

        }//end of init

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }
        /* Add records to the grid */
        $scope.AddRow = function () {
            // This is SlsCorporateSalesApplicationDetails object
            var row = {
                Id: 0,
                SlsCorporateSalesApplicationId: 0,
                SlsProductId: angular.fromJson($scope.SlsProduct).Id,
                AppliedPercentage: $scope.Discount,
                ApprovedPercentage:null,
                ProductName: angular.fromJson($scope.SlsProduct).Name,
               
              
            };

            if (state == 0) {
                if (!$scope.CheckDuplicate(angular.fromJson($scope.SlsProduct).Id)) {
                    $scope.rows.push(row);
                }
                else {
                    Erpmodal.Warning("Duplicate record!");
                }
            }
            else {
                for (var i = 0; i < $scope.rows.length; i++) {
                    if ($scope.rows[i].SlsProductId == row.SlsProductId) {
                        $scope.rows[i].AppliedPercentage = row.AppliedPercentage;
                       
                    }
                }
            }
            state = 0;
            $scope.Clear();
        };
        //check the duplicate product in the array
        $scope.CheckDuplicate = function (record) {

            for (var i = 0; i < $scope.rows.length; i++) {
                if ($scope.rows[i].SlsProductId == record) {
                    return true;
                }
            }

            return false;
        };
        /* End of Add records to the grid */
        init();//init is called

        $scope.setFortEdit = function (data) {
            //state = 1;
            //DistrictId = pid.Id;
            //$scope.District.Code = pid.Code;
            //$scope.District.Name = pid.Name;
            //$scope.District.Remarks = pid.Remarks;
            //$scope.District.CreatedBy = pid.CreatedBy;
            //$scope.District.CreatedDate = ConverttoDateString(pid.CreatedDate);
            //$scope.ButtonDisabled = false;
            //$scope.RegionId = pid.SlsRegionId;
            //$scope.OfficeId = pid.SlsOfficeId;

        }
        $scope.eidtDetails = function (data) {
            state=1
            for (var i = 0; i < $scope.Products.length; i++) {
                if ($scope.Products[i].Id == data.SlsProductId) {
                    $scope.SlsProduct = angular.toJson( $scope.Products[i]);
                }
            }           
            $scope.Discount = data.AppliedPercentage;
        }
        $scope.Save = function (isValid) {
            var cLength = $scope.rows.length;
          
            
            if (cLength > 0) {

                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 0) {
                         ;
                        //Insert mode
                        $scope.CorporateSalesApplication.Id = 0;
                        $scope.CorporateSalesApplication.SlsCorporateSalesApplicationDetails = $scope.rows;
                    }
                    else {
                        //Update mode
                        //$scope.District.Id = 0;
                    }

                    $http.post("/Sales/CorporateSales/Save", $scope.CorporateSalesApplication).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();

                            init();

                        }
                        ).error(function (error) {
                        });
                });
            }
            else { Erpmodal.Warning("Atlest one product must be selected"); }

        };//end of Save

        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            DistrictId = 0;
            state = 0;
            $scope.District = {};
            OfficeId = 0;
            RegionId = 0;
            SlsProductId = 0;


            $scope.rows = [];
            $scope.CorporateSalesApplication = new Object();
            $scope.CorporateSales.$setPristine();
            $scope.Clear();

        }//end of Reset

        $scope.Delete = function () {
            //Erpmodal.Confirm('Delete').then(function (result) {
            //    var Id = DistrictId;
            //    if (DistrictId != 0) {
            //        $http.post("/Sales/District/Delete", { Id: Id }).success(function (data) {
            //            Erpmodal.Delete(data, "Delete");                       
            //            $scope.Reset();
            //        }).error(function () {

            //        })
            //    }
            //    else { Erpmodal.Warning("Please select a District !"); }
            //});
        };

        $scope.detailInvalid = function () {
            return !($scope.SlsProduct && $scope.Discount);
        };

        $scope.Clear = function () {

            $scope.SlsProduct = new Object();
            $scope.Discount = '';


        };

    });

}).call(this);