using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Algorithm : MonoBehaviour
{
    // RB Ʈ���� ���� �� ����Ʈ����? Ʈ���� �����Ǹ� 1�� �� ��/��� 90�� ��������

    public int bulletPrefabID;
    public float coolTime;
    public float rotateSpeed;
    public float lifeTime;
    public float damage;

    public GameObject Bullet; // �Ѿ��� � ���������� �����ֱ⸸ �ϴ� �뵵   

    float timer;
    private void Awake()
    {
        A_Skill_Data skillData = GetComponentInParent<A_Skill_Data>();
        bulletPrefabID = skillData.bulletPrefabID;
        coolTime = skillData.coolTime;
        rotateSpeed = skillData.rotateSpeed;
        lifeTime = skillData.lifeTime;
        damage = skillData.damage;
    }

    private void Update()
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
        Vector2 randomCircle = Random.insideUnitCircle; // �� ���� �� ��
        Vector3 spawnPosition = new Vector3(randomCircle.x, randomCircle.y, 0);
        spawnPosition = spawnPosition.normalized; // �� ���� �� ��

        Transform bullet = GameManager.Instance.pool.Get(6).transform;
        bullet.GetComponent<Bullet_Algorithm>().Init(rotateSpeed, lifeTime, damage);

        bullet.position = transform.position + spawnPosition * 3;

    }

}
