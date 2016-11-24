using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy
{
    public class DAL
    {


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
            string strRetVal = null;

            // http://msdn.microsoft.com/en-us/library/cc716729.aspx
            switch (type)
            {
                case System.Data.DbType.Guid:
                    strRetVal = "uniqueidentifier";
                    break;
                case System.Data.DbType.Date:
                    strRetVal = "date";
                    break;
                case System.Data.DbType.Time:
                    strRetVal = "time(7)";
                    break;
                case System.Data.DbType.DateTime:
                    strRetVal = "datetime";
                    break;
                case System.Data.DbType.DateTime2:
                    strRetVal = "datetime2";
                    break;
                case System.Data.DbType.DateTimeOffset:
                    strRetVal = "datetimeoffset(7)";
                    break;

                case System.Data.DbType.StringFixedLength:
                    strRetVal = "nchar(MAX)";
                    break;
                case System.Data.DbType.String:
                    strRetVal = "nvarchar(MAX)";
                    break;

                case System.Data.DbType.AnsiStringFixedLength:
                    strRetVal = "char(MAX)";
                    break;
                case System.Data.DbType.AnsiString:
                    strRetVal = "varchar(MAX)";
                    break;

                case System.Data.DbType.Single:
                    strRetVal = "real";
                    break;
                case System.Data.DbType.Double:
                    strRetVal = "float";
                    break;
                case System.Data.DbType.Decimal:
                    strRetVal = "decimal(19, 5)";
                    break;
                case System.Data.DbType.VarNumeric:
                    strRetVal = "numeric(19, 5)";
                    break;

                case System.Data.DbType.Boolean:
                    strRetVal = "bit";
                    break;
                case System.Data.DbType.SByte:
                case System.Data.DbType.Byte:
                    strRetVal = "tinyint";
                    break;
                case System.Data.DbType.Int16:
                    strRetVal = "smallint";
                    break;
                case System.Data.DbType.Int32:
                    strRetVal = "int";
                    break;
                case System.Data.DbType.Int64:
                    strRetVal = "bigint";
                    break;
                case System.Data.DbType.Xml:
                    strRetVal = "xml";
                    break;
                case System.Data.DbType.Binary:
                    strRetVal = "varbinary(MAX)"; // or image
                    break;
                case System.Data.DbType.Currency:
                    strRetVal = "money";
                    break;
                case System.Data.DbType.Object:
                    strRetVal = "sql_variant";
                    break;

                case System.Data.DbType.UInt16:
                case System.Data.DbType.UInt32:
                case System.Data.DbType.UInt64:
                    throw new System.NotImplementedException("Uints not mapped - MySQL only");
            }

            return strRetVal;
        } // End Function SqlTypeFromDbType


        public virtual System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue)
        {
            return AddParameter(command, strParameterName, objValue, System.Data.ParameterDirection.Input);
        } // End Function AddParameter


        public virtual System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad)
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


        public virtual System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad, System.Data.DbType dbType)
        {
            System.Data.IDbDataParameter parameter = command.CreateParameter();

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


        public virtual T GetParameterValue<T>(System.Data.IDbCommand idbc, string strParameterName)
        {
            if (!strParameterName.StartsWith("@"))
            {
                strParameterName = "@" + strParameterName;
            }

            return InlineTypeAssignHelper<T>(((System.Data.IDbDataParameter)idbc.Parameters[strParameterName]).Value);
        } // End Function GetParameterValue<T>

        protected static T InlineTypeAssignHelper<T>(object UTO)
        {
            if (UTO == null)
            {
                T NullSubstitute = default(T);
                return NullSubstitute;
            }
            return (T)UTO;
        } // End Template InlineTypeAssignHelper



        public static string GetParameters(System.Data.IDbCommand cmd)
        {
            string strReturnValue = "";
            try
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                System.DateTime dtLogTime = System.DateTime.UtcNow;

                if (cmd == null || string.IsNullOrEmpty(cmd.CommandText))
                {
                    return strReturnValue;
                }


                if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                {
                    msg.AppendLine("-- ***** Listing Parameters *****");

                    foreach (System.Data.IDataParameter idpThisParameter in cmd.Parameters)
                    {
                        // http://msdn.microsoft.com/en-us/library/cc716729.aspx
                        msg.AppendLine(string.Format("DECLARE {0} AS {1} -- DbType: {2}", idpThisParameter.ParameterName, SqlTypeFromDbType(idpThisParameter.DbType), idpThisParameter.DbType.ToString()));
                    } // Next idpThisParameter

                    msg.AppendLine(Environment.NewLine);
                    msg.AppendLine(Environment.NewLine);

                    foreach (System.Data.IDataParameter idpThisParameter in cmd.Parameters)
                    {
                        string strParameterValue = null;
                        if (object.ReferenceEquals(idpThisParameter.Value, System.DBNull.Value))
                        {
                            strParameterValue = "NULL";
                        }
                        else
                        {
                            if (idpThisParameter.DbType == System.Data.DbType.Date)
                                strParameterValue = String.Format(DATEFORMAT, idpThisParameter.Value);
                            else if (idpThisParameter.DbType == System.Data.DbType.DateTime || idpThisParameter.DbType == System.Data.DbType.DateTime2)
                                strParameterValue = String.Format(DATETIMEFORMAT, idpThisParameter.Value);
                            else
                                strParameterValue = idpThisParameter.Value.ToString();

                            strParameterValue = "'" + strParameterValue.Replace("'", "''") + "'";
                        }

                        msg.AppendLine(string.Format("SET {0} = {1}", idpThisParameter.ParameterName, strParameterValue));
                    }

                    msg.AppendLine("-- ***** End Parameter Listing *****");
                    msg.AppendLine(Environment.NewLine);
                } // End if (cmd.Parameters != null && cmd.Parameters.Count > 0)

                strReturnValue = msg.ToString();
                msg = null;
            }
            catch (Exception ex)
            {
                strReturnValue = "Error in Function COR.SQL.GetParametrizedQueryText";
                strReturnValue += Environment.NewLine;
                strReturnValue += ex.Message;
            }

            return strReturnValue;
        } // End Function GetParametrizedQueryText


        public static string GetParametrizedQueryText(System.Data.IDbCommand cmd)
        {
            string strReturnValue = "";
            try
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                System.DateTime dtLogTime = System.DateTime.UtcNow;

                if (cmd == null || string.IsNullOrEmpty(cmd.CommandText))
                {
                    return strReturnValue;
                }


                if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                {
                    msg.AppendLine("-- ***** Listing Parameters *****");

                    foreach (System.Data.IDataParameter idpThisParameter in cmd.Parameters)
                    {
                        // http://msdn.microsoft.com/en-us/library/cc716729.aspx
                        msg.AppendLine(string.Format("DECLARE {0} AS {1} -- DbType: {2}", idpThisParameter.ParameterName, SqlTypeFromDbType(idpThisParameter.DbType), idpThisParameter.DbType.ToString()));
                    } // Next idpThisParameter

                    msg.AppendLine(Environment.NewLine);
                    msg.AppendLine(Environment.NewLine);

                    foreach (System.Data.IDataParameter idpThisParameter in cmd.Parameters)
                    {
                        string strParameterValue = null;
                        if (object.ReferenceEquals(idpThisParameter.Value, System.DBNull.Value))
                        {
                            strParameterValue = "NULL";
                        }
                        else
                        {
                            if (idpThisParameter.DbType == System.Data.DbType.Date)
                                strParameterValue = String.Format(DATEFORMAT, idpThisParameter.Value);
                            else if (idpThisParameter.DbType == System.Data.DbType.DateTime || idpThisParameter.DbType == System.Data.DbType.DateTime2)
                                strParameterValue = String.Format(DATETIMEFORMAT, idpThisParameter.Value);
                            else
                                strParameterValue = idpThisParameter.Value.ToString();

                            strParameterValue = "'" + strParameterValue.Replace("'", "''") + "'";
                        }

                        msg.AppendLine(string.Format("SET {0} = {1}", idpThisParameter.ParameterName, strParameterValue));
                    }

                    msg.AppendLine("-- ***** End Parameter Listing *****");
                    msg.AppendLine(Environment.NewLine);
                } // End if (cmd.Parameters != null && cmd.Parameters.Count > 0)


                msg.AppendLine(string.Format("-- ***** Start Query from {0} *****", dtLogTime.ToString(DATEFORMAT)));
                msg.AppendLine(cmd.CommandText);
                msg.AppendLine(string.Format("-- ***** End Query from {0} *****", dtLogTime.ToString(DATEFORMAT)));
                msg.AppendLine(Environment.NewLine);

                strReturnValue = msg.ToString();
                msg = null;
            }
            catch (Exception ex)
            {
                strReturnValue = "Error in Function COR.SQL.GetParametrizedQueryText";
                strReturnValue += Environment.NewLine;
                strReturnValue += ex.Message;
            }

            return strReturnValue;
        } // End Function GetParametrizedQueryText


        public static System.Data.Common.DbConnection GetConnection()
        {
            return GetConnection(null);
        } // End Function GetConnection


        public static System.Data.Common.DbConnection GetConnection(string strConnectionString)
        {
            if (string.IsNullOrEmpty(strConnectionString))
            {
                strConnectionString = GetConnectionString();
            }

            System.Data.Common.DbConnection idbcon = m_ProviderFactory.CreateConnection();
            idbcon.ConnectionString = strConnectionString;

            return idbcon;
        } // End Function GetConnection

        public static System.Data.IDbCommand CreateCommand(string strSQL)
        {
            return CreateCommand(strSQL, 30);
        }



        public static System.Data.IDbCommand CreateCommand(string strSQL, int timeout)
        {
            System.Data.IDbCommand idbc = m_ProviderFactory.CreateCommand();

            if (IsPostGreSql)
            {
                strSQL = ConvertTopNStatement(strSQL);
                strSQL = ReplaceOdbcFunctions(strSQL);
            }

            if (!string.IsNullOrEmpty(strSQL))
            {
                idbc.CommandText = strSQL;
            }

            idbc.CommandTimeout = Math.Max(timeout, 0);

            return idbc;
        } // End Function CreateCommand


        public static System.Data.IDbCommand CreateCommand()
        {
            return CreateCommand(null);
        } // End Function CreateCommand


        // Warning: getMandant() liefert immer den entsprechenden Mandant...
        //public static System.Data.IDbCommand CreateCommandFromFile(string strEmbeddedFileName)
        //{
        //    Int32 intBE_ID = -1;
        //    // Mandant manMandant = Mandant.Global;
        //    Mandant manMandant = getMandant();
        //    return CreateCommandFromFile(strEmbeddedFileName, intBE_ID, manMandant);
        // }

        public static System.Data.IDbCommand CreateCommandFromFile(string strEmbeddedFileName, Int32 intBE_ID)
        {
            return CreateCommandFromFile(strEmbeddedFileName, intBE_ID, 30);
        }


        public static System.Data.IDbCommand CreateCommandFromFile(string strEmbeddedFileName, Int32 intBE_ID, int timeOut)
        {
            //Mandant enuMandant = Mandant.Global;
            Mandant enuMandant = SQL.getMandant();

            return CreateCommandFromFile(strEmbeddedFileName, intBE_ID, enuMandant, timeOut);
        }



        public static System.Data.IDbCommand CreateCommandFromFile(string strEmbeddedFileName, Mandant enuMandant)
        {
            return CreateCommandFromFile(strEmbeddedFileName, enuMandant, 30);
        }


        public static System.Data.IDbCommand CreateCommandFromFile(string strEmbeddedFileName, Mandant enuMandant, int timeOut)
        {
            Int32 intBE_ID = -1;

            return CreateCommandFromFile(strEmbeddedFileName, intBE_ID, enuMandant, timeOut);
        }


        public static System.Data.IDbCommand CreateCommandFromFile(string strEmbeddedFileName, Int32 intBE_ID, Mandant enuMandant)
        {
            return CreateCommandFromFile(strEmbeddedFileName, intBE_ID, enuMandant, 30);
        }


        public static System.Data.IDbCommand CreateCommandFromFile(string strEmbeddedFileName, Int32 intBE_ID, Mandant enuMandant, int Timeout)
        {
            //Start: Rico Test
            if (!string.IsNullOrEmpty(strEmbeddedFileName) && !strEmbeddedFileName.StartsWith(".")) strEmbeddedFileName = "." + strEmbeddedFileName;
            //End: Rico Test

            string strRessourceName = string.Empty;
            string strSQL = GetEmbeddedSqlScript(strEmbeddedFileName, enuMandant, ref strRessourceName);

            System.Data.IDbCommand tCommand = CreateCommand(strSQL);
            tCommand.CommandTimeout = Timeout;

            strRessourceName = strRessourceName.Substring(strRessourceName.IndexOf('.') + 1);
            strRessourceName = strRessourceName.Substring(strRessourceName.IndexOf('.'));

            AddParameter(tCommand, "@___ResourceName", strRessourceName);
            AddParameter(tCommand, "@BE_ID", intBE_ID);
            AddParameter(tCommand, "@MDT_ID", (int)enuMandant);

            return tCommand;
        }



        public void ErrorLog(string sPathName, string sErrMsg)
        {
            string sErrorTime = System.DateTime.Now.ToString("yyyyMMdd'T'HHmmss", System.Globalization.CultureInfo.InvariantCulture);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(sPathName + sErrorTime, true);
            // sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }
}