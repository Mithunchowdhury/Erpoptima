(function () {

    app.controller('OrderApprovalController', ['$scope', '$http', '$modal', 'Erpmodal', 'glbsoapproval', function OrderApprovalController($scope, $http,$modal, Erpmodal, glbsoapproval) {
        var localSalesOrderId = 0;
        $scope.idSelectedApproval = 0;
        $scope.ApprovalStatusData = [];

        var init = function () {
            localSalesOrderId = glbsoapproval.soID;
            $scope.idSelectedApproval = localSalesOrderId;

            
            $scope.ResetFind();
            $scope.ResetResult();
            $scope.ResetForm();

            //fetch all items of sales order and load in grid
            $http({
                url: '/Sales/Sales/GetAllSOForApproval',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });

            

            if (localSalesOrderId !== undefined && localSalesOrderId > 0) {
                $scope.GetSOApprovalById(localSalesOrderId);
            }

            $http({
                url: '/Common/ApprovalData/GetApprovalDictionary',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                
                $scope.ApprovalStatusData = data;
            });

        };

        $scope.ResetFind = function () {
            $scope.SOFind = new Object();
        };

        $scope.ResetResult = function () {
            $scope.Resources = new Object();
        };

        $scope.ResetForm = function () {
            $scope.SOApproval = new Object();
        };

        $scope.Find = function () {
            $scope.ResetResult();
            $scope.ResetForm();

            //Finding sales order by date range of preferred delivery date
            $http({
                url: '/Sales/Sales/GetAllSOForApprovalByDate',
                method: 'GET',
                params: { fromDate: $scope.SOFind.FromDate, toDate: $scope.SOFind.ToDate },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });
        };

        $scope.Reset = function () {
            $scope.FindSOApprvlForm.$setPristine();
            init();

            //$scope.ResetFind();
            ////$scope.ResetResult();
            //$scope.ResetForm();
        };

        $scope.GetSOApproval = function (record) {
            var sOrderId = record.Id;
            $scope.GetSOApprovalById(sOrderId);
        };

        $scope.GetSOApprovalById = function (res) {
             
            $scope.ResetForm();
            $http({
                url: '/Sales/Sales/GetSOApprovalBySOId',
                method: 'GET',
                params: { salesOrderId: res.Id },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.SOApproval = data;
                $scope.SOApproval.CreatedDate = ConverttoDateString(data.CreatedDate);
                if (data.ModifiedDate !== null) {
                    $scope.SOApproval.ModifiedDate = ConverttoDateString(data.ModifiedDate);
                }
                $scope.GetSalesOrderDetail(res);
                


            });
        };

        //$scope.Approve = function () {
        //    Erpmodal.Confirm('Save').then(function (result) {
        //        $scope.SOApproval.Comment = $scope.SOApproval.Comment + ' ' + $scope.SOApproval.NewComment;

        //        $http.post("/Sales/Sales/ApproveSalesOrder", {
        //            obj: $scope.SOApproval,
        //            newComment: $scope.SOApproval.NewComment
        //        }).success(
        //                function (data) {
        //                    Erpmodal.Save(data, "Save");
        //                    init();
        //                }
        //                ).error(function (error) {

        //                });
        //    });
        //};

        //$scope.Reject = function () {
        //    Erpmodal.Confirm('Save').then(function (result) {
        //        $scope.SOApproval.Comment = $scope.SOApproval.Comment + ' ' + $scope.SOApproval.NewComment;

        //        $http.post("/Sales/Sales/RejectSalesOrder", {
        //            obj: $scope.SOApproval,
        //            newComment: $scope.SOApproval.NewComment
        //        }).success(
        //                function (data) {
        //                    Erpmodal.Save(data, "Save");
        //                    init();
        //                }
        //                ).error(function (error) {

        //                });
        //    });
        //};

        //$scope.Pass = function () {
        //    Erpmodal.Confirm('Save').then(function (result) {
                 
        //        $scope.SOApproval.Comment = $scope.SOApproval.Comment + ' ' + $scope.SOApproval.NewComment;

        //        $http.post("/Sales/Sales/PassSalesOrder", {
        //            obj: $scope.SOApproval,
        //            newComment: $scope.SOApproval.NewComment
        //        }).success(
        //                function (data) {
        //                    Erpmodal.Save(data, "Save");
        //                    init();
        //                }
        //                ).error(function (error) {

        //                });

        //    });
        //};

        init();

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        //$scope.DisableApproval = function (SelectedApproval) {
             
        //    return !(SelectedApproval !== undefined && (SelectedApproval.ApprovalStatus == 1 || SelectedApproval.ApprovalStatus == 3));           
        //};

        $scope.GetSalesOrderDetail = function (res) {
            $http({
                url: '/Sales/Sales/GetBySalesOrderId?SalesOrderId=' + res.Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.SalesOrderDetails = data;
                $scope.OpenPopup(res);
            }).error(function (data) {
            });


        };//end of GetSalesOrderDetail


        //open Modal
        $scope.SalesOrder = {
            Id: 0, RefNo: "", PreferredDeliveryDate: "", Discount: 0, Total: 0, Advance: 0, Remarks: "",
            SalesOrderDetails: $scope.SalesOrderDetails, PreComments: "", NewComments: "", SalesOrderApproval: $scope.SOApproval, ApprovalStatus: 0
        };

        $scope.OpenPopup = function (so, size) {
            var modalInstance = $modal.open({
                templateUrl: 'modal',
                controller: ModalInstanceCtrl,
                size: size,
                resolve: {
                    SalesOrder: function () {
                        $scope.SalesOrder.Id = so.Id;
                        $scope.SalesOrder.RefNo = so.RefNo;
                        $scope.SalesOrder.PreferredDeliveryDate = ConverttoDateString(so.PreferredDeliveryDate);
                        $scope.SalesOrder.Discount = so.Discount;
                        $scope.SalesOrder.Total = so.Total;
                        $scope.SalesOrder.Advance = so.Advance;
                        $scope.SalesOrder.Remarks = so.Remarks;
                        $scope.SalesOrder.SalesOrderDetails = $scope.SalesOrderDetails;
                        $scope.SalesOrder.PreComments = $scope.SOApproval.Comment;
                        $scope.SalesOrder.SalesOrderApproval = $scope.SOApproval;
                        $scope.SalesOrder.ApprovalStatus = so.ApprovalStatus;
                        return $scope.SalesOrder;
                    }
                }
            });
            modalInstance.result.then(function () {





            }, function () {
            });

            modalInstance.opened.then(function () {



            }, function () {
            });

        };


        var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, SalesOrder) {
            $scope.SalesOrder = SalesOrder;
            //$scope.ok = function () {
            //    $modalInstance.close($scope.Reqs);
            //    //$modalInstance.open($scope.Reqs);
            //};

            $scope.Pass = function () {



                $scope.SalesOrder.SalesOrderApproval.Comment = $scope.SalesOrder.PreComments + ' ' + $scope.SalesOrder.NewComments;

                $http.post("/Sales/Sales/PassSalesOrder", {
                    obj: $scope.SalesOrder.SalesOrderApproval,
                    newComment: $scope.SalesOrder.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.SalesOrder.NewComments = "";
                        }
                        ).error(function (error) {

                        });





            };



            $scope.Approve = function () {

                $scope.SalesOrder.SalesOrderApproval.Comment = $scope.SalesOrder.PreComments + ' ' + $scope.SalesOrder.NewComments;

                 
                $http.post("/Sales/Sales/ApproveSalesOrder", {
                    obj: $scope.SalesOrder.SalesOrderApproval,
                    newComment: $scope.SalesOrder.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                            init();
                            $scope.SalesOrder.NewComments = "";
                        }
                        ).error(function (error) {

                        });


            };

            $scope.Reject = function () {

                $scope.SalesOrder.SalesOrderApproval.Comment = $scope.SalesOrder.PreComments + ' ' + $scope.SalesOrder.NewComments;

                $http.post("/Sales/Sales/RejectSalesOrder", {
                    obj: $scope.SalesOrder.SalesOrderApproval,
                    newComment: $scope.SalesOrder.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.SalesOrder.NewComments = "";
                        }
                        ).error(function (error) {

                        });

            };


            $scope.DisableApproval = function () {
                var ret = !($scope.SalesOrder.ApprovalStatus !== undefined && ($scope.SalesOrder.ApprovalStatus == 1 || $scope.SalesOrder.ApprovalStatus == 3) && $scope.SalesOrder.NewComments !== "" && $scope.SalesOrder.NewComments !== undefined);
                return ret;
            };



            $scope.cancel = function () {
                init();
                $modalInstance.dismiss('cancel');
            };
        };


    }]);


}).call(this);



