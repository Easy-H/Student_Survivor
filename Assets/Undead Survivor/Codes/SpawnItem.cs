using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ʿ� �����Ǵ� ������ prefab�� ���� ��ũ��Ʈ
//prefab�� Coin �ϳ����� �������
public class SpawnItem : MonoBehaviour
{
    public SpawnItemData data;
    SpriteRenderer spriteRenderer; //�̹��� ������ ���� SpriteRenderer
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //Spawner���� �������� ������ ��, Init�Լ��� ȣ����.
    public void Init(SpawnItemData data)
    {
        this.data = data;//Spawner�� �ִ� itemDatas �迭�� �ִ� ������(Inspector�󿡼� ���� �������� Scriptable Object �����Ͱ� ������) �迭���� �����͸� �޾ƿ� ����
        spriteRenderer.sprite = data.image;//�����ۿ� �°� �̹��� ����
        GetComponent<MagnetableItem>().enabled = data.magnetable;//�ڼ��� ������ �����ʴ� ���� �����۵���
        //�ڼ����� �������� ����ϴ� MagnetableItem ��ũ��Ʈ�� ��Ȱ��ȭ ���� �������� �ʵ��� ��.
    }
    private void OnDisable()
    {
        GetComponent<MagnetableItem>().enabled = false;
    }
}
