
using System.Net.Mail;


namespace Loggy
{


    public abstract class ErrorDispatcher
    {


        public static System.Collections.Generic.Stack<System.Exception> GetExceptionStack(System.Exception exceptionChain)
        {
            System.Collections.Generic.Stack<System.Exception> stack = new System.Collections.Generic.Stack<System.Exception>();

            while (exceptionChain != null)
            {
                stack.Push(exceptionChain);
                exceptionChain = exceptionChain.InnerException;
            }

            return stack;
        }


        public virtual string StringifyException(System.Exception ex)
        {
            System.Guid entryUID = System.Guid.NewGuid();

            string type = ex.GetType().Name;
            string typeFullName = ex.GetType().FullName;
            string message = ex.Message;
            string stackTrace = ex.StackTrace;
            string source = ex.Source;
            System.Collections.IDictionary data = ex.Data;

            return ex.Message;
        }


        public virtual void LogException(
              string origin
            , int level
            , System.Exception exception
            , System.Data.Common.DbCommand cmd
            , string logMessage)
        {
            System.Collections.Generic.Stack<System.Exception> stack = GetExceptionStack(exception);


            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int sequence = 0;
            while (stack.Count > 0)
            {
                sequence++;

                System.Exception ex = stack.Pop();
                string exceptionText = StringifyException(ex);
                sb.AppendLine(exceptionText);
            }

            string text = sb.ToString();
            sb.Length = 0;
            sb = null;

            // SendMail(text);
        }


    }



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
