
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace App.Security.Authorization.Infrastructure
{
    /// <summary>
    /// Default implementation for a binary serializer for use with Redis.
    /// </summary>
    /// <remarks>Because of its unobtrusiveness, the default BinaryFormatter was used over protobuf. If this needs to change it can</remarks>
    class DefaultBinarySerializer : IBinarySerializer
    {
        public byte[] Serialize(object value)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, value);
                return stream.ToArray();
            }
        }
    
        public object Deserialize(byte[] value)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(value))
            {
                return formatter.Deserialize(stream);
            }
        }
    }
}
