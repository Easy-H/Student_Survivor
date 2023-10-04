using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_OS : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprites; // 0:�κ� �̹���, 1: �ϴ� Ŀ�� �̹���

    public float duration;
    public float damage;

    Collider2D coll;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        rigid.velocity = dir * 5f; // �ӵ� �ϴ� 5�� ��. �ϵ��ڵ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;
        rigid.velocity = Vector2.zero;

        spriteRenderer.sprite = sprites[1];//�̹����� �ϴ� Ŀ�Ƿ� ���� -> ���� ���߷� �ٲ�
        float timer = 0f;
        while (timer < duration)
        { //n�� ���� ���
            timer += Time.deltaTime;
        }

        gameObject.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        spriteRenderer.sprite = sprites[0];
        transform.position = new Vector3(0, 0, 0);
        // transform.rotation = Quaternion.identity;
        // StopAllCoroutines();
    }
}
