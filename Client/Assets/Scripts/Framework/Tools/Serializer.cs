using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using Google.Protobuf;

public static class Serializer {
    // Json序列化与反序列化
    public static void Serialize2Json(object o, string filePath) {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(o);
        if (!System.IO.File.Exists(filePath)) {
            using (System.IO.File.Create(filePath)) {
                /*let using to dispose*/
            }
        }

        using (System.IO.StreamWriter w = new System.IO.StreamWriter(filePath, false)) {
            w.Write(json);
        }
    }

    public static bool DeSerializeJson<T>(string filePath, out T o) where T : class, new() {
        bool ret = false;
        if (System.IO.File.Exists(filePath)) {
            ret = false;
            o = new T();
            return ret;
        }

        string json = null;
        using (System.IO.StreamReader r = new System.IO.StreamReader(filePath, System.Text.Encoding.UTF8)) {
            json = r.ReadToEnd();
        }

        o = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        return ret;
    }

    // 二进制序列化与反序列化
    public static byte[] Serialize<T>(T t) {
        using (MemoryStream stream = new MemoryStream()) {
            try {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, t);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
            catch (Exception) {
                return null;
            }
        }
    }

    public static T DeSerialize<T>(byte[] bytes) {
        using (MemoryStream stream = new MemoryStream()) {
            T ret = default(T);
            try {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(stream);
            }
            catch (Exception) {
                return ret;
            }
        }
    }

    // Protobuf序列化与反序列化
    public static T DeSerialize<T>(MessageParser parser, byte[] bytes) {
        T ret = default(T);
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
            IMessage message = parser.ParseFrom(bytes);
            ret = (T)message;
            return ret;
        }
    }

    public static byte[] Serialize(IMessage message) {
        byte[] ret = null;
        using (MemoryStream strenm = new MemoryStream()) {
            message.WriteTo(strenm);
            ret = strenm.ToArray();
        }

        return ret;
    }
}
