app.controller("DashBoardController", function ($scope, $http) {




    $scope.dashboard = [];


    var init = function () {

        $http.get("/Accounts/DashBoard/GetDashBoardResult").success(function (data) {

            $scope.dashboard = data;


        }).error(function (data) {





        });



    };// end of init

    init();






});