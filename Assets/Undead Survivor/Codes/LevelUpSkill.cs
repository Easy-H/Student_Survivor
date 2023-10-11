using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSkill : MonoBehaviour
{
    //UI�� rect transform
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
        // 1. ��� ������ ��Ȱ��ȭ
        foreach (SkillSelect skill in skillSelects)
        {
            skill.gameObject.SetActive(false);
        }

        // 2. �� �߿��� ���� 3�� ������ Ȱ��ȭ
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

            // 3. ���� �������� ���� �Һ���������� ��ü
            if (randomSkill.level == randomSkill.skillData.damages.Length)
            {
                //�Һ�������� �ϳ��ϱ� 4 �� ����. �������� �ε����� random.range��.
                skillSelects[4].gameObject.SetActive(true);
            }
            else
            {
                randomSkill.gameObject.SetActive(true);
            }
        }

    }
}
