
(function () {

    app.controller('UnitOfMeasurement', function ($scope, $http, Erpmodal) {


        var current;
        var UnitofMeasurementId = 0;
        var state;
        var datas = [];
        $scope.UnitOfMeasurement = new Object();
        $scope.Resources = new Object();
        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
           
            $http({
                url: '/Sales/UnitOfMeasurement/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                 ;
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
            UnitOfMeasurementId = pid.Id;
            $scope.UnitOfMeasurement.Name = pid.Name;
            $scope.UnitOfMeasurement.ShortName = pid.ShortName;
            $scope.UnitOfMeasurement.CreatedBy = pid.CreatedBy;
            $scope.UnitOfMeasurement.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
        }

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.UnitOfMeasurement.Id = UnitOfMeasurementId;
                    }
                    else { $scope.UnitOfMeasurement.Id = 0; }
                     ;
                    $http.post("/Sales/UnitOfMeasurement/Save", $scope.UnitOfMeasurement).success(
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
            UnitOfMeasurementId = 0;
            state = 0;
            $scope.UnitOfMeasurement = {};
            $scope.UnitOfMeasurements.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = UnitOfMeasurementId;
                if (UnitOfMeasurementId != 0) {
                    $http.post("/Sales/UnitOfMeasurement/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a UnitOfMeasurement !"); }
            });
        };

    });

}).call(this);