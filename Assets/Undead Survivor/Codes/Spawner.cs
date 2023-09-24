using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;
    public float ItemsRandomSpawnArea; //.. �÷��̾� ������ ������ �����Ǵ� ��Ŭ�� ������

    int level;
    float timer;

    public SpawnItemData[] itemDatas;
    //public int[] drop_rates; // ���߿� �����ۿ� ������� �ְ� �ȴٸ� ����.
    //private int total_drop_rate;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        //�ڱ� �ڽ��� ������ �ڽĵ� component �ϴ� �����

        levelTime = GameManager.Instance.maxGameTime / spawnData.Length;

        ItemsRandomSpawnArea = GameManager.Instance.ItemsRandomSpawnArea;

        //drop_rates = new int[items.Length];

        StartCoroutine(CreateCoinRoutine());
    }
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / levelTime), spawnData.Length - 1);
        //������ �Ҽ��� ����

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            SpwanEnemy();
        }
 
    }

    IEnumerator CreateCoinRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int randomItemNum = SelectRandomItem();
            SpawnItem(randomItemNum);
        }
    }
    void SpwanEnemy()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //1�� ������ �ڱ� �ڽ� ����
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

    int SelectRandomItem() //.. ���ӸŴ����� ������ �� degree��ġ�� ���� Ȯ�������� ������ ��ȣ�� �����ϴ� �Լ�
    {
        int resultItem;
        float coin_spd = GameManager.Instance.coin_spd;
        float exp0_spd = GameManager.Instance.exp0_spd;
        float exp1_spd = GameManager.Instance.exp1_spd;
        float health_spd = GameManager.Instance.health_spd;
        float mag_spd = GameManager.Instance.mag_spd;

        float total_spd = coin_spd + exp0_spd + exp1_spd + health_spd + mag_spd;
        float select_spd = Random.Range(0f, total_spd);

        /* //���� �ڵ�
        if (select_spd < coin_spd)
            resultItem = 3;
        else if (select_spd < coin_spd + exp0_spd)
            resultItem = 4;
        else if (select_spd < coin_spd + exp0_spd + exp1_spd)
            resultItem = 5;
        else if (select_spd < coin_spd + exp0_spd + exp1_spd + health_spd)
            resultItem = 6;
        else
            resultItem = 7;
        return resultItem;*/


        //resultItem�� itemDatas�迭�� �ε����� ���.
        if (select_spd < coin_spd)
            resultItem = 0;
        else if (select_spd < coin_spd + exp0_spd)
            resultItem = 1;
        else if (select_spd < coin_spd + exp0_spd + exp1_spd)
            resultItem = 2;
        else if (select_spd < coin_spd + exp0_spd + exp1_spd + health_spd)
            resultItem = 3;
        else
            resultItem = 4;
        return resultItem;

    }

    void SpawnItem(int itemNum) // 3 : ����, 4 : Exp 0, 5 : Exp 1
    {
        GameObject item = GameManager.Instance.pool.Get(3);
        item.GetComponent<SpawnItem>().Init(itemDatas[itemNum]);
        item.transform.position = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * ItemsRandomSpawnArea;
        
    }
}

//�Ӽ�-����ȭ�� �־��ָ� unity������ �� �� ����
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
