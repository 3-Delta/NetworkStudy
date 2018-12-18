using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button btnLunch;

    private void Awake()
    {
        btnLunch = btnLunch ?? GetComponentInChildren<Button>();
        btnLunch.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        BS_NwMgr.Instance.Lunch();
    }
}
