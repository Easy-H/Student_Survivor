using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Algorithm : MonoBehaviour
{
    // RB Ʈ���� ���� �� ����Ʈ����? Ʈ���� �����Ǹ� 1�� �� ��/��� 90�� ��������


    public float coolTime = 5f;
    float timer;
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

        bullet.position = transform.position + spawnPosition * 3;

    }

}
