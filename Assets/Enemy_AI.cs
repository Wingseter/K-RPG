using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public float speed_NonCombat;
    public int moveRange;

    NavMeshAgent nav;
    Animator enemyAni;

    bool onMove;
    Vector3 originPosition;
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
    
    IEnumerator EnemyAI()
    {
        enemyAni.Play("Enemy_Move");

        while(true)
        {
            originDis = (originPosition - transform.position).magnitude;
            targetDis = (target - transform.position).magnitude;

            Move_NonCombat();

            yield return null;
        }
    }
}
