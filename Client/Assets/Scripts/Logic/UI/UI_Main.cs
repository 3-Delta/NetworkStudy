using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button btnLunch;
    public Button btnMail;
    public Button btnLogin;

    private Thread thread;

    private void Awake()
    {
        btnLunch.onClick.AddListener(OnBtnLunchClicked);
        btnMail.onClick.AddListener(OnBtnMailClicked);
        btnLogin.onClick.AddListener(OnBtnLoginClicked);

        thread = new System.Threading.Thread(new System.Threading.ThreadStart(Log));
        thread.Start();
    }
    private void Log()
    {
        while (true)
        {
            Thread.Sleep(200);
            // 线程中使用unity的class[非struct], 会导致报错：get_gameObject can only be called from the main thread.
            btnMail.interactable = false;
            Debug.LogError("--------");
        }
    }
    private void OnDestroy()
    {
        thread?.Abort();
    }

    private void OnBtnLunchClicked()
    {
        NW_Mgr.Instance.Connect(NW_Def.IPv4, NW_Def.PORT);
    }
    private void OnBtnMailClicked()
    {
        Sys_Mail.Instance.ReqReadMail();
    }
    private void OnBtnLoginClicked()
    {
        Sys_Player.Instance.ReqLogin();
    }
}
