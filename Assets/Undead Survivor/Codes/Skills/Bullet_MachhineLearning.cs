using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_MachhineLearning : MonoBehaviour
{

    public float damage;
    public float speed;
    public float lifeTime;


    Rigidbody2D rigid;
    Scanner scanner;
    Transform target;
    SpriteRenderer spriteRenderer;

    float timer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        scanner = GetComponent<Scanner>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(float damage, float speed, float lifeTime)
    {
        this.damage = damage;
        this.speed = speed;
        this.lifeTime = lifeTime;
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
    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive || !target) // �÷��̾� ��� Ȥ�� Ÿ���� null�� ��� ����
            return;
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }
}
