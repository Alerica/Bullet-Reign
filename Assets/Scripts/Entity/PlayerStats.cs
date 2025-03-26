using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("References")]
    Animator animator;

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100;
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

        FindFirstObjectByType<CameraShake>()?.ShakeCamera(3f, 0.5f); 
        animator.SetTrigger(StringManager.isHit);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
