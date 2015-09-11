(function () {

    app.controller('CorpSalesApprovalController', ['$scope', '$http', '$modal', 'Erpmodal', 'glbApprovalObj', function OrderApprovalController($scope, $http,$modal, Erpmodal, glbApprovalObj) {
        var localApprovalId = 0;        
        $scope.ApprovalStatusData = [];

        var init = function () {
            localApprovalId = glbApprovalObj.appID;           

             
            $scope.ResetFind();
            $scope.ResetResult();
            $scope.ResetForm();

            //fetch all items of sales order and load in grid
            $http({
                url: '/Sales/CorporateSales/GetAllForApproval',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) { 
                 
                $scope.Resources = data;
                if (localApprovalId !== undefined && localApprovalId > 0) {
                    $scope.GetApprovalObjById(localApprovalId);
                }

            });           

            $http({
                url: '/Common/ApprovalData/GetApprovalDictionary',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.ApprovalStatusData = data;
            });

        };

        $scope.ResetFind = function () {
            $scope.ApprovalSearchObj = new Object();
        };

        $scope.ResetResult = function () {
            $scope.Resources = new Object();
        };

        $scope.ResetForm = function () {
            $scope.ApprovalObj = new Object();
            $scope.NewComment = '';
        };

        $scope.Find = function () {
            $scope.ResetResult();
            $scope.ResetForm();

            //Finding sales order by date range of preferred delivery date
            $http({
                url: '/Sales/CorporateSales/GetAllForApprovalByDate',
                method: 'GET',
                params: { fromDate: $scope.ApprovalSearchObj.FromDate, toDate: $scope.ApprovalSearchObj.ToDate },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });
        };

        $scope.Reset = function () {

            init();
            $scope.FindApprvlForm.$setPristine();

            //$scope.ResetFind();
            ////$scope.ResetResult();
            //$scope.ResetForm();
        };

        $scope.GetApprovalObj = function (record) {
            var id = record.Id;
            $scope.GetApprovalObjById(id);
        };

        $scope.GetApprovalObjById = function (res) {
             
            $scope.ResetForm();
            $http({
                url: '/Sales/CorporateSales/GetApprovalById',
                method: 'GET',
                params: { id: res.Id },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.ApprovalObj = data;
                $scope.ApprovalObj.CreatedDate = ConverttoDateString(data.CreatedDate);
                if (data.ModifiedDate !== null) {
                    $scope.ApprovalObj.ModifiedDate = ConverttoDateString(data.ModifiedDate);
                }

                $scope.GetCorporateSalesDetail(res);


            });
        };

        //$scope.Approve = function () {
        //    Erpmodal.Confirm('Save').then(function (result) {
        //        $scope.ApprovalObj.Comment = $scope.ApprovalObj.Comment + ' ' + $scope.NewComment;

        //        $http.post("/Sales/CorporateSales/Approve", {
        //            obj: $scope.ApprovalObj,
        //            newComment: $scope.NewComment
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
        //        $scope.ApprovalObj.Comment = $scope.ApprovalObj.Comment + ' ' + $scope.NewComment;

        //        $http.post("/Sales/CorporateSales/Reject", {
        //            obj: $scope.ApprovalObj,
        //            newComment: $scope.NewComment
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
                 
        //        $scope.ApprovalObj.Comment = $scope.ApprovalObj.Comment + ' ' + $scope.NewComment;

        //        $http.post("/Sales/CorporateSales/Pass", {
        //            obj: $scope.ApprovalObj,
        //            newComment: $scope.NewComment
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
        };

        //$scope.DisableApproval = function (SelectedApproval) {
             
        //    return !(SelectedApproval !== undefined && (SelectedApproval.ApprovalStatus == 1 || SelectedApproval.ApprovalStatus == 3));
        //};

        $scope.GetCorporateSalesDetail = function (res) {
            $http({
                url: '/Sales/CorporateSales/GetByCorporateSalesId?CorporateSalesId=' + res.Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.CorporateSalesDetails = data;
                $scope.OpenPopup(res);
            }).error(function (data) {
            });


        };//end of GetCorporateSalesDetail

        //open Modal
        $scope.CorporateSales = {
            Id: 0, RefNo: "", CreatedDate: "", SalesAmount: 0, AdvanceAmount: 0, ExtraExpensePerson: 0, Designation: "", Phone: "", Percentage: 0, Amount: 0,
            CorporateSalesDetails: $scope.CorporateSalesDetails, PreComments: "", NewComments: "", CorporateSalesApproval: $scope.ApprovalObj,ApprovalStatus:0
        };

        $scope.OpenPopup = function (cs, size) {
            var modalInstance = $modal.open({
                templateUrl: 'modal',
                controller: ModalInstanceCtrl,
                size: size,
                resolve: {
                    CorporateSales: function () {
                        $scope.CorporateSales.Id = cs.Id;
                        $scope.CorporateSales.RefNo = cs.RefNo;
                        $scope.CorporateSales.CreatedDate = ConverttoDateString(cs.CreatedDate);
                        $scope.CorporateSales.SalesAmount = cs.SalesAmount;
                        $scope.CorporateSales.AdvanceAmount = cs.AdvanceAmount;
                        $scope.CorporateSales.ExtraExpensePerson = cs.ExtraExpensePerson;
                        $scope.CorporateSales.Designation = cs.Designation;
                        $scope.CorporateSales.Phone = cs.Phone;
                        $scope.CorporateSales.Percentage = cs.Percentage;
                        $scope.CorporateSales.Amount = cs.Amount;
                        $scope.CorporateSales.CorporateSalesDetails = $scope.CorporateSalesDetails;
                        $scope.CorporateSales.PreComments = $scope.ApprovalObj.Comment;
                        $scope.CorporateSales.CorporateSalesApproval = $scope.ApprovalObj;
                        $scope.CorporateSales.ApprovalStatus = cs.ApprovalStatus;
                        return $scope.CorporateSales;
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


        var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, CorporateSales) {
            $scope.CorporateSales = CorporateSales;
            //$scope.ok = function () {
            //    $modalInstance.close($scope.Reqs);
            //    //$modalInstance.open($scope.Reqs);
            //};

            $scope.Pass = function () {

                

                $scope.CorporateSales.CorporateSalesApproval.Comment = $scope.CorporateSales.PreComments + ' ' + $scope.CorporateSales.NewComments;

                $http.post("/Sales/CorporateSales/Pass", {
                    obj: $scope.CorporateSales.CorporateSalesApproval,
                    newComment: $scope.CorporateSales.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.CorporateSales.NewComments = "";
                        }
                        ).error(function (error) {

                        });


             


            };



            $scope.Approve = function () {

                $scope.CorporateSales.CorporateSalesApproval.Comment = $scope.CorporateSales.PreComments + ' ' + $scope.CorporateSales.NewComments;


                $http.post("/Sales/CorporateSales/Approve", {
                    obj: $scope.CorporateSales.CorporateSalesApproval,
                    newComment: $scope.CorporateSales.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.CorporateSales.NewComments = "";
                        }
                        ).error(function (error) {

                        });


            };

            $scope.Reject = function () {

                $scope.CorporateSales.CorporateSalesApproval.Comment = $scope.CorporateSales.PreComments + ' ' + $scope.CorporateSales.NewComments;

                $http.post("/Sales/CorporateSales/Reject", {
                    obj: $scope.CorporateSales.CorporateSalesApproval,
                    newComment: $scope.CorporateSales.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.CorporateSales.NewComments = "";
                        }
                        ).error(function (error) {

                        });

            };


            $scope.DisableApproval = function () {
                var ret = !($scope.CorporateSales.ApprovalStatus !== undefined && ($scope.CorporateSales.ApprovalStatus == 1 || $scope.CorporateSales.ApprovalStatus == 3) && $scope.CorporateSales.NewComments !== "" && $scope.CorporateSales.NewComments !== undefined);
                return ret;
            };



            $scope.cancel = function () {
                init();
                $modalInstance.dismiss('cancel');
            };
        };
















    }]);


}).call(this);



