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

    float releaseTime;
    bool dragging;


    public void OnPointerDown(PointerEventData eventData)
    {
        img = GetComponent<Image>();
        Manager.instance.manager_Inven.selectedItem = transform;

        if (inBag)
            StartCoroutine("ReleaseTime");

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (inBag)
        {
            StopCoroutine("ReleaseTime");
            transform.localScale = new Vector3(1, 1, 1);

            if(releaseTime >= 0.1f)
            {
                transform.SetParent(Manager.instance.manager_Inven.curParent);
                transform.localPosition = Vector3.zero;
                img.raycastTarget = true;
                return;
            }

            if(releaseTime < 0.1f)
            {
                // 클릭 이벤트

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

            if(releaseTime >= 0.1f)
            {
                transform.localScale = new Vector3(1.3f, 1.3f, 1);

                if(!dragging)
                {
                    Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.drag);

                    dragging = true;
                    Manager.instance.manager_Inven.curParent = transform.parent;
                    transform.SetParent(Manager.instance.manager_Inven.parentOnDrag);
                    img.raycastTarget = false;
                }
            }
            yield return null;
        }
    }
}
