
namespace Loggy
{


    public class oldDAL
    {


        public enum Mandant
        {

        }


        protected static T InlineTypeAssignHelper<T>(object UTO)
        {
            if (UTO == null)
            {
                T NullSubstitute = default(T);
                return NullSubstitute;
            }
            return (T)UTO;
        } // End Template InlineTypeAssignHelper


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
