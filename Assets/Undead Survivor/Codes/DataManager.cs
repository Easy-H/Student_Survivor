using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    public int money = 0; //��
    public int selectedCharacterId = 0; //���õ� ĳ����
    public bool[] isUnlockCharacters = new bool[4]; //ĳ���Ͱ� �رݵ� �������� ����
    public static DataManager Instance //�� Ŭ������ instance�� �޾ƿ� �� �ִ� �Ӽ� - ���ӸŴ����� ���� �Ȱ��� ���� ��.
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null) //���ݱ��� ������ ��ü�� ������
            instance = this; //�̰� static���� ����.
        else if (instance != this) //������ ��ü�� �ִµ� �װ� �� ��ü�� instance�� �ƴϸ�
            Destroy(gameObject); //���� ��ü�� ���ش�.
        
        Init(); //�ʱ�ȭ�� �����Ѵ�.
        DontDestroyOnLoad(gameObject);//������ ������Ͽ� LoadScene�� ȣ��Ǿ �� ������Ʈ�� �ı����� �ʴ´�.
    }
    private void Init()
    {
        if (!PlayerPrefs.HasKey("UserData"))
            Save();
        money = PlayerPrefs.GetInt("money"); 
        selectedCharacterId = PlayerPrefs.GetInt("selectedCharacterId"); 
        for (int i = 0; i < isUnlockCharacters.Length; i++) {
            isUnlockCharacters[i] = Convert.ToBoolean(PlayerPrefs.GetInt(string.Format("isUnlockCharacter{0}", i)));
        }
    }
    public void SetSelectedCharacter(int id)//ĳ���� ���� ��ư�� ����Ǵ� �޼ҵ�
    {
        selectedCharacterId = id;
        Save();
    }
    public void Save()//��⿡ �����ϴ� �޼ҵ�
    {
        PlayerPrefs.SetInt("UserData", 1);

        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("selectedCharacterId", selectedCharacterId);
        for (int i = 0; i < isUnlockCharacters.Length; i++)
        {
            PlayerPrefs.SetInt(string.Format("isUnlockCharacter{0}", i), Convert.ToInt32(isUnlockCharacters[i]));
        }
    }
    public void UnlockCharacter(int id, int price)//ĳ���� �ر� �Լ�.
    {
        if(!CheckMoney(price))//�� ���� �˻��Ѵ�.
            return;

        SubMoney(price);//���� ����
        isUnlockCharacters[id] = true;//�ر��Ѵ�.

        Save();//�����Ѵ�.
    }
    public bool CheckMoney(int price)//���� ���ڶ��� �ʴ��� �˻��Ѵ�. �� ��� - true, �� �� ��� - false. 
    {
        return money > price;
    }

    public void AddMoney(int value)
    {
        money += value;
    }
    public void SubMoney(int value)
    {
        money -= value;
    }
}
