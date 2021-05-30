using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gacha_Store : MonoBehaviour
{
    [Header("Frame")]
    public GameObject GachaPopup;
    public GameObject ItemFrame;
    public GameObject BuffFrame;
    public Transform ItemSlot;
    public Transform[] Buff_Slot;

    [Header("word")]
    public TextMeshProUGUI gachaName;
    public TextMeshProUGUI ItemName;


    [Header("ItemList")]
    public Items_Info[] WeaponList;
    public int[] WeaponPercent;
    public Items_Info[] AccessList;
    public int[] AccessPercent;
    public BuffInfo[] BuffList;
    public int[] BuffPercent;


    private void Start()
    {
        closeGatchaPopUp();
    }

    private int randomGenerator()
    {
        return Random.Range(0, 10000);
    }

    public void closeGatchaPopUp()
    {
        GachaPopup.SetActive(false);
    } 

    public void rollWeapon()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.Gacha);
        if (ItemSlot.childCount == 4)
            Destroy(ItemSlot.GetChild(3).gameObject);

        int rand = randomGenerator();
        int select = 0;


        for(int i = 0; i < WeaponList.Length; i++)
        {
            if(rand <= WeaponPercent[i])
            {
                select = i;
                break;
            }
        }

        Instantiate(WeaponList[select], ItemSlot);
        
        GameObject obj = Instantiate(WeaponList[select].gameObject, Manager.instance.manager_Inven.Slot());
        obj.GetComponent<items_action>().inBag = true;

        gachaName.text = "Weapon Gacha";
        ItemName.text = WeaponList[select].name;

        ItemFrame.SetActive(true);
        BuffFrame.SetActive(false);
        GachaPopup.SetActive(true);
    }

    public void rollAccess()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.Gacha);

        if (ItemSlot.childCount == 4)
            Destroy(ItemSlot.GetChild(3).gameObject);
        int rand = randomGenerator();
        int select = AccessList.Length - 1;


        for (int i = 0; i < AccessList.Length; i++)
        {
            if (rand <= AccessPercent[i])
            {
                select = i;
                break;
            }
        }
        ItemFrame.SetActive(true);
        BuffFrame.SetActive(false);

        Instantiate(AccessList[select].gameObject, ItemSlot);
        GameObject obj  = Instantiate(AccessList[select].gameObject, Manager.instance.manager_Inven.Slot());
        obj.GetComponent<items_action>().inBag = true;

        gachaName.text = "Accesory Gacha";
        ItemName.text = AccessList[select].name;
        GachaPopup.SetActive(true);
    }

    public void rollBuff()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.Gacha);

        int rand;
        int select;

        for(int i = 0; i < 3; i++)
        {
            rand = randomGenerator();
            select = 0;

            if (Buff_Slot[i].childCount == 1)
                Destroy(Buff_Slot[i].GetChild(0).gameObject);

            for (int j = 0; j < BuffList.Length; j++)
            {
                if (rand <= BuffPercent[j])
                {
                    select = j;
                    break;
                }
            }

            Instantiate(BuffList[select], Buff_Slot[i]);
            Manager.instance.manager_Buff.AddBuff(BuffList[select], i);
        }
        gachaName.text = "Buff Gacha";

        ItemFrame.SetActive(false);
        BuffFrame.SetActive(true);
        GachaPopup.SetActive(true);
    }
}
