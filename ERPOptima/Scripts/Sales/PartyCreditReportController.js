app.controller("PartyCreditReportController", function ($http, $scope) {

    $scope.typeId = 0;
    $scope.partyId = 0;

    $scope.Parties = [];

    var init = function () {       
       


    };// end of init

    init();


    $scope.Reset = function () {
        $scope.PartyCreditReport.$setPristine();
        $scope.typeId = 0;
        $scope.partyId = 0;


    }

    $scope.GetParty = function () {      
        if ($scope.typeId == 1) {
            $http({
                url: '/Sales/Distributor/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });

        }

        else if ($scope.typeId == 2) {
            $http({
                url: '/Sales/Retailer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });


        }
        else if ($scope.typeId == 3) {
            $http({
                url: '/Sales/CorporateClient/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });
        }
        else if ($scope.typeId == 4) {
            $http({
                url: '/Sales/Dealer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });
        }       
        else {
            $scope.partyId = 0;
        }
      
    }












});