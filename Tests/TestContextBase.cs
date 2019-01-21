using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests
{
    public abstract class TestContextBase
    {
        [TestInitialize]
        public void Setup()
        {
            Arrange();
            Act();
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
        }

        protected abstract void Arrange();

        protected abstract void Act();

        [Conditional("DEBUG")]
        protected void DebugWriteArray(byte[] bytes)
        {
            var formatProvider = CultureInfo.InvariantCulture;
            var offset = 0;

            while (true)
            {
                var line = new StringBuilder();
                var count = Math.Max(0, Math.Min(16, bytes.Length - offset));
                
                if (0 == count)
                {

                    break;
                }

                line.AppendFormat(formatProvider, "{0:X04}", offset).Append(' ', 2);

                for (var index = 0; index < count; index++)
                {
                    if (8 == index)
                    {
                        line.Append(' ');
                    }

                    line.AppendFormat(formatProvider, "{0:X02}", bytes[offset + index]).Append(' ');
                }

                line.Append(' ', 57 - line.Length);

                for (var index = 0; index < count; index++)
                {
                    if (8 == index)
                    {
                        line.Append(' ');
                    }

                    var ch = (char) bytes[offset + index];

                    line
                        .Append(Char.IsLetterOrDigit(ch) ? ch : '.')
                        .Append(' ');
                }

                Debug.WriteLine(line.ToString());

                offset += count;
            }
        }
    }
}