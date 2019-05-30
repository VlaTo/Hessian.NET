﻿using System;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal partial class HessianObjectSerializerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private class BooleanSerializer : IObjectSerializer
        {
            public void Serialize(HessianOutputWriter writer, object graph)
            {
                writer.WriteBoolean((bool) graph);
            }

            public object Deserialize(HessianInputReader reader)
            {
                return reader.ReadBoolean();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class Int32Serializer : IObjectSerializer
        {
            public void Serialize(HessianOutputWriter writer, object graph)
            {
                writer.WriteInt32((int) graph);
            }

            public object Deserialize(HessianInputReader reader)
            {
                return reader.ReadInt32();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class Int64Serializer : IObjectSerializer
        {
            public void Serialize(HessianOutputWriter writer, object graph)
            {
                writer.WriteInt64((long) graph);
            }

            public object Deserialize(HessianInputReader reader)
            {
                return reader.ReadInt64();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class StringSerializer : IObjectSerializer
        {
            public void Serialize(HessianOutputWriter writer, object graph)
            {
                writer.WriteString((string) graph);
            }

            public object Deserialize(HessianInputReader reader)
            {
                return reader.ReadString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class DateTimeSerializer : IObjectSerializer
        {
            public void Serialize(HessianOutputWriter writer, object graph)
            {
                writer.WriteDateTime((DateTime) graph);
            }

            public object Deserialize(HessianInputReader reader)
            {
                return reader.ReadDateTime();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class DoubleSerializer : IObjectSerializer
        {
            public void Serialize(HessianOutputWriter writer, object graph)
            {
                writer.WriteDouble((double) graph);
            }

            public object Deserialize(HessianInputReader reader)
            {
                return reader.ReadDouble();
            }
        }
    }
}