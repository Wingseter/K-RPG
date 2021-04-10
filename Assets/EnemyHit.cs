using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
   public void Hit(int dmg)
    {
        EnemyState enemState = GetComponent<EnemyState>();
        enemState.curHp -= dmg;

        if(enemState.curHp <= 0)
        {
            if(Manager.instance.playerController.target == transform)
            {
                Manager.instance.playerController.target = null;
                Manager.instance.playerController.target_Tool.SetActive(false);
            }

            GetComponent<Animator>().Play("Die");
            gameObject.tag = "Dead";
        }
    }
}
