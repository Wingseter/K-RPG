using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
    public string skillName;

    float cooldown;
    public float coolTime;
    public Image cdImg;
    bool cool;
    public float castingTime;
    GameObject skillObj;
    public float spentMana;

    public void OnClickBtn()
    {
        if(!cool)
        {
            cool = true;
            StartCoroutine("CoolDown");

            switch(skillName)
            {
                case "SoulArrow":
                    skillObj = Manager.instance.manager_Obj.GetObj(Manager.instance.manager_Obj.list_Skill_Arrow);
                    break;
                case "Nuke":
                    skillObj = Manager.instance.manager_Obj.GetObj(Manager.instance.manager_Obj.list_Skill_Nuke);
                    break;
            }
            Manager.instance.playerController.Casting(castingTime, skillName, skillObj, spentMana);
        }
    }

    IEnumerator CoolDown()
    {
        cooldown = 0;

        while(true)
        {
            cooldown += Time.deltaTime;
            cdImg.fillAmount = cooldown / coolTime;

            if(cooldown >= coolTime)
            {
                cool = false;
                StopCoroutine("CoolDown");
            }

            yield return null;
        }
    }
}
