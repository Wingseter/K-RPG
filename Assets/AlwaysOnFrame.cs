using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlwaysOnFrame : MonoBehaviour
{
    public void openEquip()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnA);
        Manager.instance.manager_Inven.charInfoFrame.SetActive(true);
        Manager.instance.manager_Inven.OpenBag();
    }

    public void openShop()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnA);
        Manager.instance.manager_Shop.shopFrame.SetActive(true);
    }

    public void closeShop()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnA);
        Manager.instance.manager_Shop.shopFrame.SetActive(false);
    }

}
