
namespace Loggy
{


    // http://stackoverflow.com/questions/1172933/using-web-config-to-ban-user-agents
    public class UserAgentBanningModule : System.Web.IHttpModule
    {
        protected static readonly System.Text.RegularExpressions.Regex m_bannedUserAgentsRegex = null;
        protected static string m_strRedirectURL = null;


        static UserAgentBanningModule()
        {
            // http://eng.eelcowesemann.nl/linux-unix/nginx/nginx-blocking/
            // http://community.spiceworks.com/how_to/show/1443
            //string regex = @"(libwww-perl|msnbot/1\.1|msnbot|Java/|Purebot|Lipperhey|MaMa CaSpEr|Mail.Ru|gold crawler|MSIE)";//ConfigurationManager.AppSettings["UserAgentBasedRedirecter.UserAgentsRegex"];
            string regex = @"(libwww-perl|libcurl|sqlmap|msnbot|Java/|Purebot|Lipperhey|MaMa CaSpEr|Mail.Ru|gold crawler|MSIE 4.|MSIE 5.|MSIE 6.|MSIE 7.|MSIE 8.|MSIE 3.)";//ConfigurationManager.AppSettings["UserAgentBasedRedirecter.UserAgentsRegex"];
            System.Web.HttpContext.Current.Application["ProhibitedUserAgents"] = regex;
            
            //regex = ".*chrome.*";
            if (!string.IsNullOrEmpty(regex))
                m_bannedUserAgentsRegex = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
        } // End Static Constructor



        public void Init(System.Web.HttpApplication context)
        {
            context.PreRequestHandlerExecute += new System.EventHandler(RedirectMatchedUserAgents);
        } // End Sub Init


        private static void RedirectMatchedUserAgents(object sender, System.EventArgs e)
        {
            System.Web.HttpApplication app = sender as System.Web.HttpApplication;

            if (m_bannedUserAgentsRegex != null && app != null && app.Request != null && !string.IsNullOrEmpty(app.Request.UserAgent))
            {

                if (m_bannedUserAgentsRegex.Match(app.Request.UserAgent).Success)
                {

                    if (m_strRedirectURL == null)
                    {
                        //var cbAppContextBase = new HttpContextWrapper(app.Context);
                        //string strRedirectURL = System.Web.Mvc.UrlHelper.GenerateContentUrl("~/Ban/UserAgentBanned", cbAppContextBase);

                        // m_strRedirectURL = uh.Action("UserAgentBanned", "Ban");
                        
                    } // End if (m_strRedirectURL == null)

                    if (!System.StringComparer.OrdinalIgnoreCase.Equals(m_strRedirectURL, app.Request.Url.LocalPath))
                    {
                        app.Response.Redirect(m_strRedirectURL);
                    } // End if (!StringComparer.OrdinalIgnoreCase.Equals(m_strRedirectURL, app.Request.Url.LocalPath))

                } // End if (m_bannedUserAgentsRegex.Match(app.Request.UserAgent).Success)

            } // End if (_bannedUserAgentsRegex != null && app != null && app.Request != null && !String.IsNullOrEmpty(app.Request.UserAgent))

        } // End Sub RedirectMatchedUserAgents


        public void Dispose()
        {
        } // End Sub Dispose


    } // End Class cUserAgentBanningModule : System.Web.IHttpModule 


} // End Namespace MvcTools.HttpModules
