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

    public bool inCombat;
    bool inAtk;

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
        StartCoroutine("EnemyAI");
    }

    void ResetAI()
    {
        nav.enabled = true;
        inCombat = false;
        onMove = false;
        inAtk = false;
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
                inAtk = true;
                enemyAni.Play("Enemy_Idle");
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
        if (targetDis <= 3)
            onMove = false;

        if(!onMove)
        {
            onMove = true;
            nav.speed = speed_NonCombat;

            target = new Vector3(transform.position.x + Random.Range(-1 * moveRange, moveRange), 0,
                transform.position.z +
                Random.Range(-1 * moveRange, moveRange));

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
        if(targetDis <= 3)
        {
            transform.LookAt(target);
            enemyAni.Play("Enemy_Atk");
        }
        if(targetDis > 3)
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

            if(!inCombat)
                Move_NonCombat();

            if (inCombat)
                Move_Combat();

            yield return null;
        }
    }
}
