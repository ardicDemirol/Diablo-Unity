using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillEffects : MonoBehaviour
{
    [Header("Skill Effects")]
    public GameObject hammerSkill;
    public GameObject spellCastSkill;
    public GameObject slashSkill;
    public GameObject kickSkill;
    public GameObject healSkill;
    public GameObject comboAttackSkill;
    [Header("Skill Transforms")]
    public Transform kickTransform;
    public Transform spellCastTransform;
    public Transform hammerSkillTransform;
    public Transform comboSkillTransform;
    public Transform slashTransform;

    private void Update()
    {
        
    }

    void HammerSkill()
    {
        Instantiate(hammerSkill,hammerSkillTransform.position,Quaternion.identity);
    }
    void SpellCastSkill()
    {
        Instantiate(spellCastSkill, spellCastTransform.position, Quaternion.identity);
    }
    void SlashSkill()
    {
        Instantiate(slashSkill, slashTransform.position, Quaternion.identity);
    }
    void KickSkill()
    {
        Instantiate(kickSkill, kickTransform.position, Quaternion.identity);
    }
    void HealSkill()
    {
        Vector3 pos = transform.position;
        pos.y = 1;
        Instantiate(healSkill, pos, Quaternion.identity);
    }
    void SlashComboSkill()
    {
        Instantiate(comboAttackSkill, comboSkillTransform.position, Quaternion.identity);
    }

}
