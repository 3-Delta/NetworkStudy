using System;
using Google.Protobuf;
using pb = Google.Protobuf;
using System.Collections.Generic;
using UnityEngine;

public static class NWDelegateService {
    public interface IHandler {
        void Handle(Action<IMessage> callback, bool toBeAdd);
        void Fire(NW_ReceiveMessage mesaage);
    }

    public class Handler : IHandler {
        public ushort requestProtoType;
        public ushort responseProtoType;

        public Action<IMessage> callback;
        public MessageParser parser;

        public Handler(ushort requestProtoType, ushort responseProtoType, MessageParser parser, Action<IMessage> callback) {
            this.requestProtoType = requestProtoType;
            this.responseProtoType = responseProtoType;
            this.callback = callback;
            this.parser = parser;
        }

        // T 和 接口的 T这里是故意一样的，否则会编译错误
        public void Handle(Action<IMessage> callback, bool toBeAdd) {
            NWDelegateService.emiter.Handle(responseProtoType, callback, toBeAdd);
        }

        public void Fire(NW_ReceiveMessage message) {
            if (ProtobufUtils.TryDeserialize<IMessage>(parser, message.bytes, out IMessage msg)) {
                message.bytes = null;
                callback?.Invoke(msg);
            }
        }

        public override string ToString() {
            return $"request:{requestProtoType.ToString()} response:{responseProtoType.ToString()}";
        }
    }

    public static readonly DelegateService<ushort> emiter = new DelegateService<ushort>();

    // protoType : pb::IMessage
    private static readonly Dictionary<ushort, Handler> dict = new Dictionary<ushort, Handler>();

    // 设计目的：为了代码review者可以方便得到Request和Response的匹配关系
    // 这里记录requestProtoType的目的是：
    // 1:方便review者可以轻松匹配request和response的关系
    // 2:将来去实现网络等待光圈的时候可以利用这个request对应的response是否进行回复，如果回复，则关闭网络等待ui。 【WaitUI】
    public static void Add(ushort requestProtoType, ushort responseProtoType, Action<IMessage> callback, MessageParser parser) {
        // parserDict
        if (!dict.TryGetValue(responseProtoType, out var handler)) {
            // converter只传递一次
            handler = new Handler(requestProtoType, responseProtoType, parser, callback);
            dict.Add(responseProtoType, handler);
        }

        handler.Handle(callback, true);
    }

    public static void Remove(ushort responseProtoType, Action<IMessage> callback) {
        if (dict.TryGetValue(responseProtoType, out var handler)) {
            handler.Handle(callback, false);
        }
    }

    public static bool TryGetHandler(ushort protoType, out Handler handler) {
        return dict.TryGetValue(protoType, out handler);
    }

    public static void Handle(ushort requestProtoType, ushort responseProtoType, Action<IMessage> callback, MessageParser parser, bool toBeAdd) {
        if (toBeAdd) {
            Add(requestProtoType, responseProtoType, callback, parser);
        }
        else {
            Remove(responseProtoType, callback);
        }
    }

    // 网络中使用这个Fire
    public static void Fire(ushort protoType, NW_ReceiveMessage message) {
        if (dict.TryGetValue(protoType, out var handler)) {
            handler.Fire(message);
        }
    }

    public static void Clear() {
        dict.Clear();
    }
}
