using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRateManager : MonoBehaviour
{
    public static SkillRateManager instance;
    public LevelUpSkill LevelUpObject;


    public SkillData[] SkillDatas;

    [Header("# Skill Rates")]
    public float[] ratesOnSubject; // ����
    public float[] ratesOnGrade1; // ���� 1�г�
    public float[] ratesOnGrade2; // ���� 2�г�
    public float[] ratesOnGrade3; // ���� 3�г�
    public float[] ratesOnGrade4; // ���� 4�г�


    private void Awake()
    {
        instance = this;
    }

    public void updateSkillRate(int pahse)
    {
        // ���� ������(0~7)�� �°� ��ų ���� ���� �籸��

    }

}
