(function () {

    app.controller('RoutePlanApprovalController', ['$scope', '$http', '$modal', 'Erpmodal', 'glbApprovalObj', function OrderApprovalController($scope, $http,$modal, Erpmodal, glbApprovalObj) {
        var localApprovalId = 0;
        $scope.ApprovalStatusData = [];

        $scope.Weeks = new Object();

        var init = function () {
             
            $http({
                url: '/Sales/RouteSetup/GetAllWeeks',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Weeks = data;
            });


            localApprovalId = glbApprovalObj.appID;

             
            $scope.ResetFind();
            $scope.ResetResult();
            $scope.ResetForm();

            //fetch all items of route plan and load in grid
            $http({
                url: '/Sales/RouteSetup/GetAllForApproval',
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

            //Finding route plan by date range of preferred delivery date
            $http({
                url: '/Sales/RouteSetup/GetAllForApprovalByDate',
                method: 'GET',
                params: { fromDate: $scope.ApprovalSearchObj.FromDate, toDate: $scope.ApprovalSearchObj.ToDate },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });
        };

        $scope.Reset = function () {
            $scope.FindApprvlForm.$setPristine();

            init();

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
                url: '/Sales/RouteSetup/GetApprovalById',
                method: 'GET',
                params: { id: res.Id },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.ApprovalObj = data;
                $scope.ApprovalObj.CreatedDate = ConverttoDateString(data.CreatedDate);
                if (data.ModifiedDate !== null) {
                    $scope.ApprovalObj.ModifiedDate = ConverttoDateString(data.ModifiedDate);
                }

                $scope.GetRoutePlanDetail(res);
            });
        };

        //$scope.Approve = function () {
        //    Erpmodal.Confirm('Save').then(function (result) {
        //        $scope.ApprovalObj.Comment = $scope.ApprovalObj.Comment + ' ' + $scope.NewComment;

        //        $http.post("/Sales/RouteSetup/Approve", {
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

        //        $http.post("/Sales/RouteSetup/Reject", {
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

        //        $http.post("/Sales/RouteSetup/Pass", {
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
        }

        $scope.DisableApproval = function (SelectedApproval) {
             
            return !(SelectedApproval !== undefined && (SelectedApproval.ApprovalStatus == 1 || SelectedApproval.ApprovalStatus == 3));
        };

        $scope.GetRoutePlanDetail = function (res) {
            $http({
                url: '/Sales/RouteSetup/GetByRoutePlanId?RoutePlanId=' + res.Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].Date = ConverttoDateString(data[i].Date);
                }
                $scope.RoutePlanDetails = data;
                $scope.OpenPopup(res);
            }).error(function (data) {
            });


        };//end of GetRoutePlanDetail

        //open Modal
        $scope.RoutePlans = { RoutePlanId: 0, Week: "", Title: "", Date: "", RoutePlanDetails: $scope.RoutePlanDetails, PreComments: "", NewComments: "", RoutePlaneApproval: $scope.ApprovalObj, ApprovalStatus: 0 };

        $scope.OpenPopup = function (routePlan, size) {
            var modalInstance = $modal.open({
                templateUrl: 'modal',
                controller: ModalInstanceCtrl,
                size: size,
                resolve: {
                    RoutePlans: function () {
                        $scope.RoutePlans.RoutePlanId = routePlan.Id;
                        $scope.RoutePlans.Week = "Week No " + routePlan.WeekNo + " : " + ConverttoDateString(routePlan.DateFrom) + " to " + ConverttoDateString(routePlan.DateTo);
                        $scope.RoutePlans.Title = routePlan.Title;
                        $scope.RoutePlans.Date = ConverttoDateString(routePlan.CreatedDate);
                        $scope.RoutePlans.RoutePlanDetails = $scope.RoutePlanDetails;
                        $scope.RoutePlans.PreComments = $scope.ApprovalObj.Comment;
                        $scope.RoutePlans.RoutePlaneApproval = $scope.ApprovalObj;
                        $scope.RoutePlans.ApprovalStatus = routePlan.ApprovalStatus;
                        return $scope.RoutePlans;
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


        var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, RoutePlans) {
            $scope.RoutePlans = RoutePlans;
            //$scope.ok = function () {
            //    $modalInstance.close($scope.Reqs);
            //    //$modalInstance.open($scope.Reqs);
            //};

            $scope.Pass = function () {              

                $scope.RoutePlans.RoutePlaneApproval.Comment = $scope.RoutePlans.PreComments + ' ' + $scope.RoutePlans.NewComments;

                $http.post("/Sales/RouteSetup/Pass", {
                    obj: $scope.RoutePlans.RoutePlaneApproval,
                    newComment: $scope.RoutePlans.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.RoutePlans.NewComments = "";
                        }
                        ).error(function (error) {

                        });               


            };



            $scope.Approve = function () {

                $scope.RoutePlans.RoutePlaneApproval.Comment = $scope.RoutePlans.PreComments + ' ' + $scope.RoutePlans.NewComments;


                $http.post("/Sales/RouteSetup/Approve", {
                    obj: $scope.RoutePlans.RoutePlaneApproval,
                    newComment: $scope.RoutePlans.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.RoutePlans.NewComments = "";
                        }
                        ).error(function (error) {

                        });


            };

            $scope.Reject = function () {

                $scope.RoutePlans.RoutePlaneApproval.Comment = $scope.RoutePlans.PreComments + ' ' + $scope.RoutePlans.NewComments;

                $http.post("/Sales/RouteSetup/Reject", {
                    obj: $scope.RoutePlans.RoutePlaneApproval,
                    newComment: $scope.RoutePlans.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.RoutePlans.NewComments = "";
                        }
                        ).error(function (error) {

                        });

            };

            $scope.DisableApproval = function () {
                var ret = !($scope.RoutePlans.ApprovalStatus !== undefined && ($scope.RoutePlans.ApprovalStatus == 1 || $scope.RoutePlans.ApprovalStatus == 3) && $scope.RoutePlans.NewComments !== "" && $scope.RoutePlans.NewComments !== undefined);
                return ret;
            };




            $scope.cancel = function () {
                init();
                $modalInstance.dismiss('cancel');
            };
        };


















    }]);


}).call(this);



