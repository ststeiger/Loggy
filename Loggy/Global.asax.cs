
#define WITH_CONNECTION 
// #undef WITH_CONNECTION

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


namespace Loggy
{


    // http://stackoverflow.com/questions/14845947/unicode-bug-symbol-that-can-display-inside-browsers
    // http://xahlee.info/comp/unicode_animals.html
    // http://www.fileformat.info/info/unicode/char/1f41b/index.htm
    // http://www.fileformat.info/info/unicode/char/1f41e/index.htm
    // https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRc41xXlE6cGlEZNm-zakIoMPOojxX8m3nDn8z-E0ayplokzX86
    // http://i.stack.imgur.com/xY8bc.jpg
    // http://meta.stackexchange.com/questions/271986/arabic-diacritic-unicode-characters-break-title-links
    // https://www.cnet.com/news/quirky-ios-text-bug-works-in-twitter-snapchat/
    // https://bugtracker.zoho.com/portal/cormanagement
    // http://erraticdev.blogspot.de/2011/01/how-to-correctly-use-ihttpmodule-to.html
    // http://stackoverflow.com/questions/18829677/why-does-this-code-even-compile
    public class Global : System.Web.HttpApplication
    {


        // The Application_Start and Application_End methods are special methods that do not represent HttpApplication events. 
        // ASP.NET calls them once for the lifetime of the application domain, not for each HttpApplication instance.
        // IIS uses something called application pools. And each pool can have an arbitrary number of HttpApplication instances. Yes multiple. 
        // Application starting creates all these instances. 
        // Every one of them initializes their own list of modules but only the first one executes the Application_OnStart event handler.
        protected void Application_Start(object sender, EventArgs e)
        {
            moduleError.Init(this);
            System.Console.WriteLine("Redirect");
            System.Console.SetOut(new DebugTextWriter());
            System.Console.SetError(new DebugTextWriter());
            System.Console.WriteLine("Redirected");

            Test();
        }
        // public static IHttpModule CookieAuth = new ErrorLogModule();
        // public static IHttpModule modul404 = new ErrorLogModule();
        public static IHttpModule moduleError = new ErrorLogModule();

        // The Application_Start and Application_End methods are special methods that do not represent HttpApplication events. 
        // ASP.NET calls them once for the lifetime of the application domain, not for each HttpApplication instance.
        public override void Init()
        {
            moduleError.Init(this);
            base.Init();
        }

        public static void Test()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = System.Environment.MachineName;
            csb.IntegratedSecurity = true;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "ApertureWebServicesDE";
                csb.Password = "";
            }

            csb.InitialCatalog = "COR_Basic_Swisscom";



            cDAL DAL = cDAL.CreateInstance();
            DAL.ConnectionString = csb.ConnectionString;

            using (System.Data.Common.DbConnection dbConnection = DAL.GetConnection())
            {
                object objUser1 = DAL.ExecuteScalar("SELECT TOP 1 BE_User FROM T_Benutzer ORDER BY BE_User;", dbConnection);
                object objUser2 = DAL.ExecuteScalar("SELECT TOP 1 BE_User FROM T_Benutzer ORDER BY BE_ID;", dbConnection);

                DAL.ExecuteNonQuery("UPDATE T_Benutzer SET BE_Hash = BE_Hash;", dbConnection);
                DAL.ExecuteNonQuery("UPDATE T_Benutzer SET BE_Hash = BE_Hash;", dbConnection);





#if WITH_CONNECTION 
                //using (System.Data.Common.DbDataReader reader = DAL.ExecuteReader("SELECT * FROM T_Benutzer; SELECT * FROM T_Benutzergruppen;", dbConnection))
                using (System.Data.Common.DbDataReader reader = DAL.ExecuteReader_Buggy("SELECT * FROM T_Benutzer; SELECT * FROM T_Benutzergruppen;"))
#else
                DAL.ExecuteReader("SELECT * FROM T_Benutzer; SELECT * FROM T_Benutzergruppen;", delegate(System.Data.Common.DbDataReader reader)
#endif 
                
                
                    {

                        do
                        {
                            for (int i = 0; i < reader.FieldCount; ++i)
                            {
                                string fieldName = reader.GetName(i);
                                System.Type fieldType = reader.GetFieldType(i);

                                System.Console.WriteLine("{0}:\t{1}\t{2}", i, fieldName, fieldType.ToString());
                            } // Next i 


                            if (reader.HasRows)
                            {
                                int rowCount = 1;

                                while (reader.Read())
                                {

                                    System.Console.WriteLine(@"Row {0}", rowCount);
                                    for (int i = 0; i < reader.FieldCount; ++i)
                                    {
                                        string fieldName = reader.GetName(i);
                                        object fieldValue = reader.GetValue(i);

                                        System.Console.WriteLine(@" - {0}: {1}", fieldName, System.Convert.ToString(fieldValue));
                                    } // Next i 

                                    ++rowCount;
                                } // Whend 

                                --rowCount;
                            } // End if (reader.HasRows)

                        } while (reader.NextResult());

                    } // End Using reader 
#if !WITH_CONNECTION 
                );
#endif 
                object objUser3 = DAL.ExecuteScalar("SELECT TOP 1 BE_User FROM T_Benutzer ORDER BY BE_Hash;", dbConnection);
                object objUser4 = DAL.ExecuteScalar("SELECT TOP 1 BE_User FROM T_Benutzer ORDER BY BE_Passwort;", dbConnection);

            } // End Using dbConnection
                
        } // End Sub Test 


        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


    }


}
