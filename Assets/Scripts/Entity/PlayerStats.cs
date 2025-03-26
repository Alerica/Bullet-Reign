using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("References")]
    Animator animator;

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxStamina = 200;
    private int currentHealth;
    private int currentStamina;
    
    private void Start()
    {
        currentHealth = maxHealth;
        currentStamina = 0;
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

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxStamina()
    {
        return maxStamina;
    }

    public int GetCurrentStamina()
    {
        return currentStamina;
    }

}
