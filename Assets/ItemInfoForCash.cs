using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoForCash : MonoBehaviour
{
    public Items_Info item;
    public TextMeshProUGUI name_Item;
    public TextMeshProUGUI info_Item;

    private void OnEnable()
    {
        name_Item.text = item.name_Item;
        info_Item.text = item.info_Item;
    }
}
