using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DeBun : MonoBehaviour
{
    public float coolTime; // lifeTime �� �Ǹ� �׶����� coolTime ī��Ʈ ��
    public float lifeTime; // lifeTime << coolTime ������ lifeTime�� coolTime�� ���Ե�
    public float damage;
    public float speed;

    float timer;



    // ������ �ϴ� coolTime�� LifeTime�� ���Եǵ��� �����ϰ���.
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


        Transform bullet = GameManager.Instance.pool.Get(11).transform;

        bullet.position = transform.position + spawnPosition * 3; // ĳ���� �߽����� ������ 3�� �� ���� �� ��
        bullet.GetComponent<Bullet_MachhineLearning>().Init(damage, speed, lifeTime);


        // �����
    }
}
