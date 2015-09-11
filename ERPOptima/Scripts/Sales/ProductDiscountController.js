app.controller("ProductDiscountController", function ($scope, $http, Erpmodal, $validation) {
        
    $scope.Category = new Object();
    $scope.CategoryId = 0;

    $scope.SubCategory = new Object();
    $scope.SubCategoryId = 0;

    $scope.Region = new Object();
    $scope.RegionId = 0;


    var productId = 0;
  

    $scope.ProductDiscounts = [];



    function init() {
        $scope.ShowLoading = false;
        $scope.SaveDisabled = true;
        $scope.ProductDiscounts = [];
        GetCategory();
        GetRegion();

    }//end of init
    init();

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    function GetCategory() {

        $http({
            url: '/Sales/ChartOfProduct/GetCategories',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Category = data;
            $scope.CategoryId = 0;



        });


    }//end of GetCategory

    $scope.GetSubCategory = function (level) {
        $http({
            url: '/Sales/ChartOfProduct/GetSubCategories?categoryId=' + $scope.CategoryId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.SubCategory = data;
            $scope.SubCategoryId = 0;
        }).error(function (data) {
        });
    };

    function GetRegion() {

        $http({
            url: '/Sales/Region/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Region = data;
            $scope.RegionId = 0;



        });


    }//end of GetCategory


    $scope.GetDiscount = function () {
        $http({
            url: '/Sales/ProductDiscount/GetProductDiscount?categoryId=' + productId + '&&regionId=' + $scope.RegionId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.ProductDiscounts = [];
            for (var i = 0; i < data.length; i++) {
                data[i].Date = ConverttoDateString(data[i].Date);
                data[i].SlsRegionId = $scope.RegionId;
            }        
            $scope.ProductDiscounts = data;           
            $scope.ButtonDisabled = false;
        }).error(function (data) {
        });
    };
    $scope.SetCategoryId = function () {
        $scope.SubCategoryId = 0;
        productId = $scope.CategoryId;       
    };
    $scope.SetSubCategoryId = function () {      
        productId = $scope.SubCategoryId;       
    };

    $scope.CheckAll = function () {

        angular.forEach($scope.ProductDiscounts, function (value, key) {
            value.Status = $scope.chkAll;
        });
    };

    $scope.Save = function () {
        Erpmodal.Confirm("Save").then(function (result) {
            var updatedList = _.where($scope.ProductDiscounts, { Status: true });
            var removedList = _.where($scope.ProductDiscounts, { Status: false });

            $http.post("/Sales/ProductDiscount/Save",
                {
                    discountList: updatedList,
                    removeDiscountList: removedList
                }).success(function (data) {

                    Erpmodal.Save(data, "Save");
                    $scope.Reset();
                    $scope.GetDiscount();
                  
                }).error(function (data) {
                   
                });
        });
    };//end of Save

    $scope.Reset = function () {
        init();
        $scope.SaveDisabled = true;
        $scope.SubCategory = 0;
        $validation.reset($scope.ProductDiscount);
        $scope.ProductDiscount.$setPristine();
    };//end of Reset



});
