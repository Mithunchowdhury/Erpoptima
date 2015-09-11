(function () {
    app.controller("DeliveryChallen", function ($scope, $http, $timeout) {

        $scope.Challen = new Object();
        $scope.DeliveryId = 0;



        function init() {

            $http({
                url: '/Sales/Delivery/GetChallanList',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                params: { DeliveryId: $scope.DeliveryId }
            }).success(function (data) {
                $scope.Challen = data;
                $scope.DeliveryId = 0;
            });


        }
        init();
        $scope.Reset = function () {
            $scope.DeliveryChallen.$setPristine();
            $scope.DeliveryId = 0;

        }

    });

}).call(this);