using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //������� ����Ʈ�� 1:1 ����, ������ 2���� ����Ʈ 2��
    // .. ��������� ������ ����
    public GameObject[] prefabs;

    // .. Ǯ ��� ����Ʈ��
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
            pools[i] = new List<GameObject> ();

        //Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ... ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����

          // ... �߰��ϸ� select ������ �Ҵ�


        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

       // ... ��� �ִ� ������Ʈ�� ������ 
         
         // ... ���Ӱ� �Ҵ�
         if (!select)
        {
            //pool manager�ȿ� �ְڴ� ������ �����ؼ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        
        return select;
    }
}
