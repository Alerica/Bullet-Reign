using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;
    public float value; 
    public UpgradeType upgradeType;

    public enum UpgradeType { Health, Damage, FireRate, Speed, Special, HealthRegen, StaminaRegen }
}