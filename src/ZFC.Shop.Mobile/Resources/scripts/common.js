/// <reference path="../plugins/jquery/jquery-1.10.2.min.js" />

var isClick = false;

$(function () {

});

jQuery.fn.extend({
    getFormData: function () {
        var array = $(this).not('.ignore').find('input,select,textarea').serializeArray();
        var data = {};
        $.each(array, function (i, item) {
            data[item.name] = item.value;
        });
        return data;
    }
});

//$.getFormData = function (doc) {
//    doc = doc || document.body;
//    var array = $(doc).not('.ignore').find('input,select,textarea').serializeArray();
//    var data = {};
//    $.each(array, function (i, item) {
//        data[item.name] = item.value;
//    });
//    return data;
//}

$.alertMsg = function (o) {
    var id = '#' + o.id || 'myModal';
    var title = o.title || '提示';
    var msg = o.msg || '未设置';
    var bodyId = id + 'Body';
    var show = o.show || true;
    $(bodyId).html(msg);
    if (show) {
        $(id).modal('open');
    }
    else {
        $(id).modal('close');
    }
}

$.myAjax = function (u, d, s, o) {
    if (isClick) return;
    isClick = true;

    var options = {
        url: u,
        type: 'post',
        dataType: 'json',
        data: d,
        beforeSend: function () { $('#myModalLoding').modal('open'); },
        success: s,
        error: function (xhr, status, error) {
            console.log('网络发生错误--' + error);
        },
        complete: function () {
            isClick = false;
            $('#myModalLoding').modal('close');
        }
    };
    if (!o) o = {};
    options = $.extend(options, o);
    $.ajax(options);
}