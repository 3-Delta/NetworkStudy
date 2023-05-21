using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 在Game中统一驱动调用
public class SystemMgr : SystemBase<SystemMgr> {
    private static readonly List<ISystemBaseCallback> _sysBaseList = new List<ISystemBaseCallback>();
    private static readonly List<ISystemNetTransfer> _sysTransferList = new List<ISystemNetTransfer>();
    private static readonly List<ISystemUpdateCallback> _sysUpdateList = new List<ISystemUpdateCallback>();

    public void Register(List<ISystemBaseCallback> list) {
        _sysBaseList.Clear();
        _sysUpdateList.Clear();

        foreach (var one in list) {
            _sysBaseList.Add(one);

            if (one is ISystemUpdateCallback update) {
                _sysUpdateList.Add(update);
            }

            if (one is ISystemNetTransfer transfer) {
                _sysTransferList.Add(transfer);
            }
        }
    }

    public override void OnInit() {
        foreach (var one in _sysBaseList) {
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                one.OnInit();
                one.OnHandleEvents(true);
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnInit() Failed, because {ex.Message}!");
            }
        }
    }

    public override void OnExit() {
        foreach (var one in _sysBaseList) {
            one.OnExit();
            one.OnHandleEvents(false);
        }
    }

    public override void OnFenced() {
        foreach (var one in _sysBaseList) {
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                one.OnFenced();
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnFenced() Failed, because {ex.Message}!");
            }
        }
    }

    public override void OnBeginReconnect() {
        foreach (var one in _sysBaseList) {
            one.OnBeginReconnect();
        }
    }

    public override void OnEndReconnect(bool result) {
        foreach (var one in _sysBaseList) {
            one.OnEndReconnect(result);
        }
    }

    public override void OnLogin(ulong roleId) {
        foreach (var one in _sysBaseList) {
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                if (roleId != one.RoleId) {
                    one.OnLoadINI(roleId);
                }

                one.RoleId = roleId;
                one.OnLogin(roleId);
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnLogin() Failed, because {ex.Message}!");
            }
        }
    }

    public override void OnLogout() {
        foreach (var one in _sysBaseList) {
            one.OnLogout();
            one.RoleId = 0;
        }
    }

    public void OnUpdate() {
        foreach (var one in _sysUpdateList) {
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                one.OnUpdate();
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnUpdate() Failed, because {ex.Message}!");
            }
        }
    }

    public void SetTransfer(NW_Transfer nwTransfer) {
        foreach (var one in _sysTransferList) {
            one.nwTransfer = nwTransfer;
        }
    }
}
