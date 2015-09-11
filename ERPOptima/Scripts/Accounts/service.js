

app.service('Erpmodal', function ($q) {
    this.Confirm = function (message) {

        var deferred = $q.defer();
        bootbox.confirm("Are you sure want to "+message+"?", function (result) {
            if (result) {
                deferred.resolve(result);
                console.log("User confirmed dialog");
            } else {
                deferred.reject(result);
                console.log("User declined dialog");
            }

        });

        return deferred.promise;
    };// end of

    this.Save = function (operation, token) {

        if (operation.Success) {

            var message = operation.Message != null ? operation.Message : token+"  Successful";
            toastr.success(message);
        }
        else {
            if (operation.OperationId >= 0) {
                var message = operation.Message != null ? operation.Message : token + "  Unsuccessful";
                toastr.error(message);
            }
            if (operation.OperationId == -1) {
                var message = "Add permission Restricted !";
                toastr.info(message);
            }
            if (operation.OperationId == -2) {
                var message = "Edit permission Restricted !";
                toastr.info(message);
            }
           
        }

    };//end of Save

    this.Delete = function (operation,token) {

        if (operation.Success) {

            var message = operation.Message != null ? operation.Message : token + "  Successful";
            toastr.success(message);
        }
        else {
            var message = operation.Message != null ? operation.Message : token + "  Unsuccessful";
            toastr.error(message);
        }


    };// end of Delete

    this.Warning = function (message) {

        toastr.warning(message);



    };//end of Warning

    this.Error = function (error) {

        toastr.error("An Error Has Occured !!");


    }//end of error
    this.Info = function (message) {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-full-width",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "60000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        toastr.info(message);
    };//end of Save


});