using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DataStructure : MonoBehaviour
{
    public int bulletPrefabID;
    public float coolTime;
    public GameObject Bullet; // �Ѿ��� � ���������� �����ֱ⸸ �ϴ� �뵵


    float timer;
    private void Awake()
    {
        A_Skill_Data skillData = GetComponentInParent<A_Skill_Data>();
        bulletPrefabID = skillData.bulletPrefabID;
        coolTime = skillData.coolTime;
    }
    void Update()
    {

        if (!GameManager.Instance.isLive)
            return;
        timer += Time.deltaTime;
        if (timer > coolTime)
        {
            timer = 0f;
            Fire();
        }
    }
    void Fire()
    {
        Transform bullet = GameManager.Instance.pool.Get(bulletPrefabID).transform;
        // bullet.GetComponent<Bullet_DataStructure>().Init();
    }
}
