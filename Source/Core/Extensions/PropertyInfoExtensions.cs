using System;
using System.Reflection;
#if (NET45 || NETSTANDARD13 || NETSTANDARD20)
using System.Runtime.Serialization;
#endif

namespace LibraProgramming.Serialization.Hessian.Core.Extensions
{
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Retrieves a custom attribute that is applied to a specified element.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="property">The type to search in.</param>
        /// <param name="inherit"><c>true</c> to inspect the ancestors of element; otherwise, <c>false</c>.</param>
        /// <returns>
        /// A custom attribute that matches <typeparamref name="TAttribute" />, or <c>null</c> if no such attribute is found.
        /// </returns>
        public static TAttribute GetCustomAttribute<TAttribute>(this PropertyInfo property, bool inherit = false)
            where TAttribute : Attribute
        {
#if NET40
            var attributes = property.GetCustomAttributes(typeof(TAttribute), inherit);
            return 0 < attributes.Length ? (TAttribute) attributes[0] : null;
#else
            return CustomAttributeExtensions.GetCustomAttribute<TAttribute>(property, inherit);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this PropertyInfo property, object target)
        {
#if NET40
            return property.GetValue(target, null);
#else
            return property.GetValue(target);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="target"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetPropertyValue(this PropertyInfo property, object target, object value)
        {
#if NET40
            property.SetValue(target, value, null);
#else
            property.SetValue(target, value);
#endif
        }
    }
}