(function () {

    app.controller('SlsInsentivePayment', function ($scope, $http, Erpmodal) {
        
        var InsentivePaymentId = 0;
        $scope.EmployeeId = 0;
        var state;
        //var datas = [];
        
        $scope.Employee = new Object();

        function init() {
            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employee = data;
                $scope.EmployeeId = 0;
            });      
               

        }      
               

            // $http({
            //    url: '/Sales/UnitOfMeasurement/GetAll',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.Unit = data;
            //    $scope.UnitId = 0;

            //});



            //$http({
            //    url: '/Sales/ProductPrice/GetAll',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' },
            //    params: {
            //        productId: $scope.ProductId,
            //        unitId: $scope.UnitId
            //    }
            //}).success(function (data) {
            //    $scope.Resources = data;
            //    $scope.ProductId = 0;
            //    $scope.UnitId = 0;

            //});


        //}//end of init

        //$scope.UnitInfoByProductId = function () {
        //    $scope.Resources = {};
        //    $http({
        //        url: '/Sales/ProductPrice/GetAll',
        //        method: 'GET',
        //        headers: { 'Content-Type': 'application/json' },
        //        params: {
        //            productId: $scope.ProductId,
        //            unitId: 0
        //        }
        //    }).success(function (data) {
        //        $scope.Resources = data;

        //        $http({
        //            url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId',
        //            method: 'GET',
        //            headers: { 'Content-Type': 'application/json' },
        //            params: {
        //                productId: $scope.ProductId
        //            }
        //        }).success(function (udata) {
        //            $scope.Unit = udata;
        //        });
        //    });


        //};


        init();//init is called
        //$scope.UnitId = 0;
        //$http.post("/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId", { productId: $scope.ProductId }).success(
        //  function (data) {
        //   $scope.Unit = data;

        // }
        // ).error(function (error) {
        // });
        $scope.UnitInfo = function () {
            //$scope.Resources = data;
            // 
            $http({
                url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    productId: $scope.ProductId
                }
            }).success(function (data) {
                $scope.Unit = data;
            });
        }

        $scope.setFortEdit = function (pid) {
            state = 1;
            ProductPriceId = pid.Id;
            $scope.ProductPrice.FactoryCost = pid.FactoryCost;
            $scope.ProductPrice.DistributorCommission = pid.DistributorCommission;
            $scope.ProductPrice.RetailCommission = pid.RetailCommission;
            $scope.ProductPrice.MRP = pid.MRP;
            $scope.ProductPrice.DeclarationDate = ConverttoDateString(pid.DeclarationDate);
            $scope.ProductPrice.CreatedBy = pid.CreatedBy;
            $scope.ProductPrice.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;

            $scope.UnitId = 0;
            $scope.ProductId = 0;

            $scope.ProductId = pid.SlsProductId;
            $scope.UnitInfo();
            $scope.UnitId = pid.SlsUnitId;

        };


        $scope.ProductPriceInfo = function () {
            $scope.Resources = {};
            $http({
                url: '/Sales/ProductPrice/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    productId: $scope.ProductId,
                    unitId: $scope.UnitId
                }
            }).success(function (data) {
                $scope.Resources = data;


            });
        };
        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.ProductPrice.Id = ProductPriceId;
                    }
                    else { $scope.ProductPrice.Id = 0; }

                    $scope.ProductPrice.SlsProductId = $scope.ProductId;
                    $scope.ProductPrice.SlsUnitId = $scope.UnitId;
                    $http.post("/Sales/ProductPrice/Save", $scope.ProductPrice).success(
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
            ProductPriceId = 0;
            state = 0;
            $scope.ProductPrice = {};
            $scope.InsentivePayment.$setPristine();
            ProductId = 0;
            UnitId = 0;

        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = ProductPriceId;
                if (ProductPriceId != 0) {
                    $http.post("/Sales/ProductPrice/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Product Price !"); }
            });
        };

    });



}).call(this);