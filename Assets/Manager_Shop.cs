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
    public TextMeshProUGUI expBoost;
    public TextMeshProUGUI Lv;

    [Header("Bank")]
    public TextMeshProUGUI goldAmount;
    public TextMeshProUGUI DiaAmount;

    private void Start()
    {
        StartCoroutine("update");
    }
    private void OnDestroy()
    {
        StopCoroutine("update");
    }

    IEnumerator update()
    {
        while (true)
        {
            UpdateAllStat();
            UpdateBank();
            Lv.text = player.lev.ToString();
            expBoost.text = string.Format("x {0}", player.exp_Multiply);
            yield return null;
        }
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

