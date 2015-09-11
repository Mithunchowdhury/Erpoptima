app.service('crudservice', function ($scope, $http, Erpmodal) {

    this.GetAllURL = '';
    this.SaveURL = '';
    this.DeleteURL = '';

    $scope.GetAll = function () {
         
        $http({
            url: GetAllURL,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }

        }).success(function (data) {
             
            return data;
        }).error(function (data) {
             
            return data;
        });


    };//end of GetAll


});