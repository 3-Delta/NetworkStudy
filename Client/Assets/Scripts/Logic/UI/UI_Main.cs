using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour {
    public Button btnLunch;
    public Button btnLogin;
    public Button btnMail;
    public Button btnLogout;
    public Button btnLogoutAndLaunch;

    private void Awake() {
        btnLunch.onClick.AddListener(OnBtnLunchClicked);
        btnLogin.onClick.AddListener(OnBtnLoginClicked);
        btnMail.onClick.AddListener(OnBtnMailClicked);
        btnLogout.onClick.AddListener(OnBtnLogoutClicked);
        btnLogoutAndLaunch.onClick.AddListener(OnBtnLogoutAndLaunchClicked);
    }

    private void OnBtnLunchClicked() {
        NW_Mgr.Instance.Connect(NW_Def.IPv4, NW_Def.PORT);
    }
    private void OnBtnMailClicked() {
        Sys_Mail.Instance.ReqReadMail();
    }
    private void OnBtnLoginClicked() {
        Sys_User.Instance.ReqLogin();
    }
    private void OnBtnLogoutClicked() {
        NW_Mgr.Instance.OnExit();
    }

    private void OnBtnLogoutAndLaunchClicked() {
        NW_Mgr.Instance.OnExit();
        NW_Mgr.Instance.Connect(NW_Def.IPv4, NW_Def.PORT);
    }
}
