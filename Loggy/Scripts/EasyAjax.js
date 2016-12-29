var __extends = (this && this.__extends) || function (d, b)
{
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Http;
(function (Http)
{
    var URL = (function ()
    {
        function URL()
        {
        }
        URL.contains = function (key)
        {
            if (!key)
                return false;
            var i = this.Parameters.length;
            while (i--)
            {
                if (this.Parameters[i] !== null && key !== null && (this.Parameters[i].toLowerCase() === key.toLowerCase()))
                    return true;
            }
            return false;
        };
        // Read a page's GET URL variables and return them as an associative array.
        URL.Parameters = (function ()
        {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++)
            {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        })();
        return URL;
    })();
    Http.URL = URL; // End static class URL
    var RequestBase = (function ()
    {
        function RequestBase(pURL, pData)
        {
            this.url = pURL;
            this.data = pData;
            this.m_Async = true;
            this.XMLHttpFactories = [
                function () { return new XMLHttpRequest(); },
                function () { return new ActiveXObject("Msxml2.XMLHTTP"); },
                function () { return new ActiveXObject("Msxml3.XMLHTTP"); },
                function () { return new ActiveXObject("Microsoft.XMLHTTP"); }
            ];
            this.m_SuccessCallbacks = [];
            this.m_FailureCallbacks = [];
            this.m_CompleteCallbacks = [];
        }
        RequestBase.prototype.failureDefault = function (r)
        {
            console.log("failure");
            console.log(r);
            var msg = "Error " + r.status + " (" + r.statusText + "): \n\n";
            msg += "URL: \n" + r.responseURL + "\n\n";
            msg += r.responseText;
            alert(msg);
        };
        RequestBase.prototype.successCallback = function ()
        {
            this.Complete = true;
            for (var i = 0; i < this.m_SuccessCallbacks.length; ++i)
            {
                this.m_SuccessCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.failureCallback = function ()
        {
            this.Complete = true;
            if (this.m_FailureCallbacks.length === 0)
                this.failureDefault.apply(this, arguments);
            for (var i = 0; i < this.m_FailureCallbacks.length; ++i)
            {
                this.m_FailureCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.alwaysCallback = function ()
        {
            this.Complete = true;
            for (var i = 0; i < this.m_CompleteCallbacks.length; ++i)
            {
                this.m_CompleteCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.success = function (cb)
        {
            this.m_SuccessCallbacks.push(cb);
            return this;
        };
        RequestBase.prototype.failure = function (cb)
        {
            this.m_FailureCallbacks.push(cb);
            return this;
        };
        RequestBase.prototype.always = function (cb)
        {
            this.m_CompleteCallbacks.push(cb);
            return this;
        };
        RequestBase.prototype.async = function (b)
        {
            this.m_Async = b;
            return this;
        };
        RequestBase.prototype.urlEncode = function (pd)
        {
            if (typeof pd == 'string' || pd instanceof String)
                return pd; // encodeURI(pd); // Might be stringified JSON
            var k, sb = [];
            for (k in pd)
                sb.push(encodeURIComponent(k) + "=" + encodeURIComponent(pd[k]));
            return ("&" + sb.join("&"));
        };
        RequestBase.prototype.createXMLHTTPObject = function ()
        {
            var xmlhttp = false;
            for (var i = 0; i < this.XMLHttpFactories.length; i++)
            {
                try
                {
                    xmlhttp = this.XMLHttpFactories[i]();
                }
                catch (e)
                {
                    continue;
                }
                break;
            } // Next i
            return xmlhttp;
        }; // End Function createXMLHTTPObject
        RequestBase.prototype.sendRequest = function (url, additionalUrlParameters, onSuccess, onError, onDone, postData, contentType, method)
        {
            url += ((url.indexOf('?') === -1) ? "?" : "&") + "no_cache=" + (new Date()).getTime();
            if (additionalUrlParameters !== null)
                url += additionalUrlParameters;
            if (postData)
            {
                if (!method)
                    method = "POST";
                if (!contentType)
                    contentType = 'application/x-www-form-urlencoded';
                if (contentType.indexOf("application/json") != -1)
                {
                    if (!(typeof postData == 'string' || postData instanceof String))
                        postData = JSON.stringify(postData);
                }
                if (contentType.indexOf("application/x-www-form-urlencoded") != -1)
                    postData = this.urlEncode(postData);
            }
            else if (!method)
                method = "GET";
            var req = this.createXMLHTTPObject();
            if (!req)
                return;

            // req.onerror = function (e)
            // {
            //     console.log(e);
            //     console.log("Error Status: " + e.target.status);
            // };

            // console.log("method")
            // console.log(method)
            // console.log("contentType")
            // console.log(contentType)
            // console.log("postData")
            // console.log(postData);
            
            req.open(method, url, true);
            // req.setRequestHeader('User-Agent', 'XMLHTTP/1.0');
            req.setRequestHeader('cache-control', 'no-cache');
            if (postData)
                req.setRequestHeader('Content-type', contentType);
            req.onreadystatechange = function ()
            {
                if (req.readyState != 4)
                    return;
                if (!(req.status != 200 && req.status != 304 && req.status != 0))
                {
                    if (onSuccess)
                        onSuccess(req.responseText);
                }
                if (req.status != 200 && req.status != 304 && req.status != 0)
                {
                    if (onError)
                        onError(req);
                    else
                    {
                        alert('HTTP error ' + req.status);
                    }
                }
                if (onDone)
                    onDone(req);
            };
            if (req.readyState == 4)
                return;
            req.send(postData);
        };
        return RequestBase;
    })();
    Http.RequestBase = RequestBase; // End Class RequestBase
    var Get = (function (_super)
    {
        __extends(Get, _super);
        function Get(pURL, pData)
        {
            _super.call(this, pURL, pData);
            this.contentType = "application/urlencode";
            this.method = "GET";
        }
        Get.prototype.send = function ()
        {
            this.Complete = false;
            this.sendRequest(this.url, this.urlEncode(this.data), this.successCallback.bind(this), this.failureCallback.bind(this), this.alwaysCallback.bind(this), null, this.contentType, this.method);
        };
        return Get;
    })(RequestBase);
    Http.Get = Get; // End Class Get
    var Json = (function (_super)
    {
        __extends(Json, _super);
        function Json(pURL, pData)
        {
            _super.call(this, pURL, pData);
            this.contentType = "application/json; charset=UTF-8";
            this.method = "POST";
        }
        Json.prototype.send = function ()
        {
            this.Complete = false;
            this.sendRequest(this.url, "", this.successCallback.bind(this), this.failureCallback.bind(this), this.alwaysCallback.bind(this), this.data, this.contentType, this.method);
        };
        return Json;
    })(RequestBase);
    Http.Json = Json; // End Class Json 
    var Post = (function (_super)
    {
        __extends(Post, _super);
        function Post(pURL, pData)
        {
            _super.call(this, pURL, pData);
            this.contentType = "application/x-www-form-urlencoded";
            this.method = "POST";
        }
        Post.prototype.send = function ()
        {
            this.Complete = false;
            this.sendRequest(this.url, "", this.successCallback.bind(this), this.failureCallback.bind(this), this.alwaysCallback.bind(this), this.data, this.contentType, this.method);
        };
        return Post;
    })(RequestBase);
    Http.Post = Post; // End Class Post
    var RequestChain = (function ()
    {
        function RequestChain()
        {
            this.Requests = [];
            this.m_OnComplete = [];
        }
        RequestChain.prototype.internalComplete = function ()
        {
            var allComplete = true;
            for (var i = 0; i < this.Requests.length; ++i)
            {
                if (!this.Requests[i].Complete)
                {
                    allComplete = false;
                    break;
                }
            }
            if (allComplete)
            {
                // console.log("all complete");
                for (var i = 0; i < this.m_OnComplete.length; ++i)
                {
                    this.m_OnComplete[i]();
                }
            }
        }; // End Function internalComplete
        RequestChain.prototype.add = function ()
        {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++)
            {
                args[_i - 0] = arguments[_i];
            }
            var me = this;
            for (var i = 0; i < arguments.length; ++i)
            {
                arguments[i].always(function ()
                {
                    me.internalComplete();
                });
                this.Requests.push(arguments[i]);
            } // Next i
            return this;
        };
        RequestChain.prototype.addRange = function (req)
        {
            var me = this;
            for (var i = 0; i < req.length; ++i)
            {
                req[i].always(function ()
                {
                    me.internalComplete();
                });
                this.Requests.push(req[i]);
            } // Next i
            return this;
        };
        RequestChain.prototype.whenDone = function (cb)
        {
            this.m_OnComplete.push(cb);
            return this;
        };
        RequestChain.prototype.process = function ()
        {
            for (var i = 0; i < this.Requests.length; ++i)
            {
                this.Requests[i].send();
            }
        };
        return RequestChain;
    })();
    Http.RequestChain = RequestChain; // End Class RequestChain
})(Http || (Http = {})); // End Namespace
// new Http.Json("foo", null).send()
// new Http.Get("foo", null).send()
// new Http.Post("foo", null).send()
// new Http.RequestChain().add(req1).add(req2)
//  .whenDone(
//      function ()
//      {
//          alert("foo!");
//          console.log(this);
//      }.bind(this)
//  ).process();
