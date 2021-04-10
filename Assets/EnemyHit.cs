using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float vanishTime;
    public float respawnTime;
    public void Hit(int dmg)
    {
        Enemy_AI ai = GetComponent<Enemy_AI>();
        ai.inCombat = true;

        GetComponent<Enemy_AI>().inCombat = true;
        EnemyState enemState = GetComponent<EnemyState>();
        enemState.curHp -= dmg;

        if (enemState.curHp <= 0)
        {
            if (Manager.instance.playerController.target == transform)
            {
                Manager.instance.playerController.target = null;
                Manager.instance.playerController.target_Tool.SetActive(false);
            }

            GetComponent<Animator>().Play("Die");
            gameObject.tag = "Dead";

            ai.StopAllCoroutines();
            ai.nav.enabled = false;

            Manager.instance.manager_Mon.Respawn(gameObject, ai.originPosition, vanishTime, respawnTime);
        }
    }
}
