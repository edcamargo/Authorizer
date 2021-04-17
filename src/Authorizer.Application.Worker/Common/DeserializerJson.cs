using Authorizer.Application.Worker.Common.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace Authorizer.Application.Worker.Common
{
    public class DeserializerJson<T> : IDeserializerJson<T> where T : struct
    {
        object _objModel = new object();

        //public T Deserializer(string pathArchive)
        //{
        //    using StreamReader reader = new(pathArchive);
        //    return Deserializer(reader);
        //}

        public T Deserializer(StreamReader streamReader)
        {
            using (JsonTextReader reader = new(streamReader))
            {
                var _serializer = new JsonSerializer();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        _objModel = _serializer.Deserialize<T>(reader);
                    }
                }
            }

            return (T)_objModel;
        }        
    }
}
