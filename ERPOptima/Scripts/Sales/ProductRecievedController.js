(function () {

    app.controller('ProductRecievedController', function ($scope, $http, Erpmodal) {

        var ProductRecievesId = 0;
        var ProductRecievedDetailId = 0;
        //$scope.RequisitionId = 0;
        //$scope.IssueId = 0;
        var state;
        $scope.ProductRecievedDetail = [];
        $scope.ProductRecieved = new Object();       
        $scope.Resources = new Object();
        $scope.Requisition = new Object();
        $scope.Issues = new Object();
        $scope.Categories = [];

        var SavedRequisitionId = 0;
        var SavedRequisitionIssueId = 0;
       

        var init = function () {
            //$scope.RequisitionId = 0;            
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Inventory/Requisition/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Requisition = data;
                //$scope.RequisitionId = 0;
            });          

          
            //$http({
            //    url: '/Sales/ProductReceive/GetAll',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.Resources = data;
            //    $scope.IssueId = 0;
            //    //$scope.YearId = 0;
            //    //$scope.EmployeeId = 0;

            //});

        }//end of init      


        init();//init is called
       

        // get Issue by Requisition id
        $scope.GetIssueByRequisition = function () {
            var reqId = 0;
            if (SavedRequisitionId > 0)
            {
                reqId = SavedRequisitionId;
            }
            else
            {
                reqId = $scope.RequisitionId;
            }

            if (reqId > 0) {
                $scope.Categories = {};
                $scope.Resources = {};

                $http({
                    url: '/Inventory/Issue/GetIssueByRequisitionId',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {
                        requisitionId: reqId,
                    }
                }).success(function (data) {
                    $scope.Issues = data;
                    //$scope.IssueId = 0;

                    if (SavedRequisitionIssueId > 0)
                    {
                        $scope.GetProductListByIssue();
                    }

                });
            }
        };
        //end

        // get Product by Issue id
        $scope.GetProductListByIssue = function () {
            var reqIssueId = 0;
            if (SavedRequisitionIssueId > 0) {
                reqIssueId = SavedRequisitionIssueId;
            }
            else {
                reqIssueId = $scope.IssueId;                
            }
            if (reqIssueId > 0) {
                $scope.Categories = {};
                $scope.Resources = {};

                $http({
                    url: '/Sales/ProductReceive/GetAllProductsForIssue',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: {

                        issueId: reqIssueId
                    }
                }).success(function (data) {
                    //                 
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
                            url: '/Sales/ProductReceive/GetByIssue',
                            method: 'GET',
                            headers: { 'Content-Type': 'application/json' },
                            params: {
                                issueId: reqIssueId
                            }
                        }).success(function (data) {
                            $scope.ProductRecieve = data;
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
                    $scope.ProductRecieved.InvIssueId = $scope.IssueId;
                    $scope.ProductRecievedDetail = $scope.Categories;
                    $scope.ProductRecieved.Id = $scope.ProductRecieve.Id;
                    $scope.ProductRecieved.CreatedBy = $scope.ProductRecieve.CreatedBy;
                    $scope.ProductRecieved.CreatedDate = ConverttoDateString($scope.ProductRecieve.CreatedDate);
                    $scope.ProductRecieved.ChallanNo = $scope.ProductRecieve.ChallanNo;
                    $scope.ProductRecieved.Remarks = $scope.ProductRecieve.Remarks;
                   //  
                    $http.post("/Sales/ProductReceive/Save", { ProductRecievedobj: $scope.ProductRecieved, ProductRecievedDetailList: $scope.ProductRecievedDetail}).success(
                        
                        function (data) {
                            Erpmodal.Save(data, "Save");

                            $scope.Reset();
                        }
                        ).error(function (error) {
                            SavedRequisitionId = 0;
                            SavedRequisitionIssueId = 0;
                        });
                });

        };//end of Save

        $scope.Reset = function () {           
            $scope.IssueId = 0;
            $scope.ProductRecieve = {};
            $scope.ProductRecieved.$setPristine();
            $scope.Categories = {};
            $scope.ButtonDisabled = true;            
            state = 0;
            $scope.CollectionTarget = {};
            $scope.Resources = {};
            init();            

        }//end of Reset
                

    });

}).call(this);