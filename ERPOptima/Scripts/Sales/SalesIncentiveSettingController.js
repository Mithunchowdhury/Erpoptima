(function () {

    app.controller('SalesIncentiveSettings', function ($scope, $http, Erpmodal) {
       
        
        //$scope.SalesIncentive = new Object();// input a  value for page
        $scope.SlsIncentiveSetting = new Object();
        $scope.Resources = new Object();
        var state;// use to 3 times..Eidt,int() and save
        //var datas = [];
        var SalesIncentiveId = 0;// store Id for eidt
      
        

        var init = function () {
            $scope.SlsIncentiveSetting = new Object();
            $scope.ButtonDisabled = true;
            state = 0;
            $http({
                url: '/Sales/SalesIncentiveSettings/GetAll',
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
            SalesIncentiveId = pid.Id;
            $scope.SlsIncentiveSetting.LowerLimit = pid.LowerLimit;
            $scope.SlsIncentiveSetting.UpperLimit = pid.UpperLimit;
            $scope.SlsIncentiveSetting.CommissionPercentage = pid.CommissionPercentage;
            $scope.SlsIncentiveSetting.Remarks = pid.Remarks;
            $scope.SlsIncentiveSetting.CreatedBy = pid.CreatedBy;
            $scope.SlsIncentiveSetting.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
            
            

        }
       
        
        $scope.Save = function (isValid) {
              
                if (isValid)
                    Erpmodal.Confirm('Save').then(function (result) {
                        if (state == 1) {
                            $scope.SlsIncentiveSetting.Id = SalesIncentiveId;
                        }
                        else { $scope.SlsIncentiveSetting.Id = 0; }
                         
                        $http.post("/Sales/SalesIncentiveSettings/Save", $scope.SlsIncentiveSetting).success(
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
                var Id = SalesIncentiveId;
                if (SalesIncentiveId != 0) {
                    $http.post("/Sales/SalesIncentiveSettings/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Incentive !"); }
            });
        };


        $scope.Reset = function () {
            $scope.SalesIncentives.$setPristine();
        
            init();
           
           

            
        }//end of Reset

        
    });

}).call(this);