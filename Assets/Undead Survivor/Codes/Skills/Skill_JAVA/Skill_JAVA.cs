using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_JAVA : MonoBehaviour
{
    [SerializeField]
    public int bulletPrefabID;
    public float coolTime;
    public float flightTime; // ü���ð�. ���� Ŀ�Ƿ� ���ϴ� �ð�.
    public float rotateSpeed; // ���� ȸ���ϴ� �ӵ�
    public float lifeTime; // ������� �����ϴ� �ð�
    public float damage;

    public GameObject Bullet; // �Ѿ��� � ���������� �����ֱ⸸ �ϴ� �뵵


    float timer;
    private void Awake()
    {
        A_Skill_Data skillData = GetComponentInParent<A_Skill_Data>();
        bulletPrefabID = skillData.bulletPrefabID;
        coolTime = skillData.coolTime;
        flightTime = skillData.flightTime;
        rotateSpeed = skillData.rotateSpeed;
        lifeTime = skillData.lifeTime;
        damage = skillData.damage;
    }

    private void Update()
    {

        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;

        if (timer > coolTime)
        {
            timer = 0f;
            Fire();
        }
        /*if (!GameManager.Instance.isLive)
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

        }*/


    }
    void Fire()
    {
        if (!GameManager.Instance.player.scanner.nearestTarget)
            return;
        // GameManager.Instance.pool.Get(5);
        Transform bullet = GameManager.Instance.pool.Get(bulletPrefabID).transform;
        bullet.GetComponent<Bullet_JAVA>().Init(flightTime, rotateSpeed, lifeTime, damage);
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
