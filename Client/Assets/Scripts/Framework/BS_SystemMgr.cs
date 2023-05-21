using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 在Game中统一驱动调用
public class SystemMgr : SystemBase<SystemMgr> {
    private static readonly List<ISystemBaseCallback> _sysBaseList = new List<ISystemBaseCallback>();
    private static readonly List<ISystemUpdateCallback> _sysUpdateList = new List<ISystemUpdateCallback>();
    
    public void Register(List<ISystemBaseCallback> list) {
        _sysBaseList.Clear();
        _sysUpdateList.Clear();

        foreach (var one in list) {
            _sysBaseList.Add(one);
            if (one is ISystemUpdateCallback update) {
                _sysUpdateList.Add(update);
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
    
    public override void OnDispose() {
        foreach (var one in _sysBaseList) {
            one.OnDispose();
        }
    }

    public override void OnLoadINI(ulong roleId) {
        foreach (var one in _sysBaseList) {
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                one.OnLoadINI(roleId);
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnLoadINI() Failed, because {ex.Message}!");
            }
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
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                one.OnBeginReconnect();
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnBeginReconnect() Failed, because {ex.Message}!");
            }
        }
    }

    public override void OnEndReconnect(bool result) {
        foreach (var one in _sysBaseList) {
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                one.OnEndReconnect(result);
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnEndReconnect() Failed, because {ex.Message}!");
            }
        }
    }

    public override void OnLogin(ulong roleId) {
        foreach (var one in _sysBaseList) {
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
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
            try {
#if __DEV__
                TimeSpan oneBegin = new TimeSpan(DateTime.Now.Ticks);
#endif
                one.OnLogout();
#if __DEV__
                TimeSpan oneEnd = new TimeSpan(DateTime.Now.Ticks);
                Debug.Log($"[RecordTime] {one.GetType().ToString()}'s running time: {(oneEnd - oneBegin).Milliseconds}");
#endif
            }
            catch (Exception ex) {
                Debug.LogError($"{one.GetType().ToString()}'s OnLogout() Failed, because {ex.Message}!");
            }
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
}
