
(function () {

    app.controller('DefectEntry', function ($scope, $http, Erpmodal) {

        var state;
        $scope.DefectEntry = new Object();
        $scope.Products = new Object();//For Dropdown product name
        $scope.Product = new Object();
        $scope.Units = new Object(); // For Dropdown unit name
        $scope.Unit = new Object();
        //$scope.Defect = new Object();// For RefId parent obj
        $scope.Defects = []; // Add rows
        var init = function () {

            $scope.ButtonDisabled = true;
            state = 0;

            //RefNo
            $http({
                url: '/Sales/DefectEntry/GetAutoNumber',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.DefectEntry.RefNo = data.Refno;
            });

           
            //get all product name

            $http({
                url: '/Sales/ChartOfProduct/GetProducts',
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
                SlsDefectId: 0,
                SlsProductId: angular.fromJson($scope.Product).Id,
                ProductName: angular.fromJson($scope.Product).Name,
                Quantity: $scope.DefectEntry.Quantity,
                SlsUnitId: angular.fromJson($scope.Unit).SlsUnitId,
                UnitName: angular.fromJson($scope.Unit).Unit,
               
            };
             
            if (!CheckDuplicateProduct(angular.fromJson($scope.Product).Id)) {
                $scope.Defects.push(row);
            }
            

        };
        function CheckDuplicateProduct(productId) {

            for (var i = 0; i < $scope.Defects.length; i++) {
                if ($scope.Defects[i].SlsProductId == productId) {
                    return true;
                }
            }

            return false;


        }//end of CheckDuplicateProduct

        $scope.removeRow = function (item) {
            var index = $scope.Defects.indexOf(item);
            if (index > -1) {
                $scope.Defects.splice(index, 1);//1 row have to remove
            }
        };

        $scope.GetProductUnits = function () {
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
                    $scope.DefectEntry.Id = SlsDefectId;
                }
                else { $scope.DefectEntry.Id = 0; }
                
                $scope.DefectEntry.SlsDefectDetails = $scope.Defects;
                $http.post("/Sales/DefectEntry/Save", $scope.DefectEntry).success(
                    function (data) {
                        if (data.Success) {
                           
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
            state = 0;
            $scope.ButtonDisabled = true;
            $scope.Defects = [];
            $scope.Product = new Object();;
            $scope.Unit = new Object();
            $scope.DefectEntry = new Object();
            $scope.DefectEntries.$setPristine();
            
            $http({
                url: '/Sales/DefectEntry/GetAutoNumber',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.DefectEntry.RefNo = data.Refno;
            });


        };//end of Reset

        $scope.Clear = function () {

            $scope.Product = new Object();
            $scope.Unit = new Object();
            $scope.DefectEntry = new Object();
        };

    });
    

}).call(this);
