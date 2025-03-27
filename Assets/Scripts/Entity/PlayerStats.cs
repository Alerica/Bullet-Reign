using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("References")]
    Animator animator;

    [Header("Player Stats")]
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int maxStamina = 200;
    [SerializeField] public int staminaRegenRate = 10; 
    [SerializeField] public int healthRegenRate = 0;
    [SerializeField] private int startingGold = 100;
    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] public int damage = 10;
    private int currentGold;
    private int currentHealth;
    private int currentStamina;
    private float staminaRegenTimer = 0;
    private float healthRegenTimer = 0;
    public float damageMultiplier = 1f;
    public float fireRate = 1f;

    public static PlayerStats Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentGold = startingGold;
        animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        // Debug.Log(currentStamina);
        RegenerateStamina();
        RegenerateHealth();
    }

    private void RegenerateStamina()
    {
         if (currentStamina < maxStamina)
        {
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= 1f)
            {
                currentStamina = Mathf.Min(currentStamina + staminaRegenRate, maxStamina);
                staminaRegenTimer = 0f;
            }
        }
    }

    private void RegenerateHealth()
    {
        if (currentHealth < maxHealth)
        {
            healthRegenTimer += Time.deltaTime;
            if(healthRegenTimer >= 1f)
            {
                currentHealth = Mathf.Min(currentHealth + healthRegenRate, maxHealth);
                healthRegenTimer = 0f;
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

    public int GetGold()
    {
        return currentGold;
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
    }
}
