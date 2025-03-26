using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeMenu; 
    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        upgradeMenu.SetActive(isMenuOpen);
        Time.timeScale = isMenuOpen ? 0 : 1;
    }
}
