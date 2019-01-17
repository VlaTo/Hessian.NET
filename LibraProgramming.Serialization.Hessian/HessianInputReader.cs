﻿using System;
using System.IO;
using System.Text;
using LibraProgramming.Serialization.Hessian.Core;
using LibraProgramming.Serialization.Hessian.Core.Extensions;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public class HessianInputReader : DisposableStreamHandler
    {
        private ObjectPreamble preamble;

        /// <summary>
        /// 
        /// </summary>
        public bool IsClassDefinition => ObjectPreamble.ClassDefinition == preamble;

        /// <summary>
        /// 
        /// </summary>
        public bool IsInstanceReference => ObjectPreamble.InstanceReference == preamble;

        /// <summary>
        /// 
        /// </summary>
        protected LeadingByte LeadingByte
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public HessianInputReader(Stream stream)
            : base(stream)
        {
            LeadingByte = LeadingByte.Empty;
        }

        /// <summary>
        /// Reads <see cref="System.Boolean" /> value from the stream.
        /// </summary>
        /// <returns>The value</returns>
        public virtual bool ReadBoolean()
        {
            ReadLeadingByte();

            if (LeadingByte.IsTrue())
            {
                return true;
            }

            if (false == LeadingByte.IsFalse())
            {
                throw new HessianSerializerException();
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int ReadInt32()
        {
            ReadLeadingByte();

            if (LeadingByte.IsTinyInt32())
            {
                return LeadingByte.Value - 144;
            }

            if (LeadingByte.IsShortInt32())
            {
                return ReadShortInt32();
            }

            if (LeadingByte.IsCompactInt32())
            {
                return ReadCompactInt32();
            }

            if (false == LeadingByte.IsUnpackedInt32())
            {
                throw new HessianSerializerException();
            }

            return ReadUnpackedInt32();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual long ReadInt64()
        {
            ReadLeadingByte();

            if (LeadingByte.IsTinyInt64())
            {
                return LeadingByte.Value - 224;
            }

            if (LeadingByte.IsShortInt64())
            {
                return ReadShortInt64();
            }

            if (LeadingByte.IsCompactInt64())
            {
                return ReadCompactInt64();
            }

            if (LeadingByte.IsPackedInt64())
            {
                return ReadPackedInt64();
            }

            if (false == LeadingByte.IsUnpackedInt64())
            {
                throw new HessianSerializerException();
            }

            return ReadUnpackedInt64();
        }

        /// <summary>
        /// Reads binary data from stream.
        /// </summary>
        /// <returns>The array of bytes.</returns>
        public virtual byte[] ReadBytes()
        {
            ReadLeadingByte();

            if (LeadingByte.IsNull())
            {
                return null;
            }

            if (LeadingByte.IsCompactBinary())
            {
                return ReadBinaryCompact15();
            }

            var count = 0;
            var buffer = new byte[0];

            while (LeadingByte.IsNonFinalChunkBinary() || LeadingByte.IsFinalChunkBinary())
            {
                var length = GetChunkLength();

                if (buffer.Length < (count + length))
                {
                    var temp = new byte[count + length];

                    Buffer.BlockCopy(buffer, 0, temp, 0, buffer.Length);

                    buffer = temp;
                }

                if (Stream.Read(buffer, count, length) != length)
                {
                    throw new HessianSerializerException();
                }

                count += length;

                if (LeadingByte.IsFinalChunkBinary())
                {
                    break;
                }

                ReadLeadingByte();
            }

            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual double ReadDouble()
        {
            ReadLeadingByte();

            if (LeadingByte.IsZeroDouble())
            {
                return 0.0d;
            }

            if (LeadingByte.IsOneDouble())
            {
                return 1.0d;
            }

            if (LeadingByte.IsTinyDouble())
            {
                return Stream.ReadByte();
            }

            if (LeadingByte.IsShortDouble())
            {
                return ReadShortDouble();
            }

            if (LeadingByte.IsCompactDouble())
            {
                return ReadCompactDouble();
            }

            if (false == LeadingByte.IsUnpackedDouble())
            {
                throw new HessianSerializerException();
            }

            return ReadUnpackedDouble();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string ReadString()
        {
            ReadLeadingByte();

            if (LeadingByte.IsTinyString())
            {
                return GetStringChunk(LeadingByte.Value);
            }

            if (LeadingByte.IsCompactString())
            {
                return ReadCompactString();
            }

            var builder = new StringBuilder();

            while (LeadingByte.IsNonFinalChunkString() || LeadingByte.IsFinalChunkString())
            {
                var chunkLength = GetChunkLength();
                var chunk = GetStringChunk(chunkLength);

                builder.Append(chunk);

                if (LeadingByte.IsFinalChunkString())
                {
                    break;
                }

                ReadLeadingByte();
            }

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual DateTime ReadDateTime()
        {
            ReadLeadingByte();

            if (LeadingByte.IsCompactDateTime())
            {
                var minutes = ReadUnpackedInt32();
                return DateTimeExtension.FromMinutes(minutes);
            }

            if (false == LeadingByte.IsUnpackedDateTime())
            {
                throw new HessianSerializerException();
            }

            return DateTimeExtension.FromMilliseconds(ReadUnpackedInt64());
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void BeginObject()
        {
            ReadLeadingByte();

            if (LeadingByte.IsClassDefinition())
            {
                preamble = ObjectPreamble.ClassDefinition;
            }
            else if (LeadingByte.IsShortObjectReference() || LeadingByte.IsLongObjectReference())
            {
                preamble = ObjectPreamble.ObjectReference;
            }
            else if (LeadingByte.IsInstanceReference())
            {
                preamble = ObjectPreamble.InstanceReference;
            }
            else
            {
                throw new HessianSerializerException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void EndObject()
        {
            preamble = ObjectPreamble.None;
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
        /// <returns></returns>
        public virtual int ReadObjectReference()
        {
            ReadLeadingByte();

            if (LeadingByte.IsShortObjectReference())
            {
                return LeadingByte.Value - 0x60;
            }

            if (false == LeadingByte.IsLongObjectReference())
            {
                throw new HessianSerializerException();
            }

            return ReadInt32();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ReadInstanceReference()
        {
            return ReadInt32();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ReadLeadingByte()
        {
            var data = Stream.ReadByte();

            if (-1 == data)
            {
                throw new HessianSerializerException();
            }

            LeadingByte = new LeadingByte((byte) data);
        }

        private int ReadShortInt32()
        {
            var data = Stream.ReadByte();
            return ((LeadingByte.Value - 200) << 8) | data;
        }

        private int ReadCompactInt32()
        {
            var value = (LeadingByte.Value - 212) << 16;

            value |= (Stream.ReadByte() << 8);
            value |= Stream.ReadByte();

            return value;
        }

        private int ReadUnpackedInt32()
        {
            var value = Stream.ReadByte() << 24;

            value |= (Stream.ReadByte() << 16);
            value |= (Stream.ReadByte() << 8);
            value |= Stream.ReadByte();

            return value;
        }

        private long ReadShortInt64()
        {
            var data = Stream.ReadByte();
            return ((LeadingByte.Value - 248) << 8) | data;
        }

        private long ReadCompactInt64()
        {
            var value = (LeadingByte.Value - 60) << 16;

            value |= (Stream.ReadByte() << 8);
            value |= Stream.ReadByte();

            return value;
        }

        private long ReadPackedInt64()
        {
            var value = Stream.ReadByte() << 24;

            value |= (Stream.ReadByte() << 16);
            value |= (Stream.ReadByte() << 8);
            value |= Stream.ReadByte();

            return value;
        }

        private long ReadUnpackedInt64()
        {
            var value = (long) Stream.ReadByte() << 56;

            value |= ((long) Stream.ReadByte() << 48);
            value |= ((long) Stream.ReadByte() << 40);
            value |= ((long) Stream.ReadByte() << 32);
            value |= ((long) Stream.ReadByte() << 24);
            value |= ((long) Stream.ReadByte() << 16);
            value |= ((long) Stream.ReadByte() << 8);
            value |= (uint) Stream.ReadByte();

            return value;
        }

        private byte[] ReadBinaryCompact15()
        {
            var size = LeadingByte.Value - 32;
            var bytes = new byte[size];

            if (Stream.Read(bytes, 0, size) != size)
            {
                throw new HessianSerializerException();
            }

            return bytes;
        }

        private double ReadShortDouble()
        {
            var value = Stream.ReadByte() << 8;

            value |= Stream.ReadByte();

            return Convert.ToDouble((short)value);
        }

        private double ReadCompactDouble()
        {
            var buffer = new byte[4];

            for (var index = buffer.Length - 1; index >= 0; index--)
            {
                buffer[index] = (byte)Stream.ReadByte();
            }

            return BitConverter.ToSingle(buffer, 0);
        }

        private double ReadUnpackedDouble()
        {
            var value = 0L;

            for (var count = 8; count > 0; count--)
            {
                value <<= 8;
                value |= (uint) Stream.ReadByte();
            }

            return BitConverter.Int64BitsToDouble(value);
        }

        private string ReadCompactString()
        {
            var length = (LeadingByte.Value - 0x30) << 8;

            length |= Stream.ReadByte();

            return GetStringChunk(length);
        }

        private string GetStringChunk(int length)
        {
            var buffer = new StringBuilder();

            while (0 < length--)
            {
                var ch = ReadChar();
                buffer.Append(ch);
            }

            return buffer.ToString();
        }

        private int GetChunkLength()
        {
            var b0 = Stream.ReadByte();
            var b1 = Stream.ReadByte();

            return (b0 << 8) + b1;
        }

        private char ReadChar()
        {
            var data = Stream.ReadByte();

            if (data < 0x80)
            {
                return (char)data;
            }

            if ((data & 0xE0) == 0xC0)
            {
                var b0 = Stream.ReadByte();
                return (char) (((data & 0x1F) << 6) + (b0 & 0x3F));
            }

            if ((data & 0xF0) == 0xE0)
            {
                var b0 = Stream.ReadByte();
                var b1 = Stream.ReadByte();
                return (char) (((data & 0x0F) << 12) + ((b0 & 0x3F) << 6) + (b1 & 0x3F));
            }

            throw new HessianSerializerException();
        }

        /*
         * hessian implementation
         * https://github.com/benjamin-bader/hessian/blob/master/Hessian/ValueReader.cs
        public uint ReadUtf8Codepoint ()
        {
            const uint replacementChar = 0xFFFD;

            byte b0, b1, b2, b3;
            b0 = ReadByte ();

            if (b0 < 0x80) {
                return b0;
            }
 
            if (b0 < 0xC2) {
                return replacementChar;
            }
           
            if (b0 < 0xE0) {
                b1 = ReadByte ();

                if ((b1 ^ 0x80) >= 0x40) {
                    return replacementChar;
                }

                return (b1 & 0x3Fu) | ((b0 & 0x1Fu) << 6);
            }

            if (b0 < 0xF0) {
                b1 = ReadByte ();
                b2 = ReadByte ();

                // Valid range: E0 A0..BF 80..BF
                if (b0 == 0xE0 && (b1 ^ 0xA0) >= 0x20) {
                    return replacementChar;
                }

                // Valid range: ED 80..9F 80..BF
                if (b0 == 0xED && (b1 ^ 0x80) >= 0x20) {
                    return replacementChar;
                }

                // Valid range: E1..EC 80..BF 80..BF
                if ((b1 ^ 0x80) >= 0x40 || (b2 ^ 0x80) >= 0x40) {
                    return replacementChar;
                }

                return (b2 & 0x3Fu)
                    | ((b1 & 0x3Fu) << 6)
                    | ((b0 & 0x0Fu) << 12);
            }

            if (b0 < 0xF1) {
                b1 = ReadByte();

                if ((b1 ^ 0x90) < 0x30) {
                    return replacementChar;
                }

                b2 = ReadByte();
                b3 = ReadByte();

                if ((b2 & 0xC0) != 0x80 || (b3 & 0xC0) != 0x80) {
                    return replacementChar;
                }

                return (b3 & 0x3Fu)
                    | ((b2 & 0x3Fu) << 6)
                    | ((b1 & 0x3Fu) << 12)
                    | ((b0 & 0x07u) << 18);
            }
            
            if (b0 < 0xF4) {
                b1 = ReadByte ();
                b2 = ReadByte ();
                b3 = ReadByte ();

                // Valid range: F1..F3 80..BF 80..BF 80..BF
                if ((b1 & 0xC0) != 0x80 || (b2 & 0xC0) != 0x80 || (b3 & 0xC0) != 0x80)
                {
                    return replacementChar;
                }

                return (b3 & 0x3Fu)
                    | ((b2 & 0x3Fu) << 6)
                    | ((b1 & 0x3Fu) << 12)
                    | ((b0 & 0x07u) << 18);
            }

            if (b0 < 0xF5) {
                b1 = ReadByte ();

                // Valid range: F4 80..8F 80..BF 80..BF
                if ((b1 ^ 0x80) >= 0x10) {
                    return replacementChar;
                }

                b2 = ReadByte();
                b3 = ReadByte();

                if ((b2 & 0xC0) != 0x80 || (b3 & 0xC0) != 0x80)
                {
                    return replacementChar;
                }

                return (b3 & 0x3Fu)
                    | ((b2 & 0x3Fu) << 6)
                    | ((b1 & 0x3Fu) << 12)
                    | ((b0 & 0x07u) << 18);
            }
            
            return replacementChar;
        }*/
    }
}