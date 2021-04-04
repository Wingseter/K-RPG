using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int lev;     // 래밸

    public float hp;    // 체력
    public float hp_Cur;    // 현재 체력

    public int atk;     // 공격력
    public int def;     // 방어력
    public float cri;   // 크리티컬

    public float exp_Max;   // 맥스 경험치
    public float exp_Cur;   // 현재 경험치

    private void OnEnable()
    {
        hp_Cur = hp; 
    }
}
