
(function () {


    app.controller('ProductServiceTax', function ($scope, ngTableParams, $http, Erpmodal) {

        var state;
        var datas = [];
        var ProductServiceTaxId = 0;
        $scope.ProductServiceTax = {};


        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.AnFProductServiceTax = [];
            $scope.ProductServiceTax.Status = true;
           
            $http.get("/Accounts/ProductOrServiceTax/GetProductOrServiceTaxes").success(function (data) {
                datas = [];
                datas = data;
                $scope.tableParams.reload();
            });
        }//end of init

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10           // count per page
        }, {
            total: datas.length, // length of data
            getData: function ($defer, params) {
                $defer.resolve(datas.slice((params.page() - 1) * params.count(), params.page() * params.count()));

            }
        });
        init();//init is called

        $scope.setFortEdit = function (pid) {
            $scope.mode = true;
            state = 1;
            ProductServiceTaxId = pid.Id;
            $scope.ProductServiceTax.Name = pid.Name;
            $scope.ProductServiceTax.Percentage = pid.Percentage;
            $scope.ProductServiceTax.Remarks = pid.Remarks;
            $scope.ProductServiceTax.Status = pid.Status;         
            $scope.ProductServiceTax.CreatedBy = pid.CreatedBy;
            $scope.ProductServiceTax.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
        }
        $scope.Reset = function () {
            
            init();
            ProductServiceTaxId = 0;
            state = 0;
            $scope.ProductServiceTax = {};
            $scope.ButtonDisabled = true;
            $scope.ProductServiceTax.Status = true;
            $scope.ProductServiceTaxes.$setPristine();
        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.ProductServiceTax.Id = ProductServiceTaxId;
                }
                else { $scope.ProductServiceTax.Id = 0; }

                $http.post("/Accounts/ProductOrServiceTax/SaveProductOrServiceTax", $scope.ProductServiceTax).success(

                    function (data) {
                        Erpmodal.Save(data, "Save");
                        
                        $scope.Reset();

                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        
        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = ProductServiceTaxId;
                $http.post("/Accounts/ProductOrServiceTax/DeleteAnFProductOrServiceTax", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data, "Delete");
                    
                    $scope.Reset();
                   
                }).error(function () {

                })
            });
        };
    });

}).call(this);