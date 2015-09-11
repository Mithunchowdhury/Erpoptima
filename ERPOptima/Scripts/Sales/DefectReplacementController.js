(function () {
    app.controller('DefectReplacementController', function ($scope, $http, Erpmodal) {
        var defectId = 0;

        var init = function () {
            defectId = 0;

            $scope.Reset();            

            $scope.LoadDropDowns();

        };//end of init

        $scope.LoadDropDowns = function () {
            $scope.GetAllDefects();            
        };

        $scope.GetAllDefects = function () {

            $http({
                url: '/Sales/DefectReplacement/GetAllDefects',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }

            }).success(function (data) {
                 
                $scope.Defects = [];
                $scope.Defects = data;
            });


        };//end of GetAll

        $scope.ResetForm = function () {
            $scope.DefectDetails = [];
        };

        $scope.Reset = function () {
            defectId = 0;
            $scope.SelDefectId = 0;
            $scope.DefectReplacementForm.$setPristine();
            $scope.ResetForm();
        };

        $scope.loadDetails = function () {
            if ($scope.SelDefectId !== undefined)
                defectId = $scope.SelDefectId;
            else
                defectId = 0;

            if (defectId >= 1) {
                $http({
                    url: '/Sales/DefectReplacement/GetDetailsByDefectId',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { defectId: defectId }
                }).success(function (data) {
                     
                    $scope.DefectDetails = [];
                    $scope.DefectDetails = data;
                });
            }
        };

        $scope.Save = function () {
            if ($scope.SelDefectId !== undefined)
                defectId = $scope.SelDefectId;
            else
                defectId = 0;

            if (defectId >= 1) {
                Erpmodal.Confirm('Save').then(function (result) {
                    
                    $http.post("/Sales/DefectReplacement/Save", $scope.DefectDetails).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                        }).error(function (error) {

                        });
                });
            }

        };//end of Save

        init();

    });
}).call(this);