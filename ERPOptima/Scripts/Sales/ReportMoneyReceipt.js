(function () {
    app.controller("MoneyReceiptReport", function ($scope, $http, $timeout) {

        $scope.Resources = new Object();
        $scope.CollectionEntryID = 0;

        function init() {

            $http({
                url: '/Sales/Collection/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Resources = data;
                $scope.CollectionEntryID = 0;
            });

        }
        init();
        $scope.Reset = function () {
            $scope.MoneyReceiptReport.$setPristine();
            $scope.CollectionEntryID = 0;
        }

    });

}).call(this);