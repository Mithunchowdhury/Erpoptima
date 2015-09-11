$(document).ready(function () {
    

});

app.controller("CollectionReportController", function ($scope, $http, Erpmodal) {
   
 
    $scope.StartDate = "";
    $scope.EndDateDate = "";
    $scope.OfficeId = 0;
    $scope.Parties = new Object();
    $scope.PartyType = 0;
    $scope.Party = 0;

    
    

    var state;
    function init() {

        
        $scope.StartDate = "";
        $scope.EndDate = "";

        $http({
            url: '/Sales/Office/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Office = data;
            $scope.OfficeId = 0;

        });
        

    }//end of init
    init();

    $scope.PartyTypeChangeHandler = function () {

        $scope.Parties = new Object();

        if ($scope.PartyType == 1) {
            $http({
                url: '/Sales/Distributor/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });
        }
        else if ($scope.PartyType == 2) {
            $http({
                url: '/Sales/Retailer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });
        }
        else if ($scope.PartyType == 3) {
            $http({
                url: '/Sales/Dealer/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });
        }
        else if ($scope.PartyType == 4) {
            $http({
                url: '/Sales/CorporateClient/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Parties = data;
            });
        }
       
    }


    $scope.Reset = function () {
        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.OfficeId = 0;
        $scope.PartyType = 0;
        $scope.Party = 0;
        $scope.CorporateReports.$setPristine();
      
       
             
    }
  
   
   
    

    




});
