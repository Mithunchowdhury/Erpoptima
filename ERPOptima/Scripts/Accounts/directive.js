app.directive('datetimepicker', function ($parse) {
    return function (scope, element, attrs, controller) {
        var ngModel = $parse(attrs.ngModel);
        $(function () {
            element.datetimepicker({
                formatDate: 'd/m/Y',
                onChangeDateTime: function (dateText, inst) {
                    scope.$apply(function (scope) {
                        ngModel.assign(scope, element.val());
                    });
                }
            });
        });
    }
});

app.directive('datetimepickerwithrange', function ($parse) {
   
    return function (scope, element, attrs, controller) {
        var ngModel = $parse(attrs.ngModel);
        $(function () {
            element.datetimepicker({
                formatDate: 'd/m/Y',
                minDate: scope.minDate,
                maxDate: scope.maxDate,
                onChangeDateTime: function (dateText, inst) {
                    scope.$apply(function (scope) {
                        ngModel.assign(scope, element.val());
                    });
                }
            });
        });
    }
});

app.directive('datatable', function () {
    return function (scope, element, attrs, controller) {
        $(function () {
            element.dataTable();
        });
    }
});

app.directive('integer', function () {
    return {
        require: 'ngModel',
        link: function (scope, ele, attr, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {
                return parseInt(viewValue, 10);
            });
        }
    };
});

app.directive('number', function () {
    return function (scope, element, attrs) {

        var keyCode = [8, 9, 37, 39, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 110];
        element.bind("keydown", function (event) {
            console.log($.inArray(event.which, keyCode));
            if ($.inArray(event.which, keyCode) == -1) {
                scope.$apply(function () {
                    scope.$eval(attrs.onlyNum);
                    event.preventDefault();
                });
                event.preventDefault();
            }

        });
    };
});


app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});
//when a tab is clicked make it active
app.directive('mcToggleActive', function () {
    return {
        link: function (scope, element, attrs) {
            element.find('li').on('click', function () {
                $(this).addClass('active').siblings().removeClass('active');
            });
        }
    }
});