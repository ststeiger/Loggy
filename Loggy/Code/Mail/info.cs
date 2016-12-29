
using System.Collections.Generic;


namespace Loggy
{


    public class ApplicationInfo
    {
        public string ApplicationName;
        public string AssemblyVersion;
        public string PublicIP;
        public string LocalIP;

        public string MachineName;
        public string InstallPath;
        public string FrameworkVersion;
        public string PipeLineMode;
        public string TrustLevel;
        public int Bitness;


        public string UserName;
        public string Domain;


        public string OS;
        public string OS_Version;
        public string ApplicationServer;
        public string Processor;

        public string[] Shares;
        public string[] DriveList;


        public System.Collections.Specialized.NameValueCollection EnvironmentVariables;
    }


    public class DbInfo
    {
        public string DbType;
        public string DbVersion;
        public string ConnectionString;
    }


    public class HttpInfo
    {
        public string Method;
        public string Url;
        public string Path; // Host + VirtDir
        public string Query; // Host + VirtDir
        public string UserAgent; // Screen-size
        public string Languages; // Culture

        public string Cookies;


        public string ClientIP;
        public string ForwardedClientIP;


        public System.Collections.Specialized.NameValueCollection RequestHeaders;
        public System.Collections.Specialized.NameValueCollection ResponseHeaders; // Server:Microsoft-IIS/7.5


        public System.Collections.Specialized.NameValueCollection GetParameters;
        public System.Collections.Specialized.NameValueCollection PostParameters;

        public System.Collections.Specialized.NameValueCollection Session;

        // InputStream

        public string FormsUserId;
        public string FormsUserName;
    }


    public class ExceptionInfo
    {
        public string Type;
        public string Source;
        public string StackTrace;
    }


    public class ErrorInfo
    {
        public System.DateTime When;
        public ApplicationInfo App;
        public DbInfo Db;
        public HttpInfo Http;
        public ExceptionInfo Exception;
    }


}
