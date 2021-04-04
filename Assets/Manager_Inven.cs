using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_Inven : MonoBehaviour
{
    [Header("Frame")]
    public GameObject charInfoFrame;
    public GameObject bagFrame;

    [Header("Bag")]
    public int gold;
    public TextMeshProUGUI goldAmount;

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
