using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float vanishTime;
    public float respawnTime;
    public PlayerState player;

    public void Hit(int dmg)
    {
        Enemy_AI ai = GetComponent<Enemy_AI>();
        ai.inCombat = true;

        GetComponent<Enemy_AI>().inCombat = true;
        EnemyState enemState = GetComponent<EnemyState>();
        enemState.curHp -= dmg;

        // 몬스터 사망시
        if (enemState.curHp <= 0)
        {
            if (Manager.instance.playerController.target == transform)
            {
                Manager.instance.playerController.target = null;
                Manager.instance.playerController.target_Tool.SetActive(false);
            }

            Manager.instance.playerController.target_Boss.SetActive(false);
            GetComponent<Animator>().Play("Die");
            gameObject.tag = "Dead";

            ai.StopAllCoroutines();
            ai.nav.enabled = false;

            player.exp_Cur += enemState.expGet * player.exp_Multiply;

            if(player.exp_Max < player.exp_Cur)
            {
                player.LevelUp();
            }
            Manager.instance.manager_Mon.Respawn(gameObject, ai.originPosition, vanishTime, respawnTime);
        }
    }
}
