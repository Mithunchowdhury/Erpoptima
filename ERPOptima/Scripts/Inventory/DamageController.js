app.controller("Damage", function ($scope, $http, Erpmodal, $validation) {

  
    var state;
    $scope.RefNo = "";
    $scope.Products = new Object();
    $scope.Units = new Object();

    $scope.Product = new Object();


    $scope.rows = [];

    var DamageId = 0;
    var DamageDetailId = 0;
    $scope.Damages = new Object();


    function init() {

        state = 0;
        GetCode();
        GetProducts();

    };

    //end of init

    init();

    function GetCode() {

        $http({
            url: '/Inventory/Damage/GetCode',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.RefNo = data;

        });

    };

    //end of GetCode

    function GetProducts() {

        $http({
            url: '/Sales/ChartOfProduct/GetProducts',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Products = data;

        });
    };

    //get unit



    $scope.GetProductUnits = function () {

        $http({
            url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: { productId: angular.fromJson($scope.Product).Id }
        }).success(function (data) {
            $scope.Units = data;
        });

    };
    // end units

    $scope.GetProductsName = function () {
      
        $http({
            url: '/Sales/ChartOfProduct/GetById',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: { Id: angular.fromJson($scope.Product).Id }
        }).success(function (data) {
            $scope.Products = data;
        });

    };



    $scope.AddRow = function () {
       
        var row = {
           //  Id: DamageDetailId,
             InvDamageId: DamageId,

           SlsProductId: angular.fromJson($scope.Product).Id,
            ProductName: angular.fromJson($scope.Product).Name,

            SlsUnitsId: angular.fromJson($scope.UnitName).SlsUnitId,
           UnitName: angular.fromJson($scope.UnitName).Unit,

            Quantity: $scope.Quantity,
            Reason: $scope.Reason
        };
        
        $scope.rows.push(row);

        //if (!CheckDuplicateProduct(angular.fromJson($scope.Product).Id)) {
        //    $scope.rows.push(row);
      //  }

    };

    function CheckDuplicateProduct(productId) {

        for (var i = 0; i < $scope.rows.length; i++) {
            if ($scope.rows[i].SlsProductId == productId) {
                return true;
            }
        }

        return false;


    };//end of CheckDuplicateProduct

    $scope.removeRow = function (index) {
        $scope.rows.splice(index, 1);
    };

   
    // save 
    
    $scope.Save = function () {
        debugger
        Erpmodal.Confirm("Save").then(function (result) {
            if (state == 1) {
                $scope.Damages.Id = DamageId;
            }
            else {
                $scope.Damages.Id = 0;
            }
            $scope.Damages.RefNo = $scope.RefNo;
            $http.post("/Inventory/Damage/Save",
                    $scope.Damages
                ).success(function (data) {
                    if (data.Success == true) {
                        SaveDetail(data.OperationId);
                    }

                }).error(function (data) {

                });
        });
    };

    //end of Save

    function SaveDetail(dId) {
        for (var i = 0; i < $scope.rows.length; i++) {
            $scope.rows[i].InvDamageId = dId;
        }
        $http.post("/Inventory/Damage/SaveDetail",
                    $scope.rows
                ).success(function (data) {
                    if (data.Success == true) {
                        Erpmodal.Save(data, "Save");
                        GetAll();
                        $scope.Reset();
                    }
                    else {
                        Delete(dId);
                    }


                }).error(function (data) {

                });

    };//end of SaveDetail

  

}).call(this);
