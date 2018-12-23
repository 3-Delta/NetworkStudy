using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Src.Src
{
    public static class Utils
    {
        public static void Write(UnityEngine.Networking.NetworkWriter writer, object o)
        {
            if (o is char) { writer.Write((char)(o)); }
            if (o is byte) { writer.Write((byte)(o)); }
            if (o is sbyte) { writer.Write((sbyte)(o)); }
            if (o is short) { writer.Write((short)(o)); }
            if (o is ushort) { writer.Write((ushort)(o)); }
            if (o is int) { writer.Write((int)(o)); }
            if (o is uint) { writer.Write((uint)(o)); }
            if (o is float) { writer.Write((float)(o)); }
            if (o is double) { writer.Write((double)(o)); }
            if (o is long) { writer.Write((long)(o)); }
            if (o is ulong) { writer.Write((ulong)(o)); }
            if (o is string) { writer.Write((string)(o)); }
        }
        public static void Read(UnityEngine.Networking.NetworkReader reader, object value, out object o)
        {
            o = null;
            if (value is char) { o = reader.ReadChar(); }
            if (value is byte) { o = reader.ReadByte(); }
            if (value is sbyte) { o = reader.ReadSByte(); }
            if (value is short) { o = reader.ReadInt16(); }
            if (value is ushort) { o = reader.ReadUInt16(); }
            if (value is int) { o = reader.ReadInt32(); }
            if (value is uint) { o = reader.ReadUInt32(); }
            if (value is float) { o = reader.ReadSingle(); }
            if (value is double) { o = reader.ReadDouble(); }
            if (value is long) { o = reader.ReadInt64(); }
            if (value is ulong) { o = reader.ReadUInt64(); }
            if (value is string) { o = reader.ReadString(); }
        }
    }
}
