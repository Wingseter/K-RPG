using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public float speed_NonCombat;
    public float speed_Combat;

    public int moveRange;
    public int trackingRange;
    public int AtkSpeed;

    public bool inCombat;
    public bool isThisBoss;
    bool inAtk;
    public PlayerState player;

    public float timeSpan;  //경과 시간을 갖는 변수
    public float checkTime;  // 특정 시간을 갖는 변수

    public NavMeshAgent nav;
    Animator enemyAni;

    bool onMove;
    public Vector3 originPosition;
    float originDis;
    Vector3 target;
    float targetDis;


    private void OnEnable()
    {
        originPosition = transform.position;
        nav = GetComponent<NavMeshAgent>();
        enemyAni = GetComponent<Animator>();
        ResetAI();
    }
  

    void ResetAI()
    {
        nav.enabled = true;
        inCombat = false;
        onMove = false;
        inAtk = false;
        if (isThisBoss == true)
            gameObject.tag = "Boss";
        else
            gameObject.tag = "Enemy";
        StartCoroutine("EnemyAI");
    }

    void Move_Combat()
    {
        target = Manager.instance.playerController.player.position;

        if(!inAtk)
        {
            nav.speed = speed_Combat;
            nav.SetDestination(target);

            if(targetDis <= 3)
            {
                Enemy_Atk();
            }
        }

        if(originDis >= trackingRange)
        {
            inCombat = false;
            onMove = true;
            target = originPosition;
            nav.speed = 8;
            nav.SetDestination(target);

            GetComponent<EnemyState>().curHp = GetComponent<EnemyState>().hp;
        }
    }

    void Move_NonCombat()
    {
        enemyAni.Play("Enemy_Move");

        if (targetDis <= 3 || timeSpan > checkTime)  // 경과 시간이 특정 시간이 보다 커졋을 경우
        {
            onMove = false;
            timeSpan = 0;
        }


        if (!onMove)
        {
            onMove = true;
            nav.speed = speed_NonCombat;

            target = new Vector3(transform.position.x + Random.Range(-1 * moveRange, moveRange), transform.position.y,
                transform.position.z + Random.Range(-1 * moveRange, moveRange));
            nav.SetDestination(target);
        }

        if(originDis >= moveRange)
        {
            onMove = true;
            target = originPosition;
            nav.SetDestination(target);
        }
    }

    public void Enemy_Atk()
    {
        if(targetDis <= 3 && timeSpan > AtkSpeed)
        {
            transform.LookAt(target);
            enemyAni.Play("Enemy_Atk");
            player.hp_Cur -= GetComponent<EnemyState>().Atk;
            if(player.hp_Cur < 0)
            {
                Manager.instance.playerController.Die();
            }
            if (isThisBoss == true)
                Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.enemyAtk2);
            else
                Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.enemyAtk1);
            timeSpan = 0;
        }
        if(targetDis > 3 )
        {
            inAtk = false;
            enemyAni.Play("Enemy_Move");
        }
    }
    
    IEnumerator EnemyAI()
    {
        enemyAni.Play("Enemy_Move");

        while(true)
        {
            originDis = (originPosition - transform.position).magnitude;
            targetDis = (target - transform.position).magnitude;

            timeSpan += Time.deltaTime;  // 경과 시간을 계속 등록

            if (!inCombat)
                Move_NonCombat();

            if (inCombat)
                Move_Combat();

            yield return null;
        }
    }
}
