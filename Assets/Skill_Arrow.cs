using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skill_Arrow : MonoBehaviour
{
    Transform target;
    public float speed;
    GameObject hitEffect;

    public float skillPower;
    int dmg;
    float dmgRate;

    private void OnEnable()
    {
        target = Manager.instance.playerController.atkTarget;
        StartCoroutine("Arrow");
        Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.flying_MagicArrow);
    }

    IEnumerator Arrow()
    {
        while(true)
        {
            Vector3 dir = target.position + new Vector3(0, 2, 0);
            transform.LookAt(dir);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return null;
        }
    }
    void OnHitEffect(Vector3 hitPoint)
    {
        hitEffect = Manager.instance.manager_Obj.GetObj(Manager.instance.manager_Obj.list_hitEffect);

        hitEffect.transform.position = hitPoint + new Vector3(0, 1, 0);
        hitEffect.SetActive(true);
    }

    void CalculateDmg()
    {
        PlayerState playerState = Manager.instance.playerController.player.GetComponent<PlayerState>();

        dmgRate = Random.Range(0.8f, 1.2f);

        int cri = Random.Range(0, 100);
        if (cri < playerState.cri * 100)
            dmgRate = Random.Range(2f, 2.5f);
        dmg = (int)(playerState.atk * skillPower * dmgRate);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.tag == "Enemy" | other.gameObject.tag == "Boss" | other.gameObject.tag == "Dead")
        {
            Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.hit_MagicArrow);

            StopCoroutine("Arrow");
            gameObject.SetActive(false);

            Vector3 hitPoint = (other.transform.position + transform.position) * 0.5f;
            OnHitEffect(hitPoint);

            CalculateDmg();
            GameObject dmgText = Manager.instance.manager_Obj.GetObj(Manager.instance.manager_Obj.list_dmgText);

            dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
            dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRate;
            dmgText.transform.position = Manager.instance.playerController.cam.WorldToScreenPoint(hitPoint + new Vector3(0, 1, 0));
            dmgText.SetActive(true);

            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
                other.GetComponent<EnemyHit>().Hit(dmg);
        }
    }
}

