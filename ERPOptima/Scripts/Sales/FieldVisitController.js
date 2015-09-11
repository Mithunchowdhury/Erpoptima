
(function () {

    app.controller('FieldVisit', function ($scope, $http, Erpmodal) {

        $scope.FieldVisit = new Object();
        $scope.RefNo = new Object();
        $scope.CorporateTypesList = new Object();
        $scope.CorporateTypeId = 0;
       
        
        var state;
        var FieldVisitId;

        var init = function () {
            $scope.CorporateTypeId = 0;
            $scope.ButtonDisabled = true;
            state = 0;

            //Corporate Industry Type

            $http({
                url: '/Sales/CorporateIndustryType/GetAll',
                method: 'GET',              
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.CorporateTypesList = data;
            });

            $http({
                url: '/Sales/FieldVisit/getAutoNumber',
                method: 'GET',               
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {               
                $scope.FieldVisit.RefNo = data.Refno;
            });

          
        };//end of init

         init();

        //Save
         $scope.Save = function (isValid) {
             
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                  
                    if (state == 1) {
                        $scope.FieldVisit.Id = FieldVisitId;
                    }
                    else {
                        $scope.FieldVisit.Id = 0;
                    }
                   

                    $http.post("/Sales/FieldVisit/Save", $scope.FieldVisit).success(
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
             state = 0;
             $scope.FieldVisit = {};
             $scope.FieldVisit.$setPristine();
             $scope.CorporateTypeId = 0;
         }//end of Reset

        



    });

}).call(this);
