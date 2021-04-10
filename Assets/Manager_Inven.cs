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
    public GameObject storeFrame;

    [Header("Bag")]
    public int gold;
    public TextMeshProUGUI goldAmount;
    public Transform rect;


    [Header("Drag&Drop")]
    public Transform selectedItem;
    public Transform curParent;
    public Transform parentOnDrag;
    public void OpenBag()
    {
        goldAmount.text = gold.ToString();
        bagFrame.SetActive(true);
    }
}
