using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IPointerDownHandler
{
    [Header("Camera")]
    public Camera cam;
    public GameObject moveEffect;
    public Vector3 offset_Cam;
    RaycastHit hit;

    [Header("Player")]
    public Transform player;
    public float moveSpeed;
    public float rotateSpeed;
    public float range;
    NavMeshAgent playerNav;
    Animator playerAni;

    [Header("Targeting")]
    public Transform target;
    public GameObject target_Tool;
    public TextMeshProUGUI name_Target;
    public GameObject hpBar_Target;
    bool clickNpc;

    [Header("Boss")]
    public GameObject target_Boss;
    public TextMeshProUGUI name_Boss;
    public Slider hpBar_Boss;

    [Header("Casting")]
    public bool onCasting;
    public Image castingBar;
    float castingTime;
    string skillName;
    public Transform atkTarget;
    GameObject skillObj;
    float targetDis;

    private void Start()
    {
        playerNav = player.GetComponent<NavMeshAgent>();
        playerNav.speed = moveSpeed;
        playerNav.angularSpeed = rotateSpeed;
        playerAni = player.GetComponent<Animator>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = cam.ScreenPointToRay(eventData.position);
        Physics.Raycast(ray, out hit);

        if (hit.transform != null)
        {
            // 클릭 이펙트를 꺼주고
            moveEffect.SetActive(false);
            // 클릭 이펙트를 이동
            moveEffect.transform.position = cam.WorldToScreenPoint(hit.point);
            // 클릭 이펙트 활성화
            moveEffect.SetActive(true);

            if (hit.transform.gameObject.tag == "Land")
            {
                if (!onCasting)
                    playerNav.SetDestination(hit.point);
            }
            if (hit.transform.gameObject.tag == "Player")
            {
                // Nothing

            }

            if (hit.transform.gameObject.tag == "Enemy")
            {
                Targeting();
            }

            if (hit.transform.gameObject.tag == "Boss")
            {
                BossTargeting();
            }

            if (hit.transform.gameObject.tag == "NPC")
            {
                Targeting();
                if (!onCasting)
                    playerNav.SetDestination(hit.point);

                clickNpc = true;
            }
            if (hit.transform.gameObject.tag == "Merchant")
            {
                Targeting();
                if (!onCasting)
                    playerNav.SetDestination(hit.point);

                clickNpc = true;
            }
            if (hit.transform.gameObject.tag == "Smith")
            {
                Targeting();
                if (!onCasting)
                    playerNav.SetDestination(hit.point);

                clickNpc = true;
            }
        }
    }

    public void Casting(float time, string name, GameObject obj, float spentMana)
    {
        castingTime = time;
        skillName = name;
        atkTarget = target;
        skillObj = obj;
        player.gameObject.GetComponent<PlayerState>().mana_Cur -= spentMana;
        StartCoroutine("OnCasting");
    }

    IEnumerator OnCasting()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.casting);

        onCasting = true;
        float time = 0;
        player.LookAt(atkTarget);
        playerAni.Play("Player_Casting");
        playerNav.enabled = false;
        castingBar.transform.parent.gameObject.SetActive(true);
        castingBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skillName;

        while(true)
        {
            time += Time.deltaTime;
            castingBar.fillAmount = time / castingTime;

            if(time>=castingTime)
            {
                Manager.instance.manager_SE.seAudios.Stop();

                StopCoroutine("OnCasting");
                castingBar.transform.parent.gameObject.SetActive(false);
                playerAni.Play("Player_Shot");
                playerNav.enabled = true;
                onCasting = false;

                skillObj.transform.position = player.position + new Vector3(0, 2, 0); 
                skillObj.SetActive(true);

            }

            yield return null;
        }
    }

    public void Die()
    {
        playerAni.Play("Die");
        StartCoroutine("Respawn");
    }
    IEnumerator Respawn()
    {
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.die);

        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            if (time >= 3)
            {
                RespawnAct();
                StopCoroutine("Respawn");

            }
            yield return null;
        }
    }
    public void RespawnAct()
    {
        player.position = new Vector3(2, 0, 0);
        player.rotation = Quaternion.Euler(0, 180, 0);
        player.gameObject.GetComponent<PlayerState>().mana_Cur = player.gameObject.GetComponent<PlayerState>().mana;
        player.gameObject.GetComponent<PlayerState>().hp_Cur = player.gameObject.GetComponent<PlayerState>().hp;
    }

    void Targeting()
    {
        target = hit.transform;
        name_Target.text = target.GetComponent<Obj_Info>().obj_Name;
        hpBar_Target.SetActive(target.GetComponent<Obj_Info>().type == "Enemy");
        target_Tool.SetActive(true);

    }

    void BossTargeting()
    {
        target = hit.transform;
        name_Boss.text = target.GetComponent<Obj_Info>().obj_Name;
        target_Boss.SetActive(true);

    }

    void OnTarget()
    {
        
        if (target != null)
        {
            targetDis = (target.position - player.position).magnitude;

            if (targetDis > range)
            {
                target = null;
                target_Tool.SetActive(false);
            }

            if (target_Tool.activeSelf)
                target_Tool.transform.position = cam.WorldToScreenPoint(target.position + new Vector3(0,2,0));
        }
    }

    private void Update()
    {
        // 움직일때 걷는 애니메이션
        playerAni.SetBool("Walk", playerNav.velocity != Vector3.zero);

        OnTarget();

        if(target != null && target.gameObject.tag == "Enemy")
        {
            EnemyState enemyState = target.GetComponent<EnemyState>();
            hpBar_Target.transform.GetChild(0).GetComponent<Image>().fillAmount = enemyState.curHp / enemyState.hp ;
        }

        if (target != null && target.gameObject.tag == "Boss")
        {
            EnemyState enemyState = target.GetComponent<EnemyState>();
            hpBar_Boss.value = enemyState.curHp / enemyState.hp;
        }


        if (target != null && target.gameObject.tag == "NPC" && targetDis < 2.5f && clickNpc)
        {
            clickNpc = false;
            target.GetComponent<NPC_Dialog>().Dialog();
        }
        if (target != null && target.gameObject.tag == "Merchant" && targetDis < 2.5f && clickNpc)
        {
            clickNpc = false;
            Manager.instance.manager_Inven.storeFrame.GetComponent<ItemStoreFrame>().OpenStore();

            Manager.instance.manager_Inven.OpenBag();
        }
        if (target != null && target.gameObject.tag == "Smith" && targetDis < 2.5f && clickNpc)
        {
            clickNpc = false;
            Manager.instance.manager_Inven.smithFrame.GetComponent<SmithFrame>().OpenSmith();
            Manager.instance.manager_Inven.OpenBag();
        }


        cam.transform.position = player.position + offset_Cam;
    }
}
