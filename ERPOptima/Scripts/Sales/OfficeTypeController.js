
(function () {

    app.controller('OfficeType', function ($scope, $http, Erpmodal) {


        var current;
        var OfficeTypeId = 0;
        var state;
        var datas = [];
        $scope.OfficeType = new Object();
        $scope.Resources = new Object();
        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
           
            $http({
                url: '/Sales/OfficeType/GetAll',
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
            OfficeTypeId = pid.Id;
            $scope.OfficeType.Name = pid.Name;
            $scope.OfficeType.Remarks = pid.Remarks;
            $scope.OfficeType.CreatedBy = pid.CreatedBy;
            $scope.OfficeType.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
        }

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.OfficeType.Id = OfficeTypeId;
                    }
                    else { $scope.OfficeType.Id = 0; }
                     ;
                    $http.post("/Sales/OfficeType/Save", $scope.OfficeType).success(
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
            OfficeTypeId = 0;
            state = 0;
            $scope.OfficeType = {};
            $scope.OfficeTypes.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = OfficeTypeId;
                if (OfficeTypeId != 0) {
                    $http.post("/Sales/OfficeType/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");                       
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a OfficeType !"); }
            });
        };

    });

}).call(this);