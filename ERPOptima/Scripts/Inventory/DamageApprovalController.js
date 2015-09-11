(function () {

    app.controller('DamageApprovalController', ['$scope', '$http', '$modal', 'Erpmodal', 'glbapproval', function DamageApprovalController($scope, $http, $modal, Erpmodal, glbapproval) {
        var localAppId = 0;
        $scope.ApprovalStatusData = [];

        var init = function () {
            
            localAppId = glbapproval.appID;
            
            $scope.ResetFind();
            $scope.ResetResult();
            $scope.ResetForm();

            //fetch all items of sales order and load in grid
            $http({
                url: '/Inventory/Damage/GetAllForApproval',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Resources = data;
            });

            if (localAppId !== undefined && localAppId > 0) {
                $scope.GetAppApprovalById(localAppId);
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
            $scope.AppFind = new Object();
        };

        $scope.ResetResult = function () {
            $scope.Resources = new Object();
        };

        $scope.ResetForm = function () {
            $scope.AppApproval = new Object();
        };


        $scope.Reset = function () {
            $scope.DamageApproval.$setPristine();
            init();

            //$scope.ResetFind();
            ////$scope.ResetResult();
            //$scope.ResetForm();
        };

        $scope.GetAppApproval = function (record) {
            var id = record.Id;
            $scope.GetAppApprovalById(id);
        };

        $scope.GetAppApprovalById = function (res) {
             
            $scope.ResetForm();
            $http({
                url: '/Inventory/Damage/GetApprovalById',
                method: 'GET',
                params: { appId: res.Id },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.AppApproval = data;
                $scope.AppApproval.CreatedDate = ConverttoDateString(data.CreatedDate);
                if (data.ModifiedDate !== null) {
                    $scope.AppApproval.ModifiedDate = ConverttoDateString(data.ModifiedDate);
                }
                $scope.GetDamageDetail(res);
            });
        };

        //$scope.Approve = function () {
        //    Erpmodal.Confirm('Save').then(function (result) {
        //        $scope.AppApproval.Comment = $scope.AppApproval.Comment + ' ' + $scope.AppApproval.NewComment;

        //        $http.post("/Inventory/Damage/Approve", {
        //            obj: $scope.AppApproval,
        //            newComment: $scope.AppApproval.NewComment
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
        //        $scope.AppApproval.Comment = $scope.AppApproval.Comment + ' ' + $scope.AppApproval.NewComment;

        //        $http.post("/Inventory/Damage/Reject", {
        //            obj: $scope.AppApproval,
        //            newComment: $scope.AppApproval.NewComment
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
                 
        //        $scope.AppApproval.Comment = $scope.AppApproval.Comment + ' ' + $scope.AppApproval.NewComment;

        //        $http.post("/Inventory/Damage/Pass", {
        //            obj: $scope.AppApproval,
        //            newComment: $scope.AppApproval.NewComment
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

        $scope.GetDamageDetail = function (res) {
            $http({
                url: '/Inventory/Damage/GetByDamageId?damageId=' + res.Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.DamageDetails = data;
                $scope.OpenPopup(res);
            }).error(function (data) {
            });


        };//end of GetDamageDetail

        //open Modal
        $scope.Damages = { DamageId: 0, RefNo: "", DamageDate: "", DamageDetails: $scope.DamageDetails, PreComments: "", NewComments: "", DamageApproval: $scope.AppApproval, ApprovalStatus: 0 };

        $scope.OpenPopup = function (damage, size) {
            var modalInstance = $modal.open({
                templateUrl: 'modal',
                controller: ModalInstanceCtrl,
                size: size,
                resolve: {
                    Damages: function () {
                        $scope.Damages.DamageId = damage.Id;
                        $scope.Damages.RefNo = damage.RefNo;
                        $scope.Damages.DamageDate = ConverttoDateString(damage.CreatedDate);
                        $scope.Damages.DamageDetails = $scope.DamageDetails;
                        $scope.Damages.PreComments = $scope.AppApproval.Comment;
                        $scope.Damages.DamageApproval = $scope.AppApproval;
                        $scope.Damages.ApprovalStatus = damage.ApprovalStatus;
                        return $scope.Damages;
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


        var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, Damages) {
            $scope.Damages = Damages;
            //$scope.ok = function () {
            //    $modalInstance.close($scope.Reqs);
            //    //$modalInstance.open($scope.Reqs);
            //};

            $scope.Pass = function () {

                //if (confirm('Do you want to continue')) {


                $scope.Damages.DamageApproval.Comment = $scope.Damages.PreComments + ' ' + $scope.Damages.NewComments;

                $http.post("/Inventory/Damage/Pass", {
                    obj: $scope.Damages.DamageApproval,
                    newComment: $scope.Damages.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.Damages.NewComments = "";
                        }
                        ).error(function (error) {

                        });


                //}


            };

            

            $scope.Approve = function () {

                $scope.Damages.DamageApproval.Comment = $scope.Damages.PreComments + ' ' + $scope.Damages.NewComments;


                $http.post("/Inventory/Damage/Approve", {
                    obj: $scope.Damages.DamageApproval,
                    newComment: $scope.Damages.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.Damages.NewComments = "";
                        }
                        ).error(function (error) {

                        });


            };

            $scope.Reject = function () {

                $scope.Damages.DamageApproval.Comment = $scope.Damages.PreComments + ' ' + $scope.Damages.NewComments;

                $http.post("/Inventory/Damage/Reject", {
                    obj: $scope.Damages.DamageApproval,
                    newComment: $scope.Damages.NewComments
                }).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            init();
                            $scope.Damages.NewComments = "";
                        }
                        ).error(function (error) {

                        });

            };


            $scope.DisableApproval = function () {
                var ret = !($scope.Damages.ApprovalStatus !== undefined && ($scope.Damages.ApprovalStatus == 1 || $scope.Damages.ApprovalStatus == 3) && $scope.Damages.NewComments !== "" && $scope.Damages.NewComments !== undefined);
                return ret;
            };



            $scope.cancel = function () {
                init();
                $modalInstance.dismiss('cancel');
            };
        };
























    }]);


}).call(this);



