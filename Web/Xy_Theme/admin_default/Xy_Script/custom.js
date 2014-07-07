String.prototype.format = function (obj) {
    var _str = this.toString();
    var _matchs = _str.match(/\{\{[^}]+\}\}/g);
    for (var i = 0; i < _matchs.length; i++) {
        var key = _matchs[i].substr(2, _matchs[i].length - 4);
        if (typeof obj[key] !== 'undefined') {
            _str = _str.replace(_matchs[i], obj[key].toString());
        } else {
            _str = _str.replace(_matchs[i], '');
        }
    }
    return _str;
}

function AnalysisUrl(url) {
    if (url == null) { url = location.href; }
    var _protocol = '';
    var _domain = '';
    var _site = '';
    var _dir = '';
    var _path = '';
    var _param = '';
    var _params = {};

    function Init() {
        if (url.indexOf("://") > 0) { _protocol = url.substring(0, url.indexOf("://")); }
        if (_protocol.length > 0) {
            _domain = url.substr(_protocol.length + 3, url.indexOf("/", _protocol.length + 3) - (_protocol.length + 3));
            _site = _protocol + "://" + _domain + "/";
        } else {
            _domain = url.substr(0, url.indexOf("/"));
            if (_domain.length > 0) {
                _site = _domain;
            } else {
                _site = '/';
            }
        }
        if (url.indexOf('?') > 0) {
            _path = url.substr(_site.length - 1, url.indexOf('?') - _site.length + 1);
            _param = url.substr(url.indexOf('?'));
        } else {
            _path = url.substr(_site.length - 1);
        }
        _dir = _path.substr(0, _path.lastIndexOf('/') + 1);
        if (_param.length > 0) {
            var temp = _param.substr(1).split('&');
            if (temp.length > 0) {
                for (var i = 0; i < temp.length; i++) {
                    var _temp = temp[i]
                    var key = decodeURI(_temp.substr(0, _temp.indexOf('=')));
                    var value = decodeURI(_temp.substr(_temp.indexOf('=') + 1));
                    _params[key] = value;
                }
            }
            _param = decodeURI(_param);
        }
    }

    Init();

    return {
        OriginalURL: url,
        Protocol: _protocol,
        Domain: _domain,
        Site: _site,
        Dir: _dir,
        Path: _path,
        Param: _param,
        Params: _params,

        Get: function (key) {
            return _params[key];
        },

        Set: function (key, value) {
            _params[key] = value;
            _param = '';
            for (var _key in _params) {
                if (_param.length == 0) {
                    _param += "?" + _key + "=" + _params[_key];
                } else {
                    _param += "&" + _key + "=" + _params[_key];
                }
            }
            this.Param = _param;
            this.Params = _params;
        },

        toString: function () {
            _params = this.Params;
            _param = '';
            for (var _key in _params) {
                if (_param.length == 0) {
                    _param += "?" + _key + "=" + _params[_key];
                } else {
                    _param += "&" + _key + "=" + _params[_key];
                }
            }
            this.Param = _param
            return _protocol + "://" + _domain + _path + _param;
        }
    }
}

function SelectDefault() {
    $('select[data-default]').each(function (i, o) {
        $o = $(o);
        if ($o.find('option[selected]').length > 0) return;
        var value = $o.attr('data-default');
        $o.find('option').each(function (ii, io) {
            if ($(io).val() == value) $(io).attr('selected', 'selected');
        })
        $o.change();
    })
}

function AjaxLink() {
    $("a.ajaxlink").live('click', function (evt) {
        var $tar = $(evt.target);
        var originTxt = $tar.html();
        var ajaxData = {};
        if (typeof $tar.attr('ajax-data') != 'undefined') {
            ajaxData = eval('(' + $tar.attr('ajax-data') + ')');
        }
        var doAjax = function () {
            var link = $tar.attr('href');
            $tar.html("正在处理");
            $.ajax({
                url: link,
                data: ajaxData,
                type: 'post',
                success: function (data) {
                    if (typeof $tar.attr('ajax-success') != 'undefined') {
                        var fun = eval('(' + $tar.attr('ajax-success') + ')');
                        if (typeof fun === 'function') {
                            fun(data);
                        }
                    }
                    $tar.html(originTxt);
                },
                error: function () {
                    $tar.html(originTxt);
                }
            })
        }
        if (typeof $tar.attr('ajax-confirm') != 'undefined') {
            bootbox.confirm($tar.attr('ajax-confirm'), function (result) {
                if (result) {
                    doAjax();
                }
            })
        } else {
            doAjax();
        }
        return false;
    });
}

function PartialRefresh() {
    $('.ajaxreload').each(function (i, o) {
        
    })
}

(function ($) {
    //init ajax inform
    $(document).ajaxSend(function (evt, xhr, ajaxObj) {
        if (ajaxObj.hide) return;
        if (!isInProcess(ajaxObj)) {
            ajaxObj.notyfy = notyfy({
                text: "正在请求...Requesting..." + ajaxObj.url,
                type: "information",
                layout: "top",
                events: {
                    shown: function (evt, objthis) {
                        if (ajaxObj.notyfy == null) objthis.close();
                    }
                }
            });
        } else {
            notyfy({ text: "已经有一个请求正在处理中<br />your already doing a request", "type": 'error', layout: "topRight", timeout: 3000 })
            return false;
        }
    }).ajaxComplete(function (evt, xhr, ajaxObj) {
        if (ajaxObj.hide) return;
        EndProcess(ajaxObj);
        ajaxObj.notyfy.close();
        ajaxObj.notyfy = null;
    })

    var _store = [];
    function isInProcess(req) {
        for (var i = 0; i < _store.length; i++) {
            var _temp_store = _store[i];
            if (_temp_store.url == req.url
            && _temp_store.data == req.data
            && _temp_store.type == req.type) return true;
        }
        var _temp = {
            url: req.url,
            data: req.data,
            type: req.type
        };
        _store.push(_temp);
        return false;
    }
    function EndProcess(req) {
        for (var i = 0; i < _store.length; i++) {
            var _temp_store = _store[i];
            if (_temp_store.url == req.url
            && _temp_store.data == req.data
            && _temp_store.type == req.type) return _store.splice(i, 1);
        }
    }

    //set current navigation
    $('.menu-0').find('a').each(function (i, o) {
        var curUrl = location.href.toString();
        if (curUrl.indexOf('#') > -1)
            curUrl = curUrl.substr(0, curUrl.indexOf('#'));
        if (o.href == curUrl) {
            $(o).closest('ul.collapse').addClass('in');
            $(o).closest('li.hasSubmenu').addClass('active');
            $(o).closest('li').addClass('current');
        }
    });

    //start effect;
    SelectDefault();
    AjaxLink();
})(jQuery)