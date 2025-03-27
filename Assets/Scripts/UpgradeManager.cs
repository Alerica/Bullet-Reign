using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> allUpgrades; // Assign in Inspector
    public Transform upgradePanel; // UI Panel
    public GameObject upgradeButtonPrefab; // Prefab for buttons

    private List<UpgradeData> selectedUpgrades = new List<UpgradeData>();

    public void ShowUpgrades()
    {
        // Clear
        foreach (Transform child in upgradePanel) Destroy(child.gameObject);
        selectedUpgrades.Clear();

        while (selectedUpgrades.Count < 3) // Set to 3
        {
            UpgradeData randomUpgrade = allUpgrades[Random.Range(0, allUpgrades.Count)];
            if (!selectedUpgrades.Contains(randomUpgrade))
                selectedUpgrades.Add(randomUpgrade);
        }

        foreach (UpgradeData upgrade in selectedUpgrades)
        {
            GameObject btn = Instantiate(upgradeButtonPrefab, upgradePanel);
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();  
            if (btnText == null) 
            { 
                return; 
            }

            Image upgradeIcon = btn.transform.Find("Upgrade Icon").GetComponent<Image>();
            if (upgradeIcon != null) 
                upgradeIcon.sprite = upgrade.icon;
            
            btnText.text = upgrade.upgradeName;
            btn.GetComponent<Button>().onClick.AddListener(() => ApplyUpgrade(upgrade));
            // Debug.Log($"Button created for {upgrade.upgradeName}");
        }

        upgradePanel.gameObject.SetActive(true);
    }

    void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeData.UpgradeType.Health:
                PlayerStats.Instance.maxHealth += (int) upgrade.value;
                break;
            case UpgradeData.UpgradeType.Damage:
                PlayerStats.Instance.damage += (int) upgrade.value;
                break;
            case UpgradeData.UpgradeType.FireRate:
                PlayerStats.Instance.fireRate *= upgrade.value;
                break;
            case UpgradeData.UpgradeType.Speed:
                PlayerStats.Instance.movementSpeed += upgrade.value;
                break;
            case UpgradeData.UpgradeType.HealthRegen:
                PlayerStats.Instance.healthRegenRate += (int) upgrade.value;
                break;
            case UpgradeData.UpgradeType.StaminaRegen:
                PlayerStats.Instance.staminaRegenRate += (int)upgrade.value;
                break;
        }

        upgradePanel.gameObject.SetActive(false);
    }
}