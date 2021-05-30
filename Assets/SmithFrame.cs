using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmithFrame : MonoBehaviour
{
    [Header("Frame")]
    public GameObject ReinforceFrame;
    public GameObject CombineFrame;

    [Header("ReinforeSlot")]
    public Transform slot_Item;
    public Items_Info cur_Exist;
    public TextMeshProUGUI nextLevel;
    public TextMeshProUGUI percent;
    public TextMeshProUGUI cost;


    [Header("Combine")]
    public Transform slot_Item1;
    public Transform slot_Item2;
    GameObject item_Slot1;
    GameObject item_Slot2;
    Items_Info cur_Exist1;
    Items_Info cur_Exist2;

    [Header("reinforce Stat")]
    public TextMeshProUGUI Hp_B;
    public TextMeshProUGUI Atk_B;
    public TextMeshProUGUI Def_B;
    public TextMeshProUGUI Cri_B;
    public TextMeshProUGUI Hp_A;
    public TextMeshProUGUI Atk_A;
    public TextMeshProUGUI Def_A;
    public TextMeshProUGUI Cri_A;



    private void Start()
    {
        ActivateRein();
    }

    public void OpenSmith()
    {
        gameObject.SetActive(true);
    }

    public void ActivateRein()
    {
        ReinforceFrame.SetActive(true);
        CombineFrame.SetActive(false);
    }

    public void ActivateComb()
    {
        ReinforceFrame.SetActive(false);
        CombineFrame.SetActive(true);
        unloadReinforce();
    }

    public void reinforceLoad()
    {
        if(cur_Exist == null)
        {
            cur_Exist = Manager.instance.manager_Inven.selectedItem.GetComponent<Items_Info>();
        }
        if (slot_Item.childCount == 4)
        {
            cur_Exist.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(slot_Item.GetChild(3).gameObject);
        }

        GameObject item_Slot = Instantiate(cur_Exist.gameObject, slot_Item);

        item_Slot.GetComponent<items_action>().enabled = false;
        Hp_B.text = cur_Exist.hpBonus.ToString();
        Atk_B.text = cur_Exist.atkBonus.ToString();
        Def_B.text = cur_Exist.defBonus.ToString();
        Cri_B.text = cur_Exist.criBonus.ToString();

        Hp_A.text = ((int)(cur_Exist.hpBonus * 1.2)).ToString();
        Atk_A.text = ((int)(cur_Exist.atkBonus * 1.2)).ToString();
        Def_A.text = ((int)(cur_Exist.defBonus * 1.2)).ToString();
        Cri_A.text = (cur_Exist.criBonus + 1).ToString();

        nextLevel.text = string.Format("Next Level: {0}", cur_Exist.reinforce + 1);
        cost.text = string.Format("{0} G", (cur_Exist.reinforce + 1) * 1000);
        percent.text = string.Format("{0}%",(100 - cur_Exist.reinforce * 10));


        Manager.instance.manager_Inven.itemInfoFrame.SetActive(false);
        Manager.instance.manager_Inven.itemInfoFrame.SetActive(true);
    }

    public void unloadReinforce()
    {
        if (slot_Item.childCount == 4)
        {
            cur_Exist.transform.GetChild(0).gameObject.SetActive(false);
            cur_Exist = null;
            Destroy(slot_Item.GetChild(3).gameObject);
        }

        Hp_B.text = "-";
        Atk_B.text = "-";
        Def_B.text = "-";
        Cri_B.text = "-";

        Hp_A.text = "-";
        Atk_A.text ="-";
        Def_A.text ="-";
        Cri_A.text = "-";

        nextLevel.text = "Next Level: -";
        cost.text = "- G";
        percent.text = "-%";

    }

    public void startReinfoce()
    {
        if (cur_Exist != null)
        {
            cur_Exist.hpBonus = int.Parse(Hp_A.text);
            cur_Exist.atkBonus = int.Parse(Atk_A.text);
            cur_Exist.defBonus = int.Parse(Def_A.text);
            cur_Exist.criBonus = float.Parse(Cri_A.text);

            cur_Exist.reinforce += 1;
            reinforceLoad();
        }
        else
        {
            Manager.instance.manager_Popup.notice.text = "Load Item First";
            Manager.instance.manager_Popup.notice.gameObject.SetActive(true);
        }
    }

    public void combineLoad()
    {
        Items_Info item = Manager.instance.manager_Inven.selectedItem.GetComponent<Items_Info>();


        if (slot_Item1.childCount == 3)
        {
            item_Slot1 = Instantiate(item.gameObject, slot_Item1);
            item_Slot1.GetComponent<items_action>().enabled = false;
            cur_Exist1 = item;
        }
        else if (slot_Item2.childCount == 3)
        {
            item_Slot2 = Instantiate(item.gameObject, slot_Item2);
            item_Slot2.GetComponent<items_action>().enabled = false;
            cur_Exist2 = item;
        }
        else if (slot_Item1.childCount == 4 && slot_Item2.childCount == 4)
        {
            // do nothing
        }


        Manager.instance.manager_Inven.itemInfoFrame.SetActive(false);
        Manager.instance.manager_Inven.itemInfoFrame.SetActive(true);
    }
    public void LoadBtn()
    {
        reinforceLoad();
    }



}
