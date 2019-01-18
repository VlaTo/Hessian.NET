using System;
using System.Reflection;
using System.Runtime.Serialization;
using LibraProgramming.Serialization.Hessian.Core.Extensions;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyElement : ISerializationElement
    {
        private string propertyname;
        private int? propertyOrder;

        /// <inheritdoc cref="ISerializationElement.ObjectType" />
        public Type ObjectType => Property.PropertyType;

        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo Property
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public ISerializationElement Element
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PropertyOrder
        {
            get
            {
                if (!propertyOrder.HasValue)
                {
                    var attribute = Property.GetCustomAttribute<DataMemberAttribute>();
                    propertyOrder = attribute?.Order ?? 0;
                }

                return propertyOrder.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName
        {
            get
            {
                if (String.IsNullOrEmpty(propertyname))
                {
                    var attribute = Property.GetCustomAttribute<DataMemberAttribute>();

                    propertyname = null != attribute && false == String.IsNullOrEmpty(attribute.Name)
                        ? attribute.Name
                        : Property.Name;
                }

                return propertyname;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="element"></param>
        public PropertyElement(PropertyInfo property, ISerializationElement element)
        {
            Property = property;
            Element = element;
        }

        /// <inheritdoc cref="ISerializationElement.Serialize" />
        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            Element.Serialize(writer, graph, context);
        }

        /// <inheritdoc cref="ISerializationElement.Deserialize" />
        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            return Element.Deserialize(reader, context);
        }
    }
}