using UnityEngine;

public class BossSetting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject portalPrefab;

    public void OnBossDeath()
    {
        UpgradeManager upgrade = GameManager.Instance.GetComponent<UpgradeManager>();
        upgrade.ShowUpgrades();
        Instantiate(portalPrefab, transform.position, Quaternion.identity);
    }


}
