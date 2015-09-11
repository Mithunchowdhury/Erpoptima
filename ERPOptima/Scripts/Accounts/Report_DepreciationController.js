(function () {
    app.controller('DepreciationController ', function ($scope, ngTableParams, $http, Erpmodal) {

        var init = function () {

        }//end of init


        init();//init is called

        $scope.setFortEdit = function (pid) {

        }
        $scope.Reset = function () {
            $scope.WebForm.$setPristine();
           

        }

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {

            });

        };//end of Save


        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {

            });
        };
    });
}).call(this);