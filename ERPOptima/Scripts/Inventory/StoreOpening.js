
app.controller("StoreOpening", function ($scope, $http, Erpmodal) {
    
    $scope.StoreOpeningBalances = [];
    $scope.Stores = new Object();
    $scope.StoreId = 0;
    $scope.units = new Object();
  

    function init() {
        $scope.ShowLoading = false;
        $scope.SaveDisabled = true;
        GetStores();
    }//end of init
    init();

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    function GetStores() {

        $http({
            url: '/Inventory/Store/GetStores',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Stores = data;
            $scope.StoreId = 0;
        });


    }//end of GetStores
   

    $scope.OB = function () {
        $http({
            url: '/Inventory/StoreOpening/GetInvStoreOpeningByInvStoreId?storeId=' + $scope.StoreId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {         
            $scope.StoreOpeningBalances = data;           
            $scope.ButtonDisabled = false;
        }).error(function (data) {
        });
    };
    $scope.Unit = function (ProductId) {
        $http({
            url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId?productId=' + ProductId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.units = data;           
            $scope.ButtonDisabled = false;
        }).error(function (data) {
        });
    };

     $scope.Save = function () {
         Erpmodal.Confirm("Save").then(function (result) {
             for (var i = 0; i < $scope.StoreOpeningBalances.length; i++) {
                 $scope.StoreOpeningBalances[i].InvStoreId = $scope.StoreId;
             }            
            
            var updatedList = _.where($scope.StoreOpeningBalances, { $editable: true });

            $http.post("/Inventory/StoreOpening/Update",
                {
                    viewModelList: updatedList
                }).success(function (data) {

                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                  
                }).error(function (data) {
                   
                });
        });
    };//end of Save

    $scope.Reset = function () {
        $scope.StoreId = 0;
        //$scope.StoreOpeningBalances = [];
        $scope.StoreOpeningBalances = [];
        $scope.Stores = new Object();
        $scope.SaveDisabled = true;
        $scope.StoreOpening.$setPristine();
       
    };//end of Reset



});
