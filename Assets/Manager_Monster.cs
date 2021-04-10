using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Monster : MonoBehaviour
{
    public void Respawn(GameObject monster, Vector3 originPosition,
        float vanishTime, float respawnTime)
    {
        StartCoroutine(RegenMonster(monster, originPosition,
            vanishTime, respawnTime));
    }

    IEnumerator RegenMonster(GameObject monster, Vector3 originPosition,
        float vanishTime, float respawnTime)
    {
        yield return new WaitForSecondsRealtime(vanishTime);
        monster.SetActive(false);

        yield return new WaitForSecondsRealtime(respawnTime);
        monster.transform.position = originPosition;
        monster.SetActive(true);
    }
}
