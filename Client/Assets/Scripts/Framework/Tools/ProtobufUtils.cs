using Google.Protobuf;

public static class ProtobufUtils {
    public static bool HasValue(int nullableEnum) {
        return nullableEnum != default(int);
    }

    public static T DeSerialize<T>(MessageParser parser, byte[] bytes) {
        return Serializer.DeSerialize<T>(parser, bytes);
    }

    public static byte[] Serialize(IMessage message) {
        return Serializer.Serialize(message);
    }
}
