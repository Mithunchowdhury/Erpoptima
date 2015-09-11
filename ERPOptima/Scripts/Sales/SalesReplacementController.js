
(function () {

    app.controller('SalesReplacement', function ($scope, $http, Erpmodal) {

        var state;
        $scope.SalesReplacement = new Object();// For RefId parent obj
        $scope.SalesOrderId = 0;
        $scope.SalesOrders = new Object();
        $scope.Resources = new Object();
       var SalesReplacementId = 0;
        var init = function () {

            $scope.ButtonDisabled = true;
            state = 0;
            SalesReplacementId = 0;


            //RefNo
            $http({
                url: '/Sales/SalesReplacement/GetAutoNumber',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.SalesReplacement.RefNo = data.Refno;
            });

            $http({
                url: '/Sales/Sales/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.SalesOrders = data;
            });

            $http({
                url: '/Sales/SalesReplacement/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }                
            }).success(function (data) {
                $scope.Resources = data;
            });
           
        };
        //end of init

        init();

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (pid) {
             
            state = 1;
            SalesReplacementId = pid.Id;

            $scope.SalesReplacement = jQuery.extend(true, {}, pid);
            $scope.SalesReplacement.Date = ConverttoDateString(pid.Date);
            $scope.SalesReplacement.CreatedDate = ConverttoDateString(pid.CreatedDate);
            if (pid.ModifiedDate !== undefined) {
                $scope.SalesReplacement.ModifiedDate = ConverttoDateString(pid.ModifiedDate);
            }
            //$scope.SalesReplacement.SlsReplacementDetailVMs = pid.SlsReplacementDetailVMs;

            //$scope.SalesOrders.RefNo = pid.RefNo;
            //$scope.SalesOrders.Remarks = pid.Remarks;
            //$scope.SalesOrders.CreatedBy = pid.CreatedBy;
            //$scope.SalesOrders.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;



        }

       
        // get Product by SalesOrder id
        $scope.GetBySalesOrderId = function () {
             
            if (state == 1) {
                //TBA
            }
            else
            {
                 
                if ($scope.SalesReplacement === undefined)
                {
                    $scope.SalesReplacement = new Object();
                }
                if ($scope.SalesReplacement.SlsReplacementDetailVMs === undefined)
                {
                    $scope.SalesReplacement.SlsReplacementDetailVMs = [];
                }

                var soObj = angular.fromJson($scope.SalesOrderObj);
                //$scope.SalesReplacement.SalesOrderDetails = soObj.SalesOrderDetails;// show for gried
                $scope.SalesReplacement.Details = [];
                for (var i = 0; i < soObj.SalesOrderDetails.length; i++) {
                    var repDetail = new Object();

                    repDetail.Id = 0;
                    repDetail.SlsReplacementId = SalesReplacementId;
                    repDetail.SlsProductId = soObj.SalesOrderDetails[i].SlsProductId;
                    repDetail.Quantity = soObj.SalesOrderDetails[i].SalesOrderQuantity;
                    repDetail.SlsUnitId = soObj.SalesOrderDetails[i].SlsUnitId;
                    repDetail.AdjustedAmount = 0;
                    repDetail.Reason = "";
                    repDetail.SlsProductName = soObj.SalesOrderDetails[i].SlsProductName;
                    repDetail.SlsUnitName = soObj.SalesOrderDetails[i].SlsUnitName;

                    $scope.SalesReplacement.SlsReplacementDetailVMs[i] = repDetail;
                }
            }
        };
        //end

        $scope.GetUnitsByProductId = function () {
            $http({
                url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId?productId=' + angular.fromJson($scope.Product).Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                               
                $scope.Units=data;


            }).error(function (data) {
            });

            

        };
   

        //Save
        $scope.Save = function () {
             
            Erpmodal.Confirm('Save').then(function (result) {
                 
                if (state == 1) {
                    $scope.SalesReplacement.Id = SalesReplacementId;
                }
                else { $scope.SalesReplacement.Id = 0; }
                
                //$scope.SalesReplacement.SlsReplacementDetails = $scope.SalesOrders;
                $http.post("/Sales/SalesReplacement/Save", $scope.SalesReplacement).success(
                    function (data) {
                        if (data.Success) {
                            //$scope.GetUnitsByProductId();
                            init();
                           
                        }
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });
        };//end of Save

       

        $scope.Reset = function () {

             
            //init();
            $scope.SalesReplacement.RefNo = "";
            $scope.SalesReplacement = new Object();
            state = 0;
            $scope.ButtonDisabled = true;
            SalesReplacementId = 0;
            $scope.SalesReplacements.$setPristine();
           
            $http({
                url: '/Sales/SalesReplacement/GetAutoNumber',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.SalesReplacement.RefNo = data.Refno;
            });


        };//end of Reset

        

    });
    

}).call(this);
