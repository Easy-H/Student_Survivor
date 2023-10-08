using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_OS : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprites; // 0:�κ� �̹���, 1: �ϴ� Ŀ�� �̹���

    public float duration;
    public float damage;
    public float speed;

    Collider2D[] colls; // �ڽ� ������Ʈ�� �ִ� �ݶ��̴���
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Scanner scanner;
    Transform target;

    bool isExplosion;

    private void Awake()
    {
        colls = GetComponentsInChildren<Collider2D>(); // 0:OS���, 1:���߸��
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        scanner = GetComponent<Scanner>();
        isExplosion = false;
    }

    public void Init(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
        
    }


    private void FixedUpdate()
    {
        if (isExplosion) // ���� ���� ���������� �̵�X
            return;
        if (!scanner.nearestTarget)
            return;

        target = scanner.nearestTarget;// OS ��ü�� scanner�� ���� �ִܰŸ� �� ã�ư�
        Vector3 dirVec = target.position - transform.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;
        rigid.velocity = Vector2.zero;

        spriteRenderer.sprite = sprites[1];//�̹����� �ϴ� Ŀ�Ƿ� ���� -> ���� ���߷� �ٲ�� ��
        
        isExplosion = true; // ������������ �ٲ�

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
        isExplosion = false;
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
