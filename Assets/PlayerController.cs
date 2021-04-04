using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IPointerDownHandler
{
    [Header("Camera")]
    public Camera cam;
    public GameObject moveEffect;
    RaycastHit hit;

    [Header("Player")]
    public Transform player;
    public float moveSpeed;
    public float rotateSpeed;
    NavMeshAgent playerNav;
    Animator playerAnimator;

    private void Start()
    {
        playerNav = player.GetComponent<NavMeshAgent>();
        playerNav.speed = moveSpeed;
        playerNav.angularSpeed = rotateSpeed;
        playerAnimator = player.GetComponent<Animator>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = cam.ScreenPointToRay(eventData.position);
        Physics.Raycast(ray, out hit);

        if(hit.transform != null)
        {
            // 클릭 이펙트를 꺼주고
            moveEffect.SetActive(false);
            // 클릭 이펙트를 이동
            moveEffect.transform.position = cam.WorldToScreenPoint(hit.point);
            // 클릭 이펙트 활성화
            moveEffect.SetActive(true);

            if (hit.transform.gameObject.tag == "Land")
                playerNav.SetDestination(hit.point);
            if (hit.transform.gameObject.tag == "Player")
            {
                Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.btnA);
                Manager.instance.manager_Inven.charInfoFrame.SetActive(true);
            }
        }
    }

    private void Update()
    {
        // 움직일때 걷는 애니메이션
        playerAnimator.SetBool("Walk", playerNav.velocity != Vector3.zero);
    }
}
