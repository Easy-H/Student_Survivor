using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    CircleCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.money++;

            Debug.Log("���ΰ� �浹 �߻�");

            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Exp 0")) {
            GameManager.Instance.GetExp();

            Debug.Log("����ġ�� �浹 �߻�");

            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Exp 1"))
        {
            GameManager.Instance.GetExp(10);

            Debug.Log("�ް�����ġ �浿");

            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Health"))
        {
            GameManager.Instance.GetHealth(10); // 10��ŭ ü�� ȸ��

            Debug.Log("ü�� �浿");

            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Mag")) // �ϴ��� ���� ����?
        {

            StartCoroutine(CreateMagRoutine());


            collision.gameObject.SetActive(false);
        }

    }

    IEnumerator CreateMagRoutine() // 10�� ���� ������ 10 ����
    {
        Debug.Log("�ڼ��ڼ�");

        coll.radius += 10;
        yield return new WaitForSeconds(10);
        coll.radius -= 10;
        Debug.Log("�ڼ� ȿ�� ��");
    }

    private void LevelUpColliderRadius()
    {
        coll.radius *= 2f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Key Input �߻�");
            LevelUpColliderRadius();
        }
    }
}
