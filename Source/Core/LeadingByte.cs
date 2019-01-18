using System;

namespace LibraProgramming.Serialization.Hessian.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class LeadingByte : IEquatable<LeadingByte>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly LeadingByte Empty;

        /// <summary>
        /// 
        /// </summary>
        public byte Value
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public LeadingByte(byte value)
        {
            Value = value;
        }

        static LeadingByte()
        {
            Empty = new LeadingByte(Marker.Null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(LeadingByte other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Value == other.Value;
        }

        /// <inheritdoc cref="object.Equals(object)" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return (obj is LeadingByte leadingByte) && Equals(leadingByte);
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(LeadingByte left, LeadingByte right)
        {
            return null != left && left.Equals(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(LeadingByte left, LeadingByte right)
        {
            return null == left || false == left.Equals(right);
        }
    }
}