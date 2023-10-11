using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static SkillData;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType { ����, ���� } //��������������������������............??????????????????????????????????���
    public int id;
    public Sprite icons;

    public string skillName;
    [TextArea]
    public string skillDesc;
    public int poolIndex;
    public float[] damages;
    public float[] cooltimes;

    [Header("IoT")]
    public float[] summonTime;

    
    public enum ����ɷ�ġ { MaxHp, Magnet };
    [Header("����")]
    public ����ɷ�ġ �ɷ�ġ;


}
