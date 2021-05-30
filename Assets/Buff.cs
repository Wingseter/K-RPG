using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [Header("Player")]
    public PlayerState player;

    [Header("Character Info")]
    public Transform[] slot_Buff;
    public BuffInfo[] cur_Buff;

    [Header("Frame")]
    public GameObject BuffList;

    public void openBuffList()
    {
        BuffList.SetActive(true);
    }

    public void closeBuffList()
    {
        BuffList.SetActive(false);

    }

    public void AddBuff(BuffInfo newBuff, int num)
    {
        DeleteBuff(num);
        Transform slot = slot_Buff[num];
        
        if(num == -1)
        {
            return;
        }
        else
        {
            Instantiate(newBuff.gameObject, slot);
            cur_Buff[num] = newBuff;
            IncreaseStats(newBuff);
        }

    }

    public void DeleteBuff(int i)
    {
        if (slot_Buff[i].childCount == 1)
        {
            Destroy(slot_Buff[i].GetChild(0).gameObject);
            ReduceStats(cur_Buff[i]);
            cur_Buff[i] = null;
        }
    }

    void IncreaseStats(BuffInfo buff)
    {
        switch (buff.type)
        {
            case "Hp":
                player.hp += (int)(player.hp * buff.hpPercent);
                break;
            case "Def":
                player.def += (int)(player.def * buff.defPercent);
                break;
            case "Atk":
                player.atk += (int)(player.atk * buff.atkPercent);
                break;
            case "Cri":
                player.cri += buff.criBonus;
                break;
            case "VIPLV1":
                player.hp_Cur += 50;
                player.atk += 50;
                player.def += 50;
                player.cri += 3;
                break;
            case "VIPLV2":
                player.hp_Cur += 100;
                player.atk += 100;
                player.def += 100;
                player.cri += 6;
                break;
            case "VIPLV3":
                player.hp_Cur += 150;
                player.atk += 150;
                player.def += 150;
                player.cri += 10;
                break;
        }

    }

    void ReduceStats(BuffInfo buff)
    {
        switch (buff.type)
        {
            case "Hp":
                player.hp -= (int)(player.hp * buff.hpPercent);
                break;
            case "Def":
                player.def -= (int)(player.def * buff.defPercent);
                break;
            case "Atk":
                player.atk -= (int)(player.atk * buff.atkPercent);
                break;
            case "Cri":
                player.cri -= buff.criBonus;
                break;
            case "VIPLV1":
                player.hp_Cur -= 50;
                player.atk -= 50;
                player.def -= 50;
                player.cri -= 3;
                break;
            case "VIPLV2":
                player.hp_Cur -= 100;
                player.atk -= 100;
                player.def -= 100;
                player.cri -= 6;
                break;
            case "VIPLV3":
                player.hp_Cur -= 150;
                player.atk -= 150;
                player.def -= 150;
                player.cri -= 10;
                break;
        }
    }
}
