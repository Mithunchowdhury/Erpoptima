(function () {
    app.controller('ExpenseController', function ($scope, ngTableParams, $http, Erpmodal) {

        var init = function () {

        }//end of init


        init();//init is called

        $scope.setFortEdit = function (pid) {

        }
        $scope.Reset = function () {
            $scope.WebForm.$setPristine();


        }

        $scope.Save = function () {
            $.blockUI({ message: '<h1><i class="fa fa-spinner fa-spin"></i> Saving...</h1>' });
            Erpmodal.Confirm('Save').then(function (result) {

            });
            $.unblockUI();

        };//end of Save


        $scope.Delete = function () {
            alert('faruk');
            Erpmodal.Confirm('Delete').then(function (result) {

            });
        };


    });
}).call(this);