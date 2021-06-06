using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Charge_Cash : MonoBehaviour
{
    [Header("Frame")]
    public PlayerState player;
    public GameObject vipFrame;

    public void chargeDia(int addDia)
    {
        Manager.instance.manager_Popup.OpenPopup("hello", string.Format("Are you sure you want to charge {0}Dia with {1}$", addDia, addDia/100));
        Manager.instance.manager_Popup.SetYesCallback(() =>
        {
            Manager.instance.manager_Inven.dia += addDia;
            Manager.instance.manager_Popup.closePopup();
            player.spantDia += addDia;

            if(player.spantDia >= (player.DiaLevel + 1) * 1000)
            {
                player.spantDia = 0;
                player.DiaLevel += 1;
            }
            vipFrame.SetActive(false);
            vipFrame.SetActive(true);
        });
        Manager.instance.manager_Popup.SetNoCallback(() => {

            Manager.instance.manager_Popup.closePopup();
        });
    }

    public void chargeGold(int addGold)
    {
        int currentDia = Manager.instance.manager_Inven.dia;
        int usedDia = addGold / 100;

        if(currentDia < usedDia)
        {
            Manager.instance.manager_Popup.notice.text = "Not Enough Dia";
            Manager.instance.manager_Popup.notice.gameObject.SetActive(true);
        }
        else
        {
            Manager.instance.manager_Popup.OpenPopup("Charge gold", string.Format("Are you sure you want to charge {0}Gold with {1}Dia", addGold, addGold/ 100));
            Manager.instance.manager_Popup.SetYesCallback(() =>
            {
                Manager.instance.manager_Inven.dia -= usedDia;
                Manager.instance.manager_Inven.gold += addGold;
                Manager.instance.manager_Popup.closePopup();
            });
            Manager.instance.manager_Popup.SetNoCallback(() => {
                Manager.instance.manager_Popup.closePopup();
            });
        }

    }
}
