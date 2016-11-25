
#define WITH_CONNECTION 
// #undef WITH_CONNECTION

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Loggy
{


    public class Global : System.Web.HttpApplication
    {


        protected void Application_Start(object sender, EventArgs e)
        {
            System.Console.WriteLine("Redirect");
            System.Console.SetOut(new DebugTextWriter());
            System.Console.SetError(new DebugTextWriter());
            System.Console.WriteLine("Redirected");

            Test();
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
                using (System.Data.Common.DbDataReader reader = DAL.ExecuteReader("SELECT * FROM T_Benutzer; SELECT * FROM T_Benutzergruppen;", dbConnection))
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