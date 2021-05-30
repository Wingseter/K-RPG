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
    public Manager_Obj manager_Obj;
    public Manager_Monster manager_Mon;
    public Manager_Dialog manager_Dialog;
    public Manager_Shop manager_Shop;
    public Manager_Popup manager_Popup;
    public Buff manager_Buff;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
}
