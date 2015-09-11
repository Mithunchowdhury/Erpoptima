$(document).ready(function () {
    

});

app.controller("SalesReturnList", function ($scope, $http, Erpmodal) {
   
 
    $scope.Resources = [];
    $scope.StartDate = "";
    $scope.EndDateDate = "";
    

    var state;
    function init() {
        $scope.Resources = [];
        $scope.StartDate = "";
        $scope.EndDate = "";
        
        

    }//end of init
    init();

    $scope.GetAll = function () {

        $http({
            url: '/Sales/SalesReturnList/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: { StartDate: $scope.StartDate, EndDate: $scope.EndDate }

        }).success(function (data) {
             
            $scope.Lists = [];
            for (var i = 0; i < data.length; i++) {
                data[i].CreatedDate = ConverttoDateString(data[i].CreatedDate);
                
            }
             
            $scope.Resources = data;
        });


    };//end of GetAll

    $scope.Reset = function () {
        state = 0;
        $scope.ButtonDisabled = true;
        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.SalesReturnsLists.$setPristine();
       
             
    }
  
   
   
    

    




});
