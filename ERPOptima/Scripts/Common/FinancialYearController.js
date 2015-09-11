


(function () {
    app.controller("FinancialYear", function ($scope, $timeout, ngTableParams, $modal, $http, Erpmodal) {
        var state;
        var datas = [];
        var FinancialYearId = 0;
        $scope.FinancialYear = new Object();
        //$scope.SecModules = new Object();
        $scope.FinancialYear.Status = true;
        $scope.SecModuleId = 2; 

        $scope.SaveDisable;

     
        // ------------get view Data------------
        var init = function () {
            $scope.ButtonDisabled = true;
            $scope.LoadFinancialYearInfo();  // To Fill Grid
          

        };  //End of init()

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10           // count per page
        }, {
            total: datas.length, // length of data
            getData: function ($defer, params) {
                $defer.resolve(datas.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });
        
        // To Fill Grid
        $scope.LoadFinancialYearInfo = function () {
            if ($scope.SecModuleId != -1) {
                $http.get("/Common/FinancialYear/GetAll").success(function (data) {
                }).then(function (data) {

                    for (var i = 0; i < data.data.length; i++) {
                        data.data[i].OpeningDate = ConverttoDateString(data.data[i].OpeningDate);
                        data.data[i].ClosingDate = ConverttoDateString(data.data[i].ClosingDate);
                    }
                    var data = data.data;

                    $timeout(function () {
                        datas = data;
                        $scope.tableParams.reload();
                        $scope.$apply();
                    }, 0);
                });
            }
            else {
                datas = [];
                $scope.tableParams.reload();
                $scope.open();
            }
        }

        $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
        init();  //call of init()
        $.unblockUI();

        //------------edit row------------
        $scope.setFortEdit = function (index) {
            $scope.SaveDisable = false;
            var pid = datas[index];

            $scope.mode = true;
            FinancialYearId = pid.Id;
            $scope.FinancialYear.Id = pid.Id;
            $scope.FinancialYear.Name = pid.Name;
            $scope.FinancialYear.OpeningDate = pid.OpeningDate;
            $scope.FinancialYear.ClosingDate = pid.ClosingDate;
            $scope.SecModuleId = pid.SecModuleId;
            $scope.FinancialYear.Status = pid.Status;
            $scope.FinancialYear.CreatedBy = pid.CreatedBy;
            $scope.FinancialYear.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
        }

        //------------save------------
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
               // debugger;
                $scope.FinancialYear.Id = FinancialYearId;
                $scope.FinancialYear.SecModuleId = $scope.SecModuleId
                $http.post("/Common/FinancialYear/Save", $scope.FinancialYear).success(

                    function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.LoadFinancialYearInfo();
                        $scope.tableParams.reload();
                        FinancialYearId = 0;
                        $scope.FinancialYear = {};
                        $scope.ButtonDisabled = true;
                        $scope.FinancialYear.Status = true;
                        $.unblockUI();
                        $scope.Reset();

                    }
                    ).error(function (error) {
                        $.unblockUI();
                    });
            });
        }; //End of Save

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
                var Id = FinancialYearId;
                if (FinancialYearId != 0) {
                    $http.post("/Common/FinancialYear/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.LoadFinancialYearInfo();
                        FinancialYearId = 0;
                        $scope.FinancialYear = {};
                        state = 0;
                        $.unblockUI();
                        $scope.ButtonDisabled = true;
                        $scope.Reset();
                    }).error(function () {
                        $.unblockUI();
                    })
                }
                else { Erpmodal.Warning("Please select a FY !"); }
            });
        };  //End of Delete

        //----------------------------Reset------------------------------
        $scope.Reset = function () {
            $scope.FinancialYear = {};
            $scope.FinancialYear.Status = true;
            FinancialYearId = 0;
            $scope.FinancialYears.$setPristine();   // $scope.FormName.$setPristine()
         
        }; //end of Reset

        //open Modal    
        //$scope.Module = { Id: 0, Name: "", Remaks: "" };

        //$scope.open = function (size) {
        //    var modalInstance = $modal.open({
        //        templateUrl: 'modal',
        //        controller: ModalInstanceCtrl,
        //        size: size,
        //        resolve: {
        //            Module: function () {
        //                return $scope.Module;
        //            }
        //        }
        //    });
        //    modalInstance.result.then(function () {
        //        $http.post("/Security/Module/SaveModule", $scope.Module).success(
        //            function (data) {
        //                $scope.LoadModuleInfo(data.OperationId);
        //                $scope.Module = {};
        //            }
        //            ).error(function (error) {
        //            });

        //    }, function () {
        //    });

        //    modalInstance.opened.then(function () {
        //    }, function () {
        //    });




        //};


        //var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, Module) {
        //    $scope.Module = Module;
        //    $scope.ok = function () {
        //        $modalInstance.close($scope.Module);
        //    };

        //    $scope.cancel = function () {
        //        init();
        //        $modalInstance.dismiss('cancel');
        //    };
        //};



    });

}).call(this);
