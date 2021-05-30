using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deal : MonoBehaviour
{
    public TextMeshProUGUI notice;
    public TextMeshProUGUI gain;

    public void DiaBuy()
    {
        Manager_Inven inven = GetComponent<Manager_Inven>();
        Items_Info item = inven.selectedItem.GetComponent<Items_Info>();

        if (item.diaPrice <= inven.dia)
        {
            Transform emptySlot = Manager.instance.manager_Inven.Slot();

            if (emptySlot != null)
            {
                inven.dia -= item.diaPrice;
                GameObject obj = Instantiate(item.gameObject, emptySlot);
                inven.goldAmount.text = inven.gold.ToString();

                obj.GetComponent<items_action>().inCashStore = false;
                obj.GetComponent<items_action>().inBag = true;

                gain.text = string.Format("-{0}Dia", item.diaPrice);
                gain.color = new Vector4(1, 0.5f, 0.5f, 1);
                gain.gameObject.SetActive(true);
                return;
            }
            notice.text = "Not enough space in bag";
            notice.gameObject.SetActive(true);
            return;
        }
        notice.text = "Not enough Dia";
        notice.gameObject.SetActive(true);
    }

    public void Buy()
    {
        Manager_Inven inven = GetComponent<Manager_Inven>();
        Items_Info item = inven.selectedItem.GetComponent<Items_Info>();

        if(item.price <= inven.gold)
        {
            Transform emptySlot = Manager.instance.manager_Inven.Slot();

            if(emptySlot != null)
            {
                inven.gold -= item.price;
                GameObject obj = Instantiate(item.gameObject, emptySlot);
                inven.goldAmount.text = inven.gold.ToString();

                obj.GetComponent<items_action>().inStore = false;
                obj.GetComponent<items_action>().inBag = true;

                gain.text = string.Format("-{0}", item.price);
                gain.color = new Vector4(1, 0.5f, 0.5f, 1);
                gain.gameObject.SetActive(true);
                return;
            }
            notice.text = "Not enough space in bag";
            notice.gameObject.SetActive(true);
            return;
        }
        notice.text = "Not enough gold";
        notice.gameObject.SetActive(true);
    }

    public void Sell()
    {
        Manager_Inven inven = GetComponent<Manager_Inven>();
        Items_Info item = inven.selectedItem.GetComponent<Items_Info>();

        inven.gold += item.resalsePrice;
        inven.goldAmount.text = inven.gold.ToString();
        Destroy(inven.selectedItem.gameObject);

        gain.text = string.Format("+{0}", item.price);
        gain.color = new Vector4(0.5f, 1f, 0.5f, 1);
        gain.gameObject.SetActive(true);
    }
}
