using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Arrow : MonoBehaviour
{
    Transform target;
    public float speed;
    GameObject hitEffect;

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


    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.tag == "Enemy" | other.gameObject.tag == "Dead")
        {
            Manager.instance.manager_SE.seAudios.PlayOneShot(Manager.instance.manager_SE.hit_MagicArrow);

            StopCoroutine("Arrow");
            gameObject.SetActive(false);

            Vector3 hitPoint = (other.transform.position + transform.position) * 0.5f;
            OnHitEffect(hitPoint);
        }
    }
}

