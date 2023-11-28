using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    public Text SkillTree;
    public SkillTreeManager SkillTreeManager;

    public void OnClick()
    {
        SkillTree.text = SkillTreeManager.instance.tellMeSkillTree();
    }
}
