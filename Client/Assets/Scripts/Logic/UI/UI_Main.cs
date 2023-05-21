using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour {
    public Button btnLunch;
    public Button btnLogin;
    public Button btnMail;
    public Button btnLogout;
    public Button btnExit;

    private void Awake() {
        btnLunch.onClick.AddListener(OnBtnLunchClicked);
        btnLogin.onClick.AddListener(OnBtnLoginClicked);
        btnMail.onClick.AddListener(OnBtnMailClicked);
        btnLogout.onClick.AddListener(OnBtnLogoutClicked);
        this.btnExit.onClick.AddListener(OnBtnLogoutAndLaunchClicked);
    }

    private void OnBtnLunchClicked() {
        Sys_Login.Instance.Connect();
    }
    private void OnBtnMailClicked() {
        Sys_Mail.Instance.ReqReadMail();
    }
    private void OnBtnLoginClicked() {
        Sys_Login.Instance.ReqLogin();
    }
    private void OnBtnLogoutClicked() {
        Sys_Login.Instance.ReqLogout();
    }

    private void OnBtnLogoutAndLaunchClicked() {
        Sys_Login.Instance.Disconnect();
    }
}
