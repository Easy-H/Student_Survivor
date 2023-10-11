using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    [SerializeField]
    private int money = 0;

    //���� ����Ǿ���� ���� �߰�
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DataManager>();

            if (instance == null)
                Debug.LogError("singleton error");
            return instance;
        }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if(instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    //���� PlayerPref�� �����ϴ� �ڵ带 �߰�
    //���� �����͸� ���� ������ ����, ���� ��Ű�� �Լ��� �߰�
}
