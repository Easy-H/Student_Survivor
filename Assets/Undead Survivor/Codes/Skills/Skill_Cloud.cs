using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Cloud : MonoBehaviour
{
    public int bulletPrefabID;
    public float coolTime; // lifeTime �� �Ǹ� �׶����� coolTime ī��Ʈ ��
    public float lifeTime; // lifeTime << coolTime ������ lifeTime�� coolTime�� ���Ե�
    public float damage;
    public float speed;
    public float attackCoolTime; // ������ ���ڱ�⸦ ����߸��� �ֱ�

    public GameObject Bullet;

    float timer;

    // ������ �ϴ� coolTime�� LifeTime�� ���Եǵ��� �����ϰ���.

    private void Awake()
    {
        A_Skill_Data skillData = GetComponentInParent<A_Skill_Data>();
        bulletPrefabID = skillData.bulletPrefabID;
        coolTime = skillData.coolTime;
        lifeTime = skillData.lifeTime;
        damage = skillData.damage;
        speed = skillData.speed;
        attackCoolTime = skillData.attackCoolTime;
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
        Vector2 randomCircle = Random.insideUnitCircle.normalized; // �� ���� �� ��
        Vector3 spawnPosition = new Vector3(randomCircle.x, randomCircle.y, 0);


        Transform bullet = GameManager.Instance.pool.Get(bulletPrefabID).transform;

        bullet.position = transform.position + spawnPosition * 5; // ĳ���� �߽����� ������ 5�� �� ���� �� ��
        bullet.GetComponent<Bullet_Cloud>().Init(damage, speed, lifeTime, attackCoolTime);


        // �����
    }
}
