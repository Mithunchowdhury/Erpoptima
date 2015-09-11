
(function () {

    app.controller('OfficeController', function ($scope, $http, Erpmodal) {

        var state;
        var OfficeId;

        $scope.OfficeTypeList = new Object();
        $scope.RegionList = new Object();
        $scope.Offices = new Object();

        $scope.Office = new Object();
        $scope.CodeState = false;        

        var init = function () {
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.CodeState = false;
            $http({
                url: '/Sales/OfficeType/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.OfficeTypeList = data;

            });

            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.OfficeList = data;
            });

            $http({
                url: '/Hrm/Employee/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.EmployeeList = data;
            });

            $http({
                url: '/Sales/Office/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Offices = data;
            });

            $http({
                url: '/Sales/Region/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.RegionList = data;
            });

        }//end of init



        init();//init is called

        $scope.setFortEdit = function (rowitem) {
            state = 1;
            OfficeId = rowitem.Id;
            $scope.Office.Name = rowitem.Name;
            $scope.Office.OfficeTypeId = rowitem.SlsOfficeTypeId;
            $scope.Office.Code = rowitem.Code;
            $scope.Office.RegionId = rowitem.SlsRegionId;
            $scope.Office.Description = rowitem.Description;
            $scope.Office.HeadOfficeId = rowitem.Head;
            $scope.Office.InChargeId = rowitem.InCharge;
            $scope.Office.Address = rowitem.Address;
            $scope.Office.Phone = rowitem.Phone;
            $scope.Office.Remarks = rowitem.Remarks;

            $scope.Office.CreatedBy = rowitem.CreatedBy;
            $scope.Office.CreatedDate = ConverttoDateString(rowitem.CreatedDate);

            $scope.ButtonDisabled = false;
            $scope.CodeState = true;
        }

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }


        $scope.Reset = function () {
            
            init();
            $scope.ButtonDisabled = true;
            OfficeId = 0;
            state = 0;
            $scope.Office = {};
            $scope.OfficeForm.$setPristine();

            
        }//end of Reset

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.Office.Id = OfficeId;
                    }
                    else { $scope.Office.Id = 0; }
                    
                    $scope.Office.SlsRegionId = $scope.Office.RegionId;
                    $scope.Office.SlsOfficeTypeId = $scope.Office.OfficeTypeId;
                    $scope.Office.Head = $scope.Office.HeadOfficeId;
                    $scope.Office.InCharge = $scope.Office.InChargeId;

                    $http.post("/Sales/Office/Save", $scope.Office).success(
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
                var Id = OfficeId;
                if (OfficeId != 0) {
                    $http.post("/Sales/Office/Delete", { Id: Id }).success(function (data) {
                        Erpmodal.Delete(data, "Delete");
                        $scope.Reset();
                    }).error(function () {

                    })
                }
                else { Erpmodal.Warning("Please select an Office !"); }
            });
        };//end of Delete
                
    });

    app.directive('ngUnique', ['$http', function (async) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keyup', function (evt) {
                    scope.$apply(function () {
                        var val = elem.val();
                        
                            var req = { "officeCode": val }

                            var ajaxConfiguration = { method: 'POST', url: '/Sales/Office/IsOfficeCodeAvailable', data: req };
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