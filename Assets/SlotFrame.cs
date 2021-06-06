using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFrame : MonoBehaviour
{
    public int SlotNum;

    public GameObject Activate;
    public GameObject Close;

    public void SetActive()
    {
        Activate.SetActive(true);
        Close.SetActive(false);
    }

    public void SetDisable()
    {
        Activate.SetActive(false);
        Close.SetActive(true);
    }

}
