using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_OS : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprites; // 0:�κ� �̹���, 1: �ϴ� Ŀ�� �̹���

    public float duration;
    public float damage;

    Collider2D[] colls; // �ڽ� ������Ʈ�� �ִ� �ݶ��̴���
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    private void Awake()
    {
        colls = GetComponentsInChildren<Collider2D>(); // 0:OS���, 1:���߸��
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        
        rigid.velocity = dir * 1f; // �ӵ� �ϴ� 1�� ��. �ϵ��ڵ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;
        rigid.velocity = Vector2.zero;

        spriteRenderer.sprite = sprites[1];//�̹����� �ϴ� Ŀ�Ƿ� ���� -> ���� ���߷� �ٲ�� ��


        StartCoroutine(ExplosionRoutine(() => { gameObject.SetActive(false); }));
        
    }
    /*private void OnTriggerExit2D(Collider2D collision) // �̰� �����ϱ� �ǳ�
    {
        if (!collision.CompareTag("Area"))
            return;
        Debug.Log("OS �κ� ������ ����");
        gameObject.SetActive(false);
    }*/
    private void OnDisable()
    {

        spriteRenderer.sprite = sprites[0];
        transform.position = new Vector3(0, 0, 0);
        // transform.rotation = Quaternion.identity;
    }
    IEnumerator ExplosionRoutine(System.Action done)
    {
        colls[1].enabled = true;
        colls[0].enabled = false;

        float timer = 0f;
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        colls[0].enabled = true;
        colls[1].enabled = false;

        done.Invoke();
    }
}
