(function () {

    app.controller("SalesReturnController", function ($scope, $http, Erpmodal) {

     
        $scope.Products = new Object();
        $scope.Product = new Object();

        $scope.Units = new Object();
        $scope.Unit = new Object();

        $scope.Parties = new Object();
        $scope.Party = new Object();

        $scope.MainObj = new Object();
        $scope.MainList = [];
      
        $scope.DetailList = [];
        
        var state;
        var MainId = 0;
        var DetailId = 0;


        function init() {            
            initForm();
            
            GetAll();
            GetRefNo();
            GetProducts();
            $http({
                url: '/Sales/ChartOfProduct/GetProductsConfigured',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Products = data;
                SlsProductId = 0;


            });
        };//end of init

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        function initForm() {            
            state = 0;
            MainId = 0;
            $scope.ShowLoading = false;
            $scope.SaveDisabled = true;
            $scope.ButtonDisabled = false;
            $scope.MainObj = new Object();
            $scope.MainObj.PartyType = 0;
            //reset second grid
            $scope.Resources2 = [];
        };

        init();

        $scope.PartyTypeChangeHandler = function () {
            
            $scope.Parties = new Object();

            if ($scope.MainObj.PartyType == 1) {
                $http({
                    url: '/Sales/Distributor/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });
            }
            else if ($scope.MainObj.PartyType == 2) {
                $http({
                    url: '/Sales/Retailer/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });
            }
            else if ($scope.MainObj.PartyType == 3) {
                $http({
                    url: '/Sales/Dealer/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });
            }
            else if ($scope.MainObj.PartyType == 4) {
                $http({
                    url: '/Sales/CorporateClient/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });
            }
        };

        function GetProducts() {

            $http({
                url: '/Sales/ChartOfProduct/GetProducts',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Products = data;
            });


        };//end of GetProducts

        $scope.GetUnitsByProductId = function () {
            $http({
                url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: { productId: angular.fromJson($scope.SlsProduct).Id }
            }).success(function (data) {
                $scope.Units = data;
            }).error(function (data) {
            });
        };
        


        function GetRefNo() {

            $http({
                url: '/Sales/SalesReturn/GetRefNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                
                $scope.MainObj.RefNo = data.result;
            });
        }; //end of GetRefNo


        function GetAll() {

            $http({
                url: '/Sales/SalesReturn/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
               
                $scope.MainList = data;
            }).error(function (data) {
                
                //Erpmodal.Error(data, "Error");
            });


        };//end of GetAll

        $scope.AddRow = function () {
            //This always add new item with Id = 0
            var rateOfReturnedProduct = 0.0;
            if ($scope.Rate !== null && $scope.Rate !== undefined) {
                rateOfReturnedProduct = $scope.Rate;
            }

            var row = {
                Id: 0,
                SlsReturnId: MainId,
                SlsProductId: angular.fromJson($scope.SlsProduct).Id,
                SlsProductName: angular.fromJson($scope.SlsProduct).Name,
                ReturnedQuantity: $scope.ReturnedQuantity,
                SlsUnitId: angular.fromJson($scope.SlsUnit).SlsUnitId,
                SlsUnitName: angular.fromJson($scope.SlsUnit).Unit,
                SlsProduct: "",
                SlsUnit: "",
                Rate: rateOfReturnedProduct,
                //Price: $scope.Price
            };
            
            if ($scope.duplicateDetail(angular.fromJson($scope.SlsProduct).Id)) {
                Erpmodal.Warning("Duplicate record!");
            }
            else {
                if (!$scope.Resources2)
                    $scope.Resources2 = [];

                $scope.Resources2.push(row);
            }

            $scope.Clear();
        };

        //function CheckDuplicateProduct(productId) {

        //    for (var i = 0; i < $scope.rows.length; i++) {
        //        if ($scope.rows[i].SlsProductId == productId) {
        //            return true;
        //        }
        //    }

        //    return false;


        //};//end of CheckDuplicateProduct

        $scope.removeRow = function (item) {
            
            var index = $scope.Resources2.indexOf(item);
            if (index > -1) {
                $scope.Resources2.splice(index, 1);
            }
        };



        $scope.Save = function () {
            Erpmodal.Confirm("Save").then(function (result) {
                if (state == 1) {
                    $scope.MainObj.Id = MainId;
                }
                else {
                    $scope.MainObj.Id = 0;
                }
                $scope.MainObj.DetailList = $scope.Resources2;
                $http.post("/Sales/SalesReturn/Save",
                        $scope.MainObj
                    ).success(function (data) {                        
                        init();
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }).error(function (data) {

                    });
            });
        };//end of Save


        //function SaveDetail(rid) {
        //    for (var i = 0; i < $scope.rows.length; i++) {
        //        $scope.rows[i].InvRequisitionId = rid;
        //    }
        //    $http.post("/Inventory/Requisition/SaveDetail",
        //                $scope.rows
        //            ).success(function (data) {
        //                if (data.Success == true) {
        //                    Erpmodal.Save(data, "Save");
        //                    GetAll();
        //                    $scope.Reset();
        //                }
        //                else {
        //                    Delete(rid);
        //                }


        //            }).error(function (data) {

        //            });

        //};//end of SaveDetail

        $scope.setForEdit = function (req) {                    
            MainId = req.Id;
            $scope.MainObj = jQuery.extend(true, {}, req);
            $scope.MainObj.RefNo = req.RefNo;
            $scope.MainObj.PartyType = req.PartyType;
            $scope.PartyTypeChangeHandler();
            if (req.Party !== null && req.Party !== undefined) {
                $scope.MainObj.Party = req.Party;
            }
            if (req.Reason !== null && req.Reason !== undefined) {
                $scope.MainObj.Reason = req.Reason;
            }
            $scope.MainObj.Rate = req.Rate;
            //$scope.MainObj.Price = req.Price;
            $scope.MainObj.CreatedDate = ConverttoDateString(req.CreatedDate);
            if (req.ModifiedDate !== null && req.ModifiedDate !== undefined) {
                $scope.MainObj.ModifiedDate = ConverttoDateString(req.ModifiedDate);
            }
           
            
            $scope.Resources2 = req.DetailList;

            state = 1;
            $scope.ButtonDisabled = false;

            //$scope.MainObj = new Object();

            //state = 1;
            //$scope.ButtonDisabled = false;
           
            //$scope.MainObj = jQuery.extend(true, {}, req);
            //MainId = $scope.MainObj.Id;
            //$scope.MainObj.CreatedDate = ConverttoDateString(req.CreatedDate);
            //$scope.MainObj.ModifiedDate = ConverttoDateString(req.ModifiedDate);           

            

        };

        //function GetReqDetail() {
        //    $http({
        //        url: '/Inventory/Requisition/GetByRequisitionId?requisitionId=' + ReqId,
        //        method: 'GET',
        //        headers: { 'Content-Type': 'application/json' }
        //    }).success(function (data) {
        //        $scope.rows = data;

        //    }).error(function (data) {
        //    });


        //};//end of GetReqDetail

        //function Delete(rid) {
        //    var Id = rid;
        //    $http.post("/Inventory/Requisition/Delete", { Id: Id }).success(function (data) {

        //    }).error(function () {

        //    })

        //};

        //$scope.DeleteDetail = function (index, id) {
        //    Erpmodal.Confirm('Delete').then(function (result) {

        //        $http.post("/Inventory/Requisition/DeleteDetail", { Id: id }).success(function (data) {
        //            $scope.rows.splice(index, 1);
        //            Erpmodal.Delete(data, "Delete");
        //        }).error(function () {

        //        })

        //    });
        //};

        //$scope.setForEditDetal = function (index, r) {
        //    ReqDetailId = r.Id;
        //    ReqId = r.InvRequisitionId;
        //    $scope.Product = _.where($scope.Products, { Id: r.SlsProductId });
        //    $scope.qty = r.RequiredQuantity;
        //    $scope.GetUnitsByProductId();
        //    $scope.Unit = _.where($scope.Units, { Id: r.SlsUnitId });
        //    $scope.rows.splice(index, 1);
        //};


        $scope.Reset = function () {
            initForm();            
            $scope.Clear();
            GetRefNo();
            GetProducts();
            $scope.SalesReturnForm.$setPristine();
         
        };//end of Reset


        


        $scope.Clear = function () {           
            $scope.SlsProduct = null;
            $scope.ReturnedQuantity = "";
            $scope.SlsUnit = null;
            $scope.Units = null;
            $scope.Rate = "";
            $scope.Price = "";
        };//end of clear

        $scope.detailInvalid = function () {
            return !($scope.SlsProduct && $scope.ReturnedQuantity && $scope.SlsUnit);
        };

        $scope.duplicateDetail = function (newDetailKeyId) {
            
            for (var i = 0; i < $scope.Resources2.length; i++) {
                if ($scope.Resources2[i].SlsProductId > 0 && $scope.Resources2[i].SlsProductId == newDetailKeyId)
                    return true;
            }
            return false;
        };

        $scope.detailInvalid = function () {
            return !($scope.SlsProduct && $scope.ReturnedQuantity && $scope.SlsUnit);
        };


        $scope.formInvalid = function (forvalid) {        
            return !(forvalid && $scope.SlsProduct && $scope.ReturnedQuantity && $scope.SlsUnit);
        };




    });


}).call(this);