using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_Popup : MonoBehaviour
{
    public GameObject PopupFrame;
    public TextMeshProUGUI PopupTitle;
    public TextMeshProUGUI InnerText;

    public delegate void YesnoCallBack(); 
    private event YesnoCallBack yesCallBack; 
    private event YesnoCallBack noCallBack;

    public TextMeshProUGUI notice;
    public TextMeshProUGUI gain;

    public void OpenPopup(string Title, string innerText)
    {
        PopupTitle.text = Title;
        InnerText.text = innerText;
        PopupFrame.SetActive(true);
    }
    public void SetYesCallback(YesnoCallBack listener) { 
        yesCallBack += listener; 
    }
    public void SetNoCallback(YesnoCallBack listener) {
        noCallBack += listener; 
    }
    public void OnYes() { 
        yesCallBack?.Invoke(); 
    }
    public void OnNo() { 
        noCallBack?.Invoke(); 
    }

    public void closePopup()
    {
        PopupFrame.SetActive(false);

    }

}
