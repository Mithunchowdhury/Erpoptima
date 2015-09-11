
(function () {

    app.controller('FieldVisitList', function ($scope, $http, Erpmodal) {

        
        
        var current;
        var state;
        var datas = [];
        $scope.FieldVisit = new Object();
        $scope.Employee = new Object();
        $scope.RefNo = new Object();
        $scope.StartDate=null;
        $scope.EndDate=null;
        $scope.EmployeeId = 0;
        $scope.Requisitions = [];
     
        var state = 0;
        var FieldVisitId = 0;

        var init = function () {
            $scope.ButtonDisabled = true;
            $scope.Requisitions = [];
            state = 0;

            //Field visit List

            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Employee = data;
            });

            $http({
                url: '/Sales/FieldVisit/GetAll?employeeId="' + $scope.EmployeeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
               
            });
          
        }//end of init

        init();// int is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (pid) {
             ;
            state = 1;
            FieldVisitId = pid.Id;
            $scope.FieldVisit.RefNo = pid.RefNo;
            $scope.FieldVisit.CustomerName = pid.CustomerName;
            $scope.FieldVisit.CustomerMobileNo = pid.CustomerMobileNo;
            $scope.FieldVisit.FollowUpDate = pid.FollowUpDate;
            $scope.FieldVisit.VisitDate = pid.VisitDate;
            $scope.FieldVisit.HrmEmployeeId = pid.HrmEmployeeId;
            $scope.FieldVisit.CustomerDetails = pid.CustomerDetails;
            $scope.ButtonDisabled = false;
            $scope.EmployeeId = pid.HrmEmployeeId;
        }
        $scope.Load = function () {



            $http({
                url: '/Sales/FieldVisit/GetFieldVisitList',
                method: 'GET',
                params: {
                    employeeId: $scope.EmployeeId,
                    startDate: $scope.StartDate,
                    endDate: $scope.EndDate
                },
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Requisitions = [];
                for (var i = 0; i < data.length; i++) {
                    data[i].FollowupDate = ConverttoDateString(data[i].FollowupDate);
                    data[i].VisitDate = ConverttoDateString(data[i].VisitDate);
                }


                $scope.Requisitions = data;
               
            });
        }        

        
         $scope.Reset = function () {
             init();
            
             state = 0;
             VisitedId = 0;
             FieldVisitId = 0;
             $scope.FieldVisit = {};
             $scope.StartDate = null;
             $scope.EndDate = null;
             $scope.EmployeeId = 0;
             $scope.FieldVisitList.$setPristine();

         }//end of Reset

        



    });

}).call(this);
