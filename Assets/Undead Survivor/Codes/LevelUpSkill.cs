using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSkill : MonoBehaviour
{
    //UI는 rect transform
    RectTransform rect;
    Item[] items;
    SkillSelect[] skillSelects;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        skillSelects = GetComponentsInChildren<SkillSelect>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.Stop();

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.Instance.EffectBgm(true);
    }
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.Instance.EffectBgm(false);
    }
    public void Select(int index)
    {
        skillSelects[index].OnClick();
    }
    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach (SkillSelect skill in skillSelects)
        {
            skill.gameObject.SetActive(false);
        }

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] rand = new int[3];
        while (true)
        {
            rand[0] = Random.Range(0, skillSelects.Length);
            rand[1] = Random.Range(0, skillSelects.Length);
            rand[2] = Random.Range(0, skillSelects.Length);

            if (rand[0] != rand[1] && rand[1] != rand[2] && rand[0] != rand[2])
                break;
        }

        for (int index = 0; index < rand.Length; index++)
        {
            SkillSelect randomSkill = skillSelects[rand[index]];

            // 3. 만렙 아이템의 경우는 소비아이템으로 대체
            if (randomSkill.level == randomSkill.skillData.damages.Length)
            {
                //소비아이템이 하나니까 4 로 지정. 여러개면 인덱스에 random.range로.
                skillSelects[4].gameObject.SetActive(true);
            }
            else
            {
                randomSkill.gameObject.SetActive(true);
            }
        }

    }
}
