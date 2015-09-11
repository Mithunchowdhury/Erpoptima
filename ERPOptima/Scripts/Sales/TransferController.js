app.controller("TransferController", function ($scope, $http, Erpmodal) {


    $scope.TransferDetails = [];
    $scope.Products = new Object();
    $scope.Units = [];

    $scope.RefNo = "";
    $scope.FromOffices = [];
    $scope.ToOffices = [];
    $scope.Transfers = [];

    var fromStoreId = 0;
    var toStoreId = 0;

    var state;

    $scope.Transfer = new Object();   

    function init() {

        $scope.ButtonDisabled = true;
        GetFromOffice();
        GetToOffice();
        GetAll();
        GetTransferDetails();
        GetProducts();        
        GetRefNo();
        state = 0;

    }//end of init
    init();

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    function GetFromOffice() {

        $http({
            url: '/Sales/Office/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.FromOffices = data;
           
        });


    }//end of GetFromOffice
    function GetToOffice() {

        $http({
            url: '/Sales/Office/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.ToOffices = data;

        });


    }//end of GetFromOffice
    $scope.GetFromStore=function () {

        $http({
            url: '/Inventory/Store/GetStoresForOffice?officeId=' + $scope.Transfer.From,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.fromStoreId = data.Id;

        });


    }//end of GetFromStore
    $scope.GetToStore= function() {

        $http({
            url: '/Inventory/Store/GetStoresForOffice?officeId=' + $scope.Transfer.To,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.toStoreId = data.Id;

        });


    }//end of GetToStore

    function GetProducts() {

        $http({
            url: '/Sales/ChartOfProduct/GetProducts',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Products = data;


        });


    }//end of GetProducts
   

    function GetAll() {
        $http({
            url: '/Sales/Transfer/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Reset();
            $scope.Transfers = [];
            for (var i = 0; i < data.length; i++) {
                data[i].Date = ConverttoDateString(data[i].Date);
            }
            $scope.Transfers = data;


        }).error(function (data) {
        });
    };

    $scope.GetTransferDetailByTransferId = function () {
        $http({
            url: '/Sales/Transfer/GetTransferDetailByTransferId?transferId=' + $scope.Transfer.Id,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
                    
            $scope.TransferDetails = data;
            GetUnits();

        }).error(function (data) {
        });
    };

    function GetUnits() {

        for (var i = 0; i < $scope.TransferDetails.length; i++) {
            $scope.GetUnitsByProductId($scope.TransferDetails[i].SlsProductId);
        }


    }//end of GetUnits


    function GetTransferDetails() {

        var row = {
            Id: 0,
            SlsTransferId: 0,
            SlsProductId: 0,
            Quantity: 0,
            SlsUnitId: 0
           
        };

        $scope.TransferDetails.push(row);


    }//end of GetTransferDetails

   
    $scope.AddNewRow = function () {

        var row = {
            Id: 0,
            SlsTransferId: 0,
            SlsProductId: 0,
            Quantity: 0,
            SlsUnitId: 0
        };

        $scope.TransferDetails.push(row);


    };//end of AddNewRow

    $scope.removeRow = function (index) {
              
        $scope.TransferDetails.splice(index, 1);
     

    };

    
    $scope.GetUnitsByProductId = function (ProductId) {
        $http({
            url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId?productId=' + ProductId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
           
            var un = new Object();
            un = data;

            $scope.Units.push(un);


        }).error(function (data) {
        });
    };
    
    function GetRefNo() {

        $http({
            url: '/Sales/Transfer/GetRefNo',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.RefNo = data;

        });


    }//end of GetRefNo

    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {          
            $scope.Transfer.RefNo = $scope.RefNo;
            $scope.Transfer.FromInvStoreId = $scope.fromStoreId;
            $scope.Transfer.ToInvStoreId = $scope.toStoreId;
          
            $http.post("/Sales/Transfer/Save", { transfer: $scope.Transfer, transferDetail: $scope.TransferDetails }).success(
                function (data) {
                    if (data.Success) {
                        GetAll();
                       
                    }
                    
                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                }
                ).error(function (error) {
                });
           
        });
    };//end of Save




    $scope.setFortEdit = function (transfer) {
        state = 1;
        $scope.Transfer.Id = transfer.Id;
        $scope.RefNo = transfer.RefNo;
        $scope.Transfer.From = transfer.From;
        $scope.Transfer.FromInvStoreId = transfer.FromInvStoreId;
        $scope.Transfer.To = transfer.To;
        $scope.Transfer.ToInvStoreId = transfer.ToInvStoreId;
        $scope.Transfer.Date = transfer.Date;
        $scope.Transfer.VehicleNo = transfer.VehicleNo;
        $scope.Transfer.ChallenNo = transfer.ChallenNo;
        $scope.Transfer.GatepassNo = transfer.GatepassNo;
        $scope.Transfer.Remarks = transfer.Remarks;
        $scope.Transfer.CreatedBy = transfer.CreatedBy;
        $scope.Transfer.CreatedDate = ConverttoDateString(transfer.CreatedDate);
        $scope.ButtonDisabled = false;
    }


    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            var Id = $scope.Transfer.Id;
            $http.post("/Sales/Transfer/Delete", { Id: Id }).success(function (data) {
                if (data.Success) {
                    GetAll();
                    $scope.Reset();
                }
                Erpmodal.Delete(data, "Delete");

            }).error(function () {

            })
        });
    };

    $scope.DeleteDetail = function (index, id) {
        Erpmodal.Confirm('Delete').then(function (result) {
            $http.post("/Sales/Transfer/DeleteDetail", { Id: id }).success(function (data) {
                $scope.TransferDetails.splice(index, 1);
            }).error(function () {

            })

        });
    };


    $scope.Reset = function () {      
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.TransferDetails = [];
        GetTransferDetails();
        GetRefNo();
        $scope.Transfer.Id = 0;  
        $scope.Transfer.From = 0;
        $scope.Transfer.FromInvStoreId = 0;
        $scope.Transfer.To = 0;
        $scope.Transfer.ToInvStoreId = 0;
        $scope.Transfer.Date = null;
        $scope.Transfer.VehicleNo = "";
        $scope.Transfer.ChallenNo = "";
        $scope.Transfer.GatepassNo = "";
        $scope.Transfer.Remarks = "";
        $scope.Transfer.$setPristine();
        
    }



});
