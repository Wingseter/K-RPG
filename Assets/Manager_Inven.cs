using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_Inven : MonoBehaviour
{
    [Header("Frame")]
    public GameObject charInfoFrame;
    public GameObject bagFrame;
    public GameObject itemInfoFrame;
    public GameObject itemInfoCashFrame;
    public GameObject storeFrame;
    public GameObject smithFrame;
    public GameObject reinforceFrame;
    public GameObject combineFrame;

    [Header("Bag")]
    public int gold;
    public int dia;
    public TextMeshProUGUI goldAmount;
    public Transform rect;
    public int invenSize;
    public Transform[] slot_BagFrame;


    [Header("Checker")]
    public int action;

    [Header("Drag&Drop")]
    public Transform selectedItem;
    public Transform curParent;
    public Transform parentOnDrag;

    private void Start()
    {
        invenSize = 12;
    }

    public void OpenBag()
    {
        goldAmount.text = gold.ToString();
        bagFrame.SetActive(true);
    }

    public void UseItem()
    {
        string name = selectedItem.GetComponent<Items_Info>().name_Item;
        switch (name)
        {
            case "ExpBooster":
                Manager.instance.playerController.player.GetComponent<PlayerState>().exp_Multiply = 2.0f;
                break;
            case "InvenExpender":
                Manager.instance.manager_Inven.invenSize += 6;
                Manager.instance.manager_Inven.bagFrame.SetActive(false);
                Manager.instance.manager_Inven.bagFrame.SetActive(true);
                break;
            case "BeginnerSet":
                gold += 100000;
                break;
            case "":
                break;
        }
        Manager.instance.manager_Inven.itemInfoFrame.SetActive(false);

        Destroy(selectedItem.gameObject);
    }

    public Transform Slot()
    {

        for (int i = 0; i < slot_BagFrame.Length; i++)
        {
            if (slot_BagFrame[i].gameObject.activeSelf && slot_BagFrame[i].childCount == 0)
                return slot_BagFrame[i];
        }

        return null;
    }
}
