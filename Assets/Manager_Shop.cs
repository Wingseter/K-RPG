using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Shop : MonoBehaviour
{
    public PlayerState player;
    public GameObject shopFrame;

    [Header("Stat")]
    public Slider health;
    public Slider mana;
    public Slider exp;
    public TextMeshProUGUI Lv;

    [Header("Bank")]
    public TextMeshProUGUI goldAmount;
    public TextMeshProUGUI DiaAmount;

    private void Start()
    {
        UpdateAllStat();
        UpdateBank();
    }

    public void UpdateHealth()
    {
        health.value = player.hp_Cur / player.hp;
    }

    public void UpdateMana()
    {
        mana.value = player.mana_Cur / player.mana;
    }

    public void UpdateExp()
    {
        exp.value = player.exp_Cur / player.exp_Max;

    }

    public void UpdateLevel()
    {
        Lv.text = player.lev.ToString();
    }

    public void UpdateAllStat()
    {
        UpdateHealth();
        UpdateMana();
        UpdateExp();
        UpdateExp();
    }
    public void UpdateGold()
    {
        goldAmount.text = Manager.instance.manager_Inven.gold.ToString();
    }

    public void UpdateDia()
    {
        DiaAmount.text = Manager.instance.manager_Inven.dia.ToString();
    }

    public void UpdateBank()
    {
        UpdateDia();
        UpdateGold();
    }
}

