using UnityEngine;

public class SkillPickup : MonoBehaviour
{
    [Header("Reference")]
    public Skill skill;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().AddSkill(skill);
            Destroy(gameObject);
        }
    }
}

