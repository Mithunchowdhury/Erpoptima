
(function () {
    app.controller('passwordChange', function ($scope,$sce, $http, Erpmodal) {

        
        var UserId = 0;       
        $scope.User = new Object();
        var init = function () {           
            $http.get("/Security/User/GetUserById").success(function (data) {
                data[0].CreatedDate = ConverttoDateString(data[0].CreatedDate);
                $scope.User = data[0];
                $scope.User.Password = '';
            });
        }//end of init     

        init();//init is called        

        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if ($scope.User.Password == $scope.User.ConfirmPassword) {
                   
                    $scope.User.Password = md5($scope.User.Password);
                    $http.post("/Security/User/SaveUser", $scope.User).success(
                        function (data) {

                            Erpmodal.Save(data, "Save");                         
                            init();
                        }
                        ).error(function (error) {
                        });
                }
                else {
                    Erpmodal.Warning("Password and Confirm Password must be equal !");
                }
            });

        };//end of Save

        $scope.Reset = function () {
            $scope.User.Password = '';
            $scope.User.ConfirmPassword = '';
            $scope.User.OldPassword = '';
            //$scope.User = {};
           
        }//end of Reset

        
    });

}).call(this);