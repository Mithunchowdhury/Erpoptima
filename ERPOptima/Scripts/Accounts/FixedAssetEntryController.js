(function () {
    app.controller('FixedAssetEntry', function ($scope, ngTableParams, $http, Erpmodal) {
        var state;
        $scope.fixedAssetEntry = new Object();
        $scope.FixedAcquisitions = [];
        $scope.FixedAssests = [];
        $scope.Units = [];
        $scope.Employee = [];

        var init = function () {
            $scope.Id = 0;
            $scope.ButtonDisabled = true;
            state = 0;

            $http({
                url: '/Accounts/FixedAsset/GetAllAcquisition',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.FixedAcquisitions = data;               
            });

            $http({
                url: '/Accounts/FixedAsset/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.FixedAssests = data;
            });

            $http({
                url: '/Sales/UnitOfMeasurement/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Units = data;
            });

            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employee = data;
            });

        }//end of init


        init();//init is called

        $scope.setFortEdit = function (pid) {
            state = 1;
            fixedAssetEntryId = pid.Id;
            $scope.fixedAssetEntry.Code = pid.Code;
            $scope.fixedAssetEntry.FxdAssetId = pid.FxdAssetId;
            $scope.fixedAssetEntry.Name = pid.Name;
            $scope.fixedAssetEntry.ModelOrBatchNo = pid.ModelOrBatchNo;
            $scope.fixedAssetEntry.SerialNo = pid.SerialNo;
            $scope.fixedAssetEntry.SlsUnitId = pid.SlsUnitId;
            $scope.fixedAssetEntry.Quantity = pid.Quantity;
            $scope.fixedAssetEntry.DepreciationRate = pid.DepreciationRate;
            $scope.fixedAssetEntry.DepreciationMethod = pid.DepreciationMethod;
            $scope.fixedAssetEntry.AcquisitionCost = pid.AcquisitionCost;
            $scope.fixedAssetEntry.LifeTime = pid.LifeTime;
            $scope.fixedAssetEntry.Manufacturer = pid.Manufacturer;
            $scope.fixedAssetEntry.Supplier = pid.Supplier;
            $scope.fixedAssetEntry.Location = pid.Location;
            $scope.fixedAssetEntry.Date = pid.Date;
            $scope.fixedAssetEntry.DrepreciationStartDate = pid.DrepreciationStartDate;
            $scope.fixedAssetEntry.DepresiationEndDate = pid.DepresiationEndDate;
            $scope.fixedAssetEntry.WarrantyExpreDate = pid.WarrantyExpreDate;
            $scope.fixedAssetEntry.Description = pid.Description;
            $scope.fixedAssetEntry.Remarks = pid.Remarks;
            $scope.fixedAssetEntry.Status = pid.Status;
            $scope.ButtonDisabled = false;
        }

        $scope.Reset = function () {
            init();
            state = 0;
            fixedAssetEntryId = 0;
            $scope.fixedAssetEntry = {};
            $scope.ButtonDisabled = true;
            $scope.fxdEntry.$setPristine();
        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
            $scope.ButtonDisabled = true;
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });

            $scope.fixedAssetEntry.Id = $scope.Id;
            $scope.fixedAssetEntry.Code = $scope.Code;
            $scope.fixedAssetEntry.FxdAssetId = $scope.FxdAssetId;
            $scope.fixedAssetEntry.Name = $scope.Name;
            $scope.fixedAssetEntry.ModelOrBatchNo = $scope.ModelOrBatchNo;
            $scope.fixedAssetEntry.SerialNo = $scope.SerialNo;
            $scope.fixedAssetEntry.SlsUnitId = $scope.SlsUnitId;
            $scope.fixedAssetEntry.Quantity = $scope.Quantity;
            $scope.fixedAssetEntry.DepreciationRate = $scope.DepreciationRate;
            $scope.fixedAssetEntry.DepreciationMethod = $scope.DepreciationMethod;
            $scope.fixedAssetEntry.AcquisitionCost = $scope.AcquisitionCost;
            $scope.fixedAssetEntry.LifeTime = $scope.LifeTime;
            $scope.fixedAssetEntry.Manufacturer = $scope.Manufacturer;
            $scope.fixedAssetEntry.Supplier = $scope.Supplier;
            $scope.fixedAssetEntry.Location = $scope.Location;
            $scope.fixedAssetEntry.Date = $scope.Date;
            $scope.fixedAssetEntry.DrepreciationStartDate = $scope.DrepreciationStartDate;
            $scope.fixedAssetEntry.DepresiationEndDate = $scope.DepresiationEndDate;
            $scope.fixedAssetEntry.WarrantyExpreDate = $scope.WarrantyExpreDate;
            $scope.fixedAssetEntry.Description = $scope.Description;
            $scope.fixedAssetEntry.Remarks = $scope.Remarks;
            $scope.fixedAssetEntry.Status = $scope.Status;

                $http.post('/Accounts/FixedAsset/SaveAcquisition', $scope.fixedAssetEntry).success(function (data) {
                Erpmodal.Save(data, "Save");
                init();
                $scope.Reset();
                $.unblockUI();
                $scope.ButtonDisabled = true;
            }).error(function (error) {
                $.unblockUI();
            });

            });

        };//end of Save

        //------------------------Delete--------------------
        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
                var Id = fixedAssetEntryId;
                if (fixedAssetEntryId != 0) {
                    $http.post('/Accounts/FixedAsset/Delete', { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.fixedAssetEntry = {};
                        state = 0;
                        $.unblockUI();
                        init();
                        $scope.ButtonDisabled = true;
                        //$scope.reset()
                    }).error(function () {
                        $.unblockUI();
                    })
                }
                else { Erpmodal.Warning("Please select one !"); }
            });
        };//end of Delete
    });
}).call(this);