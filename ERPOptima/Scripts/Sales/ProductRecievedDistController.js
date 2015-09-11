(function () {

    app.controller('ProductRecievedDistController', function ($scope, $http, Erpmodal) {

        $scope.DeliveryComboDisable = true;

        var ProductRecievesId = 0;
        var ProductRecievedDetailId = 0;
        //$scope.SalesOrderId = 0;
        //$scope.DeliveryId = 0;
        var state;
        $scope.ProductRecievedDetail = [];
        $scope.ProductRecieved = new Object();       
        $scope.Resources = new Object();
        $scope.SalesOrder = new Object();
        $scope.Issues = new Object();
        $scope.Categories = [];

        var SalesOrderId = 0;
        var SalesDeliveryId = 0;
       

        var init = function () {               
             
            state = 0;
            $http({
                url: '/Sales/Sales/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.SalesOrder = data;
                //$scope.SalesOrderId = 0;
            });          

          
            //$http({
            //    url: '/Sales/ProductReceive/GetAll',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.Resources = data;
            //    $scope.DeliveryId = 0;
            //    //$scope.YearId = 0;
            //    //$scope.EmployeeId = 0;

            //});

        }//end of init      


        init();//init is called
       

        // get Issue by Requisition id
        $scope.GetDeliveryBySO = function () {
             
            var orderId = 0;
            if (SalesOrderId > 0)
            {
                orderId = SalesOrderId;
            }
            else
            {
                orderId = $scope.SalesOrderId;
            }

            if (orderId > 0) {
                $scope.Categories = {};
                $scope.Resources = {};

                $http({
                    url: '/Sales/Delivery/GetAllByOrderID',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        orderId: orderId,
                    }
                }).success(function (data) {
                     
                    $scope.Deliveries = data;
                    $scope.DeliveryId = data.Id;

                    $scope.GetProductListByDelivery();
                    
                });
            }
        };
        //end

        // get Product by Issue id
        $scope.GetProductListByDelivery = function () {
             
            var deliveryId = 0;
            if (SalesDeliveryId > 0) {
                deliveryId = SalesDeliveryId;
            }
            else {
                deliveryId = $scope.DeliveryId;
            }
            if (deliveryId > 0) {
                $scope.Categories = {};
                $scope.Resources = {};

                $http({
                    url: '/Sales/ProductReceive/GetAllProductsForDelivery',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {

                        delId: deliveryId
                    }
                }).success(function (data) {
                                     
                    $scope.Categories = data;
                    $scope.Resources = data;
                    //$http({
                    //    url: '/Sales/ProductReceive/GetAll',
                    //    method: 'GET',
                    //    headers: { 'Content-Type': 'application/json' },
                    //    params: {
                    //        issueId: reqIssueId
                    //    }
                    //}).success(function (data) {
                    //    $scope.Resources = data;
                        $http({
                            url: '/Sales/ProductReceive/GetByDelivery',
                            method: 'GET',
                            headers: { 'Content-Type': 'application/json' },
                            params: {
                                delId: deliveryId
                            }
                        }).success(function (data) {
                             
                            $scope.ProductRecieve = data;
                            $scope.ProductRecieve.RcvDate = ConverttoDateString(data.RcvDate);
                        });
                    //});

                });
            }
          
        };
        //end
             
               

        $scope.Save = function () {
            
                Erpmodal.Confirm('Save').then(function (result) {
                    
                    if (state == 1) {
                        $scope.ProductRecieved.Id = ProductRecievesId;                       
                    }
                    else {
                        $scope.ProductRecieved.Id = 0;
                        $scope.ProductRecievedDetail.Id = 0;
                    }                    
                   
                    $scope.ProductRecievedDetail = $scope.Categories;
                    $scope.ProductRecieved.Id = $scope.ProductRecieve.Id;
                    $scope.ProductRecieved.SlsDeliveryId = $scope.DeliveryId;
                    if ($scope.ProductRecieve.InvoiceNo !== undefined && $scope.ProductRecieve.InvoiceNo != null)
                        $scope.ProductRecieved.InvoiceNo = $scope.ProductRecieve.InvoiceNo;
                    if ($scope.ProductRecieve.VehicleNo !== undefined && $scope.ProductRecieve.VehicleNo != null)
                        $scope.ProductRecieved.VehicleNo = $scope.ProductRecieve.VehicleNo;
                    if ($scope.ProductRecieve.ChallanNo !== undefined && $scope.ProductRecieve.ChallanNo != null)
                        $scope.ProductRecieved.ChallanNo = $scope.ProductRecieve.ChallanNo;
                    else
                    {
                        if ($scope.Categories !== undefined && $scope.Categories.length > 0)
                            $scope.ProductRecieved.ChallanNo = $scope.Categories[0].Challan;
                    }
                    $scope.ProductRecieved.CreatedBy = $scope.ProductRecieve.CreatedBy;
                    $scope.ProductRecieved.CreatedDate = ConverttoDateString($scope.ProductRecieve.CreatedDate);
                    $scope.ProductRecieved.ModifiedBy = $scope.ProductRecieve.ModifiedBy;
                    if ($scope.ProductRecieve.ModifiedDate !== undefined && $scope.ProductRecieve.ModifiedDate != null)
                        $scope.ProductRecieved.ModifiedDate = ConverttoDateString($scope.ProductRecieve.ModifiedDate);                    
                    $scope.ProductRecieved.RcvDate = $scope.ProductRecieve.RcvDate;                    
                    $scope.ProductRecieved.Remarks = $scope.ProductRecieve.Remarks;
                   //  
                    $http.post("/Sales/ProductReceive/SaveReceivedByDistributor", { ProductRecievedobj: $scope.ProductRecieved, ProductRecievedDetailList: $scope.ProductRecievedDetail }).success(
                        
                        function (data) {
                            Erpmodal.Save(data, "Save");

                            $scope.Reset();
                        }
                        ).error(function (error) {
                            SalesOrderId = 0;
                            SalesDeliveryId = 0;
                        });
                });

        };//end of Save

        $scope.Reset = function () {           
            $scope.DeliveryId = 0;
            $scope.ProductRecieve = {};
            $scope.Categories = {};            
            state = 0;
            $scope.CollectionTarget = {};
            $scope.Resources = {};
            $scope.ProductRecieved.$setPristine();
            init();            

        }//end of Reset
                

    });

}).call(this);