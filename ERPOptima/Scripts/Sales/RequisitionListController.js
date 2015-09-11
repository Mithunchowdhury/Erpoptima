$(document).ready(function () {
    

});

app.controller("RequisionList", function ($scope, $http, Erpmodal) {
   
 
    $scope.Resources = [];
    $scope.StartDate = "";
    $scope.EndDate = "";
    $scope.Status = 0;
   
    
    

    var state;
    var init = function () {
        $scope.Resources = [];
        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.Status = 0;
        //$http({
        //    url: '/Inventory/Requisition/GetAll',
        //    method: 'GET',
        //    headers: { 'Content-Type': 'application/json' }
        //}).success(function (data) {
        //     
        //    $scope.Resources = data;


        //});



    };//end of init
    init();

    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    $scope.Show = function () {



        $http({
            url: '/Inventory/Requisition/Show',
            method: 'GET',
            params: {

                StartDate: $scope.StartDate,
                EndDate: $scope.EndDate,
                ApprovalStatus: $scope.Status
            },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Resources = [];
            for (var i = 0; i < data.length; i++) {
                data[i].CreatedDate = ConverttoDateString(data[i].CreatedDate);
                data[i].PreferredDeliveryDate = ConverttoDateString(data[i].PreferredDeliveryDate);

            }


            $scope.Resources = data;

        });
    }
    
    $scope.Reset = function () {
        
        $scope.Resources = [];
        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.Status = 0;
        $scope.RequisionLists.$setPristine();
       
       
             
    }
  
   
   
    

    




});
