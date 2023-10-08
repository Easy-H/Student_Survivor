using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_SystemProgramming : MonoBehaviour
{
    Rigidbody2D rigid;
    Collider2D coll;

    public float duration = 5f;

    public float speed = 2.0f;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        coll.enabled = false;

        rigid.velocity = Vector3.zero;

        transform.parent = collision.transform;
        transform.rotation = Quaternion.identity;
        transform.localPosition = new Vector3(0, 1, 0);
    }
    private void OnEnable()
    {
        coll.enabled = true;
        StartCoroutine(ExtinctTimerRoutine());
    }

    //�߻�ǰ� ���� ������ ��Ȱ��ȭ ��Ű�� ��ƾ
    //CompareTag("Area") �Ϸ��� �ߴµ� �� �� ���� �Ƹ� transform.parent�� �ٲٴ� �������� Area���� ����� �׷���...
    IEnumerator ExtinctTimerRoutine()
    {
        yield return new WaitForSeconds(duration + 10f);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
