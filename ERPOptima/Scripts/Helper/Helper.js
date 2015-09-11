var format = "dd-MM-yyyy";
var ConverttoDateString = function (input) {
    // Exit if the value isn't defined
    if (angular.isUndefined(input)) {
        return;
    }

    var date = new Date(parseInt(input.substr(6)));

    // John Pedrie added Moment.js support
    if (typeof moment !== 'undefined' && format) {
        var momentObj = moment(date);
        return momentObj.format(format);
    }
    else {
        var dt = date.toLocaleDateString().split("/");
        if (dt[0].length < 2) {
            dt[0] = '0' + dt[0];
        }
        if (dt[1].length < 2) {
            dt[1] = '0' + dt[1];
        }

        return dt[1] + '/' + dt[0] + '/' + dt[2];
    }
}

var convertToJsonDate = function (val) {
    var date = new Date(val);

    return "/Date(" + date.getTime() + ")/";
};

var parseJsonDate = function (jsonDateString) {
    return new Date(parseInt(jsonDateString.replace('/Date(', '')));
}

var StringToDate = function (dtstring) {
    var sp = dtstring.split("/");
    var a = new Date(parseInt(sp[2]), parseInt(sp[1] - 1),parseInt( sp[0]));
    return a;
}

var DateDiff =
    function DateDiff(date1, date2) {
        var datediff = date1.getTime() - date2.getTime(); //store the getTime diff - or +
        return (datediff / (24 * 60 * 60 * 1000)); //Convert values to -/+ days and return value
    }