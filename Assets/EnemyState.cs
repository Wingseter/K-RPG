﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float hp;
    public float curHp;

    private void OnEnable()
    {
        curHp = hp;
    }
}
