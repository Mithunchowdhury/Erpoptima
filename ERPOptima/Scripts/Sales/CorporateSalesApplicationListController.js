$(document).ready(function () {
    

});

app.controller("CorporateList", function ($scope, $http, Erpmodal) {
   
 
    $scope.Resources = [];
    $scope.StartDate = "";
    $scope.EndDate = "";
    $scope.Status = 0;
    $scope.ApprovalStatus = 0;
   
    
    

    var state;
    var init = function () {
        $scope.Resources = [];
        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.Status = 0;
        $scope.ApprovalStatus = 0;
        $http({
            url: '/Sales/CorporateClient/GetCorporateName',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.CorporateNames = data;                                                              

            
        });



    };//end of init
    init();
    $scope.Show = function () {



        $http({
            url: '/Sales/CorporateSales/Show',
            method: 'GET',
            params: {

                StartDate: $scope.StartDate,
                EndDate: $scope.EndDate,
                ApprovalStatus: $scope.ApprovalStatus,
                Status: $scope.Status
               
            },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
             
            $scope.Resources = [];
            for (var i = 0; i < data.length; i++) {
                data[i].CreatedDate = ConverttoDateString(data[i].CreatedDate);
                

            }


            $scope.Resources = data;

        });
    }
    
    $scope.Reset = function () {
        
        $scope.Resources = [];
        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.Status = 0;
        $scope.ApprovalStatus = 0;
        $scope.CorporateLists.$setPristine();
       
       
             
    }
  
   
   
    

    




});
