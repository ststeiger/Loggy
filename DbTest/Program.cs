
namespace DbTest
{


    class MainClass
    {


        public static void Test()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = System.Environment.MachineName;
            csb.DataSource = @"10.1.1.8"; // Must be IP, NETBIOS doesn't resolve on Linux

            // SQL Server Configuration Manager
            // SQL Server 2016 C:\Windows\SysWOW64\SQLServerManager13.msc
            // SQL Server 2014 C:\Windows\SysWOW64\SQLServerManager12.msc
            // SQL Server 2012 C:\Windows\SysWOW64\SQLServerManager11.msc
            // SQL Server 2008 C:\Windows\SysWOW64\SQLServerManager10.msc
            // in Network-Configuration: Activate TCP/IP & Restart Service 

            // Open firewall port for SQL-Server
            // - Windows 10: 
            //      netsh advfirewall firewall add rule name="SQL Server" dir=in action=allow protocol=TCP localport=1433
            // - Windows < 10: 
            //      netsh firewall set portopening TCP 1433 "SQLServer"

            // https://support.microsoft.com/en-us/kb/968872
            // https://blog.brankovucinec.com/2015/12/04/scripts-to-open-windows-firewall-ports-for-sql-server/


            csb.IntegratedSecurity = false;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "LoggyWebServices";
                csb.Password = "Test123";
            }

            csb.InitialCatalog = "Aperture_RSI";



            Loggy.cDAL DAL = Loggy.cDAL.CreateInstance();
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
        


        public static void Main(string[] args)
        {
            Test();
            System.Console.WriteLine("Hello World!");
        }


    }


}
