using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LibraProgramming.Serialization.Hessian.Core;
using LibraProgramming.Serialization.Hessian.Core.Extensions;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// Class for writing Hessian-encoded data into stream. 
    /// </summary>
    public class HessianOutputWriter : DisposableStreamHandler
    {
        private readonly Stack<DisposeAction<object>> arrays;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public HessianOutputWriter(Stream stream)
            : base(stream)
        {
            arrays = new Stack<DisposeAction<object>>();
        }

        /// <summary>
        /// Writes NULL token into stream
        /// </summary>
        public virtual void WriteNull()
        {
            Stream.WriteByte(Marker.Null);
        }

        /// <summary>
        /// Writes <see cref="System.Boolean" /> value into output stream.
        /// </summary>
        /// <param name="value">The value.</param>
        public virtual void WriteBoolean(bool value)
        {
            Stream.WriteByte(value ? Marker.True : Marker.False);
        }

        /// <summary>
        /// Writes array of <see cref="System.Byte" /> into output stream.
        /// </summary>
        /// <param name="buffer">The value.</param>
        public void WriteBytes(byte[] buffer)
        {
            if (null == buffer)
            {
                WriteNull();
                return;
            }

            WriteBytes(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public virtual void WriteBytes(byte[] buffer, int offset, int count)
        {
            if (offset < 0)
            {
                throw new ArgumentException("", nameof(offset));
            }

            if (null == buffer)
            {
                WriteNull();
                return;
            }

            if (count < 0x10)
            {
                Stream.WriteByte((byte)(0x20 + (count & 0x0F)));
                Stream.Write(buffer, offset, count);
                return;
            }

            const int chunkSize = 0x8000;

            while (count > chunkSize)
            {
                Stream.WriteByte(Marker.BinaryNonFinalChunk);
                Stream.WriteByte(chunkSize >> 8);
                Stream.WriteByte(chunkSize & 0xFF);
                Stream.Write(buffer, offset, chunkSize);

                count -= chunkSize;
                offset += chunkSize;
            }

            Stream.WriteByte(Marker.BinaryFinalChunk);
            Stream.WriteByte((byte)(count >> 8));
            Stream.WriteByte((byte)(count & 0xFF));
            Stream.Write(buffer, offset, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void WriteDateTime(DateTime value)
        {
            if (value.Second == 0)
            {
                var s = value.GetTotalMinutes();

                Stream.WriteByte(Marker.DateTimeCompact);
                Stream.WriteByte((byte)(s >> 24));
                Stream.WriteByte((byte)(s >> 16));
                Stream.WriteByte((byte)(s >> 8));
                Stream.WriteByte((byte)s);

                return;
            }

            var dt = value.GetTotalMilliseconds();

            Stream.WriteByte(Marker.DateTimeLong);
            Stream.WriteByte((byte)(dt >> 56));
            Stream.WriteByte((byte)(dt >> 48));
            Stream.WriteByte((byte)(dt >> 40));
            Stream.WriteByte((byte)(dt >> 32));
            Stream.WriteByte((byte)(dt >> 24));
            Stream.WriteByte((byte)(dt >> 16));
            Stream.WriteByte((byte)(dt >> 8));
            Stream.WriteByte((byte)dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void WriteDouble(double value)
        {
            if (value.Equals(0.0d))
            {
                Stream.WriteByte(Marker.DoubleZero);
                return;
            }

            if (value.Equals(1.0d))
            {
                Stream.WriteByte(Marker.DoubleOne);
                return;
            }

            var fraction = Math.Abs(value - Math.Truncate(value));

            if (Double.Epsilon >= fraction)
            {
                if (Byte.MinValue <= value && value <= Byte.MaxValue)
                {
                    Stream.WriteByte(Marker.DoubleOctet);
                    Stream.WriteByte(Convert.ToByte(value));

                    return;
                }

                if (Int16.MinValue <= value && value <= Int16.MaxValue)
                {
                    var val = Convert.ToInt16(value);

                    Stream.WriteByte(Marker.DoubleShort);
                    Stream.WriteByte((byte)(val >> 8));
                    Stream.WriteByte((byte)val);

                    return;
                }
            }

            if (Single.MinValue <= value && value <= Single.MaxValue)
            {
                var bytes = BitConverter.GetBytes((float) value);

                Stream.WriteByte(Marker.DoubleFloat);

                for (var index = bytes.Length - 1; index >= 0; index--)
                {
                    Stream.WriteByte(bytes[index]);
                }

                return;
            }

            var temp = BitConverter.DoubleToInt64Bits(value);

            Stream.WriteByte(Marker.Double);

            for (var index = 56; index >= 0; index -= 8)
            {
                Stream.WriteByte((byte) (temp >> index));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void WriteInt32(int value)
        {
            if (-16 <= value && value < 48)
            {
                Stream.WriteByte((byte)(0x90 + value));
            }
            else if (-2048 <= value && value < 2048)
            {
                Stream.WriteByte((byte)(0xC8 + (byte)(value >> 8)));
                Stream.WriteByte((byte)value);
            }
            else if (-262144 <= value && value < 262144)
            {
                Stream.WriteByte((byte)(0xD4 + (byte)(value >> 16)));
                Stream.WriteByte((byte)(value >> 8));
                Stream.WriteByte((byte)value);
            }
            else
            {
                Stream.WriteByte(Marker.UnpackedInteger);
                Stream.WriteByte((byte)(value >> 24));
                Stream.WriteByte((byte)(value >> 16));
                Stream.WriteByte((byte)(value >> 8));
                Stream.WriteByte((byte)value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void WriteInt64(long value)
        {
            if (-8 <= value && value < 16)
            {
                Stream.WriteByte((byte)(0xE0 + value));
            }
            else if (-2048 <= value && value < 2048)
            {
                Stream.WriteByte((byte)(0xF8 + (byte)(value >> 8)));
                Stream.WriteByte((byte)value);
            }
            else if (-262144 <= value && value < 262144)
            {
                Stream.WriteByte((byte)(0x3C + (byte)(value >> 16)));
                Stream.WriteByte((byte)(value >> 8));
                Stream.WriteByte((byte)value);
            }
            else if (Int32.MinValue <= value && value <= Int32.MaxValue)
            {
                Stream.WriteByte(Marker.PackedLong);
                Stream.WriteByte((byte)(value >> 24));
                Stream.WriteByte((byte)(value >> 16));
                Stream.WriteByte((byte)(value >> 8));
                Stream.WriteByte((byte)value);
            }
            else
            {
                Stream.WriteByte(Marker.UnpackedLong);
                Stream.WriteByte((byte)(value >> 56));
                Stream.WriteByte((byte)(value >> 48));
                Stream.WriteByte((byte)(value >> 40));
                Stream.WriteByte((byte)(value >> 32));
                Stream.WriteByte((byte)(value >> 24));
                Stream.WriteByte((byte)(value >> 16));
                Stream.WriteByte((byte)(value >> 8));
                Stream.WriteByte((byte)value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void WriteString(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                Stream.WriteByte(0x00);
                return;
            }

            var length = value.Length;

            if (1024 > length)
            {
                var bytes = Encoding.UTF8.GetBytes(value.ToCharArray());

                if (32 > length)
                {
                    Stream.WriteByte((byte) length);
                }
                else
                {
                    Stream.WriteByte((byte) (0x30 + (byte) (length >> 8)));
                    Stream.WriteByte((byte) length);
                }

                Stream.Write(bytes, 0, bytes.Length);

                return;
            }

            const int maxChunkLength = 1024;
            var position = 0;

            while (position < length)
            {
                var count = Math.Min(length - position, maxChunkLength);
                var final = length == (position + count);
                var chunk = value.Substring(position, count);
                var bytes = Encoding.UTF8.GetBytes(chunk.ToCharArray());

                Stream.WriteByte(final ? Marker.StringFinalChunk : Marker.StringNonFinalChunk);
                Stream.WriteByte((byte)(count >> 8));
                Stream.WriteByte((byte)count);
                Stream.Write(bytes, 0, bytes.Length);

                position += count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void BeginClassDefinition()
        {
            Stream.WriteByte(Marker.ClassDefinition);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void EndClassDefinition()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public virtual void WriteObjectReference(int index)
        {
            if (index < 0x10)
            {
                Stream.WriteByte((byte)(0x60 + index));
            }
            else
            {
                Stream.WriteByte(Marker.ClassReference);
                WriteInt32(index);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public virtual void WriteInstanceReference(int index)
        {
            Stream.WriteByte(Marker.InstanceReference);
            WriteInt32(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="length"></param>
        public virtual IDisposable BeginFixedArray(string elementType, int length)
        {
            if (8 > length)
            {
                Stream.WriteByte((byte) (0x70 + length));
                WriteString(elementType);
            }
            else
            {
                Stream.WriteByte(Marker.FixedLengthList);
                WriteString(elementType);
                WriteInt32(length);
            }

            var scope = new object();
            var disposer = new DisposeAction<object>(scope, context =>
            {
                var top = arrays.Pop();

                if (top.Context != context)
                {
                    throw new HessianSerializerException();
                }
            });

            arrays.Push(disposer);

            return disposer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementType"></param>
        public virtual IDisposable BeginArray(string elementType)
        {
            Stream.WriteByte(Marker.BeginList);
            WriteString(elementType);

            var scope = new object();
            var disposer = new DisposeAction<object>(scope, context =>
            {
                var top = arrays.Pop();

                if (top.Context != context)
                {
                    throw new HessianSerializerException();
                }

                Stream.WriteByte(Marker.EndList);
            });

            arrays.Push(disposer);

            return disposer;
        }
    }
}