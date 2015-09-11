(function () {

    app.controller('SalesDiscountSetting', function ($scope, $http, Erpmodal) {
       
        
    
        $scope.SlsDiscountSetting = new Object();// input a  value for page
        $scope.Resources = new Object();
        var state;// use to 3 times..Eidt,int() and save
        //var datas = [];
        var SalesDiscountId = 0;// store Id for eidt
      
        

        var init = function () {
            $scope.SlsDiscountSetting = new Object();// for refreash page
            $scope.ButtonDisabled = true;
            state = 0;
            
            $http({
                url: '/Sales/SalesDiscountSetting/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }

            }).success(function (data) {

                $scope.Resources = data;
            });
           
          
            
                    
        }//end of init
       

        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (pid) {
            state = 1;
            SalesDiscountId = pid.Id;
            $scope.SlsDiscountSetting = pid;
            //$scope.SlsDiscountSetting.Title = pid.Title;
            //$scope.SlsDiscountSetting.LowerLimit = pid.LowerLimit;
            //$scope.SlsDiscountSetting.UpperLimit = pid.UpperLimit;
            //$scope.SlsDiscountSetting.DiscountPercentage = pid.DiscountPercentage;
            //$scope.SlsDiscountSetting.Remarks = pid.Remarks;
            //$scope.SlsDiscountSetting.CreatedBy = pid.CreatedBy;
            $scope.SlsDiscountSetting.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
            
            

        }
       
        
        $scope.Save = function (isValid) {
              
                if (isValid)
                    Erpmodal.Confirm('Save').then(function (result) {
                        if (state == 1) {
                            $scope.SlsDiscountSetting.Id = SalesDiscountId;
                        }
                        else { $scope.SlsDiscountSetting.Id = 0; }
                         
                        $http.post("/Sales/SalesDiscountSetting/Save", $scope.SlsDiscountSetting).success(
                            function (data) {
                                Erpmodal.Save(data, "Save");
                                $scope.Reset();
                                init();
                            }
                            ).error(function (error) {
                            });
                    });
           

        };//end of Save
        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = SalesDiscountId;
                if (SalesDiscountId != 0) {
                    $http.post("/Sales/SalesDiscountSetting/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        init();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a SalesDiscount !"); }
            });
        };
        //end of Delete

        $scope.Reset = function () {
            $scope.SalesDiscounts.$setPristine();
            init();
           
            
        }//end of Reset

        
    });

    app.directive('ngUnique', ['$http', function (async) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keyup', function (evt) {
                    scope.$apply(function () {
                        var val = elem.val();

                        var req = { "title": val }

                        var ajaxConfiguration = { method: 'POST', url: '/Sales/SalesDiscountSetting/IsTitleAvailable', data: req };
                        async(ajaxConfiguration)
                            .success(function (data, status, headers, config) {
                                ctrl.$setValidity('unique', data.result);
                            });

                    });
                });
            }
        }
    }]);


}).call(this);