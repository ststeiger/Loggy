
using System.Net.Mail;


namespace Loggy
{



    public class ErrorMailer : ErrorDispatcher
    {
        public override void LogException(
              string origin
            , int level
            , System.Exception exception
            , System.Data.Common.DbCommand cmd
            , string logMessage)
        {

        }
    }


} 
