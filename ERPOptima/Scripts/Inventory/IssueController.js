app.controller("IssueController", function ($scope, $http, Erpmodal) {


    $scope.IssueDetails = [];
    $scope.Products = new Object();
    $scope.Units = [];
    

    $scope.Code = "";
    $scope.Requisitions = [];
    $scope.Issues = [];

    var state;

    $scope.Issue = new Object();   

    function init() {

        $scope.ButtonDisabled = true;
        GetStores();
        //GetIssueDetails();     
        GetRequisition();
        GetCode();
        state = 0;

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
           
        });


    }//end of GetStores

    $scope.GetProducts = function () {    

        $http({
            url: '/Sales/ChartOfProduct/GetProductsByReqId?requisitionId=' + $scope.Issue.InvRequisitionId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Products = data;


        });


    }//end of GetProducts
    function GetRequisition() {

        $http({
            url: '/Inventory/Requisition/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Requisitions = [];
            for (var i = 0; i < data.length; i++) {
                data[i].PreferredDeliveryDate = ConverttoDateString(data[i].PreferredDeliveryDate);
            }
            $scope.Requisitions = data;
            
        });


    }//end of GetRequisition

   


    $scope.GetIssueByRequisitionId = function () {
        $http({
            url: '/Inventory/Issue/GetIssueByRequisitionId?requisitionId=' + $scope.Issue.InvRequisitionId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Reset2();
            $scope.Issues = [];
            for (var i = 0; i < data.length; i++) {
                data[i].Date = ConverttoDateString(data[i].Date);
            }
            $scope.Issues = data;


        }).error(function (data) {
        });
    };

    $scope.GetReqDetail = function () {
        $http({
            url: '/Inventory/Requisition/GetByRequisitionIdForIssue?requisitionId=' + $scope.Issue.InvRequisitionId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.IssueDetails = [];
            for (var i = 0; i < data.length; i++) {
                data[i].Id = 0;
            }
            $scope.IssueDetails = data;

        }).error(function (data) {
        });


    }//end of GetReqDetail

    $scope.GetIssueDetailByIssueId = function () {
        $http({
            url: '/Inventory/Issue/GetIssueDetailByIssueId?requisitionId='+$scope.Issue.InvRequisitionId + '&&issueId=' + $scope.Issue.Id,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.IssueDetails = [];
            $scope.IssueDetails = data;
            //GetUnits();

        }).error(function (data) {
        });
    };

    function GetUnits() {

        for (var i = 0; i < $scope.IssueDetails.length; i++) {
            $scope.GetUnitByProductRequisition($scope.Issue.InvRequisitionId,$scope.IssueDetails[i].SlsProductId);
        }


    }//end of GetUnits


    function GetIssueDetails() {

        var row = {
            Id: 0,
            InvIssueId: 0,
            SlsProductId: 0,           
            RequiredQuantity: 0,
            IssuedQuantity: 0,
            SlsUnitId: 0,
        };

        $scope.IssueDetails.push(row);


    }//end of GetIssueDetails

   
    $scope.AddNewRow = function () {     
        var row = {
            Id: 0,
            InvIssueId: 0,
            SlsProductId: 0,
            RequiredQuantity: 0,
            IssuedQuantity: 0,
            SlsUnitId: 0,
        };

        $scope.IssueDetails.push(row);


    };//end of AddNewRow

    $scope.removeRow = function (index) {
              
            $scope.IssueDetails.splice(index, 1);
     

    };

    
    $scope.GetUnitByProductRequisition = function (ProductId) {
        $http({
            url: '/Sales/UnitOfMeasurement/GetUnitByProductRequisition?requisitionId='+$scope.Issue.InvRequisitionId + '&&productId=' + ProductId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
                      
            var un = new Object();
            un = data;

            $scope.Units.push(un);


        }).error(function (data) {
        });
    };
    
    function GetCode() {

        $http({
            url: '/Inventory/Issue/GetCode',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Code = data;

        });


    }//end of GetCode

    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {          
            $scope.Issue.IssueCode = $scope.Code;         
            $http.post("/Inventory/Issue/Save",{ iss: $scope.Issue, issDetail: $scope.IssueDetails}).success(
                function (data) {
                    if (data.Success) {
                        $scope.GetIssueByRequisitionId();
                    }
                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                }
                ).error(function (error) {
                });
           
        });
    };//end of Save




    $scope.setFortEdit = function (iss) {
        state = 1;
        $scope.Issue.Id = iss.Id;
        $scope.Code = iss.IssueCode;
        $scope.Issue.InvRequisitionId = iss.InvRequisitionId;
        $scope.Issue.Date = iss.Date;
        $scope.Issue.InvStoreId = iss.InvStoreId;
        $scope.Issue.Remarks = iss.Remarks;        
        $scope.Issue.CreatedBy = iss.CreatedBy;
        $scope.Issue.CreatedDate = ConverttoDateString(iss.CreatedDate);
        $scope.ButtonDisabled = false;
    }


    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            var Id = $scope.Issue.Id;
            $http.post("/Inventory/Issue/Delete", { Id: Id }).success(function (data) {
                if (data.Success) {
                    $scope.GetIssueByRequisitionId();
                    $scope.Reset();
                }
                Erpmodal.Delete(data, "Delete");

            }).error(function () {

            })
        });
    };

    $scope.DeleteDetail = function (index, id) {
        Erpmodal.Confirm('Delete').then(function (result) {
            $http.post("/Inventory/Issue/DeleteDetail", { Id: id }).success(function (data) {
                $scope.IssueDetails.splice(index, 1);              
            }).error(function () {

            })

        });
    };


    $scope.Reset = function () {
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.IssueDetails = [];
        //GetIssueDetails();
        GetCode();
        $scope.Issue.Id = 0;
        $scope.Issue.Date = null;
        $scope.Issue.InvStoreId = 0;
        $scope.Issue.Remarks = "";
        $scope.Issue.$setPristine();
    };
    $scope.Reset2 = function () {
        state = 0;
        $scope.ButtonDisabled = true;
        //$scope.IssueDetails = [];
        //GetIssueDetails();
        GetCode();
        $scope.Issue.Id = 0;
        $scope.Issue.Date = null;
        $scope.Issue.InvStoreId = 0;
        $scope.Issue.Remarks = "";
    };

    $scope.detailInvalid = function (issd) {      
        return !(issd.SlsProductId && issd.RequiredQuantity >= 0 && issd.IssuedQuantity >= 0 && issd.IssuedQuantity != '' && issd.SlsUnitId);
    };



});
