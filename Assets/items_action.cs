using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class items_action : MonoBehaviour, IPointerUpHandler,
    IPointerDownHandler, IDragHandler
{
    public Image img;

    [Header("Location")]
    public bool inBag;
    public bool inStore;

    float releaseTime;
    bool dragging;


    public void OnPointerDown(PointerEventData eventData)
    {
        img = GetComponent<Image>();
        Manager.instance.manager_Inven.selectedItem = transform;

        if (inBag)
            StartCoroutine("ReleaseTime");

        if(inStore)
        {
            Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);
            // 아이템 설명
            Manager.instance.manager_Inven.itemInfoFrame.SetActive(false);
            Manager.instance.manager_Inven.itemInfoFrame.GetComponent<itemInfoFrame>().item = GetComponent<Items_Info>();
            Manager.instance.manager_Inven.itemInfoFrame.SetActive(true);

        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (inBag)
        {
            StopCoroutine("ReleaseTime");
            transform.localScale = new Vector3(1, 1, 1);

            if(releaseTime >= 0.2f)
            {
                transform.SetParent(Manager.instance.manager_Inven.curParent);
                transform.localPosition = Vector3.zero;
                img.raycastTarget = true;
                return;
            }

            if(releaseTime < 0.2f)
            {
                Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnB);
                // 아이템 설명
                Manager.instance.manager_Inven.itemInfoFrame.SetActive(false);
                Manager.instance.manager_Inven.itemInfoFrame.GetComponent<itemInfoFrame>().item= GetComponent<Items_Info>();
                Manager.instance.manager_Inven.itemInfoFrame.SetActive(true);

                Manager.instance.manager_Inven.rect.position = transform.position;
                Manager.instance.manager_Inven.rect.gameObject.SetActive(true);
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (inBag && dragging)
            transform.position = eventData.position;
    }

    IEnumerator ReleaseTime()
    {
        releaseTime = 0;
        dragging = false;

        while(true)
        {
            releaseTime += Time.deltaTime;

            if(releaseTime >= 0.2f)
            {
                transform.localScale = new Vector3(1.3f, 1.3f, 1);

                if(!dragging)
                {
                    Manager.instance.manager_Inven.itemInfoFrame.SetActive(false);

                    Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.drag);

                    dragging = true;
                    Manager.instance.manager_Inven.curParent = transform.parent;
                    transform.SetParent(Manager.instance.manager_Inven.parentOnDrag);
                    img.raycastTarget = false;

                    Manager.instance.manager_Inven.rect.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }
}
