using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy.ajax
{


    public class ConnectionInfo
    {
        public System.DateTime TimeStamp;
        public System.Data.Common.DbConnection Connection;
        public System.Data.Common.DbTransaction Transaction;


        public ConnectionInfo(System.Data.Common.DbConnection connection)
        {
            this.Connection = connection;
        }

    }


    /// <summary>
    /// Summary description for exsql
    /// </summary>
    public class exsql : IHttpHandler
    {

        public static string getconstr()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.IntegratedSecurity = false;

            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "ApertureWebServicesEN";
                csb.Password = "meridian08";
            }

            csb.DataSource= "127.0.0.1";
            csb.InitialCatalog = "SwissRe_Test";

            return csb.ConnectionString;
        }


        public static System.Data.Common.DbConnection getcon()
        {
            System.Data.Common.DbConnection con = new System.Data.SqlClient.SqlConnection(getconstr());
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
            return con;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            
            string command = context.Request.QueryString["cmd"];
            string uid = context.Request.QueryString["uid"];
            string sql = context.Request.QueryString["sql"];

            if (System.StringComparer.OrdinalIgnoreCase.Equals(command, "add"))
            {
                if (Global.ConnectionDix.ContainsKey(uid))
                {
                    ConnectionInfo ci = Global.ConnectionDix[uid];
                    if(ci.Connection.State != System.Data.ConnectionState.Closed)
                        ci.Connection.Close();

                    Global.ConnectionDix.Remove(uid);
                }

                Global.ConnectionDix.Add(uid, new ConnectionInfo(getcon()));
                context.Response.Write("added");
                return;
            }

            if (System.StringComparer.OrdinalIgnoreCase.Equals(command, "run"))
            {
                ConnectionInfo ci = Global.ConnectionDix[uid];

                System.Data.Common.DbCommand cmd = ci.Connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Transaction = ci.Connection.BeginTransaction();
                ci.Transaction = cmd.Transaction;

                object obj = cmd.ExecuteScalar();
                context.Response.Write(obj.ToString());
                return;
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals(command, "exe"))
            {
                ConnectionInfo ci = Global.ConnectionDix[uid];

                System.Data.Common.DbCommand cmd = ci.Connection.CreateCommand();
                cmd.CommandText = sql;

                if (ci.Transaction != null && ci.Transaction.Connection != null)
                {
                    // First undo existing running transaction 
                    // parallel transactions are not supported...
                    ci.Transaction.Rollback();
                }

                cmd.Transaction = ci.Connection.BeginTransaction();
                ci.Transaction = cmd.Transaction;


                System.Data.SqlClient.SqlConnection conny = (System.Data.SqlClient.SqlConnection)ci.Connection;

                conny.InfoMessage += delegate (object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
                {
                    context.Response.Write("Message: " + e.Message + "\r\n");
                };


                int res= cmd.ExecuteNonQuery();
                context.Response.Write("Affected: " + res.ToString() + "\r\n");
                return;
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals(command, "commit"))
            {
                ConnectionInfo ci = Global.ConnectionDix[uid];
                if(ci.Transaction.Connection != null)
                    ci.Transaction.Commit();
                context.Response.Write("Committed");
                return;
            }

            if (System.StringComparer.OrdinalIgnoreCase.Equals(command, "Rollback"))
            {
                ConnectionInfo ci = Global.ConnectionDix[uid];

                if(ci.Transaction.Connection != null)
                    ci.Transaction.Rollback();

                context.Response.Write("Rolled back");
                return;
            }

            context.Response.Write("nothing");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}