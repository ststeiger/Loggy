using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy.Code
{

    // https://www.sans.org/reading-room/whitepapers/detection/detecting-preventing-anonymous-proxy-usage-32943
    // https://serverfault.com/questions/620522/does-a-company-have-implied-right-to-crawl-my-website


    // http://www.botreports.com/badbots/index.p11.shtml
    // https://perishablepress.com/2013-user-agent-blacklist/
    // https://www.projecthoneypot.org/comment_spammer_useragents.php
    // https://www.projecthoneypot.org/dictionary_attacker_usernames.php



    // http://www.bestyoucanget.com/badua.htm
    // https://journalxtra.com/blacklists/user-agent-blacklist/
    // https://perishablepress.com/2010-user-agent-blacklist/


    // https://perishablepress.com/ultimate-htaccess-blacklist/
    // hacktools
    // harvesters
    // spammers
    // libwww
    // copyright-audit
    // downloaders
    // crawlers
    



    // http://ask.xmodulo.com/block-specific-user-agents-nginx-web-server.html
    public class BlackList
    {


        // https://www.keycdn.com/blog/http-security-headers/
        // https://securityheaders.io/?q=https%3A%2F%2Fgithub.com
        // https://www.veracode.com/blog/2014/03/guidelines-for-setting-security-headers
        // http://docs.spring.io/spring-security/site/docs/current/reference/html/headers.html#headers
        public static void SetupAdditionalHeaders()
        {
            System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();

            // X-XSS-Protection is a HTTP header understood by Internet Explorer 8 (and newer versions). 
            // This header lets domains toggle on and off the "XSS Filter" of IE8, 
            // which prevents some categories of XSS attacks. 
            // IE8 has the filter activated by default, 
            // but servers can switch if off by setting  X-XSS-Protection: 0
            // [1; mode=block]
            nvc["X-XSS-Protection"] = "1";

            // https://www.owasp.org/index.php/Clickjacking_Defense_Cheat_Sheet
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
            nvc["X-Frame-Options"] = "SAMEORIGIN"; // DENY, ALLOW-FROM https://example.com/


            // Opt-out of MIME type sniffing 
            // Do not perform any MIME sniffing. Apply the MIME type given with Content-Type.
            // https://msdn.microsoft.com/en-us/library/gg622941(v=vs.85).aspx
            nvc["X-Content-Type-Options"] = "nosniff";


            // https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP
            // Content-Security-Policy: default-src 'self' *.trusted.com



            // Cache off
            // Cache-Control: no-cache, no-store, max-age=0, must-revalidate
            // Pragma: no-cache
            // Expires: 0


            // CORS
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Access_control_CORS
            // "Access-Control-Allow-Origin" 


            // X-UA-Compatible[IE=Edge,chrome=1,IE=edge]

            // https://stackoverflow.com/questions/25433258/what-is-the-x-request-id-http-header
            // https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet
            // https://www.owasp.org/index.php/Certificate_and_Public_Key_Pinning 
            // https://blog.qualys.com/ssllabs/2016/09/06/is-http-public-key-pinning-dead
            // UncommonHeaders[x-request-id,content-security-policy,strict-transport-security,public-key-pins,x-content-type-options,x-served-by,x-github-request-id],
            //  Strict-Transport-Security: max-age=86400; includeSubDomains


            // https://blog.alteroot.org/articles/2015-03-28/HTTP-alternative-services-and-opportunistic-encryption.html
            // It allows a http:// connection to use secure resources.
            //  Clients that support Alternative Services can open a connection and treat the alternative service as if it were the origin.
            // Just as importantly, using an alternative service doesn’t change the URL in the location bar
            // Alt-Svc: h2="new.example.org:443"; ma=600


        }


        // http://ask.xmodulo.com/block-specific-user-agents-nginx-web-server.html
        public string[] hacktools = new string[] { 
        "netcrawl",
        "npbot",
        "Antivirx",
        "Arian",
        "github.com/tenderlove/mechanize",
        "webbandit",
        "netcrawler",
        "backdoor",
        "malicious"

        };


        // https://neonprimetime.blogspot.ch/2016/09/snort-rules-monitoring-user-agents.html
        public string[] hacktools2 = new string[] { 
            "WPScan",
            "Wget",
            "Synapse",
            "sqlmap",
            "Python",
            "PycURL",
            "Paros",
            "OpenVAS",
            "Nmap",
            "Nikto",
            "Kazehakase",
            "curl"



        };


        
        public string[] highPriority = new string[] { 
            "Java",
            "Jakarta",
            "compatible ;", // Note the extra space. Email harvester.
            "libwww",
            "lwp-trivial",
            "curl",
            "PHP/",
            "urllib",
            "GT::WWW, ",
            "Snoopy",
            "MFC_Tear_Sample",
            "HTTP::Lite",
            "PHPCrawl",
            "URI::Fetch",
            "Zend_Http_Client",
            "http client",
            "PECL::HTTP",

            // http://johannburkard.de/blog/www/spam/panscient-com-bad-bot.html
            "panscient.com", // It creates bad requests and doesn’t respect robots.txt although they claim to do so.
            "IBM EVV",
            "Bork-edition",
            "Fetch API Request",
            "Missigua Locator",
            "Wells Search II",
            "ISC Systems iRc Search 2.1",
            "Microsoft URL Control",
            "Indy Library",
            "purebot",
            "pycurl",
            "g00g1e ",
            "WEP Search",
            "Nutch", // Highly extensible, highly scalable Web crawler - nutch.apache.org
            "larbin", // Larbin is an HTTP Web crawler that can fetch more than 5 million pages a day on a standard PC 

            // http://crawler.archive.org/index.html
            "heritrix", // Heritrix is the Internet Archive's open-source, extensible, web-scale, archival-quality web crawler project
            "ia_archiver",
            "",
        };

        // RewriteCond %{HTTP_USER_AGENT} (ia_archiver|g00g1e|seekerspider|siclab|spam|sqlmap) [NC]


        // https://cyberintruder.wordpress.com/2013/06/11/stop-the-script-kiddies-using-user-agent-blacklist/
        public string[] hacktools2a = new string[] { 
            "libwww-perl",
            "w3af.sourceforge.net",
            "dirbuster",
            "nikto",
            "sqlmap",
            "fimap", // local & remote file-inclusion bugs LFI/RFI bugs, http://tools.kali.org/web-applications/fimap
            // https://tha-imax.de/git/root/fimap
            // "https://code.google.com/hosting/moved?project=fimap"
            "nessus",
            "whatweb", // WhatWeb recognises web technologies including content management systems (CMS), http://whatweb.net/
            "Openvas ",
            "jbrofuzz ",
            "libwhisker ",
            "webshag  ",
            "WVS ",
            "BlackWidow  ",
            "Bot mailto:craftbot@yahoo.com ",
            "ChinaClaw  ",
            "Custo  ",
            "DISCo  ",
            "Download Demon  ",
            "eCatch  ",
            "Express WebPictures ",
            "EirGrabber ",
            "EmailSiphon  ",
            "EmailWolf  ",
            
            "ExtractorPro ",
            "EyeNetIE  ",
            "FlashGet  ",
            "GetRight  ",
            "GetWeb!  ",
            "Go!Zilla ",
            "Go-Ahead-Got-It ",
            "GrabNet  ",
            "Grafula  ",
            "HMView  ",
            "HTTrack  ",
            "Image Stripper  ",
            "Image Sucker ",
            "Indy Library ",
            "InterGET  ",
            "Internet Ninja ",
            "JetCar  ",
            "JOC Web Spider ",
            "larbin  ",
            "libghttp  ",
            "LeechFTP  ",
            "Mass Downloader ",
            "MIDown tool ",
            "Missigua  ",
            "Mister PiX ",
            "Navroad  ",
            "NearSite  ",
            "NetAnts  ",
            "NetSpider ",
            "Net Vampire [ ",
            "NetZIP  ",
            "Octopus  ",
            "Offline Explorer ",
            "Offline Navigator ",
            "PageGrabber  ",
            "Papa Foto ",
            "pavuk  ",
            "pcBrowser  ",
            "RealDownload  ",
            "ReGet  ",
            "SiteSnagger  ",
            "SmartDownload  ",
            "SuperBot  ",
            "SuperHTTP  ",
            "Surfbot  ",
            "tAkeOut  ",
            "Teleport Pro ",
            "VoidEYE ",
            "Web Image Collector ",
            "Web Sucker ",
            "WebAuto  ",
            "WebCopier  ",
            "WebFetch ",
            "WebGo IS ",
            "WebLeacher  ",
            "WebReaper  ",
            "WebSauger  ",
            "Website eXtractor ",
            "Website Quester [ ",
            "WebStripper  ",
            "WebWhacker  ",
            "WebZIP  ",
            "Widow  ",
            "WWWOFFLE  ",
            "Xaldon WebSpider ",
            "Zeus ",


            "bad_bot ",
            "AESOP_com_SpiderMan ",
            "Alexibot ",
            "Anonymouse.org ",
            "asterias ",
            "attach ",
            "BackDoorBot ",
            "BackWeb ",
            "Bandit ",
            "Baiduspider ",
            "BatchFTP ",
            "Bigfoot ",
            "Black.Hole ",
            "BlackWidow ",
            "BlowFish ",
            "BotALot ",
            "Buddy ",
            "BuiltBotTough ",
            "Bullseye ",
            "BunnySlippers ",
            "Cegbfeieh ",
            "CheeseBot ",
            "CherryPicker ",
            "Collector ",
            "Copier ",
            "CopyRightCheck ",
            "cosmos ",
            "Crescent ",
            "Curl ",
            "DISCo ",
            "DIIbot ",
            "DittoSpyder ",
            "Download ",
            "Download Demon ",
            "Download Devil ",
            "Download Wonder ",
            "Downloader ",
            "dragonfly ",
            "Drip ",


                        "EasyDL ",
            "ebingbong ",
            "EroCrawler ",
            "Exabot ",
                        "Extractor ",
            "FileHound ",
            "Foobot ",
            "flunky ",
            "FrontPage ",
            "GetSmart ",
            "Google Wireless Transcoder ",
            "gotit ",
                        "Grabber ",
            "GrabNet ",
            "Harvest ",
            "hloader ",
                        "httplib ",
            "HTTrack ",
            "humanlinks ",
            "ia_archiver ",
            "IlseBot ",
            "InfoNaviRobot ",
            "Iria ",
            "Internet Ninja ",
            "Jakarta ",
            "JennyBot ",
            "JetCar ",
            "JustView ",
            "LexiBot ",


            //SetEnvIfNoCase User-Agent "MaMa " keep_out
            //SetEnvIfNoCase User-Agent "choppy" keep_out
            //SetEnvIfNoCase User-Agent "heritrix" keep_out
            //SetEnvIfNoCase User-Agent "Purebot" keep_out
            //SetEnvIfNoCase User-Agent "PostRank" keep_out
            //SetEnvIfNoCase User-Agent "archive.org_bot" keep_out
            //SetEnvIfNoCase User-Agent "msnbot.htm)._" keep_out


            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
            "libwhisker ",
        };
        



    }
}