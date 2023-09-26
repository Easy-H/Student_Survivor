using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_ComNeSil : MonoBehaviour
{
    public float scaleFactor = 0.5f;
    public float speed = 15;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        InvokeRepeating("ScaleUp", 0.1f, 0.1f);  // 0.1f�ʸ��� �Լ� ����, ��� ��Ȱ��ȭ ���¿����� ��� �ݺ���
    }

    public void Init(Vector3 dir)
    {
        rigid.velocity = dir * speed;
    }

    /*
    private void Update() // �Ѿ� �ϳ��ϳ��� Ÿ�̸�
    {
        timer += Time.deltaTime;

        if (timer > 5f)
        {
            timer = 0f;
            transform.localScale = new Vector3(2f, 2f, 0f);
            gameObject.SetActive(false);
        }
    }*/
    private void OnTriggerExit2D(Collider2D collision) // �Ÿ����� �ð��� �������� ��Ȱ��ȭ�� ���� ��
    {
        if (!collision.CompareTag("Area"))
            return;
        transform.localScale = new Vector3(2f, 2f, 0f);
        gameObject.SetActive(false);
    }
    private void ScaleUp()
    {
        if (!gameObject.activeSelf)
            return;
        Vector3 newScale = transform.localScale + new Vector3(scaleFactor, scaleFactor, 0f);
        transform.localScale = newScale;
    }
}
