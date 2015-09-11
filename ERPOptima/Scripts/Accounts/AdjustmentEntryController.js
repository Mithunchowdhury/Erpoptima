//jQuery
$(document).ready(function () { $('.dpkr').datetimepicker(); });
//end jQuery

app.controller('AdjustmentEntryController', function ($http, $scope, $location, ngTableParams, Erpmodal) {
        var self = this;
        var anFAdjustmentId;
        $scope.AnFAdjustment = new Object();
        $scope.SumbitShow = true;
        $scope.advances = [];

        //for dropdown Advance Ref no
        function AdvanceRefNoCombo() {
            $http({
                url: '/Accounts/Advance/GetTransactionalHeadByCompanyId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.advances = data;
            });
        }
        //show Adjustment list in Grid view for Adjustment Entry
        function showAdjustmentList() {
            $http.post("/Accounts/Advance/AdjustmentGridView").success(function (data) {
                $scope.rows = data;
            }).error(function (error) {
               
            });
        };

        var init = function () {
            $scope.Id = 0;
            $scope.ButtonDisabled = true;
            showAdjustmentList();
            AdvanceRefNoCombo();
        };//end of init
        
        init();//init is called

        //sow adjustment List select a reference nong from dropdown
        $scope.showList = function () {
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
            $http.post("/Accounts/Advance/AdjustmentListSearch", { AnFAdvanceId: $scope.RefNo }).success(function (data) {
                $scope.AdjustmentLists = data;
                $.unblockUI();
            }).error(function (error) {
                $.unblockUI();
            });
        };

        //for save of edit Adjustment
        $scope.Save = function () {
                Erpmodal.Confirm('Save').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
                $scope.AnFAdjustment.Id = $scope.Id;
                $scope.AnFAdjustment.AnFAdvanceId = $scope.AnFAdvanceId;
                $scope.AnFAdjustment.Date = $scope.Date;
                $scope.AnFAdjustment.AdjustedAmount = $scope.AdjustedAmount;
                $http.post('/Accounts/Advance/SaveAdjustment', $scope.AnFAdjustment).success(function (data) {
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

    //-----------------Edit row----------------------
        $scope.setForEdit = function (rowitem) {
            state = 1;
            $scope.Id = rowitem.Id;
            $scope.AnFAdvanceId = rowitem.AnFAdvanceId;
            $scope.Date = ConverttoDateString(rowitem.Date);
            $scope.AdjustedAmount = rowitem.AdjustedAmount;
            $scope.ButtonDisabled = false;
        };
            
        //--------------Delete--------------------
        $scope.Delete = function () {
                Erpmodal.Confirm('Delete').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Deleting...</h1>' });
                var Id = anFAdjustmentId;
                $scope.AnFAdjustment.Id = $scope.Id;
                $scope.AnFAdjustment.AnFAdvanceId = $scope.AnFAdvanceId;
                $scope.AnFAdjustment.Date = $scope.Date;
                $scope.AnFAdjustment.AdjustedAmount = $scope.AdjustedAmount;
                if (anFAdjustmentId != 0) {
                    $http.post('/Accounts/Advance/DeleteAdjustment', $scope.AnFAdjustment).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.AnFAdjustment = {};
                        state = 0;
                        $scope.Reset();
                        $.unblockUI();
                        init();
                        $scope.ButtonDisabled = true;
                    }).error(function () {
                        $.unblockUI();
                    })
                }
                else { Erpmodal.Warning("Please select one !"); }
            });
        };//end of Delete

        //for reset
        $scope.Reset = function () {
            $scope.ButtonDisabled = true;
            $scope.AnFAdvanceId = "";
            $scope.Date = null;
            $scope.AdjustedAmount = "";
        };
    });
