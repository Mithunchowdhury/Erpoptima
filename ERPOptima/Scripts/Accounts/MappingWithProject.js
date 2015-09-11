app.controller("mappingWithProjectController", function ($scope, $http, Erpmodal) {
    $scope.Projects = [];
    $scope.SelectedProject = {};
    $scope.Businesses = new Object();
    $scope.BusinessId = 0;
    $scope.OpeningBalances = [];

    function init() {
        $scope.ShowLoading = false;
        $scope.SaveDisabled = true;
        $http({
            url: '/Accounts/ProjectClose/GetBusinesses',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.Businesses = data;
            $scope.BusinessId = 0;
        });
      
    }
    init();
    $scope.ProjectInfo = function () {

        $http.get("/Common/Project/GetProjectsByCompanyId?businessId=" + $scope.BusinessId).success(function (data) {

            $scope.Projects = data;
        }).error(function (data) {
        });
    };
    $scope.$watch('CheckedAll', function (newVal, oldVal) {
        _.each($scope.OpeningBalances, function (item) {
            item.Mapped = newVal;
        });
    });

    $scope.Change = function (index) {
        $scope.Opening = index;
       // alert(index);
    };
    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            var senditem = [];
            _.each($scope.OpeningBalances, function (item) {
                if ((item.OpeningBalanceId == "" && item.Mapped == true) || (item.OpeningBalanceId != "" && item.Mapped == false)) {
                    senditem.push(item);
                }
            });//end of each

            $http.post("/Accounts/ChartOfAccount/MapTransactionalHead",
                {
                    listAnFTransactionalHeadViewModel: senditem,
                    projectId: $scope.SelectedProject
                }).success(function (data) {
                    Erpmodal.Save(data,'Save');

                    //$scope.Reset();
                }).error(function (data) {

                    //$scope.Reset();
                });
        });//end of then
    };//end of save
    $scope.$watch('SelectedProject', function (newVal, oldVal) {
        if (!angular.equals({}, newVal)) {
            $scope.ShowLoading = true;
            //$.blockUI({ css: { border: 'none', padding: '15px', backgroundColor: '#000', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', opacity: .5, color: '#fff' } });
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Just a moment...</h1>' });
            $http.get("/Accounts/ChartOfAccount/GetTransactionalHeadByProjectId?projectId=" + newVal).success(function (data) {
                $.unblockUI();
                $scope.OpeningBalances = data;
                $scope.SaveDisabled = false;
                $scope.ShowLoading = false;
            }).error(function (data) {
            });
        }
    });//end of watch

    $scope.Reset = function () {
        $scope.SelectedProject = {};
        $scope.OpeningBalances = [];
        $scope.SaveDisabled = true;
    };//end of reset
});