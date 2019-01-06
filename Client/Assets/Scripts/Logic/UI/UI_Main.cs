using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button btnLunch;
    public Button btnMail;
    public Button btnLogin;

    private void Awake()
    {
        btnLunch.onClick.AddListener(OnBtnLunchClicked);
        btnMail.onClick.AddListener(OnBtnMailClicked);
        btnLogin.onClick.AddListener(OnBtnLoginClicked);
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
