using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasedSkill : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float coolTime;

    public int level;

    public SkillData skillData;

    Player player;
    float timer;
    private void Awake()
    {
        player = GameManager.Instance.player;
    }
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;

        if (timer > skillData.cooltimes[level])
        {
            timer = 0f;
            Fire();
        }
    }

    public void LevelUp()
    {
        level++;

        if (id == 0)
        {
            // Arrange();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    public void Init(SkillData skillData)
    {
        this.skillData = skillData;
        // skillData.level = 0; // ���� �ʱ�ȭ
        Debug.Log("����� BasedSkill�� Init�Լ� �����Դϴ�");
        name = "SKILL " + skillData.skillName; // ������Ʈ name�� �����ϴ°���
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        for (int index = 0; index < GameManager.Instance.pool.prefabs.Length; index++)
        {
            if (skillData.bulletPrefab == GameManager.Instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        /*switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                // Arrange();
                break;
            default:
                speed = 0.5f * Character.WeaponRate;
                break;
        }
*/
        // Hand Set
        /*Hand hand = player.hands[(int)skillData.itemType];
        hand.spriteRenderer.sprite = skillData.hand;
        hand.gameObject.SetActive(true);*/

        //���߿� �߰��� ���⿡�� ������ ����ǵ���
        //�� �Լ��� �����ִ� �ֵ� �� �����ض� ���
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;
        GameObject bullet = GameManager.Instance.pool.Get(prefabId);
        bullet.GetComponent<BulletBase>().Init(skillData, level); // ���� ȣ��ǰų� ������ �ÿ��� ���ǹ���
    }
}
