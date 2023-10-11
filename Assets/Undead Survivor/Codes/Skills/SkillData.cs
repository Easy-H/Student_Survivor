using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType { ����, ���� }

    public SkillType skillType;
    public int skillID;
    public string skillName;
    [TextArea]
    public string skillDesc;
    public Sprite skillIcon;

    public int pool_index;



    [Header("# Level Data")]
    public int bulletPrefabID; // Bullet�� ������ ID
    public float[] damages;
    public float[] cooltimes;
    public float[] lifeTime;
    public float[] speed;
    public float scaleFactor; // �ĳ׽� : ���� �ĵ��� ũ�� ���� �ӵ�
    public float flightTime; // �ڹ� : ü�� �ð� = ���� ���ư� Ŀ�Ƿ� ���ϱ� ������ �ð�
    public float rotateSpeed; // �ڹ� : Bullet�� ȸ���ϴ� �ӵ�
    public float[] attackCoolTime; // Ŭ����, IoT : ��ȯü�� �����ϴ� ��Ÿ��


}
