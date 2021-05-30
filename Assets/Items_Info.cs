using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Info : MonoBehaviour
{
    [Header("Common Info")]
    public int itemId;
    public string type;
    public string name_Item;
    public string info_Item;
    public int price;
    public int diaPrice;
    public int resalsePrice;
    public int reinforce;

    [Header("Equipment Info")]
    public int hpBonus;
    public int atkBonus;
    public int defBonus;
    public float criBonus;

    public bool equipped;
    public int equipNum;
    public ItemUse effect;
}
