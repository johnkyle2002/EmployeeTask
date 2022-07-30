using System.ComponentModel;
using System.Reflection;

namespace EmployeeTask.Shared.Extension
{
    public static class EnumExtensions
    {
        /// <summary>
        /// This will get the object ComponentModel descriptive attribute value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Description<T>(this T source) 
        {
            if (source == null)
                return string.Empty;

            try
            {
                FieldInfo fi = source.GetType().GetField(source.ToString());

                if (fi == null)
                    return string.Empty;

                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return source.ToString();
            }
            catch (System.Exception)
            {
                return source.ToString();
            }
        }
    }
}
