namespace LibraProgramming.Serialization.Hessian.Core.Extensions
{
    internal static class LeadingByteExtensions
    {
        #region IsNull

        internal static bool IsNull(this LeadingByte lb) => Marker.Null == lb.Value;

        #endregion

        #region Boolean

        internal static bool IsTrue(this LeadingByte lb) => Marker.True == lb.Value;

        internal static bool IsFalse(this LeadingByte lb) => Marker.False == lb.Value;

        #endregion

        #region Integer

        internal static bool IsTinyInt32(this LeadingByte lb) => 0x80 <= lb.Value && lb.Value <= 0xBF;

        internal static bool IsShortInt32(this LeadingByte lb) => 0xC0 <= lb.Value && lb.Value <= 0xCF;

        internal static bool IsCompactInt32(this LeadingByte lb) => 0xD0 <= lb.Value && lb.Value <= 0xD7;

        internal static bool IsUnpackedInt32(this LeadingByte lb) => Marker.UnpackedInteger == lb.Value;

        internal static bool IsTinyInt64(this LeadingByte lb) => 0xD8 <= lb.Value && lb.Value <= 0xEF;

        internal static bool IsShortInt64(this LeadingByte lb) => 0xF0 <= lb.Value && lb.Value <= 0xFF;

        internal static bool IsCompactInt64(this LeadingByte lb) => 0x38 <= lb.Value && lb.Value <= 0x3F;

        internal static bool IsPackedInt64(this LeadingByte lb) => Marker.PackedLong == lb.Value;

        internal static bool IsUnpackedInt64(this LeadingByte lb) => Marker.UnpackedLong == lb.Value;

        internal static bool IsCompactBinary(this LeadingByte lb) => 0x20 <= lb.Value && lb.Value <= 0x2f;

        #endregion

        #region Binary chunks

        internal static bool IsNonFinalChunkBinary(this LeadingByte lb) => Marker.BinaryNonFinalChunk == lb.Value;

        internal static bool IsFinalChunkBinary(this LeadingByte lb) => Marker.BinaryFinalChunk == lb.Value;

        #endregion

        #region Double

        internal static bool IsZeroDouble(this LeadingByte lb) => Marker.DoubleZero == lb.Value;

        internal static bool IsOneDouble(this LeadingByte lb) => Marker.DoubleOne == lb.Value;

        internal static bool IsTinyDouble(this LeadingByte lb) => Marker.DoubleOctet == lb.Value;

        internal static bool IsShortDouble(this LeadingByte lb) => Marker.DoubleShort == lb.Value;

        internal static bool IsCompactDouble(this LeadingByte lb) => Marker.DoubleFloat == lb.Value;

        internal static bool IsUnpackedDouble(this LeadingByte lb) => Marker.Double == lb.Value;

        #endregion

        #region String

        internal static bool IsTinyString(this LeadingByte lb) => lb.Value <= 0x1F;

        internal static bool IsCompactString(this LeadingByte lb) => 0x30 <= lb.Value && lb.Value <= 0x33;

        internal static bool IsNonFinalChunkString(this LeadingByte lb) => Marker.StringNonFinalChunk == lb.Value;

        internal static bool IsFinalChunkString(this LeadingByte lb) => Marker.StringFinalChunk == lb.Value;

        #endregion
        
        #region DateTime

        internal static bool IsCompactDateTime(this LeadingByte lb) => Marker.DateTimeCompact == lb.Value;

        internal static bool IsUnpackedDateTime(this LeadingByte lb) => Marker.DateTimeLong == lb.Value;


        #endregion

        #region Classes and Objects

        internal static bool IsClassDefinition(this LeadingByte lb) => Marker.ClassDefinition == lb.Value;

        internal static bool IsShortObjectReference(this LeadingByte lb) => 0x60 <= lb.Value && lb.Value <= 0x6F;

        internal static bool IsLongObjectReference(this LeadingByte lb) => Marker.ClassReference == lb.Value;

        internal static bool IsInstanceReference(this LeadingByte lb) => Marker.InstanceReference == lb.Value;

        #endregion
    }
}
