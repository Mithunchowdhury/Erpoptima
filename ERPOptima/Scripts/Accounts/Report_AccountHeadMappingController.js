﻿(function () {
    app.controller('RptAcHdMappingController', function ($scope, ngTableParams, $http, Erpmodal) {

        var init = function () {

        }//end of init


        init();//init is called

        $scope.setFortEdit = function (pid) {

        }
        $scope.Reset = function () {
            $scope.HeadMappingForm.$setPristine();


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