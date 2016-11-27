
namespace Loggy
{


    public static class DotNetCoreTypeExtensions
    {
        public static bool IsSubclassOf(this System.Type t, System.Type c)
        {
            return System.Reflection.IntrospectionExtensions.GetTypeInfo(t).IsSubclassOf(c);
        }


        public static System.Reflection.FieldInfo GetField(this System.Type t,
            string name,
            System.Reflection.BindingFlags bindingAttr)
        {
            return System.Reflection.IntrospectionExtensions.GetTypeInfo(t).GetField(name, bindingAttr);
        }


    }


}
