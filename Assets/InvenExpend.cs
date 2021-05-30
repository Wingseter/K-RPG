using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenExpend : ItemUse
{
    public new void useItem()
    {
        base.useItem();
        Manager.instance.manager_Inven.invenSize += 6;
        Manager.instance.manager_Inven.bagFrame.SetActive(false);
        Manager.instance.manager_Inven.bagFrame.SetActive(true);
    }

}
