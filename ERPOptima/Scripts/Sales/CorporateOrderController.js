(function () {

    app.controller('CorporateOrderController', function ($scope, $http, Erpmodal, focus) {

        $scope.CorpApp = new Object();

        $scope.SalesOrder = new Object();
        $scope.Resources = new Object();        
        $scope.Products = new Object();        
        $scope.ProductUnits = new Object();
        $scope.SalesOrder.PartyType = 4;
        $scope.Parties = new Object();

        $scope.SalesApps = new Object();
        $scope.SalesAppId = 0;

        $scope.Resources2 = [];
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
                $scope.ProductUnits = {};
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
                $scope.Products = {};
                $scope.Products = data;
                $scope.SalesOrder.SlsProductId = 0;
            });
        };

        $scope.GetAllCorpSalesApps = function () {
            $http({
                url: '/Sales/CorporateSales/GetAllApproved',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {             
                $scope.SalesApps = {};
                $scope.SalesApps = data;
            });
        };

        $scope.CorpAppChangeHandler = function () {           
            $http({
                url: '/Sales/CorporateSales/GetCorpareClient',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: { corporateAppId: $scope.SalesOrder.SalesAppId }
            }).success(function (data) {              
                if (data !== undefined && data.ClientId !== undefined) {
                    $scope.SalesOrder.Party = data.ClientId;
                }
                if (data !== undefined && data.app !== undefined) {
                    //Ref No & Party Id is already set - by other functions

                    //Merge - corporate sales data to sales order data.
                    $scope.CorpApp = data.app;

                    //Get and load details
                    $http({
                        url: '/Sales/CorporateSales/GetCorpareClientDetail',
                        method: 'GET',
                        headers: { 'Content-Type': 'application/json' },
                        params: { corporateAppId: $scope.SalesOrder.SalesAppId }
                    }).success(function (data) {                      
                        if (data !== undefined && data.details !== undefined) {
                            $scope.Resources2 = data.details;
                            for(var i =0; i<$scope.Resources2.length; i++)
                            {
                                $scope.GetUnitsByProductId($scope.Resources2[i]);                                
                            }
                        }
                    }).error(function (error) {
                      
                    });

                }
            }).error(function (error) {
                
            });
        };

        $scope.CheckForProductPrices = function () {
           
            if ($scope.Resources2 !== undefined && $scope.Resources2.length !== undefined) {
                for (var i = 0; i < $scope.Resources2.length; i++) {
                   
                    if ($scope.Resources2[i].Quantity !== undefined && $scope.Resources2[i].ApprovedPercentage !== undefined &&
                        $scope.Resources2[i].SlsProductId !== undefined && $scope.Resources2[i].SlsUnitId !== undefined) {
                        if ($scope.Resources2[i].SlsProductId >= 1 && $scope.Resources2[i].SlsUnitId >= 1) {
                            if ($scope.Resources2[i].Quantity < 1) {
                                $scope.Resources2[i].Quantity = 0;
                            }
                            if ($scope.Resources2[i].ApprovedPercentage < 1) {
                                $scope.Resources2[i].ApprovedPercentage = 0;
                            }
                            $scope.AddProductToOrder($scope.Resources2[i].SlsProductId, $scope.Resources2[i].Quantity, $scope.Resources2[i].SlsUnitId, $scope.Resources2[i].ApprovedPercentage);
                        }
                    }
                }
            }
        };

        $scope.PartyTypeChangeHandler = function () {

            $scope.Parties = new Object();
            $scope.GetPartyList(0);
        };

        $scope.GetPartyList = function (partyId) {

            $http({
                url: '/Sales/CorporateClient/GetAll',
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
            $scope.GetRefNo();
            $scope.GetAllCorpSalesApps();
            $scope.PartyTypeChangeHandler();
            $scope.GetAllProducts();
            //Initially unit will be empty. 
            $scope.SalesOrder.SlsUnitId = 0;
            $scope.SalesOrder.Discount = 0;
            $scope.SalesOrder.Advance = 0;

            //fetch all items of sales order and load in grid
            $http({
                url: '/Sales/CorporateSalesOrder/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
               
                $scope.Resources = {};
                $scope.Resources = data;
                
            });
                              
        };
               

        $scope.GetRefNo = function() {
           
            $http({
                url: '/Sales/CorporateSalesOrder/GetRefNo',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
               
                $scope.RefNo = "";
                $scope.RefNo = data.refNo;
            }).error(function (error) {
               
            });


        };//end of GetCode

        $scope.GetUnitsByProductId = function (resourceItem) {
          
            if (resourceItem !== undefined && resourceItem.SlsProductId !== undefined) {
                var ProductId = resourceItem.SlsProductId;
                $http({
                    url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId?productId=' + ProductId,
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                                     
                    if (data !== undefined) {
                        //$scope.Units = data;
                        resourceItem.Units = {};
                        resourceItem.Units = data;
                    }

                }).error(function (data) {
                   
                    resourceItem.Units = {};
                });
            }
        };



        //$scope.CheckNetTotal = function (amountToAdjust, isAdd) {

        //    if (amountToAdjust > 0) {
        //        if (isAdd) {
        //            if ($scope.SalesOrder.Total === undefined) {
        //                $scope.SalesOrder.Total = 0;
        //            }
        //            var total = $scope.SalesOrder.Total + amountToAdjust;
        //            return total;
        //        }
        //        else {
        //            var total = $scope.SalesOrder.Total - amountToAdjust;
        //            return total;
        //        }
        //    }
        //    return 0;
        //};

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
            //    $scope.ApplyDiscount();
            $scope.CalculateGrandTotal();
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
            //$scope.CalculateDue();
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

        //$scope.ApplyDiscount = function () {
        //    var discountFor = 0;
        //    if ($scope.SalesOrder.Total === undefined) {
        //        $scope.SalesOrder.Total = 0;
        //    }
        //    discountFor = $scope.SalesOrder.Total;

        //    $http({
        //        url: '/Sales/Sales/GetDiscount',
        //        method: 'GET',
        //        headers: { 'Content-Type': 'application/json' },
        //        params: { total: discountFor }
        //    }).success(function (data) {
        //         
        //        if ($scope.SalesOrder.DiscountRate === undefined) {
        //            $scope.SalesOrder.DiscountRate = 0;
        //        }
        //        if ($scope.SalesOrder.Discount === undefined) {
        //            $scope.SalesOrder.Discount = 0;
        //        }
        //        $scope.SalesOrder.DiscountRate = data.Discount;
        //        $scope.SalesOrder.Discount = $scope.SalesOrder.DiscountRate * discountFor / 100;

        //        $scope.CalculateGrandTotal();
        //    }).error(function (data) {
        //         
        //        //Calculate without discount
        //        $scope.SalesOrder.DiscountRate = 0;
        //        $scope.CalculateGrandTotal();
        //    });

        //};

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

        

        $scope.AddProductToOrder = function (productLcl,quantityLcl,unitLcl,discountLcl) {
           
            $http({
                url: '/Sales/CorporateSalesOrder/GetProductPrice',
                method: 'GET',
                params: {
                    productId: productLcl, quantity: quantityLcl,
                    unitId: unitLcl, discount: discountLcl
                },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
               
                if (data !== undefined && data.Total !== undefined) {
                  
                    var productToAdd = data;
                        var productTotal = productToAdd.Total;
                        $scope.CalculateNetTotal(productTotal, true);                                           
                }                
            }).error(function (error) {
                
            });
            
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

        //$scope.RemoveResource2 = function (item) {
        //    var index = $scope.Resources2.indexOf(item);
        //    if (index > -1) {
        //        var quantityToAdjust = item.Total;
        //        $scope.Resources2.splice(index, 1);
        //        $scope.CalculateNetTotal(quantityToAdjust, false);
        //    }
        //};

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                   
                    if($scope.SalesOrder !== undefined)
                    {
                        if (state == 1) {
                            //$scope.SalesOrder.Id = SavedSOId;
                        }
                        else { $scope.SalesOrder.Id = 0; }

                        $scope.SalesOrder.RefNo = $scope.RefNo;
                        $scope.SalesOrder.PartyType = 4;
                        $scope.SalesOrder.SalesOrderDetails = $scope.Resources2;
                        //1=Regular,2=Corporate,3=Retail
                        $scope.SalesOrder.SalesType = 2;
                      
                        if ($scope.SalesOrder.SalesAppId !== undefined) {
                            $scope.SalesOrder.SlsCorporateSalesApplicationId = $scope.SalesOrder.SalesAppId;
                        }

                        $http.post("/Sales/Sales/SaveSalesOrder", $scope.SalesOrder).success(
                            function (data) {
                                Erpmodal.Save(data, "Save");
                                if (data.Success) {
                                    $scope.Reset();
                                }
                            }
                            ).error(function (error) {

                            });
                    }                    
                });
        };

        //$scope.setFortEdit = function (rowitem) {

        //    $scope.FormReset();

        //    state = 1;
        //    SavedSOId = rowitem.Id;

        //    $scope.SalesOrder = jQuery.extend(true, {}, rowitem); //rowitem;


        //    $scope.Resources2 = rowitem.SalesOrderDetails;

        //    $scope.RefNo = rowitem.RefNo;
        //    if (rowitem.OrderDate !== null && rowitem.OrderDate !== undefined) {
        //        $scope.SalesOrder.OrderDate = ConverttoDateString(rowitem.OrderDate);
        //    }
        //    $scope.SalesOrder.PreferredDeliveryDate = ConverttoDateString(rowitem.PreferredDeliveryDate);
        //    $scope.SalesOrder.CreatedDate = ConverttoDateString(rowitem.CreatedDate);
        //    if (rowitem.ModifiedDate !== null && rowitem.ModifiedDate !== undefined) {
        //        $scope.SalesOrder.ModifiedDate = ConverttoDateString(rowitem.ModifiedDate);
        //    }

        //    $scope.ButtonDisabled = false;
        //    $scope.CodeState = true;

        //    $scope.CalculateGrandTotal();

        //    if ($scope.SalesOrder.PartyType > 0) {
        //        $scope.Parties = new Object;
        //        $scope.GetPartyList($scope.SalesOrder.PartyType, $scope.SalesOrder.Party);
        //    }


        //};

        $scope.Reset = function () {
           
            
            //$scope.ButtonDisabled = true;
            //SavedSOId = 0;
            state = 0;
            $scope.SalesAppId = 0;
            $scope.CorpApp = {};
            $scope.SalesOrder = {};
            $scope.Resources2 = {};
            $scope.Resources = {};
            $scope.CorporateSalesOrderForm.$setPristine();
            init();
        };

        //$scope.FormReset = function () {

        //    $scope.ButtonDisabled = true;
        //    SavedSOId = 0;
        //    state = 0;
        //    $scope.SalesAppId = 0;
        //    $scope.CorpApp = {};
        //    $scope.SalesOrder = {};
        //    $scope.Resources2 = {};
        //};

        //$scope.ClearProductGroup = function () {

        //    $scope.SalesOrder.SlsProductId = 0;
        //    $scope.ProductUnits = null;
        //    $scope.SalesOrder.SalesOrderQuantity = "";


        //};

        //$scope.detailInvalid = function () {
        //    return !($scope.SalesOrder.PartyType && $scope.SalesOrder.Party && $scope.SalesOrder.SlsProductId &&
        //        $scope.SalesOrder.SalesOrderQuantity && $scope.SalesOrder.SlsUnitId);
        //};

        //$scope.duplicateSalesOrderProduct = function (newSODproductId) {

        //    for (var i = 0; i < $scope.Resources2.length; i++) {
        //        if ($scope.Resources2[i].SlsProductId > 0 && $scope.Resources2[i].SlsProductId == newSODproductId)
        //            return true;
        //    }
        //    return false;
        //};

        init();

    });

}).call(this);

