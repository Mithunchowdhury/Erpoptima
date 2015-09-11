$(document).ready(function () {
    

});

app.controller("SalesTargetController", function ($scope, $http, Erpmodal) {
   
    $scope.TargetDetails = [];
    $scope.Products = new Object();
    $scope.Units = [];
    
   
    $scope.Id = 0;
    $scope.RefNo = "";
    $scope.Month = 0;
    $scope.Year = 0;
    $scope.HrmEmployeeId = 0;
    $scope.TargetSalesAmount = 0;
    $scope.Remarks = "";
    $scope.CreatedBy = 0;
    $scope.CreatedDate = new Date();
   
    $scope.Targets = [];
    $scope.TargetList = [];
    var List = 0;
   

    var state;
   
    $scope.SalesTarget = new Object();

    function init() {      
        GetListTag();
        if (List == 0) {
            $scope.ButtonDisabled = true;
            GetEmployee();
            GetTargetDetails();
            GetProducts();           
            GetRefNo();
            state = 0;
        }
        else {
           
            GetEmployee();
            GetTargetDetails();
            GetProducts();          
            GetData();

        }

    }//end of init
    init();

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    function GetEmployee() {

        $http({
            url: '/Hrm/Employee/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.EmployeeList = data;
        });


    }//end of GetEmployee

    function GetProducts() {

        $http({
            url: '/Sales/ChartOfProduct/GetProductsConfigured',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Products = data;


        });


    }//end of GetProducts
    

    $scope.GetTargetsByYear = function () {
        $http({
            url: '/Sales/SalesTarget/GetTargetsByYear?month=' + $scope.Month+'&&year=' + $scope.Year,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Reset();
            $scope.Targets = [];           
            $scope.Targets = data;


        }).error(function (data) {
        });
    };

    $scope.GetTargetsByYearNEmployeeId = function () {
        $http({
            url: '/Sales/SalesTarget/GetTargetsByYearNEmployeeId?month=' + $scope.Month + '&&year=' + $scope.Year + '&&employeeId=' + $scope.HrmEmployeeId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Reset();
            $scope.Targets = [];          
            $scope.Targets = data;


        }).error(function (data) {
        });
    };

    $scope.GetTargetDetailByTargetId = function () {
        $http({
            url: '/Sales/SalesTarget/GetTargetDetailByTargetId?targetId=' + $scope.Id,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.TargetDetails = data;
            GetUnits();

        }).error(function (data) {
        });
    };

    function GetUnits() {

        for (var i = 0; i < $scope.TargetDetails.length; i++) {
            $scope.GetUnitsByProductId($scope.TargetDetails[i].SlsProductId);
        }


    }//end of GetUnits


    function GetTargetDetails() {

        var row = {
            Id: 0,
            SlsSalesTargetId: 0,
            SlsProductId: 0,
            Quantity: 0,
            SlsUnitId: 0          
        };

        $scope.TargetDetails.push(row);


    }//end of GetTargetDetails


    $scope.AddNewRow = function () {

        var row = {
            Id: 0,
            SlsSalesTargetId: 0,
            SlsProductId: 0,
            Quantity: 0,
            SlsUnitId: 0
        };

        $scope.TargetDetails.push(row);


    };//end of AddNewRow

    $scope.removeRow = function (index) {

        $scope.TargetDetails.splice(index, 1);


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
            url: '/Sales/SalesTarget/GetRefNo',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.RefNo = data;

        });


    }//end of GetCode

    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            $scope.SalesTarget.Id = $scope.Id;
            $scope.SalesTarget.RefNo = $scope.RefNo;
            $scope.SalesTarget.Month = $scope.Month;
            $scope.SalesTarget.Year = $scope.Year;
            $scope.SalesTarget.HrmEmployeeId = $scope.HrmEmployeeId;
            $scope.SalesTarget.TargetSalesAmount = $scope.TargetSalesAmount;
            $scope.SalesTarget.Remarks = $scope.Remarks;
            $scope.SalesTarget.CreatedBy = $scope.CreatedBy;
            $scope.SalesTarget.CreatedDate = $scope.CreatedDate;
           
            $http.post("/Sales/SalesTarget/Save", { sTarget: $scope.SalesTarget, sTargetDetail: $scope.TargetDetails }).success(
                function (data) {
                    if (data.Success) {
                        if ($scope.HrmEmployeeId == 0) {
                            $scope.GetTargetsByYear();
                        }
                        else {
                            $scope.GetTargetsByYearNEmployeeId();
                        }
                        
                    }
                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                }
                ).error(function (error) {
                });
        });
    };//end of Save




    $scope.setFortEdit = function (t) {      
        state = 1;
        $scope.Id = t.Id;
        $scope.RefNo = t.RefNo;
        $scope.Month = t.Month;
        $scope.Year = t.Year;
        $scope.HrmEmployeeId = t.HrmEmployeeId;
        $scope.TargetSalesAmount = t.TargetSalesAmount;
        $scope.Remarks = t.Remarks;
        $scope.CreatedBy = t.CreatedBy;
        $scope.CreatedDate = ConverttoDateString(t.CreatedDate);
        $scope.ButtonDisabled = false;
    }


    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            var Id = $scope.Id;
            $http.post("/Sales/SalesTarget/Delete", { Id: Id }).success(function (data) {
                if (data.Success) {
                    if ($scope.HrmEmployeeId == 0) {
                        $scope.GetTargetsByYear();
                    }
                    else {
                        $scope.GetTargetsByYearNEmployeeId();
                    }
                    $scope.Reset();
                }
                Erpmodal.Delete(data, "Delete");

            }).error(function () {

            })
        });
    };

    $scope.DeleteDetail = function (index, id) {
        Erpmodal.Confirm('Delete').then(function (result) {
            $http.post("/Sales/SalesTarget/DeleteDetail", { Id: id }).success(function (data) {
                $scope.TargetDetails.splice(index, 1);
            }).error(function () {

            })

        });
    };


    $scope.Reset = function () {
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.TargetDetails = [];
        GetTargetDetails();
        GetRefNo();
        $scope.SalesTarget.$setPristine();
    }
    $scope.Clear = function () {
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.Targets = [];
        $scope.TargetDetails = [];
        GetTargetDetails();
        GetRefNo();
        $scope.Id = 0;
        $scope.Month = null;
        $scope.Year = 0;
        $scope.HrmEmployeeId = 0;
        $scope.TargetSalesAmount = 0;
        $scope.Remarks = "";
    }
   

    $scope.GetTargetList = function () {

        $http({
            url: '/Sales/SalesTarget/GetTargetList',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            List = 1;
            $scope.TargetList = data;


        });


    }//end of GetTargetList

    $scope.GoToSalesTarget = function (t) {

        var url = 'TargetList=' + JSON.stringify(t) + '&ListTag=1';
        window.open("/Sales/SalesTarget/Index?" + encodeURIComponent(url));
        window.focus();       
    

    }   

    function GetListTag() {        
       
        if (window.location.search.split('?').length > 1) {          
            List = angular.fromJson(decodeURIComponent(window.location.search.split('?')[1]).split('&')[1].split('=')[1]);           
        }
        else {
            List = 0;
        }
    

    }
   
    function GetData() {

        if (window.location.search.split('?').length > 1) {

            var url = decodeURIComponent(window.location.search.split('?')[1]).split('&')[0].split('=')[1];
            var t = angular.fromJson(url);
            $scope.SalesTarget = new Object();
            state = 1;
            $scope.Id = t.Id;
            $scope.RefNo = t.RefNo;
            $scope.Month = t.Month;
            $scope.Year = t.Year;
            $scope.HrmEmployeeId = t.HrmEmployeeId;
            $scope.TargetSalesAmount = t.TargetSalesAmount;
            $scope.Remarks = t.Remarks;
            $scope.CreatedBy = t.CreatedBy;
            $scope.CreatedDate = ConverttoDateString(t.CreatedDate);
            $scope.ButtonDisabled = false;

            $http({
                url: '/Sales/SalesTarget/GetTargetsByYearNEmployeeId?month=' + $scope.Month + '&&year=' + $scope.Year + '&&employeeId=' + $scope.HrmEmployeeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Targets = [];
                $scope.Targets = data;


            }).error(function (data) {
            });

            $http({
                url: '/Sales/SalesTarget/GetTargetDetailByTargetId?targetId=' + $scope.Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.TargetDetails = data;
                GetUnits();

            }).error(function (data) {
            });

             ;

        }


    };

    
    $scope.detailInvalid = function (targetdet) {
         
        return !(targetdet.SlsProductId && targetdet.Quantity >= 0 && targetdet.Quantity != '' && targetdet.SlsUnitId);
    };



});
