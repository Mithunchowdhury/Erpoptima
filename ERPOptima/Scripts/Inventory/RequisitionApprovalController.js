(function () {

    app.controller('RequisitionApprovalController', ['$scope', '$http', '$modal', 'Erpmodal', 'glbreqapproval', function OrderApprovalController($scope, $http, $modal, Erpmodal, glbreqapproval) {
        var localRequisitionId = 0;
        $scope.Resources = [];
        $scope.ApprovalStatusData = [];

        var init = function () {
            localRequisitionId = glbreqapproval.reqID;

            $scope.ResetFind();
            $scope.ResetResult();
            $scope.ResetForm();

            //fetch all items of sales order and load in grid
            $http({
                url: '/Inventory/Requisition/GetAllReqForApproval',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Resources = data;
            });


            if (localRequisitionId !== undefined && localRequisitionId > 0) {
                $scope.GetReqApprovalById(localRequisitionId);
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
            $scope.ReqFind = new Object();
        };

        $scope.ResetResult = function () {
            $scope.Resources = new Object();
        };

        $scope.ResetForm = function () {
            $scope.ReqApproval = new Object();
        };

        $scope.Find = function () {
            $scope.ResetResult();
            $scope.ResetForm();

            //Finding sales order by date range of preferred delivery date
            $http({
                url: '/Inventory/Requisition/GetAllReqForApprovalByDate',
                method: 'GET',
                params: { fromDate: $scope.ReqFind.FromDate, toDate: $scope.ReqFind.ToDate },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });
        };

        $scope.Reset = function () {
            $scope.RequisitionApproval.$setPristine();
            init();

            //$scope.ResetFind();
            ////$scope.ResetResult();
            //$scope.ResetForm();
        };

        

        //$scope.Approve = function () {
        //    Erpmodal.Confirm('Save').then(function (result) {
        //        $scope.ReqApproval.Comment = $scope.ReqApproval.Comment + ' ' + $scope.ReqApproval.NewComment;


        //        $http.post("/Inventory/Requisition/ApproveRequisition", {
        //            obj: $scope.ReqApproval,
        //            newComment: $scope.ReqApproval.NewComment
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
        //        $scope.ReqApproval.Comment = $scope.ReqApproval.Comment + ' ' + $scope.ReqApproval.NewComment;

        //        $http.post("/Inventory/Requisition/RejectRequisition", {
        //            obj: $scope.ReqApproval,
        //            newComment: $scope.ReqApproval.NewComment
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
        //        $scope.ReqApproval.Comment = $scope.ReqApproval.Comment + ' ' + $scope.ReqApproval.NewComment;

        //        $http.post("/Inventory/Requisition/PassRequisition", {
        //            obj: $scope.ReqApproval,
        //            newComment: $scope.ReqApproval.NewComment
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

        //$scope.DisableApproval = function (SelectedApproval) {          
        //    return !(SelectedApproval !== undefined && (SelectedApproval.ApprovalStatus == 1 || SelectedApproval.ApprovalStatus == 3));
        //};




        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.GetReqApproval = function (record) {
            var rId = record.Id;
            $scope.GetReqApprovalById(rId);
        };

        $scope.GetReqApprovalById = function (res) {
            //$scope.ResetForm();
            $http({
                url: '/Inventory/Requisition/GetReqApprovalByReqId',
                method: 'GET',
                params: { reqId: res.Id },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.ReqApproval = data;
                $scope.ReqApproval.CreatedDate = ConverttoDateString(data.CreatedDate);
                if (data.ModifiedDate !== null) {
                    $scope.ReqApproval.ModifiedDate = ConverttoDateString(data.ModifiedDate);
                }
                $scope.GetReqDetail(res);
            });
        };

        
        $scope.GetReqDetail= function (res) {
            $http({
                url: '/Inventory/Requisition/GetByRequisitionId?requisitionId=' + res.Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.ReqDetails = data;               
                $scope.OpenPopup(res);
            }).error(function (data) {
            });


        }//end of GetReqDetail

        //open Modal
        $scope.Reqs = { ReqId: 0, RequisitionCode: "", PreferredDeliveryDate: "", Remarks: "", ReqDetails: $scope.ReqDetails, PreComments: "", NewComments: "", ReqApproval: $scope.ReqApproval, ApprovalStatus: 0 };

        $scope.OpenPopup = function (req,size) {
            var modalInstance = $modal.open({
                templateUrl: 'modal',
                controller: ModalInstanceCtrl,
                size: size,
                resolve: {
                    Reqs: function () {                        
                        $scope.Reqs.ReqId = req.Id;
                        $scope.Reqs.RequisitionCode = req.RequisitionCode;
                        $scope.Reqs.PreferredDeliveryDate = ConverttoDateString(req.PreferredDeliveryDate);
                        $scope.Reqs.Remarks = req.Remarks;
                        $scope.Reqs.ReqDetails = $scope.ReqDetails;
                        $scope.Reqs.PreComments = $scope.ReqApproval.Comment;
                        $scope.Reqs.ReqApproval = $scope.ReqApproval;
                        $scope.Reqs.ApprovalStatus = req.ApprovalStatus;
                        return $scope.Reqs;
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


        var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, Reqs) {
            $scope.Reqs = Reqs;
            //$scope.ok = function () {
            //    $modalInstance.close($scope.Reqs);
            //    //$modalInstance.open($scope.Reqs);
            //};

            $scope.Pass = function () {
                
                //if (confirm('Do you want to continue')) {
                  

                    $scope.Reqs.ReqApproval.Comment = $scope.Reqs.PreComments + ' ' + $scope.Reqs.NewComments;

                    $http.post("/Inventory/Requisition/PassRequisition", {
                        obj: $scope.Reqs.ReqApproval,
                        newComment: $scope.Reqs.NewComments
                    }).success(
                            function (data) {
                                Erpmodal.Save(data, "Save");
                                init();
                                $scope.Reqs.NewComments = "";
                            }
                            ).error(function (error) {

                            });


                //}

                
            };

            $scope.Approve = function () {
               
                    $scope.Reqs.ReqApproval.Comment = $scope.Reqs.PreComments + ' ' + $scope.Reqs.NewComments;


                    $http.post("/Inventory/Requisition/ApproveRequisition", {
                        obj: $scope.Reqs.ReqApproval,
                        newComment: $scope.Reqs.NewComments
                    }).success(
                            function (data) {
                                Erpmodal.Save(data, "Save");
                                init();
                                $scope.Reqs.NewComments = "";
                            }
                            ).error(function (error) {

                            });

               
            };

            $scope.Reject = function () {
                
                    $scope.Reqs.ReqApproval.Comment = $scope.Reqs.PreComments + ' ' + $scope.Reqs.NewComments;

                    $http.post("/Inventory/Requisition/RejectRequisition", {
                        obj: $scope.Reqs.ReqApproval,
                        newComment: $scope.Reqs.NewComments
                    }).success(
                            function (data) {
                                Erpmodal.Save(data, "Save");
                                init();
                                $scope.Reqs.NewComments = "";
                            }
                            ).error(function (error) {

                            });
               
            };


            $scope.DisableApproval = function () {
                var ret = !($scope.Reqs.ApprovalStatus !== undefined && ($scope.Reqs.ApprovalStatus == 1 || $scope.Reqs.ApprovalStatus == 3) && $scope.Reqs.NewComments !== "" && $scope.Reqs.NewComments !== undefined);
                return ret;
            };



            $scope.cancel = function () {
                init();
                $modalInstance.dismiss('cancel');
            };
        };














    }]);


}).call(this);



