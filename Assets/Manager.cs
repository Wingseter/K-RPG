using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [Header("Manager")]
    public Manager_SE manager_SE;
    public Manager_Inven manager_Inven;
    public PlayerController playerController;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
}
