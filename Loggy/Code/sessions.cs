
using System.Reflection;


namespace Loggy
{



    public class GetSessions : System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public static System.Collections.Generic.Dictionary<string, string> KeyValuePairs(System.Web.HttpContext context)
        {
            System.Collections.Generic.Dictionary<string, string> tL = 
                new System.Collections.Generic.Dictionary<string, string>();

            string tV = context.Request.QueryString["V"];
            if (!string.IsNullOrEmpty(tV))
            {
                foreach (string tVP in tV.Split(';'))
                {
                    string[] tS = tVP.Split('=');

                    if (!tL.ContainsKey(tS[0]))
                        tL.Add(tS[0], tS[1]);
                }
            }

            return tL;
        }


        public void ProcessRequest(System.Web.HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //Dim lsSessions As List(Of SessionStateItemCollection) = GetActiveSessions()
            //Dim lsSessions As Dictionary(Of String, Object) = GetActiveSessions()
            System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>> lsSessions = 
                GetActiveSessions(context);

            context.Response.Write("Current active sessions: " + lsSessions.Count.ToString() + System.Environment.NewLine);


            // string str = COR_Library.COR.Tools.JSON.JsonHelper.SerializePretty(lsSessions);
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(lsSessions, Newtonsoft.Json.Formatting.Indented);

            context.Response.Write(str);
        } // End Sub ProcessRequest


        // http://stackoverflow.com/questions/1470334/list-all-active-asp-net-sessions
        public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>>
            GetActiveSessions(System.Web.HttpContext context)
        {
            // Dictionary(Of String, Object) 'List(Of SessionStateItemCollection)
            // Dim lsSessionStates As New List(Of SessionStateItemCollection)


            // int strLcId = System.Web.HttpContext.Current.Session.LCID;
            // string strSeId = System.Web.HttpContext.Current.Session.SessionID;

            // System.Console.WriteLine(strLcId);
            // System.Console.WriteLine(strSeId);


            System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>> 
                dictAllSession = new System.Collections.Generic
                    .Dictionary<string, System.Collections.Generic.Dictionary<string, object>>();



            //System.Web.Caching.CacheMultiple
            object obj = typeof(System.Web.HttpRuntime)
                .GetProperty("CacheInternal", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null, null);

            // List(Of System.Web.Caching.CacheSingle) 
            object[] obj2 = (object[])obj.GetType().GetField("_caches", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(obj);


            System.Collections.Generic.Dictionary<string, string> tD = KeyValuePairs(context);


            for (int i = 0; i < obj2.Length; i++)
            {

                System.Collections.Hashtable c2 = (System.Collections.Hashtable)obj2[i].GetType()
                    .GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(obj2[i]);

                System.Collections.Generic.Dictionary<string, object> dictSession =
                    new System.Collections.Generic.Dictionary<string, object>();

                string strSessionId = null;


                foreach (System.Collections.DictionaryEntry entry in c2)
                {
                    object o1 = entry.Value.GetType().GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance)
                        .GetValue(entry.Value, null);
                    if (o1.GetType().ToString() == "System.Web.SessionState.InProcSessionState")
                    {
                        System.Web.SessionState.SessionStateItemCollection sess = 
                            (System.Web.SessionState.SessionStateItemCollection)
                            o1.GetType().GetField("_sessionItems", BindingFlags.NonPublic | BindingFlags.Instance)
                            .GetValue(o1);

                        if (sess != null)
                        {
                            // yield Return sess
                            // lsSessionStates.Add(sess)

                            System.Type tKeyType = entry.Key.GetType();



                            // System.Reflection.PropertyInfo[] pis  = tKeyType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
                            // System.Reflection.FieldInfo[] fis  = tKeyType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);


                            // System.Reflection.FieldInfo fi = tKeyType.GetField("Key");
                            System.Reflection.PropertyInfo pi = tKeyType.GetProperty("Key", BindingFlags.NonPublic | BindingFlags.Instance);
                            if (pi != null)
                            {
                                strSessionId = System.Convert.ToString(pi.GetValue(entry.Key, null));
                            }

                            // string str = (string) entry.Key.GetType().GetProperty("Key").GetValue(entry.Key, null);
                            
                            for (int tC = 0; tC <= sess.Keys.Count - 1; tC++)
                            {
                                if (tD.ContainsKey(sess.Keys[tC]))
                                {
                                    sess[sess.Keys[tC]] = tD[sess.Keys[tC]];
                                }
                            }


                            foreach (string tKey in sess.Keys)
                            {
                                // dictSession.Add(i.ToString() + "-" + tKey, sess[tKey]); ' WTF ???
                                dictSession[tKey] = sess[tKey];
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(strSessionId))
                {
                    strSessionId = i.ToString();
                }
                else
                {
                    strSessionId = i.ToString() + ": " + strSessionId;
                }

                dictAllSession.Add(strSessionId, dictSession);

            }

            return dictAllSession; // dictSession 'lsSessionStates
        } // GetActiveSessions


        public bool IsReusable
        {
            get { return false; }
        } // IsReusable


    } // End Class GetSessions 


} // End Namespace Loggy 
