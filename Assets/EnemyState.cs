using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float hp;
    public float curHp;
    public int expGet;
    public int Atk;

    private void OnEnable()
    {
        curHp = hp;
    }
}
