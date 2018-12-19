using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button btnLunch;
    public Button btnMail;

    private void Awake()
    {
        btnLunch.onClick.AddListener(OnBtnLunchClicked);
        btnMail.onClick.AddListener(OnBtnMailClicked);
    }

    private void OnBtnLunchClicked()
    {
        BS_NwMgr.Instance.Lunch();
    }
    private void OnBtnMailClicked()
    {
        Sys_Mail.Instance.ReqReadMail();
    }
}
