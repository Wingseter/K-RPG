using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Drop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Manager_Inven inven = Manager.instance.manager_Inven;
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.drag);

        if (transform.childCount != 0)
        {
            Transform item = transform.GetChild(0);
            item.SetParent(inven.curParent);
            item.localPosition = Vector3.zero;
        }
        inven.selectedItem.SetParent(transform);
        inven.selectedItem.localPosition = Vector3.zero;
    }
}
