using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_MachhineLearning : BulletBase
{
    public float spawnDistance = 5;

    Rigidbody2D rigid;
    Scanner scanner;
    Transform target;

    float timer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        scanner = GetComponent<Scanner>();
    }


    private void OnEnable()
    {

        Vector2 randomCircle = Random.insideUnitCircle.normalized; // �� ���� �� ��
        Vector3 spawnPosition = new Vector3(randomCircle.x, randomCircle.y, 0);

        transform.position =GameManager.Instance.player.transform.position + spawnPosition * spawnDistance; // ĳ���� �߽����� ������ 5�� �� ���� �� ��
    }

    private void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;

        if (timer > lifeTime)
        {
            timer = 0f;
            gameObject.SetActive(false); // lifeTime �� �Ǹ� ��Ȱ��ȭ
        }
    }

    private void FixedUpdate()
    {
        if (!scanner.nearestTarget)
            return;

        target = scanner.nearestTarget;// ��ü�� scanner�� ���� �ִܰŸ� �� ã�ư�
        Vector3 dirVec = target.position - transform.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

}
