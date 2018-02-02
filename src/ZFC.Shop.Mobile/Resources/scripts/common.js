/// <reference path="../plugins/jquery/jquery-1.10.2.min.js" />
$(function () {

});

(function ($) {
    $.fn.extend({
        getFormData: function () {
            var array = $(this).not('.ignore').find('input,select,textarea').serializeArray();
            var data = {};
            $.each(array, function (i, item) {
                data[item.name] = item.value;
            });
            return data;
        }
    });

    $.extend({
        isClick: false,
        alertMsg: function (o) {
            var id = '#' + (o.id || 'myModal');
            var title = o.title || '提示';
            var msg = o.msg || '未设置信息';
            var bodyId = id + 'Body';
            var show = o.show || true;
            $(bodyId).html(msg);
            if (show) {
                $(id).modal({ dimmer: true });
            }
            else {
                $(id).modal('close');
            }
        },
        myAjax: function (u, d, s, o) {
            if ($.isClick) return;
            $.isClick = true;

            var options = {
                url: u,
                type: 'POST',
                dataType: 'json',
                data: d,
                beforeSend: function () { $('#myModalLoding').modal('open'); },
                success: function (res) {
                    $('#myModalLoding').modal('close');
                    if (s && $.isFunction(s)) s(res);
                },
                error: function (xhr, status, error) {
                    console.log('网络发生错误--' + error);
                },
                complete: function () {
                    $.isClick = false;
                }
            };
            if (!o) o = {};
            options = $.extend(options, o);
            $.ajax(options);
        }
    });
})(jQuery);
