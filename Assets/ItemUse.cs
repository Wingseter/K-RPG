using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public void useItem()
    {
        Destroy(gameObject);
    }
}
