
(function () {

    app.controller('Region', function ($scope, $http, Erpmodal) {


        var current;
        var RegionId = 0;
        var state;
        var datas = [];
        $scope.Region = new Object();
        $scope.Employee = new Object();
        
        $scope.Resources = new Object();
        $scope.EmployeeId = 0;

        var init = function () {
            $scope.EmployeeId = 0;
            $scope.ButtonDisabled = true;
            state = 0;

            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 
                $scope.Employee = data;
                $scope.EmployeeId = 0;
            });
            $http({
                url: '/Sales/Region/GetAll?employeeId="' + $scope.EmployeeId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                  
                $scope.Resources = data;
            });
            //$http({
            //    url: '/Sales/Region/ GetRegionByEmployee',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //     
            //    $scope.Employee = data;


            //});

        }//end of init



        init();//init is called

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (pid) {
             ;
            state = 1;
            RegionId = pid.Id;
            $scope.Region.Code = pid.Code;
            $scope.Region.Name = pid.Name;
            $scope.Region.Head = pid.Head;
            $scope.Region.Remarks = pid.Remarks;
            $scope.Region.CreatedBy = pid.CreatedBy;
            $scope.Region.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
            $scope.EmployeeId = pid.Head;
        }
        $scope.LoadOfficeInfo = function () {
            $scope.RegionInfo();
        };
       
        $scope.RegionInfo = function () {
            var did = $scope.EmployeeId;
            $http.get("/Sales/Region/GetAll?employeeId=" + $scope.EmployeeId).success(
                      function (data) {

                          $scope.Resources = data;
                      }
                      ).error(function (error) {
                      });
        };
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.Region.Id = RegionId;
                }
                else { $scope.Region.Id = 0; }
                
                $scope.Region.Head = $scope.EmployeeId;

                $http.post("/Sales/Region/Save", $scope.Region).success(
                    function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        $scope.Reset = function () {
            init();
            $scope.ButtonDisabled = true;
            RegionId = 0;
            state = 0;
            $scope.Region = {};
            $scope.Regions.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = RegionId;
                if (RegionId != 0) {
                    $http.post("/Sales/Region/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a Region !"); }
            });
        };

    });

}).call(this);