namespace CZLib.Config.Mvc4
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class ConfigExtension
    {
        public static string GetDisplayName(this PropertyInfo property)
        {
            var name = property.Name;
            var attr = property.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
            if (attr != null)
            {
                name = ((DisplayNameAttribute)attr).DisplayName;
            }
            return name;
        }

        public static bool IsSimpleType(this Type type)
        {
            if (type == typeof(DateTime)) return true;
            if (type == typeof(string)) return true;
            return type.IsPrimitive;
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterfaces().Any(x => x == typeof(IEnumerable));
        }

        public static DataType GetDataType(this PropertyInfo property)
        {
            var att=property.GetCustomAttributes(typeof(DataTypeAttribute), false).FirstOrDefault();
            if(att!=null)
                return ((DataTypeAttribute)att).DataType;
            return DataType.Text;
        }
    }
}