$(document).ready(function () {
    

});

app.controller("FreeProductController", function ($scope, $http, Erpmodal) {
    var state = 0;
    var MainId = 0;
    $scope.MainObj = new Object();
    $scope.MainObj.StartDate = "";
    $scope.MainObj.EndDate = "";

    var init = function () {
       
        state = 0;
        $scope.ButtonDisabled = true;       
        $scope.ResetForm();

        $scope.GetAll();

        $scope.LoadDropDowns();

    };//end of init

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    };
    

    $scope.LoadDropDowns = function () {
        $scope.GetAllProducts();
        $scope.GetAllUnits();
    };

    $scope.GetAll = function () {

        $http({
            url: '/Sales/FreeProduct/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }            

        }).success(function (data) {
            $scope.Resources = [];
            $scope.Resources = data;
        });


    };//end of GetAll

    $scope.ResetForm = function () {        
        $scope.MainObj = new Object();       
    };
  
    $scope.GetAllProducts = function () {

        $http({
            url: '/Sales/ChartOfProduct/GetProducts',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }

        }).success(function (data) {
            $scope.Products = [];
            $scope.Products = data;
        });


    };//end of GetAll

    $scope.GetAllUnits = function () {

        $http({
            url: '/Sales/UnitOfMeasurement/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }

        }).success(function (data) {
            $scope.Units = [];
            $scope.Units = data;
        });


    };//end of GetAll
   
    

    $scope.Reset = function () {
        //variable for form state
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.FreeProductForm.$setPristine();
        $scope.ResetForm();
    };

    $scope.setForEdit = function (rowobj) {
        state = 1;
        $scope.ButtonDisabled = false;

        $scope.MainObj = {};
        $scope.MainObj = jQuery.extend(true, {}, rowobj);

        MainId = rowobj.Id;
        $scope.MainObj.StartDate = ConverttoDateString(rowobj.StartDate);
        if (rowobj.EndDate !== undefined)
            $scope.MainObj.EndDate = ConverttoDateString(rowobj.EndDate);
        $scope.MainObj.CreatedDate = ConverttoDateString(rowobj.CreatedDate);
        if (rowobj.ModifiedDate !== undefined)
            $scope.MainObj.ModifiedDate = ConverttoDateString(rowobj.ModifiedDate);
    };
        
    $scope.Save = function () {
        
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.MainObj.Id = MainId;
                }
                else { $scope.MainObj.Id = 0; }
                
                $http.post("/Sales/FreeProduct/Save", $scope.MainObj).success(
                    function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                        $scope.GetAll();
                    }
                    ).error(function (error) {

                    });
            });


    };//end of Save

    $scope.Delete = function () {
        Erpmodal.Confirm('Delete').then(function (result) {
            var Id = MainId;
            if (MainId != 0) {
                $http.post("/Sales/FreeProduct/Delete", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    $scope.Reset();
                    $scope.GetAll();
                }).error(function () {

                })
            }
            else { Erpmodal.Warning("Please select a Free Product!"); }
        });
    };//end of Delete


    init();

});
