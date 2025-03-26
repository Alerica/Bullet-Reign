using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("References")]
    Animator animator;

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxStamina = 200;
    [SerializeField] private int staminaRegenRate = 10; 
    private int currentHealth;
    private int currentStamina;
    private float staminaRegenCooldown = 0;
    
    private void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.Log(currentStamina);
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
         if (currentStamina < maxStamina)
        {
            staminaRegenCooldown += Time.deltaTime;
            if (staminaRegenCooldown >= 1f)
            {
                currentStamina = Mathf.Min(currentStamina + staminaRegenRate, maxStamina);
                staminaRegenCooldown = 0f;
            }
        }
    }

    public bool HasEnoughStamina(int amount)
    {
        return currentStamina >= amount;
    }

    public void UseStamina(int amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0) currentStamina = 0;
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
