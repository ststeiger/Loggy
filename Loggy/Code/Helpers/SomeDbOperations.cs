
#define WITH_CONNECTION 
// #undef WITH_CONNECTION


namespace Loggy
{


    public class SomeDbOperations
    {


        public static void Test()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = System.Environment.MachineName;

            csb.IntegratedSecurity = true;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "DAL_Test";
                csb.Password = "Test123";
            } // End if (!csb.IntegratedSecurity) 

            csb.InitialCatalog = "COR_Basic_Demo_V4";


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


    } // End Class SomeDbOperations


} // End Namespace Loggy
