using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)
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
        public static TAttribute GetAttribute<TAttribute>(this Type type, bool inherit = false)
            where TAttribute : Attribute
        {
#if NET40
            var attributes = type.GetCustomAttributes(typeof(TAttribute), inherit);
            return 0 < attributes.Length ? (TAttribute) attributes[0] : null;
#elif (NET45 || NETSTANDARD2_0)
            return type.GetCustomAttribute<TAttribute>(inherit);
#elif NETSTANDARD1_3
            return type.GetTypeInfo().GetCustomAttribute<TAttribute>(inherit);
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
        {
#if (NET40 || NET45 || NETSTANDARD2_0)
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
#elif NETSTANDARD1_3
            return type.GetTypeInfo().DeclaredProperties;
#else
            throw new NotImplementedException();
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

#if NETSTANDARD1_3
            types = type.GenericTypeArguments;
#elif (NET40 || NET45 || NETSTANDARD2_0)
            types = type.GetGenericArguments();
#else
            throw new NotImplementedException();
#endif
            return types[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
#if (NET40 || NET45 || NETSTANDARD2_0)
            return type.IsValueType || type.IsEnum || type.IsPrimitive
#elif NETSTANDARD1_3
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
        public static bool IsTypedList(this Type type)
        {
            if (type.IsGenericTypeInternal())
            {
                var definition = type.GetGenericTypeDefinition();

                if (typeof(IList<>) == definition)
                {
                    return true;
                }
            }

            return type.GetInterfacesInternal().Any(IsTypedList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypedCollection(this Type type)
        {
            if (type.IsGenericTypeInternal())
            {
                var definition = type.GetGenericTypeDefinition();

                if (typeof(ICollection<>) == definition)
                {
                    return true;
                }
            }

            return type.GetInterfacesInternal().Any(IsTypedCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypedEnumerable(this Type type)
        {
            if (type.IsGenericTypeInternal())
            {
                var definition = type.GetGenericTypeDefinition();

                if (typeof(IEnumerable<>) == definition)
                {
                    return true;
                }
            }

            return type.GetInterfacesInternal().Any(IsTypedEnumerable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsGenericTypeInternal(this Type type)
        {
#if NETSTANDARD1_3
            var info = type.GetTypeInfo();
            return info.IsGenericType;
#elif (NET40 || NET45 || NETSTANDARD2_0)
            return type.IsGenericType;
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<Type> GetInterfacesInternal(this Type type)
        {
#if NETSTANDARD1_3
            var info = type.GetTypeInfo();
            return info.ImplementedInterfaces;
#elif (NET40 || NET45 || NETSTANDARD2_0)
            return type.GetInterfaces();
#else
            throw new NotImplementedException();
#endif
        }
    }
}