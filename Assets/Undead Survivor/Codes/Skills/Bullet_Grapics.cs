using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Grapics : MonoBehaviour
{
    public float lifeTime;

    GameObject selectedPolygon;

    public void Init(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }
    private void OnEnable()
    {
        int selectedChildNum = Random.Range(0, 4); // 0~4 �� �ڽ� �� �� ������
        selectedPolygon = transform.GetChild(selectedChildNum).gameObject;
        selectedPolygon.SetActive(true); // ������Ʈ �ϳ��� Ȱ��ȭ

        StartCoroutine(TimerRoutine()); // Ÿ�̸� �ڷ�ƾ ����
    }

    IEnumerator TimerRoutine() // lifeTime �Ŀ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    {
        float timer = 0f;
        while (timer < lifeTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        selectedPolygon.SetActive(false); // ��ų ����Ǹ� �ٽ� ��Ȱ��ȭ
    }
}
