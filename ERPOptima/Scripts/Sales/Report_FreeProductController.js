(function () {
    app.controller("FreeProduct", function ($scope, $http, $timeout) {

        $scope.Product = new Object();
        $scope.ProductId = 0;



        function init() {

            $http({
                url: '/Sales/FreeProduct/GetAllFreeProducts',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Product = data;
                $scope.ProductId = 0;
            });


        }
        init();
        $scope.Reset = function () {
            $scope.FreeProduct.$setPristine();
           ProductId = 0;

        }

    });

}).call(this);