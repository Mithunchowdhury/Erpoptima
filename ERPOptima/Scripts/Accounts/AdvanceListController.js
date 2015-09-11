
(function () {

    app.controller('AdvanceListController', function ($scope, $http, Erpmodal) {

        
        
       
        var state;
        
        $scope.RefNo = new Object();
        $scope.StartDate=null;
        $scope.EndDate=null;
        $scope.EmployeeId = 0;
        $scope.Resources = [];
     
        var state = 0;
        var AdvanceId = 0;

        var init = function () {
            
            $scope.ButtonDisabled = true;
            $scope.Resources = [];
            state = 0;

            //Advance List

            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
               
                $scope.Employee = data;
            });

          
        }//end of init
      
        init();// int is called
        //$.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });
        //$.unblockUI();

        

        $scope.Load = function () {

            //$.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Loading...</h1>' });

            $http({

                url: '/Accounts/Advance/GetAllAdvanceList',
                method: 'GET',
                params: {
                    employeeId: $scope.EmployeeId,
                    startDate: $scope.StartDate,
                    endDate: $scope.EndDate
                    
                },

                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                debugger;
                $scope.Resources = [];
                for (var i = 0; i < data.length; i++) {
                   
                    data[i].Date = ConverttoDateString(data[i].Date);
                }

               
                $scope.Resources = data;
                //$.unblockUI();
               
            });
        }        

        
         $scope.Reset = function () {
             init();
            
             state = 0;
             $scope.StartDate = null;
             $scope.EndDate = null;
             $scope.EmployeeId = 0;
             $scope.AdvanceLists.$setPristine();

         }//end of Reset


    });

}).call(this);
