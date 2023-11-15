using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;
    public float ItemsRandomSpawnArea; //.. �÷��̾� ������ ������ �����Ǵ� ��Ŭ�� ������
    public float ItemSpawnTime;

    private WaitForSeconds WaitSpawnTime; //������- �������� �����Ǵµ� �ɸ��� �ð�. default:1��

    int level;
    float timer;

    public SpawnItemData[] itemDatas;
    public int[] dropRates; //SpawnItemData ���ο� �ִ� ����� �κ��� �о�� ������� �����ϴ� �迭
    private int totalDropRate; // dropRates �迭�� ���� �� ���� ����

    public bool isPlayer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        //�ڱ� �ڽ��� ������ �ڽĵ� component �ϴ� �����

        levelTime = GameManager.Instance.maxGameTime / spawnData.Length;

        //ItemsRandomSpawnArea = GameManager.Instance.ItemsRandomSpawnArea;
        if (isPlayer)
        {
            ItemsRandomSpawnArea = 10f;

            dropRates = new int[itemDatas.Length];

            for (int i = 0; i < dropRates.Length; i++)
                dropRates[i] = itemDatas[i].dropRate;

            totalDropRate = dropRates.Sum();

            ItemSpawnTime = 1f;
            WaitSpawnTime = new WaitForSeconds(ItemSpawnTime);


            StartCoroutine(CreateCoinRoutine());
        }
    }

    private void OnValidate()//����Ƽ Inspector���� ���� ����� ��� ȣ��Ǵ� �Լ�.
    {
        totalDropRate = dropRates.Sum();//����� ���� �ٽ� ���
        WaitSpawnTime = new WaitForSeconds(ItemSpawnTime);//������ ���� �ð� ��ü �ٽ� ����
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
            yield return WaitSpawnTime; //new�� ���ֱ� ���� awake�ܰ迡�� WaitForSeconds�� ����
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

    /*
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

         //���� �ڵ�
        //if (select_spd < coin_spd)
        //    resultItem = 3;
        //else if (select_spd < coin_spd + exp0_spd)
        //    resultItem = 4;
        //else if (select_spd < coin_spd + exp0_spd + exp1_spd)
        //    resultItem = 5;
        //else if (select_spd < coin_spd + exp0_spd + exp1_spd + health_spd)
        //    resultItem = 6;
        //else
        //    resultItem = 7;
        //return resultItem;


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
*/
    int SelectRandomItem()
    {
        int resultItem = 0;
        int select = Random.Range(0, totalDropRate);

        for (int i = 0; i < dropRates.Length; i++)
        {
            select -= dropRates[i];

            if (select <= 0)
            {
                resultItem = i;
                break;
            }
        }

        return resultItem;
    }

    void SpawnItem(int itemNum) //Item : 3��, ������� ���� ���õ� �������� �޾�, �ش� �������� index�� �ϴ� �������� �������� ����.
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
