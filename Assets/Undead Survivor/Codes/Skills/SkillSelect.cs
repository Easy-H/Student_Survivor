using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    // �� ���� item ��Ʈ��Ʈ
    public SkillData skillData;
    public int level;
    public BasedSkill skill; // �� ���� weapon
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        level = 0;

        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = skillData.skillIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        //get components�� ������ hierarchy�� ������.

        textName.text = skillData.skillName;
    }

    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        switch (skillData.skillType)
        {
            case SkillData.SkillType.����:
                textDesc.text = string.Format(skillData.skillDesc);
                break;
            case SkillData.SkillType.����:
                textDesc.text = string.Format(skillData.skillDesc);
                break;
        }


    }

    public void OnClick()
    {
        switch (skillData.skillType)
        {
            case SkillData.SkillType.����:
                if (level == 0)
                {
                    GameObject newSkill = new GameObject();
                    skill = newSkill.AddComponent<BasedSkill>();
                    Debug.Log("������ ��ų�� Init�Լ� ����");
                    skill.Init(skillData);
                    level++;
                }
                else
                {
                    skill.LevelUp();
                }
                break;
            /*case SkillData.SkillType.����:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(skillData);
                }
                else
                {
                    float nextRate = skillData.damages[level];
                    gear.LevelUp(nextRate);

                }
                level++;
                break;*/
        }
        if (level == skillData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}