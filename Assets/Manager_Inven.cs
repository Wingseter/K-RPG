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
