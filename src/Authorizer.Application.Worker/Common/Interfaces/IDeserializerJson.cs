using System.IO;

namespace Authorizer.Application.Worker.Common.Interfaces
{
    public interface IDeserializerJson<T> where T : struct
    {
        //T Deserializer(string pathArchive);
        T Deserializer(StreamReader streamReader);
    }
}
