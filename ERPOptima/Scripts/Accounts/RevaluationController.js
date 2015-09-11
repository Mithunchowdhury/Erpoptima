//start jQuery
$(document).ready(function () {
    //for date picker
    $('.datetimepicker').datetimepicker();

    $("#selAsset").change(function () {
        $("#Acquisitionvalue").val($('#selAsset option:selected').attr('cost'));

    });
    
    //Calculation for revaluation value.
    $("#Presentvalue").change(function () {
        
        var requsitionval = $('#selAsset option:selected').attr('cost');
        var pval = $("#Presentvalue").val(); 
        var rval = pval - requsitionval;
        if (rval < 0) { $("#lossOrProfit").text("Loss"); }
        else { $("#lossOrProfit").text("Profit"); }
        $("#Revalue").val(rval);
    });

    //reset element without ng-model
    $(".reset").click(function () {
        $("#lossOrProfit").text("");
        //$("#Acquisitionvalue").val("");
        //$("#Revalue").val("");
    });
});
//end jQuery


    app.controller('RevaluationController', function ($scope, ngTableParams, $http, Erpmodal) {
        var self = this;
        var RevaluationId = 0;
        $scope.RefNo = "";
        $scope.FxdRevaluation = new Object();
        

        
        //auto genarate code for RefNo
        function GetCode() {
            $http({
                url: '/Accounts/FixedAsset/GetCode',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RefNo = data;
            });
        }//end of GetCode

        //for asset name with aqusation name and aquasition amount for dropdown
        var FxdRvlCombo = function () {
            $http.get("/Accounts/FixedAsset/GetFxdNAcquisition").success(function (data) {
                $scope.FxdNRvlList = data;
            }).error(function (data) {
            });//end of getrequest
        }

        //show Revaluation list in Grid view for Revaluation Entry
        function RevaluationList() {
            //alert('grid');
            $http.post("/Accounts/FixedAsset/RevaluationList").success(function (data) {
                $scope.RevaluationList = data;
            }).error(function (error) {

            });
        };

        var init = function () {
            //alert('i m revalueation init method');
            $scope.Id = 0;
            $scope.ButtonDisabled = true;
            GetCode();
            RevaluationList();
            FxdRvlCombo();

        }//end of init


        init();//init is called

        $scope.Save = function () {
            //alert('i m save method');
            Erpmodal.Confirm('Save').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
                $scope.FxdRevaluation.RefNo = $scope.RefNo;
                //$scope.Revaluation.Date = $scope.Date;
                //$scope.Revaluation.FxdAcquisitionId = $scope.FxdAcquisitionId;
                //$scope.FxdRevaluation.Acquisitionvalue = $scope.Acquisitionvalue;
                //$scope.FxdRevaluation.Revalue = $scope.Revalue;
                //$scope.Revaluation.Remarks = $scope.Remarks;
                
                $http.post('/Accounts/FixedAsset/SaveFxdRevalue', $scope.FxdRevaluation).success(function (data) {
                    Erpmodal.Save(data, "Save");
                    //init();
                    $scope.Reset();
                    $.unblockUI();
                    $scope.ButtonDisabled = true;
                    init();
                    //$route.reload();
                }).error(function (error) {
                    $.unblockUI();
                });

            });

        };//end of Save

        //-----------------Edit row----------------------
        $scope.setForEdit = function (rowitem) {
            state = 1;
            //$scope.Id = rowitem.Id;
            RevaluationId = rowitem.Id;
            $scope.RefNo = rowitem.RefNo;
            $scope.Date = ConverttoDateString(rowitem.Date);
            $scope.FxdAcquisitionId = rowitem.FxdAcquisitionId;
            $scope.Presentvalue = rowitem.Presentvalue;
            $scope.Revalue = rowitem.Revalue;
            $scope.Remarks = rowitem.Remarks;
            $scope.ButtonDisabled = false;
        };

        //------------------------Delete--------------------
        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Deleting...</h1>' });
                var Id = RevaluationId;
                if (RevaluationId != 0) {
                    $http.post('/Accounts/FixedAsset/DeleteRevaluation', { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                        $.unblockUI();
                        $scope.ButtonDisabled = true;
                        init();
                        //$scope.FxdRevaluation = {};
                        //state = 0;
                        //$scope.Reset();
                        //$.unblockUI();
                        //init();
                        //$scope.ButtonDisabled = true;
                        //$scope.reset()
                    }).error(function () {
                        $.unblockUI();
                    })
                }
                else { Erpmodal.Warning("Please select one !"); }
            });
        };//end of Delete

        //reset fixed asset revaluation form
        $scope.Reset = function () {
            //alert('i m reset');
            init();
            state = 0;
            RevaluationId = 0;
            $scope.FxdRevaluation = {};
            $scope.ButtonDisabled = true;
            $scope.WebForm.$setPristine();
            
            //$scope.RefNo = "";
            //$scope.Date = null;
            //$scope.FxdAcquisitionId = "";
            ////$scope.AcquisitionValue = "";
            //$scope.Presentvalue = "";
            //$scope.Revalue = "";
            //$scope.Remarks = "";
            //GetCode();

        }
    });
