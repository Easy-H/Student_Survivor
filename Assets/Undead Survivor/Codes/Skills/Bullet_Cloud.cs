using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Cloud : MonoBehaviour
{

    public float damage;
    public float speed;
    public float lifeTime;
    public float attackCoolTime; // ������ ���ڱ�⸦ ����߸��� �ֱ�
    public float dropTimeOfLaptop; // ��ž ��ü�� �������� �ð�.

    Rigidbody2D rigid;
    Transform target;
    GameObject laptop;
    Collider2D coll;

    float timer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponentInChildren<Collider2D>();
        coll.enabled = false;
        laptop = transform.GetChild(2).gameObject; // ��ž ������Ʈ ��������
    }

    public void Init(float damage, float speed, float lifeTime, float attackCoolTime)
    {
        this.damage = damage;
        this.speed = speed;
        this.lifeTime = lifeTime;
        this.attackCoolTime = attackCoolTime;
        GetComponentInParent<A_Skill_Data>().damage = damage; // �ܺο��� �����ϱ� ���� ���� ������ ǥ��
    }

    private void Update() // Ÿ�̸� ���
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

    private void FixedUpdate() // Ÿ�ٿ��� �̵�
    {
        if (!target || Vector3.Distance(transform.position, target.position) < 0.1f) // Ÿ���� ��Ȱ��ȭ�ų� �������� ��
            target = GameManager.Instance.player.scanner.GetRandomTarget(); // ������ Ÿ�� ����
        if (!target)
            return; // sccaner�� null�� �޾ƿ��� ��쵵 ����

        Vector3 dirVec = target.position - transform.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void OnEnable()
    {
        StartCoroutine(DropRoutine());
    }

    IEnumerator DropRoutine()
    {
        while (true)
        {
            laptop.transform.position = transform.position + new Vector3(0, 2, 0);
            laptop.SetActive(true);
            
            yield return new WaitForSeconds(dropTimeOfLaptop - 0.1f); // ��ž ������������ ���
            coll.enabled = true;
            yield return new WaitForSeconds(0.1f); // ��ž ������������ ���
            coll.enabled = false;

            laptop.SetActive(false);

            yield return new WaitForSeconds(attackCoolTime);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
