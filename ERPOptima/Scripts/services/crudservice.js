app.factory("crudservice", ['$http', '$q', 'Erpmodal', function ($http, $q, Erpmodal) {

    this.GetAllURL = '';
    this.SaveURL = '';
    this.DeleteURL = '';

    return {
        getAll: function () {
             
            var deferred = $q.defer();

            $http({
                url: this.GetAllURL,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }

            }).success(function (data) {
                 
                deferred.resolve(data);
            }).error(function (data) {
                 
                deferred.reject(data);
            });

            return deferred.promise;

        },
        save: function (savethis) {
             
            var deferred = $q.defer();
            var saveURL = this.SaveURL;
            Erpmodal.Confirm('Save').then(function (result) {
                 
                $http.post(saveURL, savethis).success(function (data) {
                                Erpmodal.Save(data, "Save");
                                 
                                deferred.resolve(data);
                            }).error(function (data) {
                                 
                                deferred.reject(data);
                            });
                 
                this.SaveURL = saveURL;
            });
            return deferred.promise;
        }
    };

}]);