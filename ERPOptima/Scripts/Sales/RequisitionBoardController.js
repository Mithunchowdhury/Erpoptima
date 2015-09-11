(function () {
    

app.controller("RequisitionBoard", function ($scope, $http, Erpmodal) {
    
    $scope.statusitems = [
    { name: 'New', Id: 0, selected: false },
    { name: 'In Progress', Id: 1, selected: false },
    { name: 'Completed', Id: 2, selected: false }
    ];

    

    var init = function () {

        $http({
            url: '/Common/Company/GetCmnCompanies',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Companies = data;
        }).error(function (data) {
        });

        $scope.GetAll();

    };//end of init
    
    $scope.selectedRow = null;  // initialize our variable to null
    $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
        $scope.selectedRow = index;
    }

    $scope.GetAll = function () {
        $http({
            url: '/Inventory/Requisition/GetAll',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Requisitions = [];
            for (var i = 0; i < data.length; i++) {
                data[i].PreferredDeliveryDate = ConverttoDateString(data[i].PreferredDeliveryDate);
                data[i].CreatedDate = ConverttoDateString(data[i].CreatedDate);
                if (data[i].ModifiedDate !== undefined)
                    data[i].ModifiedDate = ConverttoDateString(data[i].ModifiedDate);
            }
            $scope.Requisitions = data;
            $scope.RequisitionsCheck = jQuery.extend(true, {}, $scope.Requisitions);

        }).error(function (data) {
        });


    };//end of GetAll

    $scope.Reset = function () {
        $scope.SearchObj = new Object();
        $scope.Businesses.$setPristine();
        init();
    };

    //$scope.setForEdit = function (rowitem) {

    //    $http({
    //        url: '/Inventory/Requisition/Edit',
    //        method: 'GET',
    //        headers: { 'Content-Type': 'application/json' },
    //        params: {
    //            RequisitionNumber: rowitem.Id
    //        }
    //    }).success(function (data) {

    //    }).error(function (data) {
    //    });
    //};

    $scope.Search = function() {
         
        var selectedlist = [];

        for (var i = 0; i < $scope.statusitems.length; i++)
        {
            if($scope.statusitems[i].selected == true)
            {
                selectedlist.push($scope.statusitems[i].Id);               
            }
        }

        $http({
            url: '/Sales/RequisitionBoard/Search',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            params: {
                companyId: $scope.SearchObj.CompanyId, statusVal: selectedlist,
                from: $scope.SearchObj.StartDate, to: $scope.SearchObj.EndDate
            }
        }).success(function(data) {
             
            $scope.Requisitions = [];
            for (var i = 0; i < data.length; i++) {
                data[i].PreferredDeliveryDate = ConverttoDateString(data[i].PreferredDeliveryDate);
                data[i].CreatedDate = ConverttoDateString(data[i].CreatedDate);
                if (data[i].ModifiedDate !== undefined)
                    data[i].ModifiedDate = ConverttoDateString(data[i].ModifiedDate);
            }
            $scope.Requisitions = data;
            

        }).error(function (data, status) {
             
        });
        //from: $scope.SearchObj.StartDate,
        //to: $scope.SearchObj.EndDate ,
        
    };
        
    init();

});


}).call(this);