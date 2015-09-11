
(function () {

    app.controller('ProductRequisitionController', ['$scope', '$http', 'Erpmodal', 'glbApprovalObj', function OrderApprovalController($scope, $http, Erpmodal, glbApprovalObj) {
        var localApprovalId = 0;

            $scope.Code = "";

            $scope.Products = new Object();
            $scope.Product = new Object();
    
            $scope.Units = new Object();
            $scope.Unit = new Object();
    
            $scope.rows = [];
    

            $scope.Requisitions = [];
            var state;
            var ReqId = 0;
            var ReqDetailId = 0;   
            $scope.Reqs = new Object();



            function init() {
                localApprovalId = glbApprovalObj.appID;
                $scope.ShowLoading = false;
                $scope.SaveDisabled = true;
                $scope.Requisitions = [];
                GetCode();
                GetProducts();
                GetAll();
                state = 0;

            }//end of init
            init();

            $scope.selectedRow = null;  // initialize our variable to null
            $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
                $scope.selectedRow = index;
            }


            function GetProducts() {

                $http({
                    url: '/Sales/ChartOfProduct/GetProductsConfigured',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {

                    $scope.Products = data;
          
            
                });


            }//end of GetProducts

            $scope.GetUnitsByProductId = function () {
                $http({
                    url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId?productId=' + angular.fromJson($scope.Product).Id,
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {           
                    $scope.Units = data;
          
          
                }).error(function (data) {
                });
            };

            function GetCode() {

                $http({
                    url: '/Inventory/Requisition/GetCode',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Code = data;          

                });


            }//end of GetCode


            function GetAll() {

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
                
                    if (localApprovalId !== undefined && localApprovalId > 0) {
                        var indexOfOpen = 0;
                        var count = 0;
                        for (var ind = 0; ind < $scope.Requisitions.length; ind++)
                        {
                            if ($scope.Requisitions[ind].Id == localApprovalId) {
                                indexOfOpen = count;                                
                            }

                            count++;
                        }

                        var openObj = $scope.Requisitions[indexOfOpen];
                        $scope.setForEdit(openObj);
                    }


                });


            }//end of GetAll

            $scope.AddRow = function () {
        
                var row = {
                    Id: ReqDetailId,
                    InvRequisitionId: ReqId,
                    SlsProductId: angular.fromJson($scope.Product).Id,
                    ProductName: angular.fromJson($scope.Product).Name,
                    RequiredQuantity: $scope.qty,
                    SlsUnitId: angular.fromJson($scope.Unit).SlsUnitId,
                    UnitName: angular.fromJson($scope.Unit).Unit,
                };
        
                if (!CheckDuplicateProduct(angular.fromJson($scope.Product).Id)) {
                    $scope.rows.push(row);
                }

            };

            function CheckDuplicateProduct(productId) {
               
                for (var i = 0; i < $scope.rows.length; i++) {
                    if ($scope.rows[i].SlsProductId == productId) {
                        return true;
                    }
                }
  
                return false;       


            }//end of CheckDuplicateProduct

            $scope.removeRow = function (index) {
                $scope.rows.splice(index, 1);
            };


   
            $scope.Save = function () {
                Erpmodal.Confirm("Save").then(function (result) {
                    if (state == 1) {
                        $scope.Reqs.Id = ReqId;
                    }
                    else {
                        $scope.Reqs.Id = 0;               
                    }
                    $scope.Reqs.RequisitionCode = $scope.Code;
                    $http.post("/Inventory/Requisition/Save",
                            $scope.Reqs
                        ).success(function (data) {
                            if (data.Success == true) {
                                SaveDetail(data.OperationId);
                                $scope.Reset();
                            }

                        }).error(function (data) {
                   
                        });
                });
            };//end of Save


            function SaveDetail(rid) {
                for (var i = 0; i < $scope.rows.length; i++) {
                    $scope.rows[i].InvRequisitionId = rid;
                }      
                $http.post("/Inventory/Requisition/SaveDetail",
                            $scope.rows
                        ).success(function (data) {
                            if (data.Success == true) {
                                Erpmodal.Save(data, "Save");
                                GetAll();
                                $scope.Reset();
                            }
                            else {
                                Delete(rid);
                            }


                        }).error(function (data) {

                        });

            };//end of SaveDetail

            $scope.setForEdit = function (req) {
                state = 1;
                ReqId = req.Id;
                $scope.Code = req.RequisitionCode;
                $scope.Reqs.PreferredDeliveryDate = req.PreferredDeliveryDate;
                $scope.Reqs.Remarks = req.Remarks;        
                GetReqDetail();
                $scope.ButtonDisabled = false;
            }

            function GetReqDetail() {        
                $http({
                    url: '/Inventory/Requisition/GetByRequisitionId?requisitionId=' + ReqId,
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.rows = data;

                }).error(function (data) {
                });


            }//end of GetReqDetail

            function Delete (rid) {       
                var Id = rid;          
                $http.post("/Inventory/Requisition/Delete", { Id: Id }).success(function (data) {                   
                   
                }).error(function () {

                })            
        
            };

            $scope.DeleteDetail = function (index,id) {
                Erpmodal.Confirm('Delete').then(function (result) {
           
                    $http.post("/Inventory/Requisition/DeleteDetail", { Id: id }).success(function (data) {                       
                        $scope.rows.splice(index, 1);
                        Erpmodal.Delete(data, "Delete");                   
                    }).error(function () {

                    })
            
                });
            };

            $scope.setForEditDetal = function (index,r) {
                ReqDetailId = r.Id;
                ReqId = r.InvRequisitionId;
                $scope.Product = _.where($scope.Products, { Id: r.SlsProductId });        
                $scope.qty = r.RequiredQuantity;
                $scope.GetUnitsByProductId();
                $scope.Unit = _.where($scope.Units, { Id: r.SlsUnitId });       
                $scope.rows.splice(index, 1);
            }


            $scope.Reset = function () {
                init();
                $scope.SaveDisabled = true;
                $scope.Reqs = new Object();
                $scope.rows = [];               
                $scope.Clear();
                $validation.reset($scope.Requisition);
                $scope.Requisition.$setPristine();
            };//end of Reset

            $scope.Clear = function () {              
                $scope.Product = new Object();               
                $scope.qty = "";
                $scope.Unit = new Object();
                $scope.Units = new Object();
            };//end of clear


            $scope.detailInvalid = function () {
                 
                return !($scope.Product && $scope.qty && $scope.Unit);
            };

        }]);

    
    }).call(this);