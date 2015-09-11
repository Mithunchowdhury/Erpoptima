(function () {

    app.controller('GeneralDiscount', function ($scope, $http, Erpmodal) {
       
        
        $scope.GeneralDiscount = new Object();// input a  value for page
        $scope.Resources = new Object();
        var state;// use to 3 times..Eidt,int() and save
        //var datas = [];
        var GeneralDiscountId=0;// store Id for eidt
      
        

        var init = function () {
          
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Sales/Region/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }

            }).success(function (data) {
                
                $scope.Regions = data;
            });
            loadGridData();
          
            
                    
        }//end of init

        var loadGridData = function () {
            $http({
                url: '/Sales/GeneralDiscount/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }

            }).success(function (data) {

                $scope.Resources = data;
            });
        }

        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (pid) {
            state = 1;
            GeneralDiscountId = pid.Id;
            $scope.GeneralDiscount.Discount = pid.Discount;
            $scope.GeneralDiscount.SlsRegionId = pid.SlsRegionId;
            $scope.GeneralDiscount.CreatedBy = pid.CreatedBy;
            $scope.GeneralDiscount.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
            
            

        }
       
        
        $scope.Save = function (isValid) {
             
                if (isValid)
                    Erpmodal.Confirm('Save').then(function (result) {
                        if (state == 1) {
                            $scope.GeneralDiscount.Id = GeneralDiscountId;
                        }
                        else { $scope.GeneralDiscount.Id = 0; }
                        
                        $http.post("/Sales/GeneralDiscount/Save", $scope.GeneralDiscount).success(
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
                var Id = GeneralDiscountId;
                if (GeneralDiscountId != 0) {
                    $http.post("/Sales/GeneralDiscount/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Region !"); }
            });
        };


        $scope.Reset = function () {
        
            loadGridData();
            $scope.ButtonDisabled = true;
            state = 0;
            GeneralDiscountId = 0;
            $scope.GeneralDiscount = {};
            $scope.GeneralDiscounts.$setPristine();
           

            
        }//end of Reset

        
    });

}).call(this);