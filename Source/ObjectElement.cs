using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using LibraProgramming.Serialization.Hessian.Core.Extensions;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectElement : ISerializationElement
    {
        private string classname;

        /// <inheritdoc cref="ISerializationElement.ObjectType" />
        public Type ObjectType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName
        {
            get
            {
                if (String.IsNullOrEmpty(classname))
                {
                    var attribute = ObjectType.GetCustomAttribute<DataContractAttribute>();

                    classname = null != attribute && false == String.IsNullOrEmpty(attribute.Name)
                        ? attribute.Name
                        : ObjectType.FullName;
                }

                return classname;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<PropertyElement> ObjectProperties
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectProperties"></param>
        public ObjectElement(Type objectType, IList<PropertyElement> objectProperties)
        {
            ObjectType = objectType;
            ObjectProperties = objectProperties;
        }

        /// <inheritdoc cref="ISerializationElement.Serialize" />
        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            var index = context.Instances.IndexOf(graph);

            if (index > -1)
            {
                writer.WriteInstanceReference(index);
                return;
            }

            context.Instances.Add(graph);

            index = context.Classes.IndexOf(ObjectType);

            if (0 > index)
            {
                writer.BeginClassDefinition();
                writer.WriteString(ClassName);
                writer.WriteInt32(ObjectProperties.Count);

                foreach (var property in ObjectProperties)
                {
                    writer.WriteString(property.PropertyName);
                }
                
                writer.EndClassDefinition();

                index = context.Classes.Count;

                context.Classes.Add(ObjectType);
            }

            writer.WriteObjectReference(index);

            foreach (var item in ObjectProperties)
            {
                var value = item.Property.GetPropertyValue(graph);
                item.Serialize(writer, value, context);
            }
        }

        /// <inheritdoc cref="ISerializationElement.Deserialize" />
        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            reader.BeginObject();

            if (reader.IsClassDefinition)
            {
                var className = reader.ReadString();
                var propertiesCount = reader.ReadInt32();

                if (!String.Equals(ClassName, className))
                {
                    throw new HessianSerializerException();
                }

                if (ObjectProperties.Count != propertiesCount)
                {
                    throw new HessianSerializerException();
                }

                for (var index = 0; index < propertiesCount; index++)
                {
                    var propertyName = reader.ReadString();
                    var exists = ObjectProperties.Any(property => String.Equals(property.PropertyName, propertyName));

                    if (!exists)
                    {
                        throw new HessianSerializerException();
                    }
                }

                context.Classes.Add(ObjectType);

                reader.EndClassDefinition();
            }
            else if (reader.IsInstanceReference)
            {
                var index = reader.ReadInstanceReference();
                return context.Instances[index];
            }

            var number = reader.ReadObjectReference();
            var instance = Activator.CreateInstance(ObjectType);

            context.Instances.Add(instance);

            foreach (var item in ObjectProperties)
            {
                var value = item.Deserialize(reader, context);
                item.Property.SetPropertyValue(instance, value);
            }

            reader.EndObject();

            return instance;
        }
    }
}