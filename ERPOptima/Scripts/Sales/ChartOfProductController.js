deps.push('angularBootstrapNavTree');

if (angular.version.full.indexOf("1.2") >= 0) {
    deps.push('ngAnimate');
}

(function () {
    app.controller('ChartOfProductController', function ($scope, $timeout, $http, Erpmodal, $validation) {
        var state;

        var current, rawdata, currentObject;
        var parents = [];

        $scope.AllData = [];

        $scope.Category = new Object();
        $scope.CategoryId = 0;      

        $scope.SubCategory = new Object();
        $scope.SubCategoryId = 0;      

        $scope.HideCatSubCat = false;
        $scope.HideSubCat = false;
        $scope.HideIsProduct = false;
        $scope.HideNoCredit = false;
        $scope.ProductDisable = false;


        var init = function () {
            state = 0;
            current = null;
            rawdata = null;
            currentObject = null;

            $scope.ShowLoading = false;          

            $scope.ButtonDeleteDisabled = true;          
            $scope.Status = true;
            //for checkboxes
            //$scope.HideIsProduct = true;
            //$scope.HideNoCredit = true;

            $scope.Category = new Object();
            $scope.CategoryId = 0;
            $scope.SubCategory = new Object();
            $scope.SubCategoryId = 0;

            $scope.selected = false;//for current selection

            $scope.SlsChartOfProduct = {};
            $scope.SlsChartOfProduct.information = {};
            $scope.SlsChartOfProduct.parents;
            $scope.SlsChartOfProduct.childrens;

            $scope.my_data = [];

            parents = [];

            $scope.ShowLoading = true;
            $http.get("/Sales/ChartOfProduct/GetAll").success(function (data) {
                $scope.ShowLoading = false;
                rawdata = _.sortBy(data, function (item) {
                    return item.SlsProductId;
                });

                $scope.my_data = createTreeMap(null, data);
                $scope.AllData = data;
            });


            GetCategory();


        }//end of init

        init();//init is called

        function GetCategory() {
           
            $http({
                url: '/Sales/ChartOfProduct/GetCategories',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Category = data;
                $scope.CategoryId = 0;

               

            });


        }//end of GetCategory

        $scope.GetChild = function () {
            $http({
                url: '/Sales/ChartOfProduct/GetBySlsProductId?categoryId=' + $scope.CategoryId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
              
                if (data.length > 0) {                  
                    if (data[0].IsProduct == false) {
                        //$scope.HideIsProduct = false;
                        //$scope.HideNoCredit = false;
                        $scope.SlsChartOfProduct.information.IsProduct = false;
                        $scope.ProductDisable = false;
                    }                    
                }
                else {
                    $scope.ProductDisable = false;
                }
                

            }).error(function (data) {
            });
        };

        $scope.GetSubCategory = function (level) {
            $http({
                url: '/Sales/ChartOfProduct/GetSubCategories?categoryId=' + $scope.CategoryId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
              
                $scope.SubCategory = data;
                $scope.SubCategoryId = 0;               

                //$scope.HideIsProduct = false;
                //$scope.HideNoCredit = false;
                $scope.SlsChartOfProduct.information.IsProduct = true;
                //$scope.ProductDisable = true;            
                
                if (level != 3) {
                    $scope.GetChild();
                }
                


            }).error(function (data) {
            });
        };

        $scope.GetRootCategory = function () {
            $http({
                url: '/Sales/ChartOfProduct/GetRootCategories',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Category = data;                

            }).error(function (data) {
            });
        };



        function createTreeMap(parentId, list) {
            var nodes = [];
            for (var i = 0, l; l = list[i]; i++) {
                if (l.SlsProductId === parentId) {                  
                    nodes.push({
                        Id: l.Id
                      , SlsProductId: l.SlsProductId                    
                      , Code: l.Code
                      , Name: l.Name
                      , SecCompanyId: l.SecCompanyId                    
                      , Level: l.Level
                      , IsProduct: l.IsProduct
                      , Description: l.Description
                      , NoCredit: l.NoCredit
                      , children: createTreeMap(l.Id, list)
                    });
                }
            }
            return nodes;
        }//end of findChildren

        function findParent(parentId, list) {
            for (var i = 0, l; l = list[i]; i++) {
                if (l.Id === parentId) {
                    parents.push({
                        Id: l.Id
                     , SlsProductId: l.SlsProductId
                     , Name: l.Name
                     , Code: l.Code
                    }
                    );

                    findParent(l.SlsProductId, list);
                }
            }
        }//end of findParent

        $scope.my_tree_handler = function (branch) {

             

            $scope.selected = true;
            $scope.ButtonDeleteDisabled = false;
            state = 1;
            current = branch;

            if (branch.Level == 0) {
                $scope.ButtonDeleteDisabled = true;
                $scope.SlsChartOfProduct.information = {};
            }
            else {
                $scope.SlsChartOfProduct.information = {
                    Id: branch.Id
                    , SlsProductId: branch.SlsProductId
                    , Code: branch.Code
                    , Name: branch.Name
                    , Level: branch.Level
                    , Description: branch.Description
                    , IsProduct: branch.IsProduct
                    , NoCredit: branch.NoCredit

                };
            }

           
        

            $scope.SlsChartOfProduct.childrens = _.where(branch.children, { SlsProductId: branch.Id });


            parents = [];

            findParent(branch.SlsProductId, rawdata);
             
            if (branch.Level == 1) {
                $scope.HideCatSubCat = true;
                $scope.HideSubCat = true;
                $scope.HideIsProduct = false;
                $scope.ProductDisable = true;
                //$scope.HideNoCredit = true;
            }
            if (branch.Level == 2) {
                $scope.HideCatSubCat = true;
                $scope.HideSubCat = true;
                $scope.HideIsProduct = false;
                $scope.ProductDisable = true;
                //$scope.HideNoCredit = false;

                $scope.CategoryId = branch.SlsProductId;
                $scope.SlsChartOfProduct.information.SlsProductId = $scope.CategoryId;                
            }
            if (branch.Level == 3) {               
                $scope.HideCatSubCat = true;
                $scope.HideSubCat = true;
                $scope.HideIsProduct = false;
                $scope.ProductDisable = true;
                //$scope.HideNoCredit = false;

                $scope.CategoryId = parents[1].Id;
                $scope.GetSubCategory(3);
                $scope.SubCategoryId = branch.SlsProductId;
               
                $scope.SlsChartOfProduct.information.SlsProductId = $scope.SubCategoryId;
                
            }
            
          

            $scope.SlsChartOfProduct.parents = _.sortBy(parents, function (item) {
                return item.Id;
            });
        };//end of tree_handler

        


        $scope.Save = function () {
             
            var grantPermission = 1;
            if ($scope.SlsChartOfProduct.information.IsProduct == true)
            {
                //For product entry

                if ($scope.CategoryId === undefined || $scope.CategoryId == 0)
                {
                    grantPermission = 0;
                }
            }
            else
            {
                //For category entry

                if (($scope.CategoryId !== undefined && $scope.CategoryId > 0) && ($scope.SubCategoryId !== undefined && $scope.SubCategoryId > 0))
                {
                    grantPermission = 0;
                }
            }

             ;
            if (grantPermission == 1) {
                Erpmodal.Confirm('Save').then(function (result) {
                     
                    if (state == 0) {
                        $scope.SlsChartOfProduct.information.SlsProductId = $scope.CategoryId;
                        if ($scope.SubCategoryId > 0) {
                            $scope.SlsChartOfProduct.information.SlsProductId = $scope.SubCategoryId;
                        }


                        if ($scope.SlsChartOfProduct.information.SlsProductId == 0) {
                            $scope.SlsChartOfProduct.information.Level = 1;
                            $scope.SlsChartOfProduct.information.SlsProductId = 1;
                        }
                        else {
                            var lvl = _.find($scope.AllData, function (rw) { return rw.Id == $scope.SlsChartOfProduct.information.SlsProductId });
                            $scope.SlsChartOfProduct.information.Level = lvl.Level + 1;
                        }

                    }



                    $http.post("/Sales/ChartOfProduct/Save", $scope.SlsChartOfProduct.information).success(

                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                            //init();

                            ///////////////////////////////
                            //$scope.ShowLoading = true;
                            //$http.get("/Sales/ChartOfProduct/GetAll").success(function (data) {
                            //    $scope.ShowLoading = false;
                            //    rawdata = _.sortBy(data, function (item) {
                            //        return item.SlsProductId;
                            //    });

                            //    $scope.my_data = createTreeMap(null, data);
                            //});

                            ///////////////////////////////
                            //GetCategory();
                            ///////////////////////////////

                            //current.childrens.push($scope.SlsChartOfProduct.information);
                            //$validation.reset($scope.Product);

                        }

                        ).error(function (error) {
                        });
                });
            }
            else
            {
                Erpmodal.Info("Please check the page again.");
            }

        };//end of Save

       
        $scope.Reset = function () {
            init();
            $scope.HideCatSubCat = false;
            $scope.HideSubCat = false;
            $scope.HideIsProduct = false;
            $scope.HideNoCredit = false;
            $scope.ProductDisable = false;
            $validation.reset($scope.Product);
            $scope.Product.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {

                var Id = current.Id;
                $http.post("/Sales/ChartOfProduct/Delete", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    init();
                    $validation.reset($scope.Product);
                }).error(function () {

                });// end of http
            });// end of then
        };// end of delete
    });
}).call(this);