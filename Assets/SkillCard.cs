using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    public Image iconImage;
    private Skill skill;

    public void SetSkill(Skill newSkill)
    {
        Debug.Log(newSkill.name);
        skill = newSkill;
        if (skill != null)
        {
            iconImage.sprite = skill.icon;
            iconImage.enabled = true;
        }
        else
        {
            iconImage.enabled = false; 
        }
    }
}