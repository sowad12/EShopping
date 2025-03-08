using System.ComponentModel;
using System.Dynamic;
using System.Reflection;


namespace EShopping.Utilities
{
    public static class ObjectUtilities
    {
        public static object GetValue(this object source, string property, object defaultValue = null)
        {
            string[] array = property.Split('.');
            object obj = source;
            PropertyInfo propertyInfo = null;
            int num = array.Length;
            for (int i = 0; i < num; i++)
            {
                propertyInfo = obj.GetProperty(array[i]);
                obj = propertyInfo?.GetValue(obj);
                if (obj == null || propertyInfo == null)
                {
                    break;
                }
            }

            if (obj == null || propertyInfo == null)
            {
                return defaultValue;
            }

            return obj;
        }

        public static T GetValue<T>(this object source, string property, T defaultValue = default(T))
        {
            object value = source.GetValue(property);
            if (value != null)
            {
                return (T)value;
            }

            return defaultValue;
        }

        public static bool SetValue(this object source, object value, string property)
        {
            string[] array = property.Split('.');
            object obj = source;
            PropertyInfo propertyInfo = null;
            int num = array.Length;
            for (int i = 0; i < num; i++)
            {
                propertyInfo = obj.GetProperty(array[i]);
                if (propertyInfo == null)
                {
                    return false;
                }

                if (i != array.Length - 1)
                {
                    obj = propertyInfo.GetValue(obj);
                }
            }

            if (obj != null)
            {
                propertyInfo.SetValue(obj, value);
                return true;
            }

            return obj == null;
        }

        public static PropertyInfo GetProperty(this object source, string name)
        {
            PropertyInfo[] properties = source.GetType().GetProperties();
            int num = properties.Length;
            for (int i = 0; i < num; i++)
            {
                if (properties[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return properties[i];
                }
            }

            return null;
        }

        public static object InvokeMethod(this object source, string dottedMethodName, params object[] parameters)
        {
            var (obj, methodInfo) = source.GetMethodAndSource(dottedMethodName);
            if (obj != null && methodInfo != null)
            {
                return methodInfo.Invoke(obj, parameters);
            }

            return null;
        }

        public static T InvokeMethod<T>(this object source, string dottedMethodName, params object[] parameters)
        {
            return (T)source.InvokeMethod(dottedMethodName, parameters);
        }

        public static object InvokeGenericMethod<T>(this object source, string methodName, params object[] parameters)
        {
            return source.InvokeGenericMethod(methodName, typeof(T), parameters);
        }

        public static object InvokeGenericMethod(this object source, string methodName, Type typeArgument, params object[] parameters)
        {
            MethodInfo method = source.GetType().GetMethod(methodName);
            MethodInfo methodInfo = method.MakeGenericMethod(typeArgument);
            return methodInfo.Invoke(source, parameters);
        }

        public static object InvokeGenericMethod(this object source, string methodName, Type[] typeArguments, params object[] parameters)
        {
            MethodInfo method = source.GetType().GetMethod(methodName);
            MethodInfo methodInfo = method.MakeGenericMethod(typeArguments);
            return methodInfo.Invoke(source, parameters);
        }

        public static MethodInfo GetMethod(this object source, string name)
        {
            MethodInfo[] methods = source.GetType().GetMethods();
            int num = methods.Length;
            for (int i = 0; i < num; i++)
            {
                if (methods[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return methods[i];
                }
            }

            return null;
        }

        public static (object, MethodInfo) GetMethodAndSource(this object source, string dottedMethodName)
        {
            string[] array = dottedMethodName.Split('.');
            int num = array.Length;
            if (num == 1)
            {
                return (source, source.GetMethod(dottedMethodName));
            }

            object obj = source;
            PropertyInfo propertyInfo = null;
            for (int i = 0; i < num; i++)
            {
                propertyInfo = obj.GetProperty(array[i]);
                if (propertyInfo == null)
                {
                    break;
                }

                if (i != array.Length - 1)
                {
                    obj = propertyInfo.GetValue(obj);
                }
            }

            if (obj != null)
            {
                return (obj, obj.GetMethod(array[num - 1]));
            }

            return (null, null);
        }

        public static void CopyProperties(this object source, object destination)
        {
            if (source == null || destination == null)
            {
                throw new Exception("Source or/and Destination Objects are null");
            }

            Type typeDest = destination.GetType();
            Type type = source.GetType();
            var enumerable = from srcProp in type.GetProperties()
                             let targetProperty = typeDest.GetProperty(srcProp.Name)
                             where srcProp.CanRead && targetProperty != null && targetProperty.GetSetMethod(nonPublic: true) != null && !targetProperty.GetSetMethod(nonPublic: true)!.IsPrivate && (targetProperty.GetSetMethod()!.Attributes & MethodAttributes.Static) == 0 && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                             select new
                             {
                                 sourceProperty = srcProp,
                                 targetProperty = targetProperty
                             };
            foreach (var item in enumerable)
            {
                item.targetProperty.SetValue(destination, item.sourceProperty.GetValue(source, null), null);
            }
        }

        public static void DynamicUsing(this object resource, Action action)
        {
            try
            {
                action();
            }
            finally
            {
                (resource as IDisposable)?.Dispose();
            }
        }

        public static bool IsAllPropertyNull(this object source)
        {
            IEnumerable<string> source2 = from x in source.GetType().GetProperties()
                                          where x.PropertyType == typeof(string)
                                          select (string)x.GetValue(source);
            return source2.All((string x) => !x.HasValue());
        }

        public static string ObjectToString(this object source)
        {
            IEnumerable<string> values = from x in source.GetType().GetProperties()
                                         select "[" + x.Name + "] = [" + (string)x.GetValue(source) + "]";
            return string.Join(", ", values);
        }

        public static ExpandoObject ObjectToExpando(this object value)
        {
            IDictionary<string, object> dictionary = new ExpandoObject();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            {
                dictionary.Add(property.Name, property.GetValue(value));
            }

            return dictionary as ExpandoObject;
        }

        public static dynamic ToDynamicObject(this IEnumerable<KeyValuePair<string, object>> pairs)
        {
            IDictionary<string, object> dictionary = new ExpandoObject();
            foreach (KeyValuePair<string, object> pair in pairs)
            {
                dictionary.Add(pair.Key, pair.Value);
            }

            return dictionary;
        }

        //public static string ObjectToJsonString(this object source)
        //{
        //    if (source == null)
        //    {
        //        return "";
        //    }

        //    return JsonConvert.SerializeObject(source);
        //}
    }
}
