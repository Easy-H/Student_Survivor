using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Algorithm : MonoBehaviour
{
    public float rotateSpeed;
    public float lifeTime;
    public float damage;


    Collider2D coll;
    float timer;


    private void Awake()
    {
        coll = GetComponentInChildren<Collider2D>();
    }

    public void Init(float rotateSpeed, float lifeTime, float damage)
    {
        this.rotateSpeed = rotateSpeed;
        this.lifeTime = lifeTime;
        this.damage = damage;
        GetComponentInParent<A_Skill_Data>().damage = damage; // �ܺο��� �����ϱ� ���� ���� ������ ǥ��
    }

    IEnumerator RotateRoutine()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, 90f);
        float rotationThreshold = 0.01f;
        // Ÿ��ȸ���������� ���̰� 0.01 ������ ������ �ݺ�
        while (Quaternion.Angle(transform.rotation, targetRotation) > rotationThreshold)
        {
            // deltaTime�� ���� �������� �޶� ���� �ӵ� ����
            float step = rotateSpeed * Time.deltaTime;
            // ���� �������� Ÿ��ȸ�������� step��ŭ ȸ��
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            yield return null; // ���� �����ӱ��� �纸
        }
        yield return StartCoroutine(MainRoutine(() =>
        {
            gameObject.SetActive(false);
        } ));
    }

    IEnumerator MainRoutine(System.Action done) // LifeTime���� �׳� Ÿ�̸� ��� �ڷ�ƾ
    {
        coll.enabled = true; // collider Ȱ��ȭ
        while (timer < lifeTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        done.Invoke(); // ���� ��Ȱ��ȭ �Լ� ȣ��
    }

    private void OnEnable()
    {
        Vector2 randomCircle = Random.insideUnitCircle; // �� ���� �� ��
        Vector3 spawnPosition = new Vector3(randomCircle.x, randomCircle.y, 0);
        spawnPosition = spawnPosition.normalized; // �� ���� �� ��

        transform.position = GameManager.Instance.player.transform.position + spawnPosition * 3;

        StartCoroutine(RotateRoutine()); // �ش� ������Ʈ�� On �� ������ ����
    }
    private void OnDisable()
    {
        coll.enabled = false;
        transform.rotation = Quaternion.identity;
        StopAllCoroutines();
    }

}
