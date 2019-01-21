using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if (NET45 || NETSTANDARD13 || NETSTANDARD20)
using System.Runtime.Serialization;
#endif

namespace LibraProgramming.Serialization.Hessian.Core.Extensions
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Retrieves a custom attribute that is applied to a specified element.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="type">The type to search in.</param>
        /// <param name="inherit"><c>true</c> to inspect the ancestors of element; otherwise, <c>false</c>.</param>
        /// <returns>
        /// A custom attribute that matches <typeparamref name="TAttribute" />, or <c>null</c> if no such attribute is found.
        /// </returns>
        public static TAttribute GetCustomAttribute<TAttribute>(this Type type, bool inherit = false)
            where TAttribute : Attribute
        {
#if NET40
            var attributes = type.GetCustomAttributes(typeof(TAttribute), inherit);
            return 0 < attributes.Length ? (TAttribute) attributes[0] : null;
#elif (NET45 || NETSTANDARD20)
            return CustomAttributeExtensions.GetCustomAttribute<TAttribute>(type, inherit);
#else
            return type.GetTypeInfo().GetCustomAttribute<TAttribute>(inherit);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
        {
#if (NET40 || NET45 || NETSTANDARD20)
            return type.GetProperties(BindingFlags.DeclaredOnly);
#else
            return type.GetTypeInfo().DeclaredProperties;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetInterfaces(this Type type)
        {
#if NETSTANDARD13
            var info = type.GetTypeInfo();
            return info.ImplementedInterfaces;
#else
            return type.GetInterfaces();
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetGenericTypeArgument(this Type type)
        {
            Type[] types;

#if NETSTANDARD13
            types = type.GenericTypeArguments;
#else
            types = type.GetGenericArguments();
#endif
            return types[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericType(this Type type)
        {
#if NETSTANDARD13
            var info = type.GetTypeInfo();
            return info.IsGenericType && info.ContainsGenericParameters;
#else
            return type.IsGenericType && type.ContainsGenericParameters;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
#if (NET40 || NET45 || NETSTANDARD20)
            return type.IsValueType || type.IsEnum || type.IsPrimitive
#else
            var info = type.GetTypeInfo();
            return info.IsValueType || info.IsEnum || info.IsPrimitive
#endif
                   || typeof(string) == type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypedArray(this Type type)
        {
            return type.IsArray && type.HasElementType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypedCollection(this Type type)
        {
            if (type.IsGenericType())
            {
                var definition = type.GetGenericTypeDefinition();
                return typeof(ICollection) == definition;
            }

            return type.GetInterfaces().Any(IsTypedCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(this Type type)
        {
            if (typeof(IEnumerable) == type)
            {
                return true;
            }

            if (type.IsGenericType())
            {
                var temp = type.GetGenericTypeDefinition();
                return typeof(IEnumerable) == temp;
            }

            var interfaces = type.GetInterfaces();

            foreach (var @interface in interfaces)
            {
                if (@interface.IsEnumerable())
                {
                    return true;
                }
            }

            return false;
        }
    }
}