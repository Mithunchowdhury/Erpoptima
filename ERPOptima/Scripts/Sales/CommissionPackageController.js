(function () {

    app.controller("CommissionPackageController", function ($scope, $http, Erpmodal) {

        $scope.MainObj = new Object();
        $scope.MainList = [];

        $scope.Years = new Object();
        $scope.Months = new Object();

        var state;
        var MainId = 0;

        function init() {
            initForm();
            
            GetAll(false);
            GetYears(true);
            GetMonths(true);
            
        };//end of init

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        function initForm() {
            state = 0;
            MainId = 0;
            $scope.ButtonDisabled = true;
            $scope.MainObj = new Object();            
        };

        init();

        $scope.setForEdit = function (rowitem) {

            initForm();
            $scope.ButtonDisabled = false;
            
            state = 1;
            MainId = rowitem.Id;

            $scope.MainObj = jQuery.extend(true, {}, rowitem); //rowitem;
            $scope.MainObj.Year = rowitem.Year;
            $scope.MainObj.Month = rowitem.Month;
            $scope.MainObj.CreatedDate = ConverttoDateString(rowitem.CreatedDate);
            $scope.MainObj.ModifiedDate = ConverttoDateString(rowitem.ModifiedDate);
        };


        $scope.Reset = function () {            
            initForm();
            GetYears(true);
            GetMonths(true);
            $scope.CommissionPackageForm.$setPristine();
        };//end of Reset

        function GetYears(selectElem) {

            $http({
                url: '/Sales/CommissionPackage/GetYears',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Years = data;
                if (selectElem) {
                    $scope.MainObj.Year = $scope.Years[0].Id;
                }
            });


        };//end of GetYears

        function GetMonths(selectElem) {

            $http({
                url: '/Sales/CommissionPackage/GetMonths',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Months = data;
                if (selectElem) {
                    $scope.MainObj.Month = $scope.Months[0].Id;
                }
            });


        };//end of GetMonths

        function GetAll(selectElem) {

            $http({
                url: '/Sales/CommissionPackage/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.MainList = data;
            });


        };//end of GetAll

        $scope.ShowMonth = function (actualId, checkId) {            
            if(actualId === checkId)
            {
                return true;
            }
            return false;
        };

        $scope.Save = function (isValid) {
            
            if (isValid) {
                if (!$scope.DuplicateMainRecord()) {
                    Erpmodal.Confirm("Save").then(function (result) {
                        if (state == 1) {
                            $scope.MainObj.Id = MainId;
                        }
                        else {
                            $scope.MainObj.Id = 0;
                        }
                        $scope.MainObj.DetailList = $scope.Resources2;
                        $http.post("/Sales/CommissionPackage/Save",
                                $scope.MainObj
                            ).success(function (data) {
                                Erpmodal.Save(data, "Save");
                                init();
                                $scope.Reset();
                            }).error(function (data) {

                            });
                    });
                }
                else {
                    Erpmodal.Warning("Duplicate record!");
                }
            }
        };//end of Save

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = MainId;                
                if (MainId != 0) {
                    $scope.MainObj.Id = Id;
                    $http.post("/Sales/CommissionPackage/Delete", $scope.MainObj).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Delete Failed!"); }
            });
        };//end of Delete

        $scope.DuplicateMainRecord = function () {
            for (rowId = 0; rowId < $scope.MainList.length; ++rowId) {
                
                if ($scope.MainObj.Id <= 0 &&
                    $scope.MainList[rowId].Year == $scope.MainObj.Year &&
                    $scope.MainList[rowId].Month == $scope.MainObj.Month)
                {
                    return true;
                }
            }
            return false;
        };

    });

}).call(this);