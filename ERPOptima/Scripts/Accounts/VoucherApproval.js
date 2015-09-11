app.controller("VoucherApprovalController", function ($scope, $timeout, $modal, $http, Erpmodal) {
    $scope.AccountNameAndCode = new Object();
    $scope.accountName;
    $scope.CostCenters;
    $scope.CmnProjectIds;
    $scope.CmnBusinesses;
    $scope.TotalAmount = 0;
    $scope.accountCode;
    
    $scope.Date;
    var dt = new Date();
    $scope.VoucherId = 0;
    $scope.table = [];
    $scope.VoucherList = [];
    $scope.VoucherDetail = [];
    $scope.Voucher = {};//{ Date: "", Type: 2, CmnBusinessesId: 1, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
    $scope.VoucherComment = {};
    $scope.minDate;
    $scope.maxDate;

    //$scope.Voucher.Date = dt.getDate() + '/' + (dt.getMonth() + 1) + '/' + dt.getFullYear();

    //Get Business
    $scope.GetCmnBusinesses = function () {
        $http({
            url: '/Common/Business/GetCmnBusinessesIdAndName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $timeout(function () {
                
                $scope.CmnBusinesses = data;
                //$scope.Voucher.CmnBusinessesId = 1;
                $scope.$apply();
            }, 0);
        }).error(function (data) {
        });
    }

    //Get Projects
    $scope.ProjectInfo = function () {
        $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.Voucher.CmnBusinessesId).success(function (data) {
            $scope.CmnProjectIds = data;
        }).error(function (data) {
        });
    };
       

    //Fiscal year date check
    //$scope.GetFiscalYearDates = function () {
    //    $http({
    //        url: '/Common/FinancialYear/CheckFinancialYear',
    //        method: 'POST',
    //        headers: { 'Content-Type': 'application/json' }
    //    }).success(function (data) {
    //        $timeout(function () {
    //            $scope.minDate = data.OpeningDate;
    //            $scope.maxDate = data.ClosingDate;

    //            var dec = DateDiff(StringToDate(data.ClosingDate), dt);
    //            if (dec <= 0) {
    //                $scope.Voucher.Date = data.ClosingDate;
    //            }
    //            $scope.$apply();

    //            $("#txtVoucherDate").datetimepicker({
    //                formatDate: 'd/m/Y',
    //                minDate: $scope.minDate,
    //                maxDate: $scope.maxDate
    //            });
    //        }, 0);
    //    }).error(function (data) {
    //    });
    //}

   

    //$scope.GetFiscalYearDates();
    $scope.GetCmnBusinesses();
    

   // $scope.Voucher = { Date: $scope.Voucher.Date, Type: 2, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
    //$scope.Count = 1;
    //$scope.selectedItem = new Object();

    //$scope.search = { voucherNumber: "", fromDate: "", toDate: "", CmnProjects: null, AnfCostCenters: null };

    //$scope.reset = function () {
    //    $scope.table = [];
    //    $scope.form.SubVoucherNumber = "";
    //    $scope.Voucher = { Date: "", Type: 2, CmnBusinessesId: 1, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
    //}

    //New button Event
    $scope.loadData = function () {
        $scope.Voucher = { BusinessId: $scope.Voucher.CmnBusinessesId, ProjectId: $scope.Voucher.CmnProjectId, DateFrom: $scope.Voucher.FromDate, ToDate: $scope.Voucher.ToDate};
       
                    $http({
                        url: "/Accounts/VoucherApproval/BindVoucherList",
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        data: $scope.Voucher
                    }).success(function (data) {
                        $scope.VoucherList = data;
                    }).error(function (data) {
                    });
    };
    $scope.GetVoucherDetails = function (data) {
        
        $scope.VoucherNo = data.VoucherNumber;
        $scope.Date = data.DateString;

        $http.get("/Accounts/VoucherApproval/GetVoucherDetailsByVoucherId?id=" + data.id).success(function (details) {
           
            $scope.VoucherId = data.id;
            $scope.VoucherDetail = details;

            $scope.GetApprovalComment(data.id)

        }).error(function (data) {
        });   

    };

    $scope.GetApprovalComment = function (Id) {       
        $http.get("/Accounts/VoucherApproval/GetApprovalComment?id=" +Id).success(function (datas) {         
           
            var comment = "";
            for (var i = 0; i < datas.length; i++) {
                comment += datas[i].Comments + '\n';
            }
            $scope.PreviouComments = comment;

        }).error(function (datas) {
        });


    };
   
   // Approval save
    $scope.SaveApproval = function () {
        Erpmodal.Confirm('Save').then(function (result) {
           
            if ($scope.Voucher.Naration == "") {
                Erpmodal.Warning("Please enter naration!");
            } else {
                
                $scope.VoucherComment = { Id: $scope.VoucherId, Naration: $scope.Voucher.Naration };
               
                $http({
                    url: '/Accounts/VoucherApproval/SaveApprovalComment',
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    data: $scope.VoucherComment
                }).success(function (data) {                             

                    Erpmodal.Save(data, "Save");
                }).error(function (data) {
                });
            }
        })
    }


    // Approval Comments
    $scope.Comments = function () {
        Erpmodal.Confirm('Save').then(function (result) {

            if ($scope.Voucher.Naration == "") {
                Erpmodal.Warning("Please enter naration!");
            } else {
                
                $scope.VoucherComment = { Id: $scope.VoucherId, Naration: $scope.Voucher.Naration };

                $http({
                    url: '/Accounts/VoucherApproval/Comments',
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    data: $scope.VoucherComment
                }).success(function (data) {

                    Erpmodal.Save(data, "Save");
                }).error(function (data) {
                });
            }
        })
    }

    // Approval Reject
    $scope.Reject = function () {
        Erpmodal.Confirm('Save').then(function (result) {

            if ($scope.Voucher.Naration == "") {
                Erpmodal.Warning("Please enter naration!");
            } else {
                
                $scope.VoucherComment = { Id: $scope.VoucherId, Naration: $scope.Voucher.Naration };

                $http({
                    url: '/Accounts/VoucherApproval/Reject',
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    data: $scope.VoucherComment
                }).success(function (data) {

                    Erpmodal.Save(data, "Save");
                }).error(function (data) {
                });
            }
        })
    }
  
   
});