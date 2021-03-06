using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Obj : MonoBehaviour
{
    [Header("Pool")]
    public Transform pool_SkillObj;
    public List<GameObject> list_Skill_Arrow = new List<GameObject>();
    public List<GameObject> list_Skill_Nuke = new List<GameObject>();

    [Space(10)]
    public Transform pool_Effect;
    public List<GameObject> list_hitEffect = new List<GameObject>();

    [Space(10)]
    public Transform pool_text;
    public List<GameObject> list_dmgText = new List<GameObject>();

    [Header("Prefab")]
    public GameObject prefab_skill_SoulArrow;
    public GameObject prefab_skill_Nuke;
    public GameObject prefab_hitEffect;
    public GameObject prefab_dmgText;


    GameObject AddObj(GameObject prefab, Transform pool, List<GameObject> list)
    {
        GameObject obj = Instantiate(prefab, pool);
        list.Add(obj);

        return obj;
    }

    public GameObject GetObj(List<GameObject> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
                return list[i];
        }
        if(list == list_Skill_Nuke)
        {
            GameObject obj = AddObj(prefab_skill_Nuke, pool_SkillObj, list_Skill_Nuke);
            return obj;
        }

        if(list == list_Skill_Arrow)
        {
            GameObject obj = AddObj(prefab_skill_SoulArrow, pool_SkillObj, list_Skill_Arrow);
            return obj;
        }
        if (list == list_hitEffect)
        {
            GameObject obj = AddObj(prefab_hitEffect, pool_Effect, list_hitEffect);
            return obj;
        }

        if(list == list_dmgText)
        {
            GameObject obj = AddObj(prefab_dmgText, pool_text, list_dmgText);
            return obj;
        }

        return null;
    }
}
