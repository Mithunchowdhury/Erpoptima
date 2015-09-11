(function () {

    app.controller('DistributorReceiveController', function ($scope, $http, Erpmodal) {

        var DistributorRcievesId = 0;       
        $scope.OrderId = 0;        
        var state;        
        $scope.SalesOrder = new Object();
        $scope.SalesDelivery = new Object;
        $scope.SalesDeliveryDetails = {}       


        var init = function () {
            $scope.OrderId = 0;
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Sales/Delivery/GetAllOrders',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {                
                $scope.SalesOrder = data;             
                
            });
            
        }//end of init      


        init();//init is called


        // get all by orderId 
        $scope.SalesDeliveryInfo = function () {
            $http({
                url: '/Sales/Delivery/GetAllByOrderID',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    orderId: $scope.OrderId,
                }
            }).success(function (data) {
                $scope.SalesDelivery = data;
                $http({
                    url: '/Sales/Delivery/GetDetailsByOrderID',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        orderId: $scope.OrderId,
                    }
                }).success(function (data) {
                    $scope.SalesDeliveryDetails = data;
                });
            });
        };
        //end

       
        $scope.Save = function () {

            Erpmodal.Confirm('Save').then(function (result) {

               
                $scope.SalesDelivery.Id = $scope.SalesDelivery.Id;
                $scope.SalesDelivery.ReceivedStatus = 1;
                
                $scope.SalesDelivery.CreatedDate = ConverttoDateString($scope.SalesDelivery.CreatedDate);
                $scope.SalesDelivery.DeliveryDate = ConverttoDateString($scope.SalesDelivery.DeliveryDate);
                $scope.SalesDelivery.ModifiedDate = ConverttoDateString($scope.SalesDelivery.ModifiedDate);               
                $scope.SalesDeliveries = $scope.SalesDelivery;
                               
                
                $http.post("/Sales/Delivery/Update", {objSalesReceive: $scope.SalesDeliveries}).success(

                    function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();

                    }
                    ).error(function (error) {

                    });
            });

        };//end of Save

        $scope.Reset = function () {
            $scope.OrderId = 0;
            $scope.SalesDelivery = 0;           
            $scope.ButtonDisabled = true;
            state = 0;            
            init();
            $scope.DistributorReceive.$setPristine();

        }//end of Reset


    });

}).call(this);