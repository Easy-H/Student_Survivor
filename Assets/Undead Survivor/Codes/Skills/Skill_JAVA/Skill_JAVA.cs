using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_JAVA : MonoBehaviour
{
    [SerializeField]
    public int id = 2;
    public float cooltime = 5f;
    public float timer;

    
    public GameObject cup;

    public int space_count = 0;

    //��Ÿ�� ����ؼ� ȣ���ϴ� ������ �Լ�.

    private void Update()
    {
        /*
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;

        if (timer > cooltime)
        {
            timer = 0f;
            Fire();
        }*/
        if (!GameManager.Instance.isLive)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(string.Format("space�� {0}��° ����", space_count));
            switch (space_count)
            {
                case 0:
                    GameManager.Instance.player.skillManager.AddSkill(0);
                    break;
                case 1:
                    GameManager.Instance.player.skillManager.LevelUp(0);
                    break;
                case 2:
                    GameManager.Instance.player.skillManager.LevelUp(0);
                    break;
                case 3:
                    break;
                case 4:
                    GameManager.Instance.player.skillManager.AddSkill(1);
                    break;
                case 5:
                    GameManager.Instance.player.skillManager.LevelUp(1);
                    break;
                case 6:
                    GameManager.Instance.player.skillManager.LevelUp(1);
                    break;
                case 7:
                    break;
            }
            space_count++;

        }
            

    }
    void Fire()
    {
        if (!GameManager.Instance.player.scanner.nearestTarget)
            return;
        // GameManager.Instance.pool.Get(5);
        GameManager.Instance.pool.Get(8);
    }
    IEnumerator ThrowCupRoutine()
    {
        yield return null;
    }
    IEnumerator CoffeeLava()
    {
        yield return null;
    }

}
//Ŀ�Ƕ� �� ������ �� �ҽ� ����.
//���� �� �� sprite
//�ð��Ǹ� ���� ��Ű�� Ŀ�� sprite �� ���� ���� collider enable; �� �ð� ������ ���� ���ǵ� ���ֱ�

//Pool Manager�� �߰�.

//��ų ��Ÿ�� ������ ���� �Ŵ��� Ŭ���� �߰��� �ʿ�. 
