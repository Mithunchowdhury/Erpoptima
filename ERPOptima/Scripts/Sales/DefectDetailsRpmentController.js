(function () {

    app.controller('DefectReplacement', function ($scope, $http, Erpmodal) {

        $scope.SalesDelivery = new Object();
        $scope.Resources = new Object();
        $scope.SalesRefNos = new Object();
        $scope.Resources2 = [];
        var SavedDelId = 0;
        var state = 0;

        $scope.ChallanNo = "";
        $scope.InvoiceNo = "";

        
        $scope.GetAllSORef = function () {
            $http({
                url: '/Sales/Sales/GetAllSORefNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.SalesRefNos = data;
            });
        };

        var init = function () {
            
            state = 0;
            $scope.GetAllSORef();            

            //fetch all items of sales order and load in grid
            $http({
                url: '/Sales/Delivery/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });


            GetChallanNo();
            GetInvoiceNo();


        };

        init();

        $scope.GetDefectDetailsReplacementByDefectId = function (defectId) {

            $http({
                url: '/Sales/DefectReplacement/GetDefectDetailsReplacementByDefectId?defectId=' + defectId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources2 = data;
                $scope.SalesDelivery.SlsDeliverDetails = data;
            });


        }//end of GetChallanNo

        function GetChallanNo() {

            $http({
                url: '/Sales/Delivery/GetChallanNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.ChallanNo = data;              

            });


        }//end of GetChallanNo

        function GetInvoiceNo() {

            $http({
                url: '/Sales/Delivery/GetInvoiceNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.InvoiceNo = data;

            });


        }//end of GetInvoiceNo

       

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {

                    if (state == 1) {
                        $scope.SalesDelivery.Id = SavedDelId;
                    }
                    else { $scope.SalesDelivery.Id = 0; }

                    $scope.SalesDelivery.ChallanNo = $scope.ChallanNo;
                    $scope.SalesDelivery.InvoiceNo = $scope.InvoiceNo;

                    $http.post("/Sales/Delivery/SaveSalesDelivery", $scope.SalesDelivery).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                        }
                        ).error(function (error) {

                        });
                });
        };

        $scope.setForEdit = function (rowitem) {            
            $scope.FormReset();

            state = 1;
            SavedDelId = rowitem.Id;
          
            $scope.SalesDelivery = jQuery.extend(true, {}, rowitem); //rowitem;
            //$scope.Resources2 = rowitem.SalesOrderDetails;

            if (rowitem.SlsSalesOrderId != undefined)
                $scope.SalesDelivery.SlsSalesOrderId = rowitem.SlsSalesOrderId;

            $scope.ChallanNo = rowitem.ChallanNo;
            if (rowitem.InvoiceNo != undefined)
                $scope.InvoiceNo = rowitem.InvoiceNo;
            if (rowitem.VehicleNo != undefined)
                $scope.SalesDelivery.VehicleNo = rowitem.VehicleNo;
            if (rowitem.Remarks != undefined)
                $scope.SalesDelivery.Remarks = rowitem.Remarks;
            if (rowitem.DeliveryDate != undefined)
                $scope.SalesDelivery.DeliveryDate = ConverttoDateString(rowitem.DeliveryDate);

            $scope.SalesDelivery.CreatedDate = ConverttoDateString(rowitem.CreatedDate);
            if (rowitem.ModifiedDate != undefined)
                $scope.SalesDelivery.ModifiedDate = ConverttoDateString(rowitem.ModifiedDate);
            
            $scope.ButtonDisabled = false;
            $scope.CodeState = true;
            if ($scope.SalesDelivery.SlsDeliverDetails != undefined)
                $scope.SalesDelivery.SlsDeliverDetails = {};
            $scope.Resources2 = $scope.SalesDelivery.SlsDeliverDetails;
        };

        $scope.Reset = function () {

            init();
            $scope.CodeState = false;
            $scope.ButtonDisabled = true;
            SavedDelId = 0;
            state = 0;
            $scope.SalesDelivery = {};
            $scope.Resources2 = {};
            $scope.DefectReplacement.$setPristine();
        };

        $scope.FormReset = function () {            
            $scope.CodeState = false;
            $scope.ButtonDisabled = true;
            SavedDelId = 0;
            state = 0;
            $scope.SalesDelivery = {};
            $scope.Resources2 = {};
        };

       

    });

}).call(this);