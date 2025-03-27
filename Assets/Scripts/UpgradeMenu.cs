using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeMenu; 
    [SerializeField] private GameObject mapMenu;
    private bool isMenuOpen = false;
    private bool isMapMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            ToggleMapMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        upgradeMenu.SetActive(isMenuOpen);
        Time.timeScale = isMenuOpen ? 0 : 1;
    }

    void ToggleMapMenu()
    {
        isMapMenuOpen = !isMapMenuOpen;
        mapMenu.SetActive(isMapMenuOpen);
    }
}
