using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_SystemProgramming : MonoBehaviour
{
    //��... �Ѿ��� ���� -> ������ -> 1.collider ����, 2.ȸ�� ���� 3.�θ��ڽĺ���, 3-1.y�� local��ǥ ���� 4.�� �ӵ� ����, 5.�ð� �� ������ �θ��ڽ� �ٽ� Ǯ�Ŵ����� ����
    //5-1�ð� �� ������ �̹��� ����.
    //���� ���� �߿� �װ� �ȴٸ�? �θ��ڽ� ����

    //�Ѿ� ������ ���� �����̼� �ʱ�ȭ, �ݶ��̴� Ȱ��

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Collider2D coll;

    public float speed = 2.0f;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        coll.enabled = false;
    }
    private void OnEnable()
    {
        Transform target = GameManager.Instance.player.scanner.nearestTarget;
        Transform player = GameManager.Instance.player.transform;
        transform.position = player.position;

        Vector3 dir = target.position - player.position;
        transform.LookAt(dir);

        rigid.velocity = dir.normalized * speed;

        coll.enabled = true;

        spriteRenderer.sprite = sprites[0];
    }
}
