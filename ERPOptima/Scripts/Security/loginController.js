//------------Log In Controller------------
app.controller("LoginController", function ($http, $scope, Erpmodal) {


    // ------------Check Login------------
    $scope.form = { LoginName: "", Password: "" };
    $scope.checkLogin = function () {
        var obj = new Object();
        // $scope.form.Password = md5($scope.form.Password);

        obj.LoginName = $scope.form.LoginName;
        obj.Password = md5($scope.form.Password);
        $http({
            url: '/Security/user/GetUserByLoginId',
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            data: obj
        }).success(function (data) {
            if (data === "1") {
                window.location.href = '/Home/SelectCompany';
            }
            else {
                Erpmodal.Warning("Incorrect user id or password !!");
            };

        }).error(function (data) {
            Erpmodal.Error("You aren't  authorized user !!");

        });
    };


});