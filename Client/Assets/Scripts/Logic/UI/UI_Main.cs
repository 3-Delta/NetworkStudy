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
        /*
         * https://blog.csdn.net/gy373499700/article/details/46970243
            1、当在主线程中创建了一个线程，那么该线程的IsBackground默认是设置为FALSE的。  描述正确
            2、当主线程退出的时候，IsBackground=FALSE的线程还会继续执行下去，直到线程执行结束。 描述正确
            3、只有IsBackground=TRUE的线程才会随着主线程的退出而退出 描述错误
         */
        // 默认false
        // thread.IsBackground = true;
        thread.Start();
    }
    private void OnDestroy()
    {
        thread?.Abort();
    }
    private void Log()
    {
        {
            while (true)
            {
                Thread.Sleep(200);
                Debug.LogError("--------");
            }
        };
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
