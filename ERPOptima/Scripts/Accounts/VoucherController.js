//The file contains old logic, commented lines are not planned to remove now.

(function () {

    app.controller('VoucherController', ['$scope', '$http', '$modal', 'Erpmodal', 'vouchertype', function ($scope, $http, $modal, Erpmodal, vouchertype) {
        $scope.genVoucherNO = false;
        $scope.form = { AnFChartOfAccountId: "", AcCode: "", AnfCostCenterId: "", ShortNaration: "", SubVoucher: "", Debit: 0, Credit: 0 };
        var voucherTypeInt = 0;
        var financialYearId = 0;
        var companyId = 0;
        var officeId = 0;
        var dt = new Date();
        
        //For detail Calculate: VoucherSerial [done], SubVoucherNumber.
        //For Main Calculate: VoucherNumber, TotalAmount (total of debit) [done]
        //Validation on Detail: [Sum of all debits and credits should be equal] on Main Save check this. [done]
        //

        var init = function () {
            debugger;
            financialYearId = vouchertype.financialYearId;
            companyId = vouchertype.companyId;
            officeId = vouchertype.officeId;
            voucherTypeInt = GetIntValOfType(vouchertype.typeStr);
            $scope.TypeOfVoucher = voucherTypeInt;
            $scope.AccountNameAndCode = new Object();
            $scope.table = [];
            $scope.CostCenters = [];
            $scope.VoucherDetail = new Object();
            $scope.Voucher = {
                CmnCompanyId: companyId, Type: voucherTypeInt, VoucherNumber: "", Date: "", CmnFinancialYearId: financialYearId,
                TotalAmount: 0.00, Naration: "", SlsOfficeId: officeId, AnFVoucherDetails: $scope.table };

            $scope.GetFiscalYearDates();
            //X/$scope.GetCmnBusinesses();            
            $scope.GetTransactionalHead();
            $scope.GetAnFCostCenters();
        };        

        var GetIntValOfType = function(typeStr){
            debugger;
            var typeInt = 0;
            //1-CR,2-BR,3-CP,4-BP,5-JV,6-CT
            switch(typeStr) {
                case "CR":
                    typeInt=1;
                    break;
                case "BR":
                    typeInt=2;
                    break;
                case "CP":
                    typeInt3;
                    break;
                case "BP":
                    typeInt=4;
                    break;
                case "JV":
                    typeInt=5;
                    break;
                case "CT":
                    typeInt=6;
                    break;
                default:
                    typeInt = 0;
                    break;
            }
            return typeInt;
        };

        $scope.loadData = function (voucherDate) {
            if ($scope.Voucher.Date != "") {
                $scope.GetVoucherNo();
            } else {
                Erpmodal.Warning("Date can't be empty !!");
            }
        };
                 
        //Fiscal year date check
        $scope.GetFiscalYearDates = function () {
            $http({
                url: '/Common/FinancialYear/CheckFinancialYear',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                debugger;
                $scope.minDate = data.OpeningDate;
                $scope.maxDate = data.ClosingDate;
                if ($scope.minDate == "" && $scope.maxDate == "") {
                    $("#txtVoucherDate").hide();                    
                }
                else
                {
                    $("#txtVoucherDate").datetimepicker({
                        formatDate: 'd/m/Y',
                        minDate: $scope.minDate,
                        maxDate: $scope.maxDate
                    });

                    var dec = DateDiff(StringToDate(data.ClosingDate), dt);
                    if (dec <= 0) {
                        $scope.Voucher.Date = data.ClosingDate;
                    }                                      
                }
            }).error(function (data) {
            });
        };
                
        $scope.GetVoucherNo = function () {

            $http({
                url: '/Accounts/Voucher/GetVoucherNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                data: { Type: 1, Prefix: "PV", CmnCompanyId: $scope.Voucher.CmnCompanyId, Date: $scope.Voucher.Date }
            }).success(function (data) {
                $scope.genVoucherNO = true;
                $scope.Voucher.VoucherNumber = data;   
                
                //                $scope.genVoucherNO = true;
                //                $scope.Voucher.VoucherNumber = data;
                //                $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + $scope.Count;
                //                $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + parseInt($scope.table.length + 1);
                
            }).error(function (data) {
            });
        };

        //Get Cost Centers
        $scope.GetAnFCostCenters = function () {
            $http({
                url: '/Accounts/CostCenter/GetCostCenters',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {                    
                $scope.CostCenters = data;                        
            }).error(function (data) {
            });
        };

        //Get Transactional Heads
        $scope.GetTransactionalHead = function () {
            $http({
                url: '/Accounts/Voucher/GetAnfTransactionalHeadsByCompanyId',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {                    
                $scope.AccountNameAndCode = data;                  
            }).error(function (data) {
            });
        };
            //Setting Code based on transactional Head
        $scope.SetTransactionalHeadCode = function () {
            for (var i = 0; i < $scope.AccountNameAndCode.length; i++) {
                if ($scope.AccountNameAndCode[i].Id == $scope.VoucherDetail.AnFChartOfAccountId) {
                    $scope.VoucherDetail.AcCode = $scope.AccountNameAndCode[i].code;
                    break;
                }
            }
        };

            //add voucher details
        $scope.AddDetail = function () {

            var debit = 0;
            var credit = 0;
            var costCenterName = "";
            var accountParticularName = "";
            var accountParticularCode = "";
            var chartOfAccountId = 0;
            var shortNarrat = "";
            if ($scope.VoucherDetail.AnFChartOfAccountId !== undefined && $scope.VoucherDetail.AnFChartOfAccountId != "")
            {
                chartOfAccountId = $scope.VoucherDetail.AnFChartOfAccountId;
            }
            if ($scope.VoucherDetail.Debit !== undefined && $scope.VoucherDetail.Debit != "")
            {
                debit = $scope.VoucherDetail.Debit;
            }
            if ($scope.VoucherDetail.Credit !== undefined && $scope.VoucherDetail.Credit != "") {
                credit = $scope.VoucherDetail.Credit;
            }
            if ($scope.VoucherDetail.AcCode !== undefined && $scope.VoucherDetail.AcCode != "") {
                accountParticularCode = $scope.VoucherDetail.AcCode;
            }
            if ($scope.VoucherDetail.ShortNarration !== undefined && $scope.VoucherDetail.ShortNarration != "") {
                shortNarrat = $scope.VoucherDetail.ShortNarration;
            }

            if (debit != 0 || credit != 0) {
                if ($scope.Voucher.Date != "" && $scope.Voucher.Date != null) {
                    
                    $http({
                        url: '/Common/FinancialYear/IsMonthLocked',
                        method: 'GET',
                        headers: { 'Content-Type': 'application/json' },
                        params: { VoucherDate: $scope.Voucher.Date }
                    }).success(function (data) {
                        //If Month is not locked
                        if (!data.result) {
                            //add detail item to list
                            for (var i = 0; i < $scope.AccountNameAndCode.length; i++) {
                                if ($scope.AccountNameAndCode[i].Id == $scope.VoucherDetail.AnFChartOfAccountId) {
                                    accountParticularName = $scope.AccountNameAndCode[i].Particular;
                                    break;
                                }
                            }
                            for (var i = 0; i < $scope.CostCenters.length; i++) {
                                if ($scope.CostCenters[i].Id == $scope.Voucher.AnFCostCenterId) {
                                    costCenterName = $scope.CostCenters[i].Name;
                                    break;
                                }
                            }

                            $scope.table.push({
                                VoucherSerial: $scope.table.length + 1, CostCenterName: costCenterName,
                                AnFChartOfAcountName: accountParticularName, AcCode: accountParticularCode, Debit: debit, Credit: credit,
                                AnFChartOfAccountId: chartOfAccountId, ShortNarration: shortNarrat
                            });
                        }
                        else Erpmodal.Warning("Month is locked !!");

                    }).error(function (data) {
                        
                    });
                                                    
                } else {                    
                    Erpmodal.Warning("Date can't be empty !!");
                }
            } else {
                Erpmodal.Warning("Data missing!");
            }
        };

            //Calculate Debit
        $scope.DebitCal = function () {
            var dbt = 0;
            angular.forEach($scope.table, function (acc, index) {
                dbt += acc.Debit;
            });
            //$scope.TotalAmount = dbt;            
            return dbt;
        };
            //Calculate Credit
        $scope.CreditCal = function () {
            var dbt = 0;
            angular.forEach($scope.table, function (acc, index) {
                dbt += acc.Credit;
            });

            return dbt;
        };

            //Save Voucher
        $scope.SaveVouchers = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if ($scope.table.length > 0) {
                    if ($scope.DebitCal() != $scope.CreditCal()) {
                                                Erpmodal.Warning("Total Debit And Total Credit must be same!");
                    } else {
                        debugger;
                        $scope.Voucher.TotalAmount = $scope.DebitCal();
                        $scope.Voucher.AnFVoucherDetails = $scope.table;                        

                        $http.post("/Accounts/Voucher/SaveVoucher", $scope.Voucher).success(
                       function (data) {
                           Erpmodal.Save(data, "Save");
                           if (data.Success) {
                               Erpmodal.Info("Saved....");
                               //clear
                               //$scope.VoucherNo = "";
                               //$scope.table = [];
                               //$scope.form.SubVoucherNumber = "";
                               //$scope.Voucher = { Date: "", Type: 1, CmnBusinessesId: 1, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
                               //$scope.GetFiscalYearDates();
                               //$.unblockUI();
                               //$scope.Id = 0;
                               //Erpmodal.Info("Voucher no. is " + data.OperationId);

                           }
                       }
                       ).error(function (error) {

                       });
                       
                        //End of debit credit validation
                    }
                }
                
            })
        };


        init();
    }]);
}).call(this);

    //app.controller("paymentVoucherController12333", function ($scope, $timeout, $modal, $http, Erpmodal) {
    //    $scope.AccountNameAndCode = new Object();
    //    $scope.accountName;
    //    $scope.CostCenters;
    //    $scope.CmnProjectIds;
    //    $scope.CmnBusinesses;
    //    $scope.TotalAmount = 0;
    //    $scope.accountCode;
    //    $scope.genVoucherNO = false;
    //    $scope.Date;
    //    var dt = new Date();
    //    $scope.VoucherNo = "";
    //    $scope.form = { AnFChartOfAccountId: "", AcCode: "", CmnProjectId: "", AnfCostCenterId: "", ShortNaration: "", SubVoucher: "", Debit: 0, Credit: 0 };
    //    $scope.table = [];
    //    $scope.Voucher = { Date: "", Type: 1, CmnBusinessesId: 1, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
    //    $scope.minDate;
    //    $scope.maxDate;
    //    $scope.ShowOrHide;
    //    $scope.Voucher.Date = dt.getDate() + '/' + (dt.getMonth() + 1) + '/' + dt.getFullYear();
    //    $scope.Id = 0;
    //    //Get Business
    //    $scope.GetCmnBusinesses = function () {
    //        $http({
    //            url: '/Common/Business/GetCmnBusinessesIdAndName',
    //            method: 'GET',
    //            headers: { 'Content-Type': 'application/json' }
    //        }).success(function (data) {
    //            $timeout(function () {

    //                $scope.CmnBusinesses = data;
    //                $scope.Voucher.CmnBusinessesId = 1;
    //                //$scope.GetVoucherNo();

    //                $scope.$apply();
    //            }, 0);
    //        }).error(function (data) {
    //        });
    //    }

    //    //Get Transactional Heads
    //    $scope.GetTransactionalHead = function () {
    //        $http({
    //            url: '/Accounts/Voucher/GetAnfTransactionalHeadsByCompanyId',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' }
    //        }).success(function (data) {
    //            $timeout(function () {
    //                $scope.AccountNameAndCode = data;
    //            }, 0);
    //        }).error(function (data) {
    //        });
    //    }

    //    //Get Projects
    //    $scope.GetProjectsByCompanyIdAndBusinessIdAndTransactionalHeadId = function () {

    //        $scope.obj = { CmnBusinessId: $scope.Voucher.CmnBusinessesId, AnFChartOfAccountId: $scope.form.AnFChartOfAccountId };
    //        $http({
    //            url: '/Common/Project/GetProjectsByCompanyIdAndBusinessIdAndAnfChartOfAccountId',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' },
    //            data: $scope.obj
    //        }).success(function (data) {
    //            $timeout(function () {
    //                $scope.CmnProjectIds = data;
    //                //$scope.search = { voucherNumber: "", fromDate: "", toDate: "", ProjectId: 0, CostCenterId: 0, CmnProjects: $scope.CmnProjectIds, AnfCostCenters: $scope.CostCenters };
    //                $scope.$apply();
    //            }, 0);
    //        }).error(function (data) {
    //        });
    //    }

    //    //Fiscal year date check
    //    $scope.GetFiscalYearDates = function () {
    //        $http({
    //            url: '/Common/FinancialYear/CheckFinancialYear',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' }
    //        }).success(function (data) {
    //            $timeout(function () {
    //                $scope.minDate = data.OpeningDate;
    //                $scope.maxDate = data.ClosingDate;

    //                var dec = DateDiff(StringToDate(data.ClosingDate), dt);
    //                if (dec <= 0) {
    //                    $scope.Voucher.Date = data.ClosingDate;
    //                }
    //                $scope.$apply();

    //                $("#txtVoucherDate").datetimepicker({
    //                    formatDate: 'd/m/Y',
    //                    minDate: $scope.minDate,
    //                    maxDate: $scope.maxDate
    //                });
    //            }, 0);
    //        }).error(function (data) {
    //        });
    //    }

    //    //Get Voucher Number
    //    $scope.GetVoucherNo = function () {
    //        $http({
    //            url: '/Accounts/Voucher/GetVoucherNo',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' },
    //            data: { Type: 1, Prefix: "PV", CmnBusinessId: $scope.Voucher.CmnBusinessesId, Date: $scope.Voucher.Date }
    //        }).success(function (data) {
    //            $timeout(function () {

    //                $scope.genVoucherNO = true;
    //                $scope.Voucher.VoucherNumber = data;
    //                $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + $scope.Count;
    //                $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + parseInt($scope.table.length + 1);
    //                // $scope.search = { voucherNumber: "", fromDate: "", toDate: "", CmnProjects: $scope.CmnProjectIds, AnfCostCenters: $scope.CostCenters,voucher:$scope.voucher };

    //                $scope.$apply();
    //            }, 0);
    //        }).error(function (data) {
    //        });
    //    }

    //    //Get Cost Centers
    //    $scope.GetAnFCostCenters = function () {
    //        $http({
    //            url: '/Accounts/CostCenter/GetCostCenters',
    //            method: 'GET',
    //            headers: { 'Content-Type': 'application/json' }
    //        }).success(function (data) {
    //            $timeout(function () {
    //                $scope.CostCenters = data;
    //                $scope.$apply();
    //            }, 0);
    //        }).error(function (data) {
    //        });
    //    }

    //    $scope.GetFiscalYearDates();
    //    $scope.GetCmnBusinesses();
    //    $scope.GetTransactionalHead();
    //    $scope.GetAnFCostCenters();

    //    $scope.Voucher = { Date: $scope.Voucher.Date, Type: 1, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
    //    $scope.Count = 1;
    //    $scope.selectedItem = new Object();

    //    $scope.search = { voucherNumber: "", fromDate: "", toDate: "", CmnProjects: null, AnfCostCenters: null };

    //    $scope.reset = function () {
    //        $scope.table = [];
    //        $scope.form.SubVoucherNumber = "";
    //        $scope.GetFiscalYearDates();

    //        $scope.Voucher = { Type: 1, CmnBusinessesId: 1, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
    //        $scope.GetCmnBusinesses();
    //    }

    //    //New button Event
    //    $scope.loadData = function () {
    //        if ($scope.Voucher.Date != "") {
    //            $scope.GetVoucherNo();
    //        } else {
    //            Erpmodal.Warning("Date can't be empty !!");
    //        }
    //    };
    //    //Setting Code based on transactional Head
    //    $scope.setCode = function () {
    //        for (var i = 0; i < $scope.AccountNameAndCode.length; i++) {
    //            if ($scope.AccountNameAndCode[i].Id == $scope.form.AnFChartOfAccountId) {
    //                $scope.form.AcCode = $scope.AccountNameAndCode[i].code;

    //                break;
    //            }
    //        }

    //        $scope.GetProjectsByCompanyIdAndBusinessIdAndTransactionalHeadId();
    //    };

    //    //push voucher details
    //    $scope.pushItem = function (data) {
    //        if ((data.Debit != 0 || data.Credit != 0) && (data.Debit != "" || data.Credit != "") && (data.CmnProjectId != "" || data.CmnProjectId != null) && (data.AnFCostCenterId != "" && data.AnFCostCenterId != null)) {
    //            if ($scope.Voucher.Date != "" && $scope.Voucher.Date != null) {
    //                $scope.Particular = "";
    //                $scope.CostCenterName = "";
    //                $scope.ProjectName = "";

    //                for (var i = 0; i < $scope.AccountNameAndCode.length; i++) {
    //                    if ($scope.AccountNameAndCode[i].Id == data.AnFChartOfAccountId) {
    //                        $scope.Particular = $scope.AccountNameAndCode[i].Particular;
    //                        break;
    //                    }
    //                }

    //                for (var i = 0; i < $scope.CostCenters.length; i++) {
    //                    if ($scope.CostCenters[i].Id == data.AnFCostCenterId) {
    //                        $scope.CostCenterName = $scope.CostCenters[i].Name;
    //                        break;
    //                    }
    //                }

    //                for (var i = 0; i < $scope.CmnProjectIds.length; i++) {
    //                    if ($scope.CmnProjectIds[i].Id == data.CmnProjectId) {
    //                        $scope.ProjectName = $scope.CmnProjectIds[i].Name;
    //                        break;
    //                    }
    //                }

    //                $scope.table.push({ AnFChartOfAccountId: data.AnFChartOfAccountId, AnFChartOfAcountName: $scope.Particular, AcCode: data.AcCode, AnFCostCenterId: data.AnFCostCenterId, AnFCostCenterName: $scope.CostCenterName, CmnProjectId: data.CmnProjectId, CmnProjectName: $scope.ProjectName, ShortNaration: data.ShortNaration, SubVoucherNumber: data.SubVoucherNumber, VoucherSerial: $scope.Count, Debit: data.Debit, Credit: data.Credit });
    //                $scope.Count++;
    //                $scope.form = { AnFChartOfAccountId: "", AcCode: "", CmnProjectId: "", AnFCostCenterId: "", ShortNaration: "", TotalAmount: $scope.TotalAmount, SubVoucher: "", Debit: 0, Credit: 0 };
    //                $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + parseInt($scope.table.length + 1);
    //            } else {
    //                //alert("date can't be empty !!");
    //                Erpmodal.Warning("Date can't be empty !!");
    //            }
    //        } else {
    //            //alert("Ensure all data are inserted!");
    //            Erpmodal.Warning("Ensure all data are inserted!");
    //        }
    //    }

    //    //Calculate Debit
    //    $scope.DebitCal = function () {
    //        var dbt = 0;
    //        angular.forEach($scope.table, function (acc, index) {
    //            dbt += acc.Debit;
    //        });
    //        $scope.TotalAmount = dbt;
    //        $scope.$apply;
    //        return dbt;
    //    }
    //    //Calculate Credit
    //    $scope.CreditCal = function () {
    //        var dbt = 0;
    //        angular.forEach($scope.table, function (acc, index) {
    //            dbt += acc.Credit;
    //        });

    //        return dbt;
    //    }
    //    //Save Voucher
    //    $scope.SaveVouchers = function () {
    //        Erpmodal.Confirm('Save').then(function (result) {
    //            if ($scope.table != "") {
    //                if ($scope.DebitCal() != $scope.CreditCal()) {
    //                    //alert("Total Debit And Total Credit ust Be Same");
    //                    Erpmodal.Warning("Total Debit And Total Credit must be same!");
    //                } else {
    //                    $scope.Voucher = { Id: $scope.Id, CmnBusinessId: $scope.Voucher.CmnBusinessesId, Date: $scope.Voucher.Date, Type: 1, VoucherNumber: $scope.Voucher.VoucherNumber, Naration: $scope.Voucher.Naration, VouchrerDetails: $scope.table, VoucherSerial: $scope.Count, TotalAmount: $scope.TotalAmount };
    //                    $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Just a moment...</h1>' });

    //                    $http({
    //                        url: '/Accounts/Voucher/InsertVoucher',
    //                        method: 'POST',
    //                        headers: { 'Content-Type': 'application/json' },
    //                        data: $scope.Voucher
    //                    }).success(function (data) {
    //                        //clear
    //                        $scope.VoucherNo = "";
    //                        $scope.table = [];
    //                        $scope.form.SubVoucherNumber = "";
    //                        $scope.Voucher = { Date: "", Type: 1, CmnBusinessesId: 1, VoucherNumber: "", Naration: "", VouchrerDetails: $scope.table, VoucherSerial: 0, TotalAmount: 0 };
    //                        $scope.GetFiscalYearDates();
    //                        $.unblockUI();
    //                        $scope.Id = 0;
    //                        Erpmodal.Info("Voucher no. is " + data.OperationId);
    //                    }).error(function (data) {
    //                    });
    //                }
    //            }
    //            //else { Erpmodal.Warning("Add new ");}
    //        })
    //    }

    //    //Search Vouchers
    //    $scope.searchByItems = function (obj) {
    //        $http({
    //            url: '/Accounts/Voucher/SearchByItems',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' },
    //            data: { VoucherTypeId: 1, VoucherNumber: $scope.search.VoucherNumber, DateFrom: $scope.search.fromDate, ToDate: $scope.search.toDate, ProjectId: $scope.search.ProjectId, CostCenterId: $scope.search.CostCenterId }
    //        }).success(function (data) {
    //            $scope.Voucher.VoucherNumber = data;
    //            //    $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + $scope.Count;
    //            $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + parseInt($scope.table.length + 1);
    //        }).error(function (data) {
    //        });
    //    };

    //    //Remove From Table
    //    $scope.remove = function (val) {
    //        $scope.table = $scope.table
    //               .filter(function (el) {
    //                   return el.AnFChartOfAccountId !== val.AnFChartOfAccountId;
    //               });

    //        for (var i = 0; i < $scope.table.length; i++) {
    //            $scope.table[i].SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + parseInt(i + 1);
    //        }

    //        // $scope.Count--;
    //        $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + parseInt($scope.table.length + 1);
    //    };

    //    //Search Voucher Details By Voucher
    //    $scope.searchVouchersById = function (obj) {
    //        $http({
    //            url: '/Accounts/Voucher/GetVoucherDetailsbyVoucherId',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' },
    //            data: obj
    //        }).success(function (data) {
    //            $timeout(function () {

    //                $scope.table = data;
    //                $scope.$apply();
    //                $("#gridTable").dataTable();
    //            }, 0);

    //            //$scope.$apply();

    //            $scope.Voucher = { Date: obj.DateString, CmnBusinessesId: obj.CmnBusinessId, Type: 1, VoucherNumber: obj.VoucherNumber, Naration: obj.Naration, VouchrerDetails: $scope.table, VoucherSerial: $scope.Count, TotalAmount: $scope.TotalAmount };

    //            $scope.form.SubVoucherNumber = $scope.Voucher.VoucherNumber + "-" + parseInt(data.length + 1);
    //            $scope.VoucherNo = $scope.Voucher.VoucherNumber;
    //        }).error(function (data) {
    //        });
    //    }

    //    //open Modal
    //    $scope.open = function (size) {
    //        var modalInstance = $modal.open({
    //            templateUrl: 'modal',
    //            controller: ModalInstanceCtrl,
    //            size: size,
    //            resolve: {
    //                search: function () {
    //                    $scope.search = { voucherNumber: "", fromDate: "", toDate: "", CmnBusinesses: $scope.CmnBusinesses, CmnProjects: $scope.CmnProjectIds, AnfCostCenters: $scope.CostCenters, voucher: [$scope.Voucher], VouchrerDetails: $scope.table, VoucherSerial: $scope.Count };
    //                    return $scope.search;
    //                }
    //            }
    //        });

    //        modalInstance.result.then(function (selectedItem) {

    //            $scope.selectedItem = selectedItem;
    //            $scope.Id = selectedItem.id;

    //            $scope.searchVouchersById(selectedItem);
    //        }, function () {
    //        });

    //        modalInstance.opened.then(function (selectedItem) {
    //            $scope.selectedItem = selectedItem;

    //            // $scope.searchVouchersById(selectedItem.id);
    //        }, function () {
    //        });
    //    };
    //});

    ////Modal Controller
    //var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, search) {
    //    $scope.search = search;
    //    $scope.ShowOrHide = false;
    //    $scope.Projects = {};
    //    $scope.result = new Object();

    //    $scope.ok = function () {
    //        $modalInstance.close($scope.search);
    //    };
    //    $scope.selectedItem = new Object();

    //    $scope.cancel = function () {
    //        $modalInstance.dismiss('cancel');
    //    };
    //    $scope.Clear = function () {
    //        $scope.search.voucherNumber = '';
    //        $scope.search.fromDate = '';
    //        $scope.search.toDate = '';
    //        $scope.search.CmnBusinessesId = '';
    //        $scope.Projects = {};
    //        $scope.search.ProjectId = 0;
    //        $scope.search.CostCenterId = '';
    //    };
    //    $scope.setId = function (obj) {
    //        $scope.selectedItem = obj;
    //        $modalInstance.close(obj);
    //    };

    //    $scope.load = function () {
    //        $http({
    //            url: '/Accounts/Voucher/SearchByItems',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' },
    //            data: { VoucherTypeId: 1, VoucherNumber: search.VoucherNumber, DateFrom: search.fromDate, ToDate: search.toDate, ProjectId: search.ProjectId, CostCenterId: search.CostCenterId }
    //        }).success(function (data) {
    //            $timeout(function () {
    //                $scope.result = data;
    //                $scope.$apply();
    //                //$("#searchTable").dataTable();
    //                $scope.ShowOrHide = true;
    //            }, 0);

    //            //

    //            //
    //        }).error(function (data) {
    //        });

    //        // $modalInstance.opened($scope.search);
    //    };

    //    $scope.loadProjects = function () {
    //        $http({
    //            url: '/Common/Project/GetProjectsByCompanyId',
    //            method: 'POST',
    //            headers: { 'Content-Type': 'application/json' },
    //            data: { businessId: search.CmnBusinessesId }
    //        }).success(function (data) {
    //            $timeout(function () {
    //                $scope.Projects = data;
    //                $scope.$apply();

    //            }, 0);

    //            //

    //            //
    //        }).error(function (data) {
    //        });

    //        // $modalInstance.opened($scope.search);
    //    };

    //};