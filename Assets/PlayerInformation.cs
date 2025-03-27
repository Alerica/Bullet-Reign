using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text moneyText;


    [Header("Stats")]
    [SerializeField] private TMP_Text maxHealthText;
    [SerializeField] private TMP_Text regenHealthText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text damageText;
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
                moneyText.text = playerStats.GetGold().ToString();
                maxHealthText.text = PlayerStats.Instance.GetMaxHealth().ToString();
                regenHealthText.text = PlayerStats.Instance.healthRegenRate.ToString();
                speedText.text = PlayerStats.Instance.movementSpeed.ToString();
                damageText.text = PlayerStats.Instance.damage.ToString();
            }
        }
        else
        {
            healthSlider.value = 0;
        }
        
    }
}