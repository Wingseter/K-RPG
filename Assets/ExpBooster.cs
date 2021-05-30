using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBooster : ItemUse
{
    public PlayerState player;

    public new void useItem()
    {
        base.useItem();
        player.exp_Multiply = 2.0f;
    }
}
