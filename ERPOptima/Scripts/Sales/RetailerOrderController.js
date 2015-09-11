(function () {

    app.controller('RetailerOrderController', function ($scope, $http, Erpmodal, focus) {

        $scope.SalesOrder = new Object();
        $scope.Resources = new Object();
        $scope.Products = new Object();
        $scope.ProductUnits = new Object();
        //$scope.SalesOrder.PartyType = 2;
        $scope.Parties = new Object();
        //$scope.Resources2 = [];
        //var SavedSOId = 0;
        var state = 0;
        $scope.RefNo = ""; 
        //$scope.ShowFocusButton = false;

        $scope.ProductChangeHandler = function () {
            $http({
                url: '/Sales/ProductUnits/GetUnitsConfigured',
                method: 'GET',
                params: { productId: $scope.SalesOrder.SlsProductId },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.ProductUnits = data;
                $scope.SalesOrder.SlsUnitId = 0;
            }).error(function (data) {

            });
        };

        $scope.GetAllProducts = function () {
            $http({
                url: '/Sales/ChartOfProduct/GetProductsConfigured',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Products = data;
                $scope.SalesOrder.SlsProductId = 0;
            });
        };

        $scope.PartyTypeChangeHandler = function () {

            $scope.Parties = new Object();
            //get retailers
            $scope.GetPartyList(0);
        };

        $scope.GetPartyList = function (partyId) {

            $http({
                url: '/Sales/Retailer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
                if (partyId > 0) {
                    $scope.SalesOrder.Party = partyId;
                }
            }).error(function () {
                //Erpmodal.Warning("Error!");
            });

        };

        var init = function () {
             
            $scope.SalesOrder = {};
            state = 0;
            GetRefNo();
            $scope.PartyTypeChangeHandler();
            $scope.GetAllProducts();
            //Initially unit will be empty. 
            $scope.SalesOrder.SlsUnitId = 0;

            //fetch all items of sales order and load in grid
            $http({
                url: '/Sales/RetailerSales/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Resources = data;
            });

        };


        function GetRefNo() {

            $http({
                url: '/Sales/Sales/GetRefNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.RefNo = data;
            });


        };//end of GetCode


        $scope.CalculateNetTotal = function (amountToAdjust, isAdd) {

            if (amountToAdjust > 0) {
                if ($scope.SalesOrder.Total === undefined) {
                    $scope.SalesOrder.Total = 0;
                }
                if (isAdd) {
                    $scope.SalesOrder.Total = $scope.SalesOrder.Total + amountToAdjust;
                }
                else {
                    $scope.SalesOrder.Total = $scope.SalesOrder.Total - amountToAdjust;
                }
                if ($scope.SalesOrder.Total < 1) {
                    $scope.SalesOrder.Total = 0;
                }
            }
            $scope.ApplyDiscount();
        };

        $scope.CalculateGrandTotal = function () {

            if ($scope.SalesOrder.Total !== undefined && $scope.SalesOrder.Discount !== undefined) {
                if ($scope.SalesOrder.GrandTotal === undefined) {
                    $scope.SalesOrder.GrandTotal = 0;
                }
                var discountAmount = $scope.SalesOrder.Discount;// * $scope.SalesOrder.Total / 100;
                $scope.SalesOrder.GrandTotal = $scope.SalesOrder.Total - discountAmount;
                if ($scope.SalesOrder.GrandTotal < 1) {
                    $scope.SalesOrder.GrandTotal = 0;
                }
            }
            else if ($scope.SalesOrder.Total !== undefined) {
                $scope.SalesOrder.GrandTotal = $scope.SalesOrder.Total;
            }
            else {
                $scope.SalesOrder.GrandTotal = 0;
            }
            $scope.CalculateDue();
        };

        //$scope.ApplyDiscountRate = function () {
        //    if ($scope.SalesOrder.DiscountRate === undefined) {
        //        $scope.SalesOrder.DiscountRate = 0;
        //    }
        //    if ($scope.SalesOrder.Total === undefined) {
        //        $scope.SalesOrder.Total = 0;
        //    }
        //    var discountRate = $scope.SalesOrder.DiscountRate;
        //    $scope.SalesOrder.Discount = discountRate * $scope.SalesOrder.Total / 100;
        //    $scope.CalculateGrandTotal();
        //};

        $scope.ApplyDiscountAmount = function () {
            var discountAmount = 0;
            var total = 0;
            if ($scope.SalesOrder.Discount !== undefined) {
                discountAmount = $scope.SalesOrder.Discount;
            }
            if ($scope.SalesOrder.Total !== undefined) {
                total = $scope.SalesOrder.Total;
            }
            var rate = discountAmount * 100 / total;
            rate = rate.toFixed(2);
            if (rate >= 0)
                $scope.SalesOrder.DiscountRate = rate;
            else
                $scope.SalesOrder.DiscountRate = 0;
            $scope.CalculateGrandTotal();
        };

        $scope.ApplyDiscount = function () {
            var discountFor = 0;
            if ($scope.SalesOrder.Total === undefined) {
                $scope.SalesOrder.Total = 0;
            }
            discountFor = $scope.SalesOrder.Total;

            $http({
                url: '/Sales/Sales/GetDiscount',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: { total: discountFor }
            }).success(function (data) {
                 
                //if ($scope.SalesOrder.DiscountRate === undefined) {
                //    $scope.SalesOrder.DiscountRate = 0;
                //}
                if ($scope.SalesOrder.Discount === undefined) {
                    $scope.SalesOrder.Discount = 0;
                }
                //$scope.SalesOrder.DiscountRate = data.Discount;
                var discountRate = data.Discount.toFixed(2);
                $scope.SalesOrder.DiscountRate = discountRate;
                $scope.SalesOrder.Discount = discountRate * discountFor / 100;

                $scope.CalculateGrandTotal();
            }).error(function (data) {
                 
                //Calculate without discount
                $scope.SalesOrder.Discount = 0;
                $scope.CalculateGrandTotal();
            });

        };

        $scope.CalculateDue = function () {

            if ($scope.SalesOrder.GrandTotal !== undefined && $scope.SalesOrder.Advance !== undefined) {
                if ($scope.SalesOrder.Due === undefined) {
                    $scope.SalesOrder.Due = 0;
                }
                $scope.SalesOrder.Due = $scope.SalesOrder.GrandTotal - $scope.SalesOrder.Advance;
                if ($scope.SalesOrder.Due < 1) {
                    $scope.SalesOrder.Due = 0;
                }
            }
            else if ($scope.SalesOrder.GrandTotal !== undefined) {
                $scope.SalesOrder.Due = $scope.SalesOrder.GrandTotal;
            }
            else {
                $scope.SalesOrder.Due = 0;
            }
        };



        $scope.AddProductToOrder = function () {

            var partytypeIdLcl = 0;
            var partyIdLcl = 0;            

            $http({
                url: '/Sales/Sales/ShowSOProductDetails',
                method: 'GET',
                params: {
                    productId: $scope.SalesOrder.SlsProductId, quantity: $scope.SalesOrder.SalesOrderQuantity,
                    unitId: $scope.SalesOrder.SlsUnitId, partyTypeId: partytypeIdLcl, paryId: partyIdLcl
                },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                if (data !== undefined && data.length > 0) {
                    //first element is the main product, others are free products.
                    var productToAdd = data[0];
                    if ($scope.duplicateSalesOrderProduct(productToAdd.SlsProductId)) {
                        Erpmodal.Warning("Duplicate record!");
                    }
                    else {

                        var productTotal = productToAdd.Total;
                        $scope.CalculateNetTotal(productTotal, true);
                        if ($scope.Resources2 === undefined || $scope.Resources2.length === undefined) {
                            $scope.Resources2 = [];
                        }
                        for (var i = 0; i < data.length; i++)
                            $scope.Resources2.push(data[i]);

                        $scope.ClearProductGroup();
                    }
                }
                else {
                    Erpmodal.Warning("Failed, lack of information!");
                }
            }).error(function () {
                Erpmodal.Warning("Failed, lack of information!");
            });

            $("#cmbProduct").focus();
            //focus('txtProductQty');
        };

        //$scope.CheckCreditLimitNSave = function (isValid) {
        //    if ($scope.SalesOrder.Total === undefined) {
        //        $scope.SalesOrder.Total = 0;
        //    }
        //    if ($scope.SalesOrder.Advance === undefined) {
        //        $scope.SalesOrder.Advance = 0;
        //    }
        //    if ($scope.SalesOrder.PartyType === undefined) {
        //        return;
        //    }
        //    if ($scope.SalesOrder.Party === undefined) {
        //        return;
        //    }
        //    var newTotal = $scope.SalesOrder.Total;
        //    var advance = $scope.SalesOrder.Advance;
        //    var partyTypeId = $scope.SalesOrder.PartyType;
        //    var partyId = $scope.SalesOrder.Party;

        //    $http({
        //        url: '/Sales/Sales/CheckCreditLimit',
        //        method: 'GET',
        //        params: {
        //            partytype: partyTypeId, partyId: partyId,
        //            creditLimit: newTotal, advance: advance
        //        },
        //        headers: { 'Content-Type': 'application/json' }
        //    }).success(function (data) {

        //        if (data !== undefined) {
        //            if (data.result) {
        //                //Execute Save Operation
        //                $scope.Save(isValid);
        //            }
        //            else {
        //                Erpmodal.Warning("No available Credit Limit.");
        //            }
        //        }
        //        else {
        //            Erpmodal.Warning("No available Credit Limit.");
        //        }
        //    }).error(function () {
        //        Erpmodal.Warning("No available Credit Limit.");
        //    });
        //};

        $scope.RemoveResource2 = function (item) {
            var index = $scope.Resources2.indexOf(item);
            if (index > -1) {
                var quantityToAdjust = item.Total;
                $scope.Resources2.splice(index, 1);
                $scope.CalculateNetTotal(quantityToAdjust, false);
            }
        };

        $scope.Save = function (isValid) {
             
            if ($scope.retailerValid()) {
                if (isValid) {
                    if ($scope.SalesOrder !== undefined) {
                        Erpmodal.Confirm('Save').then(function (result) {
                             
                            if (state == 1) {
                                //$scope.SalesOrder.Id = SavedSOId;
                            }
                            else { $scope.SalesOrder.Id = 0; }
                            $scope.SalesOrder.PartyType = 2;
                            //Cash type always
                            $scope.SalesOrder.TransactionType = 4;
                            $scope.SalesOrder.RefNo = $scope.RefNo;
                            $scope.SalesOrder.SalesOrderDetails = $scope.Resources2;

                            if ($scope.SalesOrder.Party !== undefined && $scope.SalesOrder.Party >= 1) {
                                $scope.SalesOrder.OptionalPartyName = "";
                            }
                            //1=Regular,2=Corporate,3=Retail
                            $scope.SalesOrder.SalesType = 3;
                            $http.post("/Sales/Sales/SaveSalesOrder", $scope.SalesOrder).success(
                                function (data) {

                                    if (data.Success) {
                                        $scope.SalesOrder.Id = data.OperationId;

                                        $http.post("/Sales/Delivery/SaveSalesDeliveryInternal", $scope.SalesOrder).success(function (data2) {
                                            Erpmodal.Save(data, "Save");
                                            $scope.Reset();
                                        }).error(function (error2) {
                                        });

                                        //$http({
                                        //    url: '/Sales/Delivery/SaveSalesDeliveryInternal',
                                        //    method: 'POST',
                                        //    headers: { 'Content-Type': 'application/json' },
                                        //    params: { objS: $scope.SalesOrder }
                                        //}).success(function (data2) {
                                        //    Erpmodal.Save(data, "Save");
                                        //    $scope.Reset();
                                        //}).error(function (error2) {
                                        //});
                                    }
                                }
                                ).error(function (error) {

                                });
                        });
                    }
                }
            }
            else
            {
                Erpmodal.Warning("Retailer information is missing.");
            }
        };

        $scope.setFortEdit = function (rowitem) {

            $scope.FormReset();

            state = 1;
            SavedSOId = rowitem.Id;

            $scope.SalesOrder = jQuery.extend(true, {}, rowitem); //rowitem;


            $scope.Resources2 = rowitem.SalesOrderDetails;

            $scope.RefNo = rowitem.RefNo;
            if (rowitem.OrderDate !== null && rowitem.OrderDate !== undefined) {
                $scope.SalesOrder.OrderDate = ConverttoDateString(rowitem.OrderDate);
            }
            $scope.SalesOrder.PreferredDeliveryDate = ConverttoDateString(rowitem.PreferredDeliveryDate);
            $scope.SalesOrder.CreatedDate = ConverttoDateString(rowitem.CreatedDate);
            if (rowitem.ModifiedDate !== null && rowitem.ModifiedDate !== undefined) {
                $scope.SalesOrder.ModifiedDate = ConverttoDateString(rowitem.ModifiedDate);
            }
             
            if ($scope.SalesOrder.Discount !== undefined) {
                $scope.SalesOrder.Discount = $scope.SalesOrder.Discount.toFixed(2);
            }
            else {
                $scope.SalesOrder.Discount = 0;
            }

            $scope.ButtonDisabled = false;
            $scope.CodeState = true;

            $scope.ApplyDiscountAmount();
            //$scope.CalculateGrandTotal();

            if ($scope.SalesOrder.PartyType > 0) {
                $scope.Parties = new Object;
                $scope.GetPartyList($scope.SalesOrder.PartyType, $scope.SalesOrder.Party);
            }


        };

        $scope.Reset = function () {

            init();
            $scope.ButtonDisabled = true;
            SavedSOId = 0;
            state = 0;
            $scope.SalesOrder = {};
            $scope.Resources2 = {};
            $scope.RetailerSalesForm.$setPristine();
        };

        $scope.FormReset = function () {

            $scope.ButtonDisabled = true;
            SavedSOId = 0;
            state = 0;
            $scope.SalesOrder = {};
            $scope.Resources2 = {};
        };

        $scope.ClearProductGroup = function () {

            $scope.SalesOrder.SlsProductId = 0;
            $scope.ProductUnits = null;
            $scope.SalesOrder.SalesOrderQuantity = "";


        };

        $scope.retailerValid = function () {
            if ($scope.SalesOrder.Party !== undefined || $scope.SalesOrder.OptionalPartyName !== undefined)
                return true;
            return false;
        };

        $scope.detailInvalid = function () {
            return !($scope.SalesOrder.SlsProductId && $scope.SalesOrder.SalesOrderQuantity && $scope.SalesOrder.SlsUnitId);
        };

        $scope.duplicateSalesOrderProduct = function (newSODproductId) {
            if ($scope.Resources2 !== undefined) {
                for (var i = 0; i < $scope.Resources2.length; i++) {
                    if ($scope.Resources2[i].SlsProductId > 0 && $scope.Resources2[i].SlsProductId == newSODproductId)
                        return true;
                }
            }
            return false;
        };

        $scope.printInvoice = function (rowitem) {
             
            var url = "invoiceNo=" + rowitem.InvoiceNo;
            var pageUrl = "/Sales/Delivery/GetInvoiceReport?" + url;
            window.open(pageUrl);
            window.focus();
        };

        $scope.printChallan = function (rowitem) {
             
            var url = "DeliveryId=" + rowitem.DeliveryId;
            var pageUrl = "/Sales/Delivery/GetChallenList?" + url;
            window.open(pageUrl);
            window.focus();
        };

        init();

    });

}).call(this);

