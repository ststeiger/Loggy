// https://github.com/borisyankov/DefinitelyTyped
// callback function     :(...args)=>any
// optional parameter:   ? 
// Avoid `console` errors in browsers that lack a console.
(function () {
    var method;
    var noop = function () { };
    var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeStamp', 'trace', 'warn'
    ];
    var length = methods.length;
    window.console = window.console || {};
    var console = (window.console = window.console || {});
    while (length--) {
        method = methods[length];
        // Only stub undefined methods.
        if (!console[method]) {
            console[method] = noop;
        }
    }
}());
var REQ = {
    XMLHttpFactories: [
        function () { return new XMLHttpRequest(); },
        function () { return new ActiveXObject("Msxml2.XMLHTTP"); },
        function () { return new ActiveXObject("Msxml3.XMLHTTP"); },
        function () { return new ActiveXObject("Microsoft.XMLHTTP"); }
    ],
    createXMLHTTPObject: function () {
        var xmlhttp = false;
        for (var i = 0; i < this.XMLHttpFactories.length; i++) {
            try {
                xmlhttp = this.XMLHttpFactories[i]();
            }
            catch (e) {
                continue;
            }
            break;
        } // Next i
        return xmlhttp;
    } // End Function createXMLHTTPObject
    ,
    urlEncode: function (pd) {
        if (typeof pd == 'string' || pd instanceof String)
            return pd; // encodeURI(pd); // Might be stringified JSON
        var k, i = 0, str = "";
        ;
        for (k in pd) {
            //if(i!=0)
            str += "&";
            str += encodeURIComponent(k) + "=" + encodeURIComponent(pd[k]);
            ++i;
        }
        return str;
    },
    "webMethod": function (url, method, onSuccess, onError, onDone, postData, addParams) {
        var url = url + "/" + method;
        this.sendRequest(url, function (r) {
            if (onSuccess)
                onSuccess(r, addParams);
        }, function (r) {
            if (onError) {
                var ex = null;
                try {
                    ex = JSON.parse(r.responseText);
                }
                catch (e) {
                }
                onError(r, ex);
            }
            else
                alert('HTTP error ' + r.status);
        }, onDone // Always
        , postData, "application/json; charset=utf-8", "POST");
    },
    "get": function (url, onSuccess, postData) {
        this.sendRequest(url, onSuccess, null, null, postData, 'application/urlencode', "GET");
    },
    "post": function (url, onSuccess, postData, contentType) {
        this.sendRequest(url, onSuccess, null, null, postData, 'application/x-www-form-urlencoded', "POST");
    },
    "postJSON": function (url, onSuccess, postData, contentType) {
        this.sendRequest(url, onSuccess, null, null, postData, 'application/json; charset=UTF-8', "POST");
    },
    "sendRequest": function (url, onSuccess, onError, onDone, postData, contentType, method) {
        url += ((url.indexOf('?') === -1) ? "?" : "&") + "no_cache=" + (new Date()).getTime();
        if (postData) {
            if (!method)
                method = "POST";
            if (method.toUpperCase() === "GET")
                url += this.urlEncode(postData);
            else {
                if (!contentType)
                    contentType = 'application/x-www-form-urlencoded';
                if (contentType.indexOf("application/json") != -1) {
                    if (!(typeof postData == 'string' || postData instanceof String))
                        postData = JSON.stringify(postData);
                }
                if (contentType.indexOf("application/x-www-form-urlencoded") != -1)
                    postData = this.urlEncode(postData);
            }
        }
        else if (!method)
            method = "GET";
        var req = this.createXMLHTTPObject();
        if (!req)
            return;
        if (false || !true) {
            if (url.indexOf("popDrop") === -1) {
                console.log("url: " + url);
                console.log("method: " + method);
                console.log("contentType: " + contentType);
                console.log("data:");
                if (method.toUpperCase() === "GET")
                    console.log(url);
                else
                    console.log(postData);
            }
        }
        // req.onerror = function (e)
        // {
        //     console.log(e);
        //     console.log("Error Status: " + e.target.status);
        // };
        req.open(method, url, true);
        // req.setRequestHeader('User-Agent', 'XMLHTTP/1.0');
        req.setRequestHeader('cache-control', 'no-cache');
        if (postData)
            req.setRequestHeader('Content-type', contentType);
        req.onreadystatechange = function () {
            if (req.readyState != 4)
                return;
            if (!(req.status != 200 && req.status != 304 && req.status != 0)) {
                // if (url.indexOf("FormsTranslation") !== -1) console.log(req);
                if (onSuccess)
                    onSuccess(req.responseText);
            }
            if (req.status != 200 && req.status != 304 && req.status != 0) {
                if (onError)
                    onError(req);
                else {
                    alert('HTTP error ' + req.status);
                }
            }
            if (onDone)
                onDone(req);
        };
        if (req.readyState == 4)
            return;
        req.send(postData);
    }
} // End Obj REQ
;
/* @license: Licensed under The MIT License. See license.txt and http://www.datejs.com/license/.
https://github.com/datejs/Datejs/blob/master/src/core.js
https://code.google.com/p/datejs/wiki/FormatSpecifiers
http://stackoverflow.com/questions/3552461/how-to-format-a-javascript-date
http://blog.stevenlevithan.com/archives/date-time-format
http://stackoverflow.com/questions/6002808/is-there-any-way-to-get-current-time-in-nanoseconds-using-javascript
*/
var FormatTools = {
    // https://github.com/datejs/Datejs/blob/master/src/globalization/en-US.js
    $i18n: {
        dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
        abbreviatedDayNames: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        shortestDayNames: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"],
        firstLetterDayNames: ["S", "M", "T", "W", "T", "F", "S"],
        monthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        abbreviatedMonthNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
        amDesignator: "AM",
        pmDesignator: "PM"
    },
    p: function (val, len) {
        val = String(val);
        len = len || 2;
        while (val.length < len)
            val = "0" + val;
        return val;
    },
    mp: function (d, n) {
        var i = 3, res = this.p(d.getMilliseconds(), 3).substr(0, n);
        for (; i < n; ++i)
            res += "0";
        return res;
    },
    tzo: function (d) {
        var o = d.getTimezoneOffset();
        return (o > 0 ? "-" : "+") + this.p(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4);
    },
    tz: function (date) {
        var timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g, timezoneClip = /[^-+\dA-Z]/g;
        return (String(date).match(timezone) || [""]).pop().replace(timezoneClip, "");
    },
    ord: function (num) {
        if (num <= 0)
            return num.toString();
        switch (num % 100) {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }
        switch (num % 10) {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }
    } // End Function ord
    ,
    "format": function (str) {
        if (!str)
            return str;
        str = str.toString();
        if (arguments.length < 2)
            return str;
        var t = typeof arguments[1], args = "string" == t || "number" == t ? Array.prototype.slice.call(arguments) : arguments[1];
        for (var arg in args)
            str = str.replace(new RegExp("\\{" + arg + "\\}", "gi"), args[arg]);
        return str;
    },
    "formatDate": function (x, formatString) {
        // "S dd'd'.MM (MMMM).yyyy ".replace(/(\\)?(dd?d?d?|MM?M?M?|yy?y?y?|hh?|HH?|mm?|ss?|tt?|S)/g, 
        return formatString.replace(/d{1,4}|M{1,4}|f{1,7}|yy(?:yy)?|([HhmsTt])\1?|[oSZ]|"[^"]*"|'[^']*'/g, function (m) {
            var p = this.p, mp = this.mp.bind(this), tzo = this.tzo.bind(this), tz = this.tz.bind(this), ord = this.ord.bind(this), i18n = this.$i18n;
            x.h = x.getHours;
            if (m.charAt(0) === "\\") {
                return m.replace("\\", "");
            }
            switch (m) {
                case "hh":
                    return p(x.h() < 13 ? (x.h() === 0 ? 12 : x.h()) : (x.h() - 12));
                case "h":
                    return x.h() < 13 ? (x.h() === 0 ? 12 : x.h()) : (x.h() - 12);
                case "HH":
                    return p(x.h());
                case "H":
                    return x.h();
                case "mm":
                    return p(x.getMinutes());
                case "m":
                    return x.getMinutes();
                case "ss":
                    return p(x.getSeconds());
                case "s":
                    return x.getSeconds();
                case "yyyy":
                    return p(x.getFullYear(), 4);
                case "yy":
                    return p(x.getFullYear());
                case "dddd":
                    return i18n.dayNames[x.getDay()];
                case "ddd":
                    return i18n.abbreviatedDayNames[x.getDay()];
                case "dd":
                    return p(x.getDate());
                case "d":
                    return x.getDate();
                case "MMMM":
                    return i18n.monthNames[x.getMonth()];
                case "MMM":
                    return i18n.abbreviatedMonthNames[x.getMonth()];
                case "MM":
                    return p((x.getMonth() + 1));
                case "M":
                    return x.getMonth() + 1;
                case "t":
                    return (x.h() < 12 ? i18n.amDesignator.substring(0, 1) : i18n.pmDesignator.substring(0, 1)).toLowerCase();
                case "tt":
                    return (x.h() < 12 ? i18n.amDesignator : i18n.pmDesignator).toLowerCase();
                case "T":
                    return x.h() < 12 ? i18n.amDesignator.substring(0, 1) : i18n.pmDesignator.substring(0, 1);
                case "TT":
                    return x.h() < 12 ? i18n.amDesignator : i18n.pmDesignator;
                case "S":
                    return ord(x.getDate());
                case "fffffff":
                    return mp(x, 7);
                case "ffffff":
                    return mp(x, 6);
                case "fffff":
                    return mp(x, 5);
                case "ffff":
                    return mp(x, 4);
                case "fff":
                    return mp(x, 3);
                case "ff":
                    return mp(x, 2);
                case "f":
                    return mp(x, 1);
                case "o":
                    return tzo(x);
                case "Z":
                    return tz(x);
                default:
                    return m;
            } // End Switch
        } // End Fn
            .bind(this));
    }
};
// var x = new Date();
// FormatTools.formatDate(x, "dddd (ddd)   S dd'd'.MM (MMM MMMM).yyyy HH:mm:ss.fff t tt T TT (o) {Z}")
// FormatTools.format("hello {foo} name", { foo: "bar" });
// FormatTools.formatString("hello {foo} name");
var HtmlTools = {
    "DomReady": function (a, b, c) {
        // https://developer.mozilla.org/en/docs/Web/API/Document/readyState
        // b = document, c = 'addEventListener';
        // b[c] ? b[c]('DOMContentLoaded', d) : window.attachEvent('onload', d);
        // alternative to DOMContentLoaded 
        // if (document.readyState === "interactive") { a(); return;}
        // alternative to load event
        if (document.readyState === "complete") {
            a();
            return;
        }
        document.onreadystatechange = function () {
            console.log("onreadystatechange:", document.readyState);
            // alternative to DOMContentLoaded 
            //if (document.readyState === "interactive")
            //{
            //    a(); // initApplication();
            //}
            // alternative to load event
            if (document.readyState == "complete") {
                a(); // initApplication();
            }
        };
    },
    "dispatchEvent": function (el, e) {
        // console.log("dispatching");
        if ("createEvent" in document) {
            var evt = document.createEvent("HTMLEvents");
            evt.initEvent(e, false, true);
            el.dispatchEvent(evt);
        }
        else {
            el.fireEvent("on" + e);
        }
    },
    "userAgent": function () {
        var s = null;
        if (navigator && navigator.userAgent && navigator.userAgent != null)
            s = navigator.userAgent.toLowerCase();
        else
            s = "";
        var uai = {
            Android: function () {
                return s.match(/android|opera mobi|opera mini/i);
            },
            BlackBerry: function () {
                return s.match(/blackberry/i);
            },
            iOS: function () {
                return s.match(/iphone|ipad|ipod/i);
            },
            WindowsMobile: function () {
                return s.match(/iemobile|windows ce|pocket pc/i);
            },
            anyMobile: function () {
                return (uai.Android() || uai.BlackBerry() || uai.iOS() || uai.WindowsMobile());
            },
            hasWindowResizeBug: null
        };
        uai.hasWindowResizeBug = uai.anyMobile();
        return uai;
    },
    "addEvent": function (elem, event, fn) {
        if (elem.addEventListener)
            elem.addEventListener(event, fn, false);
        else {
            elem.attachEvent("on" + event, function () {
                // set the this pointer same as addEventListener when fn is called
                return (fn.call(elem, window.event));
            });
        }
    },
    "popDrop": function (sel, data) {
        // console.log(data);
        if (typeof sel === 'string' || sel instanceof String)
            sel = document.getElementById(sel);
        sel.options.length = 0; // clear out existing items
        var ogl = sel.getElementsByTagName('optgroup');
        for (var i = ogl.length - 1; i >= 0; i--)
            sel.removeChild(ogl[i]);
        var docfrag = document.createDocumentFragment();
        // var combinationGroup = document.createElement("optgroup");
        // combinationGroup.label = GroupName;
        // var data = [{ t: "Internet Explorer", v: "MSFT" }, { t: "Mozilla Firefox", v: "MOZ" }, { t: "Safari", v: "AAPL" }, { t: "Chrome", v: "GOOG" }, { t: "Opera", v: "O" }];
        for (var i = 0; i < data.length; i++) {
            // opts.push('<option value="' + value.abc + '" text="' + value.abc + '" />');
            // var opt = document.createElement('option');
            // opt.value = cuisines[i];
            // if (opt.textContent) opt.textContent = cuisines[i];
            // else opt.innerText = cuisines[i];
            var d = data[i];
            var opt = new Option(d.t, d.v, null, d.s);
            // if(d.s) opt.selected = true;
            docfrag.appendChild(opt);
        } // Next i 
        sel.appendChild(docfrag);
        this.dispatchEvent(sel, "change");
    } // End Function PopDrop
} // End Obj HtmlTools
;
//# sourceMappingURL=HtmlTools.js.map