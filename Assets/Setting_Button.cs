using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_Button : MonoBehaviour
{
 public GameObject settingObject;  // Setting ������Ʈ�� ������ ����

    public void OnButtonClick()
    {
        // Setting ������Ʈ�� �����ϴ��� Ȯ��
        if (settingObject != null)
        {
            // Setting ������Ʈ�� Ȱ��ȭ ���θ� ������Ŵ
            settingObject.SetActive(!settingObject.activeSelf);
        }
    }
}
