using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_Dialog : MonoBehaviour
{
    public GameObject dialog_Frame;
    public TextMeshProUGUI npc_Name;
    public TextMeshProUGUI npc_Dialog;

    public void OpenDialog(string npc, string dialog)
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnA);
        npc_Name.text = npc;
        npc_Dialog.text = dialog;
        dialog_Frame.SetActive(true);
    }
}
