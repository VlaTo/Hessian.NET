using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LibraProgramming.Hessian.Tests.UWP
{
    [TestClass]
    public class HessianSerializerTests
    {
        [TestMethod]
        [TestCategory("Object Serialization")]
        public void SimpleSerialize()
        {
            byte[] expected =
            {
                0x43, 0x0a, 0x54, 0x65, 0x73, 0x74, 0x43, 0x6c, 0x61, 0x73, 0x73, 0x31, 0x92, 0x06, 0x43, 0x6c,
                0x61, 0x73, 0x73, 0x32, 0x04, 0x4e, 0x65, 0x78, 0x74, 0x60, 0x43, 0x0a, 0x54, 0x65, 0x73, 0x74,
                0x43, 0x6c, 0x61, 0x73, 0x73, 0x32, 0x93, 0x03, 0x49, 0x6e, 0x74, 0x06, 0x53, 0x74, 0x72, 0x69,
                0x6e, 0x67, 0x04, 0x4c, 0x69, 0x6e, 0x6b, 0x61, 0x91, 0x0b, 0x4c, 0x6f, 0x72, 0x65, 0x6d, 0x20,
                0x49, 0x70, 0x73, 0x75, 0x6d, 0x51, 0x90, 0x51, 0x90
            };
            var graph = new TestClass1
            {
                Class2 = new TestClass2
                {
                    IntValue = 1,
                    StringValue = "Lorem Ipsum"
                }
            };

            graph.Next = graph;
            graph.Class2.Parent = graph;

            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractHessianSerializer(typeof (TestClass1));
                
                serializer.WriteObject(stream, graph);
                stream.Flush();

                Assert.IsTrue(ByteArray.Equals(expected, stream.ToArray()));
            }
        }

        [TestMethod]
        [TestCategory("Object Deserialization")]
        public void SimpleDeserialize()
        {
            byte[] expected =
            {
                0x43, 0x0a, 0x54, 0x65, 0x73, 0x74, 0x43, 0x6c, 0x61, 0x73, 0x73, 0x31, 0x92, 0x06, 0x43, 0x6c,
                0x61, 0x73, 0x73, 0x32, 0x04, 0x4e, 0x65, 0x78, 0x74, 0x60, 0x43, 0x0a, 0x54, 0x65, 0x73, 0x74,
                0x43, 0x6c, 0x61, 0x73, 0x73, 0x32, 0x93, 0x03, 0x49, 0x6e, 0x74, 0x06, 0x53, 0x74, 0x72, 0x69,
                0x6e, 0x67, 0x04, 0x4c, 0x69, 0x6e, 0x6b, 0x61, 0x91, 0x0b, 0x4c, 0x6f, 0x72, 0x65, 0x6d, 0x20,
                0x49, 0x70, 0x73, 0x75, 0x6d, 0x51, 0x90, 0x51, 0x90
            };
            var graph = new TestClass1
            {
                Class2 = new TestClass2
                {
                    IntValue = 1,
                    StringValue = "Lorem Ipsum"
                }
            };

            graph.Next = graph;
            graph.Class2.Parent = graph;

            using (var stream = new MemoryStream(expected))
            {
                var serializer = new DataContractHessianSerializer(typeof (TestClass1));
                var instance = serializer.ReadObject(stream) as TestClass1;

                Assert.IsNotNull(instance);
                Assert.AreEqual(graph.GetType(), instance.GetType());
                Assert.AreEqual(instance, instance.Next);
                Assert.AreEqual(typeof (TestClass2), instance.Class2.GetType());
                Assert.AreEqual(instance, instance.Class2.Parent);
                Assert.AreEqual(graph.Class2.IntValue, instance.Class2.IntValue);
                Assert.AreEqual(graph.Class2.StringValue, instance.Class2.StringValue);
            }
        }

/*
        private static void WriteOutput(IReadOnlyList<byte> bytes)
        {
            const int size = 16;

            var pattern = new String(' ', 3);

            for (var offset = 0; offset < bytes.Count; )
            {
                var count = Math.Min(bytes.Count - offset, size);
                var line = new StringBuilder();

                line.AppendFormat("{0:X08}: ", offset);

                for (var position = 0; position < size; position++)
                {
                    if (position < count)
                    {
                        line.AppendFormat("{0:x02} ", bytes[position + offset]);
                    }
                    else
                    {
                        line.Append(pattern);
                    }
                }

                line.Append(new string(' ', 2));

                for (var position = 0; position < count; position++)
                {
                    var ch = (char) bytes[position + offset];
                    line.Append(Char.IsLetterOrDigit(ch) ? ch : '.');
                }

                Debug.WriteLine(line.ToString());

                offset += count;
            }
        }
*/

        [DataContract(Name = "TestClass1")]
        public class TestClass1
        {
            [DataMember(Name = "Class2")]
            public TestClass2 Class2
            {
                get;
                set;
            }

            [DataMember(Name = "Next")]
            public TestClass1 Next
            {
                get;
                set;
            }
        }

        [DataContract(Name = "TestClass2")]
        public class TestClass2
        {
            [DataMember(Name = "Int", Order = 1)]
            public int IntValue
            {
                get;
                set;
            }

            [DataMember(Name = "String", Order = 2)]
            public string StringValue
            {
                get;
                set;
            }

            [DataMember(Name = "Link", Order = 3)]
            public TestClass1 Parent
            {
                get;
                set;
            }
        }
    }
}