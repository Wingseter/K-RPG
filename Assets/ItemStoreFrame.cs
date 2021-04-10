using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemStoreFrame : MonoBehaviour
{
    public Transform[] slot_saleItems;

    public void OpenStore()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);

        GameObject[] saleItems_Npc = Manager.instance.playerController.target.GetComponent<NPC_Store>().saleItems;

        for(int i = 0; i < slot_saleItems.Length; i++)
        {
            slot_saleItems[i].gameObject.SetActive(false);

            if (slot_saleItems[i].childCount == 2)
                Destroy(slot_saleItems[i].GetChild(1).gameObject);
        }

        for(int i = 0; i< saleItems_Npc.Length; i++)
        {
            if(saleItems_Npc[i] != null)
            {
                GameObject obj = Instantiate(saleItems_Npc[i], slot_saleItems[i]);
                obj.GetComponent<items_action>().inStore = true;
                obj.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0}G", obj.GetComponent<Items_Info>().price);
                obj.transform.parent.gameObject.SetActive(true);
                    
            }
        }
        gameObject.SetActive(true);
    }
}
