(function () {
    app.controller("ReportInvoice", function ($scope, $http, $timeout) {

        $scope.Invoice = new Object();
        $scope.InvoiceId = 0;



        function init() {

            //$http({
            //    url: '/Sales/ReportInvoice/GetAll',
            //    method: 'GET',
            //    headers: { 'Content-Type': 'application/json' }
            //}).success(function (data) {
            //    $scope.Invoice = data;
            //    $scope.InvoiceId = 0;
            //});

            $http({
                url: '/Sales/Delivery/GetInvoiceList',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Invoice = data;
                $scope.InvoiceId = 0;
            });


        }
        init();
        $scope.Reset = function () {
            //init();
            $scope.InvoiceId = 0;
            $scope.ReportInvoice.$setPristine();

        }

    });

}).call(this);