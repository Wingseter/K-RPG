using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Manager.instance.manager_Inven.slot_BagFrame.Length; i++)
        {
            if(i < Manager.instance.manager_Inven.invenSize)
            {
                Manager.instance.manager_Inven.slot_BagFrame[i].gameObject.SetActive(true);
            }
            else
            {
                Manager.instance.manager_Inven.slot_BagFrame[i].gameObject.SetActive(false);
            }
        }
    }


}
