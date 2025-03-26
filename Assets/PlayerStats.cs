using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("References")]
    Animator animator;

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 60;
    private int currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        } 
        animator.SetTrigger("isHit");
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
