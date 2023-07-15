using System.Diagnostics;
using UnityEngine;

// 心跳系统，设计为 随时热插拔组件
public class NW_HeartBeat {
    // 客户端主导心跳，每间隔10s执行一次心跳
    public const float HEART_BEAT_INTERVAL = 10f;
    // 心跳最大请求次数，超过对端没有回复则认为断开了链接
    public const int HEART_BEAT_MAX_REQ_COUNT = 3;

    // 是否开启心跳机制
    public bool enable { get; set; } = false;

    // 服务器主导还是客户端主导的心跳？
    // 登录/选角/创角阶段是被动心跳，进入scene之后是主动心跳
    // 默认客户端被动， 进入sceneFinish之后切换为主动，因为登录选角创角服务器资源比较紧缺，所以服务器主导，进入scene之后切换为客户端主导
    public bool cliPositive { get; set; } = false;

    public bool forceHB { get; private set; }

    // 是否存在心跳异常
    public bool isHBValid {
        get {
            return this._hasReqCount >= HEART_BEAT_MAX_REQ_COUNT;
        }
    }
    
    public NW_Transfer transfer { get; set; }

    // 已经请求次数
    private int _hasReqCount = 0;
    private float _lastSendTime = 0;

#region 逻辑修改部分
    // 心跳请求协议
    public const ushort ReqHBProtoType = 1;
    // 心跳回复协议
    public const ushort NtfHBProtoType = 2;
#endregion
    
    public NW_HeartBeat(NW_Transfer transfer = null) {
        this.transfer = transfer;
        this.Reset();
    }

    public void Reset() {
        _hasReqCount = 0;
        _lastSendTime = 0;
    }

    public void TrySend() {
        if (!this.enable) {
            return;
        }

        if (!cliPositive) {
            return;
        }

        float curTime = Time.realtimeSinceStartup;
        if (curTime < this._lastSendTime + HEART_BEAT_INTERVAL) {
            return;
        }

        this._lastSendTime = curTime;

        if (this.isHBValid) {
            // todo 心跳异常处理，通常是断开socket, 弹出断线提示
            return;
        }

        this._Send();
        ++this._hasReqCount;
        UnityEngine.Debug.LogFormat("{0} 发送心跳： {1}", this.GetHashCode().ToString(), this._hasReqCount.ToString());
    }

    private void _Send() {
        // todo 发送心跳协议给server
    }

    public void TryReceive(ushort protoType) {
        if (!this.enable) {
            return;
        }

        // 收到心跳包
        if (protoType == NtfHBProtoType) {
            // 客户端主导模式，则重置
            // 服务器主导模式，则直接回复
            if (this.cliPositive) {
                this._hasReqCount = 0;
            }
            else {
                this._Send();
            }
        }
    }
}
