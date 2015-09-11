






(function () {
    app.controller("fiscalYearClosing", function ($scope, $timeout, ngTableParams, $modal, $http, Erpmodal) {
            $scope.FinancialYearId = 0;
        $scope.FinancialYearIdAndName = new Object();
        $scope.FinancialYear = new Object();
        $scope.SecModules = new Object();
        $scope.SecModuleId = 0;
        $scope.LoadFinancialYear = function (fid) {
            $http.get("/Common/FinancialYear/GetAll?SecModuleId=" + 2).success(function (data) {
            }).then(function (data) {
                $scope.FinancialYearIdAndName = data.data;
                $scope.FinancialYearId = fid;
            });
        };
        // ------------get view Data------------
        var init = function () {
            $scope.LoadFinancialYear(0);
            $http({
                url: '/Security/Module/GetSecModules',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.SecModules = data;
            });
        };
        init();
        $scope.AddNew = function () {
            if ($scope.FinancialYearId == -1)
            {
                $scope.open();
            }
        };
        //open Modal
        $scope.search = {};

        $scope.open = function (size) {
            var modalInstance = $modal.open({
                templateUrl: 'modal',
                controller: ModalInstanceCtrl,
                size: size,
                resolve: {
                    search: function () {
                        $scope.search = { SecModules: $scope.SecModules};
                        return $scope.search;
                    }
                }
            });
            modalInstance.result.then(function () {
                 ;
                $scope.FinancialYear = $scope.search;
                $http.post("/Common/FinancialYear/Save", $scope.FinancialYear).success(
                    function (data) {
                        $scope.LoadFinancialYear(data.OperationId);
                        $scope.FinancialYear = {};
                    }
                    ).error(function (error) {
                    });

            }, function () {
            });

            modalInstance.opened.then(function () {
               
            }, function () {
            });




        };


        var ModalInstanceCtrl = function ($scope, $timeout, $http, $modalInstance, search) {
            $scope.search = search;
            $scope.ok = function () {
                $modalInstance.close($scope.search);
            };

            $scope.cancel = function () {
                init();
                $modalInstance.dismiss('cancel');
            };            
        };
    });

}).call(this);