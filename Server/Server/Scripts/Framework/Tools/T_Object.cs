using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

public static class T_Object
{
    public static byte[] Serialize<T>(T t)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, t);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
            catch (Exception) { return null; }
        }
    }
    public static T DeSerialize<T>(byte[] bytes)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            T ret = default(T);
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(stream);
            }
            catch (Exception) { return ret; }
            
        }
    }
}