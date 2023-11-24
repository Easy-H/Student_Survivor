using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //static - ����, �޸𸮿� ��ڴ�
    //inspector�� ��Ÿ���� ����
    public static GameManager Instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    //public float maxGameTime = 2 * 10f;
    public readonly float[] semesterDifficulty = { 1, 1.2f, 1.4f, 1.6f, 1.8f, 2f, 3f, 3.5f }; //���̵� ���

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    private readonly int maxLevel = 120;
    public int kill;
    public Dictionary<int, int> killByType;
    public int exp;
    public float manBoGi;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    //public int money;
    public float expRate = 1.0f;
    public int currentPhase; //���� ���̵�

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public AI_Player ai_Player;
    public LevelUp uiLevelUp;
    public LevelUpSkill uiLevelUpSkill; // �ӽ� �߰�
    public Result uiResult;
    public Transform uiJoy;
    public GameObject enemyCleaner;
    public GameObject QuestBox;
    public GameObject bossSet;
    public GameObject HealthInHUD;

    public int MaxQuestCount = 3;
    public List<UIQuest> freeQuestUI;

    int questCount = 0;

    private IEnumerator currentBossSpawn;
    private Enemy _boss;
    public Enemy SpawnedBoss
    {
        get { return _boss; }
        set { _boss = value; }
    }
    public bool CanAddQuest() {
        if (questCount < MaxQuestCount) return true;
        return false;
    }

    public UIQuest AddQuest(string skillName, int level, QuestChecker checker, QuestData data) {
        UIQuest questUI = freeQuestUI[0];
        freeQuestUI.RemoveAt(0);
        questCount++;

        questUI.QuestSet(skillName, level, checker, data);

        return questUI;
    }

    public void EndQuest(UIQuest endQuestUI) {
        endQuestUI.gameObject.SetActive(false);
        freeQuestUI.Add(endQuestUI);
        questCount--;

    }

    private void Awake()
    {
        Instance = this;
        killByType = new();
        for(int i = 0; i < 3; i++)
            killByType.Add(i, 0);
        Application.targetFrameRate = 60;
    }
    public void GameStart(int id)
    {
        playerId = id;

        health = maxHealth;

        player.gameObject.SetActive(true);

        //ù��° ĳ���� ����
        // uiLevelUp.Select(playerId % 2); // ĳ���� �����ϸ� ���� �����ߴ��� �ּ�ó�� ��
        Resume();

        AudioManager.Instance.PlayBgm(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);

        currentBossSpawn = SpawnBoss();
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
    }
    public void GameRetry()
    {
        DataManager.Instance.Save();
        SceneManager.LoadScene(0); // build setting�� scene ��ȣ
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetExp(10);
        }
    }
    public void GetExp() //.. 1��ŭ �����ϴ� ����ġ ȹ�� �Լ�
    {
        if (!isLive)
            return;
        exp++;
        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)]) //index bound error �� ��������
        {
            level++;
            exp = 0;
            uiLevelUpSkill.Show();
            currentPhase = level / (maxLevel / semesterDifficulty.Length);
            if(level == 50 || level == 100)
            {
                Debug.Log("���� ��ȯ");
                currentBossSpawn.MoveNext();
            }
        }
    }
    public void GetExp(int e) //... e��ŭ �����ϴ� ����ġ ȹ�� �Լ�
    {
        if (!isLive)
            return;
        if(expRate != 1.0f) //����ġ ���� ���罺ų�� �߰��Ǹ� �� �κ��� �����. - �Ǽ����� ���ؼ� �ݿø��� ������
            e = Convert.ToInt32(expRate * e);
        exp += e;
        int nextexp = nextExp[Mathf.Min(level, nextExp.Length - 1)]; //index bound error �� ��������
        if (exp >= nextexp)
        {
            level++;
            exp -= nextexp;
            uiLevelUpSkill.Show();
            currentPhase = level / (maxLevel / semesterDifficulty.Length);
            if (level == 59 || level == 119)
            {
                Debug.Log("���� ��ȯ");
                currentBossSpawn.MoveNext();
            }
            SkillRateManager.instance.updateSkillRate(currentPhase);
        }
    }
    public void GetHealth(int h) //.. h��ŭ ü�� ȸ��
    {
        if (!isLive)
            return;
        health = Mathf.Min(maxHealth, health + h);
    }

    public void AddManBogi(float distance)
    {
        manBoGi += distance;
    }

    //�� ��ũ��Ʈ�� Update �迭 ������ isLive ���� �߰�
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; //����Ƽ�� �ð� �ӵ� ����
        uiJoy.localScale = Vector3.zero;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; //���� 2�� �ð��� �׸�ŭ ���� �귯��.
        uiJoy.localScale = Vector3.one;
    }
    private IEnumerator SpawnBoss()
    {
        int currentBoss = 0;

        while ( currentBoss != bossSet.transform.childCount - 1 ) {
            Transform nextBoss = bossSet.transform.GetChild(currentBoss);
            _boss = nextBoss.GetComponent<Enemy>();
            nextBoss.localPosition = player.transform.position + Vector3.up * 10;
            nextBoss.gameObject.SetActive(true);
            Debug.Log(string.Format("���� ��ȯ {0}��°",currentBoss));
            currentBoss++;
            yield return null;
        }
    }
}
