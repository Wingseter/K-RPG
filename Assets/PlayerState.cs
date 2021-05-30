using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("State")]
    public int lev;     // 래밸

    public float hp;    // 체력
    public float hp_Cur;    // 현재 체력

    public int atk;     // 공격력
    public int def;     // 방어력
    public float cri;   // 크리티컬

    public float exp_Max;   // 맥스 경험치
    public float exp_Cur;   // 현재 경험치

    public float mana;
    public float mana_Cur;

    public float spantDia;
    public float DiaLevel;

    [Header("Frame")]
    public GameObject LevelUpPopup;
    public TextMeshProUGUI Level;

    [Header("Item")]
    public float exp_Multiply;    


    private void OnEnable()
    {
        exp_Multiply = 1.0f;
    }

    public void LevelUp()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.LevelUp);
        lev = lev + 1;
        exp_Max = exp_Max + 100;
        exp_Cur = 0;
        Level.text = lev.ToString();
        LevelUpPopup.SetActive(true);
    }
}
