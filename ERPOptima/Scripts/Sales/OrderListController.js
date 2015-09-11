(function () {

    app.controller('OrderListController', function ($scope, $http, Erpmodal) {

        var init = function () {
            $scope.Resources = [];
            $scope.SearchObj = new Object();
            $scope.Find();            
        };

        $scope.Find = function () {

             

            var from = $scope.SearchObj.DateFrom;
            var to = $scope.SearchObj.DateTo;

            //fetch all items of sales order and load in grid
            $http({
                url: '/Sales/Sales/FindAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: { from: from, to: to }
            }).success(function (data) {
                $scope.Resources = [];
                $scope.Resources = data;
            });
        };

        init();

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }
        

        $scope.Reset = function () {
            $scope.OrderListForm.$setPristine();
            init();            
        };

    });

}).call(this);

