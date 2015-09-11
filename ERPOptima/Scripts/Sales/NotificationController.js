
(function () {
    
    app.controller('NotificationController', ['$scope', '$http', '$location', 'Erpmodal', function ($scope, $http, $location, Erpmodal) {


        var init = function () {
           
            $http.get("/Sales/Notification/GetNewNotifications").success(function (data) {
                $scope.Resources = data;
            }).error(function (data) {
            });
        };

        init();

        var GetAll = function () {
            $http.get("/Sales/Notification/GetAll").success(function (data) {
                $scope.Resources = data;
            }).error(function (data) {
            });
        };

        var GetAllNew = function () {
            $http.get("/Sales/Notification/GetNewNotifications").success(function (data) {
                $scope.Resources = data;
            }).error(function (data) {
            });
        };

        $scope.ViewAll = function () {
            
            if($scope.IsAllCompany)
            {
                GetAll();
            }
            else
            {
                GetAllNew();
            }
        };

        $scope.changeView = function (resource) {
            $scope.ignoreNotification(resource);
        };

        $scope.ignoreNotification = function (notification) {
             
            //Set isRead to true - when user ignores the notification. 
            //So that it does not display any more in notification grid.
            $http({
                url: '/Sales/Notification/IgnoreNotification',
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                params: {
                    nId: notification.Id
                }
            }).success(function (data) {
                 
                if (data.Success) {
                    Erpmodal.Info("Notification ignored");
                    init();
                }
            }).error(function (error) {

            });
            
        };


    }]);

}).call(this);