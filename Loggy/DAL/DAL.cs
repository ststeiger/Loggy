
namespace Loggy
{
    public delegate void DataReaderCallback_t(System.Data.Common.DbDataReader reader);


    public abstract partial class cDAL
    {
        protected System.Data.Common.DbProviderFactory m_ProviderFactory;
        protected string m_ConnectionString;

        protected const string DATEFORMAT = "yyyyMMdd";
        protected const string DATETIMEFORMAT = "yyyy-MM-ddTHH:mm:ss.fff";



        public virtual bool IsMS_SQL
        {
            get
            {
                return false;
            }
        } // End Property IsMS_SQL 


        public virtual bool IsPostGreSql
        {
            get
            {
                // System.Data.Common.DbProviderFactory providerFactory = null;
                // providerFactory = this.GetFactory(typeof(Npgsql.NpgsqlFactory));


                return false;
            }
        } // End Property IsPostGreSql 




        // Anything else than a struct or a class
        public virtual bool IsSimpleType(System.Type tThisType)
        {
            if (tThisType.IsPrimitive)
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.String)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.DateTime)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Decimal)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Object)))
            {
                return true;
            }

            return false;
        } // End Function IsSimpleType


        private static bool IsNullable(System.Type t)
        {
            if (t == null)
                return false;

            return t.IsGenericType && object.ReferenceEquals(t.GetGenericTypeDefinition(), typeof(System.Nullable<>));
        } // End Function IsNullable


        private static object MyChangeType(object objVal, System.Type t)
        {
            bool typeIsNullable = IsNullable(t);
            bool typeCanBeAssignedNull = !t.IsValueType || typeIsNullable;

            if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value))
            {
                if (typeCanBeAssignedNull)
                    return null;
                else
                    throw new System.ArgumentNullException("objVal ([DataSource] => SetProperty => MyChangeType => you're trying to NULL a type that NULL cannot be assigned to...)");
            }

            //getbasetype
            System.Type tThisType = objVal.GetType();

            if (typeIsNullable)
            {
                t = System.Nullable.GetUnderlyingType(t);
            }


            if (object.ReferenceEquals(tThisType, t))
                return objVal;

            // Convert Guid => string 
            if (object.ReferenceEquals(t, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return objVal.ToString();
            }

            // Convert string => Guid 
            if (object.ReferenceEquals(t, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                return new System.Guid(objVal.ToString());
            }

            return System.Convert.ChangeType(objVal, t);
        } // End Function MyChangeType


        protected const System.Reflection.BindingFlags m_CaseSensitivity = System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase
        ;


        public virtual System.Collections.Generic.List<T> GetList<T>(System.Data.Common.DbCommand cmd)
        {
            System.Collections.Generic.List<T> lsReturnValue = new System.Collections.Generic.List<T>();
            T tThisValue = default(T);
            System.Type tThisType = typeof(T);

            lock (cmd)
            {

                this.ExecuteReader(cmd, 
                    delegate (System.Data.Common.DbDataReader idr)
                    {

                        lock (idr)
                        {
                            if (IsSimpleType(tThisType))
                            {
                                while (idr.Read())
                                {
                                    object objVal = idr.GetValue(0);
                                    tThisValue = (T)MyChangeType(objVal, typeof(T));
                                    //tThisValue = System.Convert.ChangeType(objVal, T),

                                    lsReturnValue.Add(tThisValue);
                                } // End while (idr.Read())
                            }
                            else
                            {
                                int myi = idr.FieldCount;

                                System.Reflection.FieldInfo[] fis = new System.Reflection.FieldInfo[idr.FieldCount];
                                //Action<T, object>[] setters = new Action<T, object>[idr.FieldCount];

                                for (int i = 0; i < idr.FieldCount; ++i)
                                {
                                    string strName = idr.GetName(i);
                                    System.Reflection.FieldInfo fi = tThisType.GetField(strName, m_CaseSensitivity);
                                    fis[i] = fi;

                                    //if (fi != null)
                                    //    setters[i] = GetSetter<T>(fi);
                                } // Next i


                                while (idr.Read())
                                {
                                    //idr.GetOrdinal("")
                                    tThisValue = System.Activator.CreateInstance<T>();

                                    // Console.WriteLine(idr.FieldCount);
                                    for (int i = 0; i < idr.FieldCount; ++i)
                                    {
                                        string strName = idr.GetName(i);
                                        object objVal = idr.GetValue(i);

                                        //System.Reflection.FieldInfo fi = t.GetField(strName, m_CaseSensitivity);
                                        if (fis[i] != null)
                                        //if (fi != null)
                                        {
                                            //fi.SetValue(tThisValue, System.Convert.ChangeType(objVal, fi.FieldType));
                                            fis[i].SetValue(tThisValue, MyChangeType(objVal, fis[i].FieldType));
                                        } // End if (fi != null) 
                                        else
                                        {
                                            System.Reflection.PropertyInfo pi = tThisType.GetProperty(strName, m_CaseSensitivity);
                                            if (pi != null)
                                            {
                                                //pi.SetValue(tThisValue, System.Convert.ChangeType(objVal, pi.PropertyType), null);
                                                pi.SetValue(tThisValue, MyChangeType(objVal, pi.PropertyType), null);
                                            } // End if (pi != null)

                                            // Else silently ignore value
                                        } // End else of if (fi != null)

                                        //Console.WriteLine(strName);
                                    } // Next i

                                    lsReturnValue.Add(tThisValue);
                                } // Whend

                            } // End if IsSimpleType(tThisType)

                            idr.Close();
                        } // End Lock idr

                    }// End Delegate ExecuteReader
                );

            } // End lock cmd

            return lsReturnValue;
        } // End Function GetList


        public static System.Data.Common.DbProviderFactory GetFactory(System.Type type)
        {
            if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
            {
                // Provider factories are singletons with Instance field having
                // the sole instance
                System.Reflection.FieldInfo field = type.GetField(
                    "Instance"
                    ,System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static
                );

                if (field != null)
                {
                    return (System.Data.Common.DbProviderFactory)field.GetValue(null);
                    //return field.GetValue(null) as DbProviderFactory;
                } // End if (field != null)

            } // End if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))

            throw new System.InvalidOperationException("DataProvider is missing");
        } // End Function GetFactory


        public static cDAL CreateInstance(System.Data.Common.DbProviderFactory factory)
        {
            // this.m_ProviderFactory = factory;
            return null;
        }


        public static cDAL CreateInstance(System.Type type)
        {
            System.Data.Common.DbProviderFactory factory = GetFactory(type);
            return CreateInstance(factory);;
        }


        // https://github.com/dotnet/coreclr/issues/919
        protected static System.Collections.Generic.List<System.Type> GetTypesInNamespace(
            System.Reflection.Assembly assembly
            , string nameSpace
        )
        {
            System.Collections.Generic.List<System.Type> lsTypes = new System.Collections.Generic.List<System.Type>();

            System.Type[] types = assembly.GetTypes();

            for (int i = 0; i < types.Length; ++i)
            {
                if (types[i] != null && types[i].IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
                {
                    if (string.Equals(types[i].Namespace, nameSpace, System.StringComparison.Ordinal))
                        lsTypes.Add(types[i]);
                }

            }

            return lsTypes;
        }

        public static cDAL CreateInstance(string factoryNamespace)
        {
            System.Data.Common.DbProviderFactory factory = null;
            return CreateInstance(factory);
        }


        public static cDAL CreateInstance<T>()
        {
            System.Data.Common.DbProviderFactory factory = GetFactory(typeof(T));
            return CreateInstance(factory);
        }


        public static cDAL CreateInstance()
        {
            return new cMS_SQL();
        }



        public virtual string GetConnectionString(string initialCatalog)
        {
            string strReturnValue = null;
            string strProviderName = null;


            if (initialCatalog == null && !string.IsNullOrEmpty(this.m_ConnectionString))
                return this.m_ConnectionString;


            if (initialCatalog != null && !string.IsNullOrEmpty(this.m_ConnectionString))
            {
                System.Data.Common.DbConnectionStringBuilder sb = this.m_ProviderFactory.CreateConnectionStringBuilder();
                sb.ConnectionString = this.m_ConnectionString;
                sb["Database"] = initialCatalog;
                strReturnValue = sb.ConnectionString;
                sb = null;
                return strReturnValue;
            }


            string strConnectionStringName = System.Environment.MachineName;

            if (string.IsNullOrEmpty(strConnectionStringName))
            {
                strConnectionStringName = "LocalSqlServer";
            }

            System.Configuration.ConnectionStringSettingsCollection settings = System.Configuration.ConfigurationManager.ConnectionStrings;
            if ((settings != null))
            {
                foreach (System.Configuration.ConnectionStringSettings cs in settings)
                {
                    if (System.StringComparer.OrdinalIgnoreCase.Equals(cs.Name, strConnectionStringName))
                    {
                        strReturnValue = cs.ConnectionString;
                        strProviderName = cs.ProviderName;
                        break; // TODO: might not be correct. Was : Exit For
                    } // End if (System.StringComparer.OrdinalIgnoreCase.Equals(cs.Name, strConnectionStringName))

                } // Next cs 

            } // End if ((settings != null)) 

            if (string.IsNullOrEmpty(strReturnValue))
            {
                strConnectionStringName = "server";

                System.Configuration.ConnectionStringSettings conString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnectionStringName];

                if (conString != null)
                {
                    strReturnValue = conString.ConnectionString;
                } // End if (conString != null) 

            } // End if (string.IsNullOrEmpty(strReturnValue)) 

            if (string.IsNullOrEmpty(strReturnValue))
            {
                throw new System.ArgumentNullException("No connectionString \"" + strConnectionStringName + "\" in file web.config.");
            } // End if (string.IsNullOrEmpty(strReturnValue)) 

            settings = null;
            strConnectionStringName = null;

            // Got value from web.config at this point 
            { 
                System.Data.Common.DbConnectionStringBuilder sb = this.m_ProviderFactory.CreateConnectionStringBuilder();
                sb.ConnectionString = strReturnValue;


                if (string.IsNullOrEmpty(this.m_ConnectionString))
                {
                    if (!System.Convert.ToBoolean(sb["Integrated Security"]))
                    {
                        sb["Password"] = Cryptography.DES.DeCrypt(System.Convert.ToString(sb["Password"]));
                    }
                    strReturnValue = sb.ConnectionString;
                    this.m_ConnectionString = strReturnValue;
                } // End if (string.IsNullOrEmpty(this.m_ConnectionString)) 

                if (!string.IsNullOrEmpty(initialCatalog))
                {
                    sb["Database"] = initialCatalog;
                    strReturnValue = sb.ConnectionString;
                }

                sb = null;
            }

            return strReturnValue;
        } // End Function GetConnectionString


        public virtual string GetConnectionString()
        {
            return GetConnectionString(null);
        }


        public virtual string ConnectionString
        {
            get
            {
                if (m_ConnectionString != null)
                    return m_ConnectionString;

                m_ConnectionString = GetConnectionString();
                return m_ConnectionString;
            }
            set
            {
                m_ConnectionString = value;
            }
        }


        public virtual System.Data.Common.DbConnection GetConnection(string connectionString)
        {
            System.Data.Common.DbConnection connect = m_ProviderFactory.CreateConnection();

            if (string.IsNullOrEmpty(connectionString))
                connectionString = this.ConnectionString;

            connect.ConnectionString = connectionString;

            return connect;
        }


        public virtual System.Data.Common.DbConnection GetConnection()
        {
            return GetConnection(this.ConnectionString);
        }


        public System.Data.Common.DbCommand CreateCommand(string strSQL, int timeout)
        {
            System.Data.Common.DbCommand idbc = m_ProviderFactory.CreateCommand();
            idbc.CommandText = strSQL;
            idbc.CommandTimeout = timeout;
            return idbc;
        } // End Function CreateCommand


        public System.Data.Common.DbCommand CreateCommand(string strSQL)
        {
            return CreateCommand(strSQL, 30);
        }


        public System.Data.Common.DbCommand CreateCommand()
        {
            return CreateCommand(null);
        } // End Function CreateCommand


        // From Type to DBType
        protected virtual System.Data.DbType GetDbType(System.Type type)
        {
            // http://social.msdn.microsoft.com/Forums/en/winforms/thread/c6f3ab91-2198-402a-9a18-66ce442333a6
            string strTypeName = type.Name;
            System.Data.DbType DBtype = System.Data.DbType.String; // default value

            try
            {
                if (object.ReferenceEquals(type, typeof(System.DBNull)))
                {
                    return DBtype;
                }

                if (object.ReferenceEquals(type, typeof(System.Byte[])))
                {
                    return System.Data.DbType.Binary;
                }

                DBtype = (System.Data.DbType)System.Enum.Parse(typeof(System.Data.DbType), strTypeName, true);

                // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
                // http://msdn.microsoft.com/en-us/library/bbw6zyha(v=vs.71).aspx
                if (DBtype == System.Data.DbType.UInt64)
                    DBtype = System.Data.DbType.Int64;
            }
            catch (System.Exception)
            {
                // add error handling to suit your taste
            }

            return DBtype;
        } // End Function GetDbType


        protected virtual string SqlTypeFromDbType(System.Data.DbType type)
        {
            return type.ToString();
        } // End Function SqlTypeFromDbType


        public virtual System.Data.Common.DbParameter AddParameter(System.Data.Common.DbCommand command, string strParameterName
            , object objValue, System.Data.ParameterDirection pad, System.Data.DbType dbType)
        {
            System.Data.Common.DbParameter parameter = command.CreateParameter();

            if (!strParameterName.StartsWith("@"))
            {
                strParameterName = "@" + strParameterName;
            } // End if (!strParameterName.StartsWith("@"))

            parameter.ParameterName = strParameterName;
            parameter.DbType = dbType;
            parameter.Direction = pad;

            // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
            // No association  DbType UInt64 to a known SqlDbType

            if (objValue == null)
                parameter.Value = System.DBNull.Value;
            else
                parameter.Value = objValue;

            command.Parameters.Add(parameter);
            return parameter;
        } // End Function AddParameter


        public virtual System.Data.Common.DbParameter AddParameter(System.Data.Common.DbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad)
        {
            if (objValue == null)
            {
                //throw new ArgumentNullException("objValue");
                objValue = System.DBNull.Value;
            } // End if (objValue == null)

            System.Type tDataType = objValue.GetType();
            System.Data.DbType dbType = GetDbType(tDataType);

            return AddParameter(command, strParameterName, objValue, pad, dbType);
        } // End Function AddParameter


        public virtual System.Data.Common.DbParameter AddParameter(System.Data.Common.DbCommand command, string strParameterName, object objValue)
        {
            return AddParameter(command, strParameterName, objValue, System.Data.ParameterDirection.Input);
        } // End Function AddParameter


        public virtual T GetParameterValue<T>(System.Data.IDbCommand idbc, string parameterName)
        {
            if (!parameterName.StartsWith("@"))
            {
                parameterName = "@" + parameterName;
            }

            object obj = ((System.Data.Common.DbParameter)idbc.Parameters[parameterName]).Value;
            return (T)obj;
        } // End Function GetParameterValue<T>


        public virtual System.Data.Common.DbCommand CreateCommandFromFile(System.Reflection.Assembly asm, string embeddedFileName, int Timeout)
        {
            //Start: Rico Test
            if (!string.IsNullOrEmpty(embeddedFileName) && !embeddedFileName.StartsWith(".")) 
                embeddedFileName = "." + embeddedFileName;
            //End: Rico Test

            string strRessourceName = string.Empty;
            string strSQL = null; // GetEmbeddedSqlScript(strEmbeddedFileName, enuMandant, ref strRessourceName);

            System.Data.Common.DbCommand tCommand = this.CreateCommand(strSQL);
            tCommand.CommandTimeout = Timeout;

            strRessourceName = strRessourceName.Substring(strRessourceName.IndexOf('.') + 1);
            strRessourceName = strRessourceName.Substring(strRessourceName.IndexOf('.'));

            this.AddParameter(tCommand, "@___ResourceName", strRessourceName);
            // this.AddParameter(tCommand, "@BE_ID", intBE_ID);
            // this.AddParameter(tCommand, "@MDT_ID", (int)enuMandant);

            return tCommand;
        }


        public string GetParameters(System.Data.Common.DbCommand cmd)
        {
            string strReturnValue = "";

            if (cmd == null)
                return strReturnValue;

            try
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                System.DateTime dtLogTime = System.DateTime.UtcNow;

                if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                {
                    msg.AppendLine("-- ***** Listing Parameters *****");

                    foreach (System.Data.Common.DbParameter thisParameter in cmd.Parameters)
                    {
                        // http://msdn.microsoft.com/en-us/library/cc716729.aspx
                        msg.AppendLine(string.Format("DECLARE {0} AS {1} -- DbType: {2}", thisParameter.ParameterName, SqlTypeFromDbType(thisParameter.DbType), thisParameter.DbType.ToString()));
                    } // Next idpThisParameter

                    msg.AppendLine(System.Environment.NewLine);
                    msg.AppendLine(System.Environment.NewLine);

                    foreach (System.Data.IDataParameter thisParameter in cmd.Parameters)
                    {
                        string strParameterValue = null;
                        if (object.ReferenceEquals(thisParameter.Value, System.DBNull.Value))
                        {
                            strParameterValue = "NULL";
                        }
                        else
                        {
                            if (thisParameter.DbType == System.Data.DbType.Date)
                                strParameterValue = string.Format(DATEFORMAT, thisParameter.Value);
                            else if (thisParameter.DbType == System.Data.DbType.DateTime || thisParameter.DbType == System.Data.DbType.DateTime2)
                                strParameterValue = string.Format(DATETIMEFORMAT, thisParameter.Value);
                            else
                                strParameterValue = thisParameter.Value.ToString();

                            strParameterValue = "'" + strParameterValue.Replace("'", "''") + "'";
                        }

                        msg.AppendLine(string.Format("SET {0} = {1}", thisParameter.ParameterName, strParameterValue));
                    } // Next thisParameter 

                    msg.AppendLine("-- ***** End Parameter Listing *****");
                    msg.AppendLine(System.Environment.NewLine);
                } // End if (cmd.Parameters != null && cmd.Parameters.Count > 0)

                strReturnValue = msg.ToString();
                msg.Length = 0;
                msg = null;
            }
            catch (System.Exception ex)
            {
                strReturnValue = "Error in Function GetParameters";
                strReturnValue += System.Environment.NewLine;
                strReturnValue += ex.GetType().FullName;
                strReturnValue += System.Environment.NewLine;
                strReturnValue += ex.Message;
                strReturnValue += System.Environment.NewLine;
                strReturnValue += ex.StackTrace;
            }

            return strReturnValue;
        } // End Function GetParameters


        public string GetParametrizedQueryText(System.Data.Common.DbCommand cmd)
        {
            string strReturnValue = "";

            if (cmd == null)
                return strReturnValue;

            try
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                System.DateTime dtLogTime = System.DateTime.UtcNow;

                if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                {
                    msg.AppendLine("-- ***** Listing Parameters *****");

                    foreach (System.Data.IDataParameter thisParameter in cmd.Parameters)
                    {
                        // http://msdn.microsoft.com/en-us/library/cc716729.aspx
                        msg.AppendLine(string.Format("DECLARE {0} AS {1} -- DbType: {2}", thisParameter.ParameterName, SqlTypeFromDbType(thisParameter.DbType), thisParameter.DbType.ToString()));
                    } // Next idpThisParameter

                    msg.AppendLine(System.Environment.NewLine);
                    msg.AppendLine(System.Environment.NewLine);

                    foreach (System.Data.IDataParameter thisParameter in cmd.Parameters)
                    {
                        string strParameterValue = null;
                        if (object.ReferenceEquals(thisParameter.Value, System.DBNull.Value))
                        {
                            strParameterValue = "NULL";
                        }
                        else
                        {
                            if (thisParameter.DbType == System.Data.DbType.Date)
                                strParameterValue = string.Format(DATEFORMAT, thisParameter.Value);
                            else if (thisParameter.DbType == System.Data.DbType.DateTime || thisParameter.DbType == System.Data.DbType.DateTime2)
                                strParameterValue = string.Format(DATETIMEFORMAT, thisParameter.Value);
                            else
                                strParameterValue = thisParameter.Value.ToString();

                            strParameterValue = "'" + strParameterValue.Replace("'", "''") + "'";
                        }

                        msg.AppendLine(string.Format("SET {0} = {1}", thisParameter.ParameterName, strParameterValue));
                    } // Next thisParameter 
                    
                    msg.AppendLine("-- ***** End Parameter Listing *****");
                    msg.AppendLine(System.Environment.NewLine);
                } // End if (cmd.Parameters != null && cmd.Parameters.Count > 0)


                msg.AppendLine(string.Format("-- ***** Start Query from {0} *****", dtLogTime.ToString(DATEFORMAT)));
                if(cmd.CommandText != null)
                    msg.AppendLine(cmd.CommandText);
                msg.AppendLine(string.Format("-- ***** End Query from {0} *****", dtLogTime.ToString(DATEFORMAT)));
                msg.AppendLine(System.Environment.NewLine);

                strReturnValue = msg.ToString();
                msg = null;
            }
            catch (System.Exception ex)
            {
                strReturnValue = "Error in Function GetParametrizedQueryText";
                strReturnValue += System.Environment.NewLine;
                strReturnValue += ex.GetType().FullName;
                strReturnValue += System.Environment.NewLine;
                strReturnValue += ex.Message;
                strReturnValue += System.Environment.NewLine;
                strReturnValue += ex.StackTrace;
            }

            return strReturnValue;
        } // End Function GetParametrizedQueryText


        public void OpenConnection(System.Data.Common.DbConnection connect)
        {
            if (connect.State != System.Data.ConnectionState.Open)
                connect.Open();
        }


        public void CloseConnection(System.Data.Common.DbConnection connect)
        {
            if (connect.State != System.Data.ConnectionState.Closed)
                connect.Close();
        }


        public int ExecuteNonQuery(System.Data.Common.DbCommand cmd, System.Data.Common.DbConnection connection)
        {
            cmd.Connection = connection;
            this.OpenConnection(connection);
            return cmd.ExecuteNonQuery();
        }


        public int ExecuteNonQuery(System.Data.Common.DbCommand cmd, string connectionString)
        {
            int retVal = 0;

            using (System.Data.Common.DbConnection connection = this.GetConnection(connectionString))
            {
                retVal = this.ExecuteNonQuery(cmd, connection);
                this.CloseConnection(connection);
            }

            return retVal;
        }


        public int ExecuteNonQuery(string sql, System.Data.Common.DbConnection connection)
        {
            int retVal = 0;

            using (System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                retVal = this.ExecuteNonQuery(cmd, connection);
            }

            return retVal;
        }


        public int ExecuteNonQuery(string sql, string connectionString)
        {
            int retVal = 0;

            using (System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                retVal = this.ExecuteNonQuery(cmd, connectionString);
            }

            return retVal;
        }


        public int ExecuteNonQuery(System.Data.Common.DbCommand cmd)
        {
            return this.ExecuteNonQuery(cmd, this.ConnectionString);
        }


        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, this.ConnectionString);
        }


        public object ExecuteScalar(System.Data.Common.DbCommand cmd, System.Data.Common.DbConnection connection)
        {
            cmd.Connection = connection;
            this.OpenConnection(connection);
            return cmd.ExecuteScalar();
        }


        public object ExecuteScalar(System.Data.Common.DbCommand cmd, string connectionString)
        {
            object objRetVal = 0;

            using (System.Data.Common.DbConnection connect = GetConnection(connectionString))
            {
                objRetVal = this.ExecuteScalar(cmd, connect);

                this.CloseConnection(connect);
            }

            return objRetVal;
        }


        public object ExecuteScalar(string sql, System.Data.Common.DbConnection connection)
        {
            object objRetVal = 0;

            using (System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                objRetVal = this.ExecuteScalar(cmd, connection);
            }

            return objRetVal;
        }


        public object ExecuteScalar(string sql, string connectionString)
        {
            object objRetVal = 0;

            using (System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                objRetVal = this.ExecuteScalar(cmd, connectionString);
            }

            return objRetVal;
        }


        public object ExecuteScalar(System.Data.Common.DbCommand cmd)
        {
            return this.ExecuteScalar(cmd, this.ConnectionString);
        }


        public object ExecuteScalar(string sql)
        {
            return this.ExecuteScalar(sql, this.ConnectionString);
        }


        public System.Data.Common.DbDataReader ExecuteReader(System.Data.Common.DbCommand cmd, System.Data.CommandBehavior behaviour, System.Data.Common.DbConnection connection)
        {
            cmd.Connection = connection;
            this.OpenConnection(connection);

            return cmd.ExecuteReader(behaviour);
        }


        public System.Data.Common.DbDataReader ExecuteReader(string sql, System.Data.CommandBehavior behaviour, System.Data.Common.DbConnection connection)
        {
            System.Data.Common.DbDataReader dataReader = null;
            using(System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                dataReader = this.ExecuteReader(cmd, behaviour, connection);
            }

            return dataReader;
        }


        public System.Data.Common.DbDataReader ExecuteReader(System.Data.Common.DbCommand cmd, System.Data.Common.DbConnection connection)
        {
            return this.ExecuteReader(cmd,System.Data.CommandBehavior.SequentialAccess, connection);
        }


        public System.Data.Common.DbDataReader ExecuteReader(string sql, System.Data.Common.DbConnection connection)
        {
            System.Data.Common.DbDataReader dataReader = null;
            using(System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                dataReader = this.ExecuteReader(cmd, connection);
            }

            return dataReader;
        }

        // ConnectionLess

        public void ExecuteReader(System.Data.Common.DbCommand cmd, System.Data.CommandBehavior behaviour, DataReaderCallback_t readCallback)
        {
            using (System.Data.Common.DbConnection connect = this.GetConnection())
            {
                using (System.Data.Common.DbDataReader dataReader = this.ExecuteReader(cmd, behaviour, connect))
                {
                    readCallback(dataReader);
                }

                this.CloseConnection(connect);
            }

        } // End Sub ExecuteReader 


        public void ExecuteReader(string sql, System.Data.CommandBehavior behaviour, DataReaderCallback_t readCallback)
        {
            using(System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                this.ExecuteReader(cmd, behaviour, readCallback);
            }
        }


        public void ExecuteReader(System.Data.Common.DbCommand cmd, DataReaderCallback_t readCallback)
        {
            using (System.Data.Common.DbConnection dbConnection = this.GetConnection())
            {
                using (System.Data.Common.DbDataReader dataReader = this.ExecuteReader(cmd, dbConnection))
                {
                    readCallback(dataReader);
                } // End Using dataReader 

                this.CloseConnection(dbConnection);
            } // End Using dbConnection 

        }


        public void ExecuteReader(string sql, DataReaderCallback_t readCallback)
        {
            using (System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                this.ExecuteReader(cmd, readCallback);
            }

        } // End Sub ExecuteReader 



        // WARNING: BUGGY - CONNECTION IS DISPOSED BEFORE IT IS USED 
        internal System.Data.Common.DbDataReader ExecuteReader_Buggy(System.Data.Common.DbCommand cmd, System.Data.CommandBehavior behaviour)
        {
            System.Data.Common.DbDataReader dataReader = null;

            using (System.Data.Common.DbConnection connect = this.GetConnection())
            {
                dataReader = this.ExecuteReader(cmd, behaviour, connect);
                this.CloseConnection(connect);
            }

            return dataReader;
        } // End Function ExecuteReader_Buggy 


        // WARNING: BUGGY - CONNECTION IS DISPOSED BEFORE IT IS USED 
        internal System.Data.Common.DbDataReader ExecuteReader_Buggy(System.Data.Common.DbCommand cmd)
        {
            System.Data.Common.DbDataReader dataReader = null;

            using (System.Data.Common.DbConnection connect = this.GetConnection())
            {
                dataReader = this.ExecuteReader(cmd, connect);
                this.CloseConnection(connect);
            } // End Using connect 

            return dataReader;
        } // End Function ExecuteReader_Buggy 


        // WARNING: BUGGY - CONNECTION IS DISPOSED BEFORE IT IS USED 
        internal System.Data.Common.DbDataReader ExecuteReader_Buggy(string sql)
        {
            System.Data.Common.DbDataReader dataReader = null;

            using (System.Data.Common.DbCommand cmd = this.CreateCommand(sql))
            {
                dataReader = this.ExecuteReader_Buggy(cmd);
            }

            return dataReader;
        } // End Sub ExecuteReader 


    } // End Class cDAL 


} // End Namespace 
