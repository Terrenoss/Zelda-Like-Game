using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject menuSettingsCanvas;
    public GameObject inventoryCanvas;

    private GameObject activeMenu = null;

    void Start()
    {
        menuCanvas.SetActive(false);
        menuSettingsCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeMenu != null)
            {
                if (activeMenu == menuSettingsCanvas || activeMenu == inventoryCanvas)
                {
                    activeMenu.SetActive(false);
                    menuCanvas.SetActive(true);
                    activeMenu = menuCanvas;
                }
                else
                {
                    activeMenu.SetActive(false);
                    activeMenu = null;
                }
            }
            else
            {
                menuCanvas.SetActive(true);
                activeMenu = menuCanvas;
            }
        }
    }

    public void OpenMenu(GameObject menu)
    {
        if (activeMenu != null)
        {
            activeMenu.SetActive(false);
        }

        menu.SetActive(true);
        activeMenu = menu;
    }

    public void OpenSettings()
    {
        OpenMenu(menuSettingsCanvas);
    }

    public void OpenInventory()
    {
        OpenMenu(inventoryCanvas);
    }
}
