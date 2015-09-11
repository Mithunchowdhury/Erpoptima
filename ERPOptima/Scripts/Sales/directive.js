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

        var keyCode = [8, 9, 37, 39, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 110,190];
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

app.directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                elem.on('click', function (e) {
                    e.preventDefault();
                });
            }
        }
    };
});

app.directive('nm', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;
            ngModel.$parsers.unshift(function (inputValue) {
                var digits = inputValue.split('').filter(function (s) { return (!isNaN(s) && s != ' '); }).join('');
                ngModel.$viewValue = digits;
                ngModel.$render();
                return digits;
            });
        }
    };
    //return {
    //    equire: 'ngModel',
    //    restrict: 'A',
    //    link: function (scope, element, attrs) {
    //        if (element != 0)
    //        {
    //            alert(element);
    //        }
    //        $(element).numeric();
    //    }
    //}
    //return {
    //    require: 'ngModel',
    //    restrict: 'A',
    //    link: function (scope, element, attr, ctrl) {
    //        function inputValue(val) {
    //            if (val) {
    //                var digits = val.replace(/[^0-9.]/g, '');

    //                if (digits !== val) {
    //                    ctrl.$setViewValue(digits);
    //                    ctrl.$render();
    //                }
    //                return parseFloat(digits);
    //            }
    //            return undefined;
    //        }
    //        ctrl.$parsers.push(inputValue);
    //    }
    //};
});

//region Focus Event

app.directive('eventFocus', function(focus) {
    return function(scope, elem, attr) {
        elem.on(attr.eventFocus, function() {
            focus(attr.eventFocusId);
        });

        // Removes bound events in the element itself
        // when the scope is destroyed
        scope.$on('$destroy', function() {
            elem.off(attr.eventFocus);
        });
    };
});

app.factory('focus', function($timeout, $window) {
    return function(id) {
        // timeout makes sure that it is invoked after any other event has been triggered.
        // e.g. click events that need to run before the focus or
        // inputs elements that are in a disabled state but are enabled when those events
        // are triggered.
        $timeout(function() {
            var element = $window.document.getElementById(id);
            if(element)
                element.focus();
        });
    };
});

//endregion Focus Event