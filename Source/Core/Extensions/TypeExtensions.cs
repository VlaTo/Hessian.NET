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
#if (NET45 || NETSTANDARD20)
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
#if (NET45 || NETSTANDARD20)
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
#else
            return type.GetTypeInfo().DeclaredProperties;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetInterfaces2(this Type type)
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
        public static bool IsGenericType2(this Type type)
        {
#if NETSTANDARD13
            var info = type.GetTypeInfo();
            return info.IsGenericType;
#else
            return type.IsGenericType;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
#if (NET45 || NETSTANDARD20)
            return type.IsValueType || type.IsEnum || type.IsPrimitive
#else
            var info = type.GetTypeInfo();
            return info.IsValueType || info.IsEnum || info.IsPrimitive
#endif
                   || typeof(string) == type;
        }

        /// <summary>
        /// Determines whether an instance of a specified type can be assigned to an instance of the current type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="candidate">The type to compare with the <paramref name="type"/>.</param>
        /// <returns></returns>
        public static bool IsAssignableFrom2(this Type type, Type candidate)
        {
#if NETSTANDARD13
            return type.GetTypeInfo().IsAssignableFrom(candidate.GetTypeInfo());
#else
            return type.IsAssignableFrom(candidate);
#endif
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
            if (type.IsGenericType2())
            {
                var definition = type.GetGenericTypeDefinition();

                if (typeof(IList<>) == definition)
                {
                    return true;
                }
            }

            return type.GetInterfaces2().Any(IsTypedList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypedCollection(this Type type)
        {
            if (type.IsGenericType2())
            {
                var definition = type.GetGenericTypeDefinition();

                if (typeof(ICollection<>) == definition)
                {
                    return true;
                }
            }

            return type.GetInterfaces2().Any(IsTypedCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypedEnumerable(this Type type)
        {
            if (type.IsGenericType2())
            {
                var definition = type.GetGenericTypeDefinition();

                if (typeof(IEnumerable<>) == definition)
                {
                    return true;
                }
            }

            return type.GetInterfaces2().Any(IsTypedEnumerable);
        }
    }
}