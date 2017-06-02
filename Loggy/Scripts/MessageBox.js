var MessageBoxButton = (function () {
    function MessageBoxButton(id, text, callback) {
        this.m_id = id;
        this.m_text = text;
        this.m_class = "button";
        this.m_callback = callback;
    }
    MessageBoxButton.prototype.create = function () {
        var aa = document.createElement("a");
        aa.text = this.m_text;
        aa.href = "#";
        aa.className = this.m_class;
        aa.target = "_blank";
        aa.onclick = this.m_callback;
        if (this.m_onclick != null)
            aa.setAttribute("onclick", this.m_onclick);
        return aa;
    };
    Object.defineProperty(MessageBoxButton.prototype, "class", {
        get: function () {
            return this.m_class;
        },
        set: function (value) {
            this.m_class = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageBoxButton.prototype, "id", {
        get: function () {
            return this.m_id;
        },
        set: function (value) {
            this.m_id = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageBoxButton.prototype, "text", {
        get: function () {
            return this.m_text;
        },
        set: function (value) {
            this.m_text = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageBoxButton.prototype, "callback", {
        get: function () {
            return this.m_callback;
        },
        set: function (value) {
            this.m_callback = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageBoxButton.prototype, "onclick", {
        get: function () {
            return this.m_onclick;
        },
        set: function (value) {
            this.m_onclick = value;
        },
        enumerable: true,
        configurable: true
    });
    return MessageBoxButton;
}());
var List = (function () {
    function List() {
        this.m_items = [];
    }
    Object.defineProperty(List.prototype, "items", {
        get: function () {
            return this.m_items;
        },
        set: function (value) {
            this.m_items = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(List.prototype, "length", {
        get: function () {
            return this.m_items.length;
        },
        enumerable: true,
        configurable: true
    });
    List.prototype.add = function (value) {
        this.m_items.push(value);
    };
    List.prototype.get = function (index) {
        return this.m_items[index];
    };
    return List;
}());
var MessageBox = (function () {
    function MessageBox(title, message) {
        this.m_buttonCollection = new List();
        this.m_title = title;
        this.m_message = message;
    }
    Object.defineProperty(MessageBox.prototype, "buttons", {
        get: function () {
            return this.m_buttonCollection;
        },
        set: function (value) {
            this.m_buttonCollection = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageBox.prototype, "title", {
        get: function () {
            return this.m_title;
        },
        set: function (value) {
            this.m_title = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageBox.prototype, "message", {
        get: function () {
            return this.m_message;
        },
        set: function (value) {
            this.m_message = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageBox.prototype, "htmlMessage", {
        get: function () {
            return this.m_html;
        },
        set: function (value) {
            this.m_html = value;
        },
        enumerable: true,
        configurable: true
    });
    MessageBox.prototype.foo = function (ev) {
    };
    MessageBox.prototype.show = function () {
        var overlayStyle = "\nposition:fixed; \nleft: 0px; right: 0px; top: 0px; bottom: 0px; \nbackground: url(../../images/Confirm/ie.png);\nbackground: -moz-linear-gradient(rgba(11,11,11,0.1), rgba(11,11,11,0.6)) repeat-x rgba(11,11,11,0.2);\nbackground: -webkit-gradient(linear, 0% 0%, 0% 100%, from(rgba(11,11,11,0.1)), to(rgba(11,11,11,0.6))) repeat-x rgba(11,11,11,0.2);\nz-index: 9990;\n}\n";
        var msgboxStyle = "\ndisplay: block; width: 500px; height: 500px; \nposition:absolute;\nleft: 50%;\ntop: 50%;\n\n-webkit-transform: translate(-50%,-50%);\n-ms-transform: translate(-50%,-50%);\ntransform: translate(-50%,-50%);\n\nmargin: auto;\nbackground-color: #888;\nborder: 1px solid black;\nbox-shadow: 4px 4px 5px 0px #000;\n-moz-box-shadow: 4px 4px 5px 0px #000;\n-webkit\n";
        var titleStyle = "font: 0.65cm/1 'Cuprum','Lucida Sans Unicode', 'Lucida Grande', sans-serif;\nbackground: url(../../images/Confirm/header_bg.jpg) repeat-x left bottom #f5f5f5;\n\nbackground: rgba(255,255,255,1);\nbackground: -moz-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);\nbackground: -webkit-gradient(left top, left bottom, color-stop(0%, rgba(255,255,255,1)), color-stop(37%, rgba(246,246,246,1)), color-stop(100%, rgba(237,237,237,1)));\nbackground: -webkit-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);\nbackground: -o-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);\nbackground: -ms-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);\nbackground: linear-gradient(to bottom, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);\nfilter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#ededed', GradientType=0 );\n\n\npadding: 10px 25px;\ntext-shadow: 0.5mm 0.5mm 0 rgba(255, 255, 255, 0.6);\ncolor: #666;\n\nletter-spacing: 0.3px;\nwhite-space: nowrap;\noverflow: hidden;\ncolor: #888;\npadding: 14px 25px;\nmargin-top: 0px;\n";
        var overlay = document.createElement("div");
        var msgbox = document.createElement("div");
        var title = document.createElement("div");
        var titleAfter = document.createElement("div");
        var content = document.createElement("div");
        var contentAfter = document.createElement("div");
        var footer = document.createElement("div");
        var footerAfter = document.createElement("div");
        overlay.id = "uuid_" + this.guid("");
        // overlay.setAttribute("style", "position:fixed; left: 0px; right: 0px; top: 0px; bottom: 0px; z-index: 9999; background-color: red;");
        overlay.setAttribute("style", overlayStyle);
        msgbox.setAttribute("style", msgboxStyle);
        title.setAttribute("style", titleStyle);
        titleAfter.setAttribute("style", "clear: both;");
        content.setAttribute("style", "background-color: red; color: white; font-weight: bold; padding: 5mm;");
        contentAfter.setAttribute("style", "clear: both;");
        //footer.setAttribute("style", "background-color: #888;");
        footer.setAttribute("style", "padding: 3mm;");
        footerAfter.setAttribute("style", "clear: both;");
        var cb = "\nconfirmBox h1 \n{\n   letter-spacing: 0.3px;\n   white-space: nowrap;\n   overflow: hidden;\n   color: #888;\n   padding: 14px 25px;\n   margin-top: 0px;\n}\n\n#confirmBox h1, #confirmBox p \n{\n   font: 19px/1 'Cuprum','Lucida Sans Unicode', 'Lucida Grande', sans-serif;\n   background: url(../../images/Confirm/header_bg.jpg) repeat-x left bottom #f5f5f5;\n   padding: 10px 25px;\n   text-shadow: 1px 1px 0 rgba(255, 255, 255, 0.6);\n   color: #666;\n}\n";
        var tn = document.createTextNode(this.title);
        title.appendChild(tn);
        if (this.m_html == null) {
            var lines = this.message.split("\n");
            for (var l = 0; l < lines.length; ++l) {
                var tnContent = document.createTextNode(lines[l]);
                content.appendChild(tnContent);
                if (l != lines.length - 1) {
                    content.appendChild(document.createElement("br"));
                }
            } // Next l
        }
        else {
            // https://developer.mozilla.org/en-US/docs/Web/API/Element/insertAdjacentHTML
            content.insertAdjacentHTML('beforeend', this.m_html);
        }
        msgbox.appendChild(title);
        msgbox.appendChild(titleAfter);
        msgbox.appendChild(content);
        msgbox.appendChild(contentAfter);
        for (var k = 0; k < this.m_buttonCollection.length; ++k) {
            var btn = this.m_buttonCollection.items[k].create();
            footer.appendChild(btn);
        }
        msgbox.appendChild(footer);
        msgbox.appendChild(footerAfter);
        overlay.appendChild(msgbox);
        document.body.appendChild(overlay);
    };
    // ID and NAME tokens must begin with a letter ([A-Za-z]) 
    // and may be followed by any number of 
    // letters, digits([0 - 9]), hyphens("-"), underscores("_"), colons(":"), and periods (".").
    // You can technically use colons and periods in id/name attributes, 
    // but I would strongly suggest avoiding both.
    // In CSS both the period and the colon have special meaning 
    // Periods are class selectors 
    // and colons are pseudo-selectors(eg., ":hover").
    MessageBox.prototype.guid = function (s) {
        if (s === void 0) { s = "-"; }
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }
        return s4() + s4() + s + s4() + s + s4() + s + s4() + s + s4() + s4() + s4();
    };
    MessageBox.prototype.hide = function () {
        var buttons = "\n<div id=\"" + "uuid_" + this.guid("") + "\" class=\"overlay\">\n    <div id=\"\" class=\"confirmBox\">\n        <h1>Title</h1>\n        <div class=\"content\"></div>\n        <div class=\"buttons\"></div>\n    </div>\n</div>\n";
    };
    MessageBox.prototype.toggle = function () { };
    return MessageBox;
}());
// let greeter = new MessageBox("Bemerkung Quartalsreport", "world");
// greeter.buttons.add(new MessageBoxButton("1", "OK"));
// greeter.show();
//greeter.buttons.add(new MessageBoxButton("foo", "bar"));
//var map: { [email: string]: string; } = {};
//map["foo"] = "bar";
//console.log(map["foo"]);
//# sourceMappingURL=MessageBox.js.map