using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LibraProgramming.Serialization.Hessian.Core;
using LibraProgramming.Serialization.Hessian.Core.Extensions;

namespace LibraProgramming.Serialization.Hessian
{
    internal sealed class HessianSerializationScheme
    {
        public Type ObjectType
        {
            get;
        }

        public ISerializationElement Element
        {
            get;
        }

        private HessianSerializationScheme(Type objectType, ISerializationElement element)
        {
            ObjectType = objectType;
            Element = element;
        }

        public static HessianSerializationScheme CreateFromType(Type type, IObjectSerializerFactory factory)
        {
            var catalog = new Dictionary<Type, ISerializationElement>();
            var element = CreateSerializationElement(type, catalog, factory);

            return new HessianSerializationScheme(type, element);
        }

        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            Element.Serialize(writer, graph, context);
        }

        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            return Element.Deserialize(reader, context);
        }

        private static ISerializationElement CreateSerializationElement(Type type, IDictionary<Type, ISerializationElement> catalog, IObjectSerializerFactory factory)
        {
            if (type.IsSimpleType())
            {
                var serializer = factory.GetSerializer(type);
                return new ValueElement(type, serializer);
            }

            if (type.IsTypedArray())
            {
                return BuildArraySerializationElement(type, catalog, factory);
            }

            if (type.IsTypedCollection())
            {
                return BuildCollectionSerializationElement(type, catalog, factory);
            }

            return BuildClassSerializationElement(type, catalog, factory);
        }

        private static ISerializationElement BuildClassSerializationElement(Type type, IDictionary<Type, ISerializationElement> catalog, IObjectSerializerFactory factory)
        {
            if (catalog.TryGetValue(type, out var existing))
            {
                return existing;
            }

            var contract = type.GetCustomAttribute<DataContractAttribute>();

            if (null == contract)
            {
                throw new HessianSerializerException();
            }

            var properties = new List<PropertyElement>();
            var element = new ObjectElement(type, properties);

            catalog.Add(type, element);

            foreach (var property in type.GetDeclaredProperties())
            {
                var attribute = property.GetCustomAttribute<DataMemberAttribute>();

                if (null == attribute)
                {
                    continue;
                }

                if (!property.CanRead || !property.CanWrite)
                {
                    continue;
                }

                properties.Add(new PropertyElement(
                    property,
                    CreateSerializationElement(property.PropertyType, catalog, factory)
                ));
            }

            properties.Sort(new ObjectPropertyComparer());

            return element;
        }

        private static ISerializationElement BuildArraySerializationElement(Type type, IDictionary<Type, ISerializationElement> catalog, IObjectSerializerFactory factory)
        {
            if (1 != type.GetArrayRank())
            {
                throw new HessianSerializerException();
            }

            var elementType = type.GetElementType();

            if (typeof(object) == elementType)
            {
                // untyped
            }

            return new FixedLengthTypedArrayElement(
                CreateSerializationElement(elementType, catalog, factory)
            );
        }

        private static ISerializationElement BuildCollectionSerializationElement(Type type, IDictionary<Type, ISerializationElement> catalog, IObjectSerializerFactory factory)
        {
            var elementType = type.GetGenericTypeArgument();

            return new TypedCollectionElement(
                CreateSerializationElement(elementType, catalog, factory)
            );
        }

        /*private static ISerializationElement BuildEnumerableSerializationElement(Type type, IDictionary<Type, ISerializationElement> catalog, IObjectSerializerFactory factory)
        {
            var genericType = type.GetGenericType();

            return new VariableLengthTypedArrayElement(
                genericType
            );
        }*/
    }
}