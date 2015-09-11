app.controller("StockBalanceReportController", function ($http, $scope, $location) {

    $scope.Stores = new Object();
    $scope.StoreId = 0;


    var init = function () {
        
        GetStores();


    };// end of init

    init();


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
   











});