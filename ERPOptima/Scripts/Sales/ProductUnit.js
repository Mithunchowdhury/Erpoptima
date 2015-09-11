app.controller("ProductUnitController", function ($scope, $http, Erpmodal) {

   
    $scope.Products = new Object(); 
    $scope.Units = new Object(); 
    $scope.ParentUnits = new Object(); 
  
    $scope.ProductUnits = new Object();
    $scope.ProductUnit = {};
    var ProductUnitId = 0;


   
    var state;
   



    function init() {
      
        $scope.ButtonDisabled = true;       
        GetProducts();
        GetUnits();
        GetParentUnits();       
        state = 0;

    }//end of init
    init();

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    function GetProducts() {

        $http({
            url: '/Sales/ChartOfProduct/GetProducts',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Products = data;


        });


    }//end of GetProducts

    function GetUnits() {

        $http({
            url: '/Sales/UnitOfMeasurement/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Units = data;


        });


    }//end of GetUnits

    function GetParentUnits() {

        $http({
            url: '/Sales/UnitOfMeasurement/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.ParentUnits = data;


        });


    }//end of GetParentUnits

    $scope.GetUnitsByProductId = function () {
        $http({
            url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId?productId=' + angular.fromJson($scope.ProductUnit).SlsProductId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.ProductUnits = data;


        }).error(function (data) {
        });
    };

   
    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            if (state == 1) {
                $scope.ProductUnit.Id = ProductUnitId;
            }
            else { $scope.ProductUnit.Id = 0; }

            $http.post("/Sales/ProductUnits/Save", $scope.ProductUnit).success(
                function (data) {
                    if (data.Success) {
                        $scope.GetUnitsByProductId();
                        
                    }
                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                }
                ).error(function (error) {
                });
        });
    };//end of Save


    

    $scope.setForEdit = function (pu) {
        state = 1;
        ProductUnitId = pu.Id;
        $scope.ProductUnit.SlsProductId = pu.SlsProductId;
        $scope.ProductUnit.SlsUnitId = pu.SlsUnitId;
        $scope.ProductUnit.ParentUnitId = pu.ParentUnitId;
        $scope.ProductUnit.ConversionRate = pu.ConversionRate;
        $scope.ProductUnit.Remarks = pu.Remarks;
        $scope.ProductUnit.CreatedBy = pu.CreatedBy;
        $scope.ProductUnit.CreatedDate = ConverttoDateString(pu.CreatedDate);
        $scope.ButtonDisabled = false;
    }  


    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            var Id = ProductUnitId;
            $http.post("/Sales/ProductUnits/Delete", { Id: Id }).success(function (data) {
                if (data.Success) {
                    $scope.GetUnitsByProductId();
                    $scope.Reset();
                }                               
                Erpmodal.Delete(data, "Delete");              

            }).error(function () {

            })
        });
    };
   

    $scope.Reset = function () {
        ProductUnitId = 0;
        $scope.ProductUnit = {};
        $scope.ProductUnitForm.$setPristine();
        state = 0;       
        $scope.ButtonDisabled = true;

    }



});
