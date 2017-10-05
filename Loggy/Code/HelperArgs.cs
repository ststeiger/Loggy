
namespace Loggy
{


    // http://acodez.in/choose-php-over-asp-net/
    public class HelperArgs
    {


        private static object FlexibleChangeType(object objVal, System.Type targetType)
        {
            System.Reflection.TypeInfo ti = System.Reflection.IntrospectionExtensions.GetTypeInfo(targetType);
            bool typeIsNullable = (ti.IsGenericType && object.ReferenceEquals(targetType.GetGenericTypeDefinition(), typeof(System.Nullable<>)));
            bool typeCanBeAssignedNull = !ti.IsValueType || typeIsNullable;

            if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value))
            {
                if (typeCanBeAssignedNull)
                    return null;
                else
                    throw new System.ArgumentNullException("objVal ([DataSource] => SetProperty => FlexibleChangeType => you're trying to assign NULL to a type that NULL cannot be assigned to...)");
            } // End if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value))

            // Get base-type
            System.Type tThisType = objVal.GetType();

            if (typeIsNullable)
            {
                targetType = System.Nullable.GetUnderlyingType(targetType);
            } // End if (typeIsNullable) 


            if (object.ReferenceEquals(tThisType, targetType))
                return objVal;

            // Convert Guid => string
            if (object.ReferenceEquals(targetType, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return objVal.ToString();
            } // End if (object.ReferenceEquals(targetType, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid)))

            // Convert string => System.Net.IPAddress
            if (object.ReferenceEquals(targetType, typeof(System.Net.IPAddress)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                return System.Net.IPAddress.Parse(objVal.ToString());
            } // End if (object.ReferenceEquals(targetType, typeof(System.Net.IPAddress)) && object.ReferenceEquals(tThisType, typeof(string)))

            // Convert string => TimeSpan
            if (object.ReferenceEquals(targetType, typeof(System.TimeSpan)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                // https://stackoverflow.com/questions/11719055/why-does-timespan-parseexact-not-work
                // This is grotesque... ParseExact ignores the 12/24 hour convention...
                // return System.TimeSpan.ParseExact(objVal.ToString(), "HH':'mm':'ss", System.Globalization.CultureInfo.InvariantCulture); // Exception 
                // return System.TimeSpan.ParseExact(objVal.ToString(), "hh\\:mm\\:ss", System.Globalization.CultureInfo.InvariantCulture); // This works, bc of lowercase ?
                // return System.TimeSpan.ParseExact(objVal.ToString(), "hh':'mm':'ss", System.Globalization.CultureInfo.InvariantCulture); // Yep, lowercase - no 24 notation...
                return System.TimeSpan.Parse(objVal.ToString());
            } // End if (object.ReferenceEquals(targetType, typeof(System.TimeSpan)) && object.ReferenceEquals(tThisType, typeof(string))) 

            // Convert string => DateTime
            if (object.ReferenceEquals(targetType, typeof(System.DateTime)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                return System.DateTime.Parse(objVal.ToString(), System.Globalization.CultureInfo.InvariantCulture);
            } // End if (object.ReferenceEquals(targetType, typeof(System.DateTime)) && object.ReferenceEquals(tThisType, typeof(string)))

            // Convert string => Guid 
            if (object.ReferenceEquals(targetType, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                return new System.Guid(objVal.ToString());
            } // End if (object.ReferenceEquals(targetType, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string))) 

            return System.Convert.ChangeType(objVal, targetType);
        } // End Function FlexibleChangeType 


        private static string GetParam(string key, System.Web.HttpContext context)
        {
            string value = context.Request.QueryString[key];

            if (value == null)
                value = context.Request.Form[key];

            if (value == null)
                value = context.Request.Headers[key];

            return value;
        } // End Function GetParam 


        public static T GetArgs<T>(System.Web.HttpContext context)
        {
            System.Type t = typeof(T);
            T args = System.Activator.CreateInstance<T>();

            System.Reflection.FieldInfo[] fis = t.GetFields();
            System.Reflection.PropertyInfo[] pis = t.GetProperties();

            bool setNoValues = true;

            for (int i = 0; i < fis.Length; ++i)
            {
                string key = fis[i].Name;
                string value = GetParam(key, context);

                if (value != null)
                {
                    fis[i].SetValue(args, FlexibleChangeType(value, fis[i].FieldType));
                    setNoValues = false;
                } // End if (value != null)

            } // Next i 

            for (int i = 0; i < pis.Length; ++i)
            {
                string key = pis[i].Name;
                string value = GetParam(key, context);

                if (value != null)
                {
                    pis[i].SetValue(args, FlexibleChangeType(value, pis[i].PropertyType), null);
                    setNoValues = false;
                } // End if (value != null)

            } // Next i 

            if (setNoValues)
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(context.Request.InputStream))
                {

                    using (Newtonsoft.Json.JsonTextReader jsonTextReader = new Newtonsoft.Json.JsonTextReader(sr))
                    {
                        args = (T)serializer.Deserialize(jsonTextReader, typeof(T));
                    } // End Using jsonTextReader 

                } // End Using sr 

            } // End if (setNoValues)

            return args;
        } // End Function GetArgs


    } // End Class HelperArgs 


} // End Namespace Loggy 
