app.controller("StockIn", function ($scope, $http, Erpmodal) {

   
   
   
    $scope.Stock = new Object();
    $scope.StockIn = new Object();
    $scope.Units = new Object();
    $scope.Unit = new Object();
    $scope.Resources = [];
    $scope.AllUnits = new Object();
    $scope.Date = "";
    $scope.ProductUnits = new Object();
    $scope.StoreId = 1;
    $scope.StoreDisable = true;
    var ProductUnitId = 0;
    //$scope.RefId = "";
   
    var status = 1;
    var transactiontype = 1;

    var StockInId = 0;
    var state;
   



    function init() {

        $scope.ButtonDisabled = true;


        //$scope.ProductId = 0;
        //$scope.UnitId = 0;
        //$scope.UnitId = 0;

        status = 1;
        transactiontype = 1;
        StockInId = 0;
        state = 0;
        $scope.Date = "";
        $scope.Resources = [];
        $http({
            url: '/Sales/ChartOfProduct/GetProductsConfigured',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.Products = data;
            SlsProductId = 0;


        });

        $http({
            url: '/Sales/UnitOfMeasurement/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            
        }).success(function (data) {
            $scope.AllUnits = data;
        });

        


        $http({
            url: '/Inventory/Store/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Stores = data;
            $scope.StoreId = 1;

        });

        //$http({
        //    url: '/Sales/StockIn/GetRefNo',
        //    method: 'GET',
        //    headers: { 'Content-Type': 'application/json' }
        //}).success(function (data) {
        //    $scope.RefId = data;

        //});
        $http({
            url: '/Sales/StockIn/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Resources = [];
            for (var i = 0; i < data.length; i++) {
                data[i].TransactionDate = ConverttoDateString(data[i].TransactionDate);
               
            }
            $scope.Resources = data;

        });

    };//end of init

    
    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    init();
    $scope.GetUnitsByProductId = function () {
        $http({
            url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: { productId: $scope.ProductId }
        }).success(function (data) {
            $scope.Units = data;
        });
    };

   
    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
             
            if (state == 1) {
                $scope.StockIn.Id = StockInId;
            }
            else { $scope.StockIn.Id = 0; }
             
            $scope.StockIn.SlsProductId = $scope.ProductId;
            $scope.StockIn.SlsUnitId = $scope.UnitId;
            $scope.StockIn.Quantity = $scope.StockQuantity;
            $scope.StockIn.InvStoreId = $scope.StoreId;
            $scope.StockIn.RefId = null;
            $scope.StockIn.TransactionDate = $scope.Date;

            $scope.StockIn.Status = status;
            $scope.StockIn.TransactionType = transactiontype;

            $http.post("/Sales/StockIn/Save", $scope.StockIn).success(

                function (data) {
                     

                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                
                }
                ).error(function (error) {
                });

        });
    };//end of Save


    

    $scope.setForEdit = function (pu) {
         
        state = 1;
        StockInId = pu.Id;
        status = pu.Status;
        transactiontype = pu.TransactionType;
       

        $http({
            url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: { productId: pu.SlsProductId }
        }).success(function (data) {
            $scope.ProductId = pu.SlsProductId;
            $scope.Units = data;
            $scope.UnitId = pu.SlsUnitId;
            $scope.StockQuantity = pu.Quantity;
            //$scope.RefId = pu.RefId;
            $scope.Date = ConverttoDateString(pu.TransactionDate);
            $scope.ButtonDisabled = false;
        });
        
 
    };


    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            var Id = StockInId;
            if (StockInId != 0) {
                $http.post("/Sales/StockIn/Delete", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();
                }).error(function () {

                })
            }
            else { Erpmodal.Warning("Please select a StockIn !"); }

            //var Id = StockInId;
            //$http.post("/Sales/StockIn/Delete", { Id: Id }).success(
            //    function (data) {
            //        Erpmodal.Save(data, "Delete");
            //        $scope.Reset();
            //    }                               
             

            //).error(function () {

            //})

        });
    };
   

    $scope.Reset = function () {
        init();
        $scope.ProductId = 0;
        $scope.UnitId = 0;
        //$scope.RefId = "";
        $scope.StockIn = {};
        $scope.StockQuantity = 0;
        $scope.StockIns.$setPristine();
       



    };



});
