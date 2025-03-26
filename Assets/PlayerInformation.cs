using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private GameObject player;

    PlayerStats playerStats;
    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            healthSlider.maxValue = playerStats.GetMaxHealth();
            staminaSlider.maxValue = playerStats.GetMaxStamina();
        }
    }

    private void Update()
    {
        if(player != null) 
        {
            if (playerStats != null)
            {
                healthSlider.value = playerStats.GetCurrentHealth();
                staminaSlider.value = playerStats.GetCurrentStamina();
            }
        }
        else
        {
            healthSlider.value = 0;
        }
        
    }
}