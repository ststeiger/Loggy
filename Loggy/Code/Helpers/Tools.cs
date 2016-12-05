
namespace Loggy
{


    public class ResourceHelper 
    {


        public static string GetResource(System.Type t, string endsWith)
        {
            System.Reflection.Assembly ass = t.Assembly;

            string res = null;
            foreach (string str in ass.GetManifestResourceNames())
            {
                if (str.EndsWith(endsWith, System.StringComparison.OrdinalIgnoreCase))
                {
                    res = str;
                    break;
                } // End if(str.EndsWith(endsWith, System.StringComparison.OrdinalIgnoreCase)) 

            } // Next str 


            using (System.IO.Stream strm = ass.GetManifestResourceStream(res))
            {

                using (System.IO.StreamReader sr = new System.IO.StreamReader(strm))
                {
                    res = sr.ReadToEnd();
                } // End Using sr 

            } // End Using strm 

            return res;
        } // End Function GetResource 


    } // End Class ResourceHelper 


} // End Namespace Loggy 
