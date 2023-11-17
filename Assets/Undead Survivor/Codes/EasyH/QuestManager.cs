using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int level;
    public string Name;

    QuestChecker _checker; // 하나의 퀘스트를 관리하는 클래스
    SkillData _SkillData;
    BasedSkill skill, AIskill;

    public UIQuest questUI;

    public float progress;

    public void SetQuest(QuestData newQuest, SkillData skillData) {
        if (!GameManager.Instance.CanAddQuest()) return;

        _SkillData = skillData;
        Name = newQuest.Name;
        transform.parent = GameManager.Instance.QuestBox.transform; // 위치를 퀘스트박스로 이동

        switch (newQuest.Type) {
            case QuestData.QuestType.killQuiz:
                _checker = new KillCountQuestChecker(newQuest.IntValue);
                break;
            case QuestData.QuestType.killHW:
                _checker = new KillCountQuestChecker(newQuest.IntValue);
                break;
            case QuestData.QuestType.killTest:
                _checker = new KillCountQuestChecker(newQuest.IntValue);
                break;
            case QuestData.QuestType.walk:
                _checker = new WalkQuestChecker(newQuest.FloatValue);
                break;
            case QuestData.QuestType.safeTime:
                _checker = new SafeTimeQuestChecker(newQuest.FloatValue);
                break;
            default:
                _checker = new HealthMakeToQuestChecker(newQuest.FloatValue);
                break;
        }

        questUI = GameManager.Instance.AddQuest(_checker, newQuest);
    }

    public float GetProgress() {
        return _checker.GetProgress();
    }

    public void QuestAchieve()
    {
        Debug.Log("퀘스트 성공");

        GameManager.Instance.EndQuest(questUI);

        if (level == 0)
        {
            GameObject newSkill = new GameObject();
            GameObject newAISkill = new GameObject();
            skill = newSkill.AddComponent<BasedSkill>();
            AIskill = newAISkill.AddComponent<BasedSkill>();

            skill.Init(false, _SkillData);
            AIskill.Init(true, _SkillData);
            level++;
        }
        else
        {
            skill.LevelUp();
            AIskill.LevelUp();
            level++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_checker == null) return;
        progress = _checker.GetProgress();
        if (!_checker.CheckAchieve()) return;

        QuestAchieve();
        _checker = null;

    }
}
