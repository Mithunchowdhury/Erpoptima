/// <reference path="jquery-1.4.1.js" />
/// <reference path="JSLib/json2.js" />



var LoggedInUserName = '';
var serviceRoot = "..";
var CurrentUser = null;
var datetimeformat = "dd-mm-yy";
var tbl;
var columns;
var errmsg = "Failed.";
function getColumns() {
    var tableName = tbl;
    var jsonParam = 'tableName=' + tableName;
    var serviceURL = "../Security/GetSysNotNullColumnByTableName";
    AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
    function onSuccess(jsonData) {
        columns = jsonData;
    }
    function onFailed(error) {
        window.alert(error.statusText);
        
    }
}

function ButtonAccessPermission(mode) {
    var jsonParam = '';
    var serviceURL = "../Security/ButtonAccessPermission";
    AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
    function onSuccess(jsonData) {
        var permission = jsonData;

        $("#btnSave").attr('disabled', 'disabled');
        $("#btnSave").addClass('disabled');
        
        $("#btnDelete").attr('disabled', 'disabled');
        $("#btnDelete").addClass('disabled');
        
        if (mode == '0') {            
            if (permission.ReadOnly == true) {

            }
            if (permission.Add == true) {
                $("#btnSave").removeAttr('disabled');
                $("#btnSave").removeClass('disabled');
            }            
            if (permission.Print == true) {

            }
        }
        else { 
            if (permission.Edit == true) {
                $("#btnSave").removeAttr('disabled');
                $("#btnSave").removeClass('disabled');
            }
            if (permission.Delete == true) {
                $("#btnDelete").removeAttr('disabled');
                $("#btnDelete").removeClass('disabled');
            }
        }
      
      
      
      
    }
    function onFailed(error) {
        window.alert(error.statusText);

    }

}



function validate(obj) {
    if (columns.length > 0) {

        for (var i = 0; i < columns.length; i++) {
            for (var property in obj) {
                if (columns[i] == property.toString()) {
                    if (columns[i] != "Id") {
                        if (obj[columns[i]] === "" || obj[columns[i]] === "0") {
                            notif({
                                msg: "Please fill up required field!",
                                type: "warning",
                                position: 'center',
                                autohide:false
                            });
                            return false;
                            break;
                        }
                    }
                }
            }
        }
        return true;
    }
    else {

        return false;
    }
}
function validatewithzero(obj) {
    if (columns.length > 0) {

        for (var i = 0; i < columns.length; i++) {
            for (var property in obj) {
                if (columns[i] == property.toString()) {
                    if (columns[i] != "Id") {
                        if (obj[columns[i]] === "") {                          
                            notif({
                                msg: "Please fill up required field!",
                                type: "warning",
                                position: 'center',
                                autohide: false
                            });
                            return false;
                            break;
                        }
                    }
                }
            }
        }
        return true;
    }
    else {

        return false;
    }
}
var AjaxManager = {
    //    GetJson: function (serviceUrl, jsonParams, successCallback, errorCallback) {

    //        jQuery.ajax({
    //            url: serviceUrl,
    //            data: jsonParams,
    //            type: "POST",
    //            processData: true,
    //            contentType: "application/json",
    //            dataType: "json",
    //            success: successCalback,
    //            error: errorCallback
    //        });
    //    },
    SendJson: function(serviceUrl, jsonParams, successCallback, errorCallback) {
        jQuery.ajax({
            url: serviceUrl,
            data: jsonParams,
            type: "POST",
            success: successCallback,
            error: errorCallback
        });
    },

    getGridConfig: function(opt, urllink, sortColumnName, orderBy) {
        return $.extend(true, {
            url: urllink,
            datatype: 'json',
            mtype: 'GET',
            pager: '#pager',
            rownumbers: true,
            rowNum: 50,
            rowList: [50, 100, 200, 'All'],
            sortname: sortColumnName,
            sortorder: orderBy,
            shrinkToFit: false,
            viewrecords: true,
            jsonReader: {
                root: "Data",
                page: "PageIndex",
                total: "TotalPages",
                records: "TotalCount",
                startrow: "startRow",
                endrow: "endRow",
                repeatitems: false
            },
            loadBeforeSend: function(xhr) {
                xhr.setRequestHeader("content-type", "application/json");
            },
            prmNames: { page: 'pageIndex', rows: 'pageSize', sort: 'orderByField', order: 'orderByType' },
            height: '200'
        }, opt);
    },
    getGridConfig2: function(opt, urllink, sortColumnName, orderBy) {
        return $.extend(true, {
            url: urllink,
            datatype: 'json',
            mtype: 'GET',
            pager: '#pager2',
            rownumbers: true,
            rowNum: 50,
            rowList: [50, 100, 200, 'All'],
            sortname: sortColumnName,
            sortorder: orderBy,
            shrinkToFit: false, 
            viewrecords: true,
            jsonReader: {
                root: "Data",
                page: "PageIndex",
                total: "TotalPages",
                records: "TotalCount",
                startrow: "startRow",
                endrow: "endRow",
                repeatitems: false
            },
            loadBeforeSend: function(xhr) {
                xhr.setRequestHeader("content-type", "application/json");
            },
            prmNames: { page: 'pageIndex', rows: 'pageSize', sort: 'orderByField', order: 'orderByType' },
            height: '200'
        }, opt);
    },

    disablePopup: function(popupDivName, backgroundDivName) {
        $(popupDivName).fadeOut("slow");
    },
    centerPopup: function(popupDivName) {
        var windowWidth = document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;
        var popupHeight = $(popupDivName).height();
        var popupHeight = popupHeight;
        var popupWidth = $(popupDivName).width();

        $(popupDivName).css({
            "position": "absolute",
            "top": windowHeight / 2 - popupHeight / 2,
            "left": windowWidth / 2 - popupWidth / 2,
            "height": popupHeight
        });

        $(backgroundDivName).css({
            "height": windowHeight
        });

    },

    showlink: function(el, cellval, opts) {
        var op = { baseLinkUrl: opts.baseLinkUrl, showAction: opts.showAction, addParam: opts.addParam };
        if (!isUndefined(opts.colModel.formatoptions)) {
            op = $.extend({}, op, opts.colModel.formatoptions);
        }
        idUrl = op.baseLinkUrl + op.showAction + '?id=' + opts.rowId + op.addParam;
        if (isString(cellval)) {	//add this one even if its blank string
            $(el).html("<a class=\"aColumn\" href=\"#\"" + "onclick=\"Page.Test(' " + opts.rowId + "')\">" + cellval + "</a>");
        } else {
            $.fn.fmatter.defaultFormat(el, cellval);
        }
    },

    changeDateFormat: function(value, isTime) {
        var dateFormat = "";
        if (!isEmpty(value) && value != "/Date(-6816290400000)/" && value != "/Date(-62135596800000)/") {
            var time = value.replace(/\/Date\(([0-9]*)\)\//, '$1');
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            if (isTime != 0) {
                timeformat = (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
                dateFormat = dd + '-' + mm + '-' + yyyy + ' ' + timeformat;
            }
            else {
                dateFormat = dd + '-' + mm + '-' + yyyy;
            }
        }
        return dateFormat;
    },

    changeToSQLDateFormat: function(value, isTime) {
        //
        if (!isEmpty(value) && value != "/Date(-6816290400000)/" && value != "/Date(-62135596800000)/") {
            var time = value.replace(/\/Date\(([0-9]*)\)\//, '$1');
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            var sqlFormatedDate = "";
            if (isTime != 0) {
                timeformat = '<br> ' + (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
                sqlFormatedDate = dd + '-' + mm + '-' + yyyy + ' ' + timeformat;
            }
            else {
                sqlFormatedDate = dd + '-' + mm + '-' + yyyy;
            }
            return sqlFormatedDate;
        }

    },
    jqGridDate: function(el, cellval, opts) {
        //
        if (!isEmpty(cellval) && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")
        //            
            $(el).html(AjaxManager.changeToSQLDateFormat(cellval, 0));
    },
    jqGridDateTime: function(el, cellval, opts) {
        if (!isEmpty(cellval) && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")

            $(el).html(AjaxManager.changeDateFormat(cellval, 1));
    },
    changeTimeFormat: function(value) {
        var time = value.Hours + ':' + value.Minutes;
        return time;
    },
    jqGridTime: function(el, cellval, opts) {
        if (!isEmpty(cellval) && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")

            $(el).html(AjaxManager.changeTimeFormat(cellval));
    },
    DMYToMDY: function(value) {
        var datePart = value.match(/\d+/g);
        var day = datePart[0]
        var month = datePart[1]
        var year = datePart[2];
        return month + '/' + day + '/' + year;
    },
    MDYToDMY: function(value) {
        var datePart = value.match(/\d+/g);
        var month = datePart[0]
        var day = datePart[1]
        var year = datePart[2];
        return day + '/' + month + '/' + year;
    },
    DMYToYMD: function(value) {
        var datePart = value.match(/\d+/g);
        var day = datePart[0]
        var month = datePart[1]
        var year = datePart[2];
        return year + '/' + month + '/' + day;
    }

};
//End AjaxManager

