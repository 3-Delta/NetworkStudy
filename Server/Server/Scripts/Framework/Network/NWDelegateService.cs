﻿using System;
using Google.Protobuf;
using pb = Google.Protobuf;
using System.Collections.Generic;

public static class NWDelegateService {
    public interface IHandler {
        void Handle<T>(Action<T> callback, bool toBeAdd);
        void Fire(NW_ReceiveMessage message);
    }

    public class Handler : IHandler {
        public ushort requestProtoType;
        public ushort responseProtoType;

        public MessageParser parser;

        public Handler(ushort requestProtoType, ushort responseProtoType, MessageParser parser) {
            this.requestProtoType = requestProtoType;
            this.responseProtoType = responseProtoType;
            this.parser = parser;
        }

        // T 和 接口的 T这里是故意一样的，否则会编译错误
        public void Handle<TMsg>(Action<TMsg> callback, bool toBeAdd) {
            NWDelegateService.emiter.Handle<TMsg>(responseProtoType, callback, toBeAdd);
        }

        public void Fire(NW_ReceiveMessage message) {
            if (ProtobufUtils.TryDeserialize<IMessage>(parser, message.bytes, out IMessage msg))
            {
                message.bytes = null;
                message.message = msg;
                NWDelegateService.emiter.Fire(responseProtoType, message);
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
    public static void Add<T>(ushort requestProtoType, ushort responseProtoType, Action<T> callback, MessageParser parser) {
        // parserDict
        if (!dict.TryGetValue(responseProtoType, out var handler)) {
            // converter只传递一次
            handler = new Handler(requestProtoType, responseProtoType, parser);
            dict.Add(responseProtoType, handler);
        }

        handler.Handle(callback, true);
    }

    public static void Remove<T>(ushort responseProtoType, Action<T> callback) {
        if (dict.TryGetValue(responseProtoType, out var handler)) {
            handler.Handle(callback, false);
        }
    }

    public static bool TryGetHandler(ushort protoType, out Handler handler) {
        return dict.TryGetValue(protoType, out handler);
    }

    public static void Handle<T>(ushort requestProtoType, ushort responseProtoType, Action<T> callback, MessageParser parser, bool toBeAdd) {
        if (toBeAdd) {
            Add<T>(requestProtoType, responseProtoType, callback, parser);
        }
        else {
            Remove<T>(responseProtoType, callback);
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
