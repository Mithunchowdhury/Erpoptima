
(function () {

    app.controller('CorporateIndustryType', function ($scope, $http, Erpmodal) {


        var current;
        var CorporateIndustryTypeId = 0;
        var state;
        var datas = [];
        $scope.CorporateIndustryType = new Object();
        $scope.Resources = new Object();
        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;

            $http({
                url: '/Sales/CorporateIndustryType/GetAll',
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
            CorporateIndustryTypeId = pid.Id;
            $scope.CorporateIndustryType.Type = pid.Type;
            $scope.CorporateIndustryType.Description = pid.Description;
            $scope.CorporateIndustryType.CreatedBy = pid.CreatedBy;
            $scope.CorporateIndustryType.CreatedDate = ConverttoDateString(pid.CreatedDate);
            $scope.ButtonDisabled = false;
        }

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.CorporateIndustryType.Id = CorporateIndustryTypeId;
                    }
                    else { $scope.CorporateIndustryType.Id = 0; }
                
                    $http.post("/Sales/CorporateIndustryType/Save", $scope.CorporateIndustryType).success(
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
            CorporateIndustryTypeId = 0;
            state = 0;
            $scope.CorporateIndustryType = {};
            $scope.CorporateIndustryTypes.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = CorporateIndustryTypeId;
                if (CorporateIndustryTypeId != 0) {
                    $http.post("/Sales/CorporateIndustryType/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select a CorporateIndustryType !"); }
            });
        };

    });

}).call(this);