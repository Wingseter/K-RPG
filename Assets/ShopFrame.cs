using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopFrame : MonoBehaviour
{
    [Header("Frame")]
    public GameObject CashStoreFrame;
    public GameObject GachaFrame;
    public GameObject DiamondFrame;
    public GameObject GoldFrame;

    [Header("CashShop")]
    public Transform[] slot_cashItems;
    public GameObject[] saleItems;


    private void Start()

    {
        OpenCashStore();
    }


    public void DisAbleAll()
    {
        CashStoreFrame.SetActive(false);
        GachaFrame.SetActive(false);
        DiamondFrame.SetActive(false);
        GoldFrame.SetActive(false);
    }


    public void OpenCashStore()
    {
        DisAbleAll();
        CashStoreFrame.SetActive(true);

        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);

        for (int i = 0; i < slot_cashItems.Length; i++)
        {
            slot_cashItems[i].gameObject.SetActive(false);

            if (slot_cashItems[i].childCount == 6)
                Destroy(slot_cashItems[i].GetChild(5).gameObject);
        }

        for (int i = 0; i < saleItems.Length; i++)

        {
            if (saleItems[i] != null)
            {
                GameObject obj = Instantiate(saleItems[i], slot_cashItems[i]);
                obj.GetComponent<items_action>().inCashStore = true;
                obj.transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>().text = string.Format("{0}Dia", obj.GetComponent<Items_Info>().diaPrice);
                obj.transform.parent.GetChild(4).GetComponent<TextMeshProUGUI>().text = obj.GetComponent<Items_Info>().name_Item;
                obj.transform.parent.gameObject.SetActive(true);

            }
        }
        gameObject.SetActive(true);
    }

    public void OpenGacha()
    {
        DisAbleAll();
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);

        GachaFrame.SetActive(true);
    }

    public void OpenDia()
    {
        DisAbleAll();
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);
        DiamondFrame.SetActive(true);
    }

    public void OpenGold()
    {
        DisAbleAll();
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);
        GoldFrame.SetActive(true);

    }

}
