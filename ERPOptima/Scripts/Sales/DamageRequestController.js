
(function () {

    app.controller('Damage', function ($scope, $http, Erpmodal) {

        var state;
        $scope.DamageRequest = new Object();
        $scope.Products = new Object();//For Dropdown product name
        $scope.Product = new Object();
        $scope.Units = new Object(); // For Dropdown unit name
        $scope.Unit = new Object();
        $scope.Damage = new Object();// For RefId parent obj
        $scope.Damages = []; // Add rows
        var init = function () {

            $scope.ButtonDisabled = true;
            state = 0;

            //RefNo
            $http({
                url: '/Sales/DamageRequest/GetAutoNumber',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Damage.RefNo = data.Refno;
            });

           
            //get all product name

            $http({
                url: '/Sales/ChartOfProduct/GetProductsConfigured',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Products = data;


            });
           
            


           


        };
        //end of init

        init();

       
        $scope.AddRow = function () {
            var row = {
                Id: 0,
                InvDamageId: 0,
                SlsProductId: angular.fromJson($scope.Product).Id,
                ProductName: angular.fromJson($scope.Product).Name,
                Quantity: $scope.DamageRequest.Quantity,
                SlsUnitId: angular.fromJson($scope.Unit).SlsUnitId,
                UnitName: angular.fromJson($scope.Unit).Unit,
                Reason: $scope.DamageRequest.Reason
                
            };
         
            if (!CheckDuplicateProduct(angular.fromJson($scope.Product).Id)) {
                $scope.Damages.push(row);
            }
            

        };
        function CheckDuplicateProduct(productId) {

            for (var i = 0; i < $scope.Damages.length; i++) {
                if ($scope.Damages[i].SlsProductId == productId) {
                    return true;
                }
            }

            return false;


        }//end of CheckDuplicateProduct

        $scope.removeRow = function (item) {
            var index = $scope.Damages.indexOf(item);
            if (index > -1) {
                $scope.Damages.splice(index, 1);//1 row have to remove
            }
        };

        $scope.GetUnitsByProductId = function () {
            $http({
                url: '/Sales/ProductUnits/GetSlsProductUnitsBySlsProductId?productId=' + angular.fromJson($scope.Product).Id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                               
                $scope.Units=data;


            }).error(function (data) {
            });
        };

        //Save
        $scope.Save = function () {
          
            Erpmodal.Confirm('Save').then(function (result) {
               
                if (state == 1) {
                    $scope.Damage.Id = InvDamageId;
                }
                else { $scope.Damage.Id = 0; }
                
                $scope.Damage.InvDamageDetails = $scope.Damages;
                $http.post("/Sales/DamageRequest/Save", $scope.Damage).success(
                    function (data) {
                        if (data.Success) {
                            //$scope.GetUnitsByProductId();
                            
                        }
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });
        };//end of Save

       

        $scope.Reset = function () {
            //init();
            $scope.Damage = new Object();
            state = 0;
            $scope.ButtonDisabled = true;
            $scope.Damages = [];
            $scope.Product = new Object();;
            $scope.Unit = new Object();
            $scope.DamageRequest = new Object();
            $scope.Damage.$setPristine();
            
            $http({
                url: '/Sales/DamageRequest/GetAutoNumber',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Damage.RefNo = data.Refno;
            });


        };//end of Reset

        $scope.Clear = function () {

            $scope.Product = new Object();
            $scope.Unit = new Object();
            $scope.DamageRequest = new Object();
        };

    });
    

}).call(this);
