using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class itemInfoFrame : MonoBehaviour
{
    public Items_Info item;

    public GameObject statBonus;

    public TextMeshProUGUI name_Item;
    public TextMeshProUGUI info_Item;
    public TextMeshProUGUI resalePrice;  
    public TextMeshProUGUI hpBonus;
    public TextMeshProUGUI atkBonus;
    public TextMeshProUGUI defBonus;
    public TextMeshProUGUI criBonus;

    [Header("Button")]
    public GameObject equipBtn;
    public GameObject releaseBtn;
    public GameObject buyBtn;
    public GameObject sellBtn;

    private void OnEnable()
    {
        statBonus.SetActive(false);
        name_Item.text = item.name_Item;
        info_Item.text = item.info_Item;
        resalePrice.text = string.Format("Used Price : {0}", item.resalsePrice);

        if(item.type == "Equipment")
        {
            hpBonus.text = string.Format("HP+{0}",item.hpBonus);
            atkBonus.text = string.Format("Atk+{0}", item.atkBonus);
            defBonus.text = string.Format("Def +{0}", item.defBonus);
            criBonus.text = string.Format("Cri+{0}", item.criBonus);
            statBonus.SetActive(true);

            if (!Manager.instance.manager_Inven.storeFrame.activeSelf)
            {
                if (!item.equipped)
                    equipBtn.SetActive(true);
                if (item.equipped)
                    releaseBtn.SetActive(true);
            }
            if (Manager.instance.manager_Inven.storeFrame.activeSelf)
            {
                if (item.GetComponent<items_action>().inStore)
                    buyBtn.SetActive(true);
                if (item.GetComponent<items_action>().inBag && !item.equipped)
                    sellBtn.SetActive(true);
            }

        }
    }

    private void OnDisable()
    {
        equipBtn.SetActive(false);
        releaseBtn.SetActive(false);
        buyBtn.SetActive(false);
        sellBtn.SetActive(false);
    }
}
